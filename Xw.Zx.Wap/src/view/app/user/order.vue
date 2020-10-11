<template>
  <div class="wrapper">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>

    <div
      class="product"
      v-for="(order, index) in orders"
      :key="index"
      @click="$router.push({ path: `/sqb/order/${order.id}` })"
    >
      <div class="product-title">
        <h2>订单编号: {{ order.timestamp }}</h2>
        <p v-if="order.orderState == 1">交易成功</p>
        <van-button
          v-if="order.orderState == 0"
          class="product-title-btn"
          color="#ff5000"
          round
          plain
          size="mini"
          @click="$router.push({ path: `/sqb/order/${order.id}` })"
        >
          付款
        </van-button>
      </div>

      <div class="product-content">
        <div class="product-content-img">
          <img :src="order.productDto.images" alt="" />
        </div>
        <div class="product-content-info">
          <h1 class="product-content-info-name">
            {{ order.productDto.name }}
          </h1>
          <p class="product-content-info-price">￥{{ order.amount }}</p>
          <div class="product-content-info-time">
            <p>{{ order.addTime }}</p>
            <van-button
              v-if="order.orderState == 1 && order.productId == 10"
              class="product-content-info-time-btn"
              color="#ff5000"
              round
              plain
              size="mini"
              @click.stop="$router.push({ path: `/sqb/product/content/chapter` })"
            >
              查看课程
            </van-button>
          </div>
        </div>
      </div>
    </div>
    <div class="none" v-if="orders.length == 0">--您还没有订单--</div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "",
  props: [""],
  data() {
    return {
      orders: null,
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    api.order
      .gets({
        Sorts: "-id",
      })
      .then((res) => {
        this.orders = res.result;
      });
  },

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.product {
  //margin: 10px;
  background-color: #fff;
  padding: 10px;
  margin-bottom: 20px;
  // border-radius: 10px;
  // box-shadow: 0.02667rem 0.02667rem 0.21333rem #999;
  &-title {
    margin-top: 5px;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    h2 {
      font-size: 15px;
      font-weight: bold;
    }
    p {
      font-size: 14px;
      color: #ff5000;
      font-weight: bold;
    }
  }

  &-content {
    margin-top: 15px;
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: flex-start;

    &-img {
      img {
        width: 100px;
      }
    }
    &-info {
      margin-left: 10px;
      width: calc(100%-100px);

      &-name {
        font-size: 16px;
        line-height: 20px;
        color: #333333;
        overflow: hidden;
        height: 39px;
      }
      &-price {
        margin: 8px 0px;
        font-size: 18px;
        color: #ff5000;
        width: 100%;
      }
      &-time {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: flex-start;
        p {
          font-size: 13px;
          line-height: 20px;
          color: #999999;
        }
        &-btn {
          width: 60px;
        }
      }
    }
  }
}
.none {
  font-size: 14px;
  text-align: center;
  color: #999999;
  margin-top: 20px;
}
</style>