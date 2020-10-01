<template>
  <div>
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>
    <div class="product" v-if="product">
      <div class="product-title">
        <img class="product-title-image" :src="product.images" alt="" />
        <h1 class="product-title-name">{{ product.name }}</h1>
        <p class="product-title-price">￥{{ product.price }}</p>
        <p class="product-title-sales">销量{{ product.salesVolume }}</p>
      </div>

      <div class="product-content">
        <div class="product-content-fenge">商品详情</div>
        <img class="product-title-image" :src="product.images" alt="" />
      </div>
    </div>
    <div class="foot" @click="buy">立即购买</div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
import api from "@/api/sqbApi";
export default {
  name: "Product",
  props: [""],
  data() {
    return {
      product: null,
    };
  },

  components: {},

  computed: {
    ...mapGetters({
      user: "user/user",
    }),
  },

  beforeMount() {
    api.product
      .get({
        id: this.$route.params.id,
      })
      .then((res) => {
        this.product = res.result;
      });
  },

  mounted() {},

  methods: {
    async buy() {
      if (!this.user) {
        this.$router.push(`/sqb/login`);
      }
      //检查是否有未处理的订单
      let unbalanceOrder = await api.order.gets({
        Filters: "OrderState==0",
        Sorts: "-id",
        PageSize: "1",
      });
      if (
        unbalanceOrder.statusCode == 200 &&
        unbalanceOrder.result.length > 0
      ) {
        this.$toast("您有未完成的订单, 请处理和再操作");
        setTimeout(() => {
          this.$router.push(`/sqb/order/${unbalanceOrder.result[0].id}`);
        }, 2000);
      }
      console.log(unbalanceOrder);
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.product {
  &-title {
    display: flex;
    flex-direction: column;
    justify-content: flex-start;
    align-items: flex-start;
    font-size: 16px;
    background-color: #fff;
    padding-bottom: 10px;
    &-image {
      width: 100%;
    }
    &-name {
      margin: 10px 10px 5px 10px;
      font-size: 16px;
      line-height: 20px;
      color: #333333;
      overflow: hidden;
    }
    &-sales {
      margin: 0px 10px;
      font-size: 13px;
      line-height: 20px;
      color: #999999;
    }
    &-price {
      margin: 8px 10px;
      font-size: 18px;
      color: #ff5000;
      width: 100%;

      box-sizing: border-box;
    }
  }
  &-content {
    &-fenge {
      height: 20px;
      color: #999;
      text-align: center;
      font-size: 13px;
    }
    margin-top: 10px;
    padding-bottom: 60px;
  }
}
.foot {
  height: 50px;
  line-height: 50px;
  color: #fff;
  text-align: center;
  //background-color:#ff5000 ;
  background-image: linear-gradient(to right, #ff7a00, #ff5000);
  position: fixed;
  bottom: 0;
  width: 100%;
  font-size: 18px;
  letter-spacing: 5px;
}
</style>