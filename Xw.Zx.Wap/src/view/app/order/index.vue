<template>
  <div class="wrapper" v-if="order && product">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>
    <div class="product">
      <h2>订单编号: {{ order.timestamp }}</h2>
      <div class="product-content">
        <div class="product-content-img">
          <img :src="product.images" alt="" />
        </div>
        <div class="product-content-info">
          <h1 class="product-content-info-name">
            {{ product.name }}
          </h1>
          <p class="product-content-info-price">￥{{ order.amount }}</p>
          <p class="product-content-info-time">{{ order.addTime }}</p>
        </div>
      </div>
    </div>
    <div class="foot">
      <van-button
        class="foot-btn"
        color="#999"
        round
        plain
        size="small"
        @click="delOrder"
      >
        删除订单
      </van-button>
      <van-button
        class="foot-btn"
        type="primary"
        round
        size="small"
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="payOrder"
      >
        付款
      </van-button>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "Order",
  props: [""],
  data() {
    return {
      product: null,
      order: null,
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    this.await();
  },

  mounted() {},

  methods: {
    async await() {
      const _this = this;
      _this.order = (await api.order.get(this.$route.params.id)).result;
      _this.product = (
        await api.product.get({
          id: _this.order.productId,
        })
      ).result;
    },
    delOrder() {
      const _this = this;
      this.$dialog.confirm({
        title: "提示",
        message: "确定删除该订单?",
        beforeClose: (action, done) => {
          if (action === "confirm") {
            api.order
              .delete(_this.order.id)
              .then(() => {
                this.$toast("订单已删除");
                this.$router.push(`/sqb/home`);
                done();
              })
              .catch(() => {
                this.$toast("异常:订单未能删除");
                done();
              });
          } else {
            done();
          }
        },
      });
    },
    payOrder() {
      const _this = this;
      api.alipay.wapPay(_this.order.id).then((res) => {
        console.log(res);
        const div = document.createElement("div");
        /* 此处form就是后台返回接收到的数据 */
        div.innerHTML = res.result.alipayTradeAppPayResponse;
        document.body.appendChild(div);
        document.forms[0].submit();
      });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  min-height: 100vh;
  .product {
    //margin: 10px;
    background-color: #fff;
    padding: 10px;
    border-radius: 10px;
    // box-shadow: 0.02667rem 0.02667rem 0.21333rem #999;
    h2 {
      margin-top: 5px;
      font-size: 14px;
      font-weight: bold;
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
          font-size: 13px;
          line-height: 20px;
          color: #999999;
        }
      }
    }
  }
  .foot {
    position: fixed;
    bottom: 0px;
    height: 50px;
    background-color: #fff;
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    align-items: center;
    padding: 0px 10px;
    box-sizing: border-box;
    &-btn {
      width: 80px;
      margin-left: 10px;
    }
  }
}
</style>