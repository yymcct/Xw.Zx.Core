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
      <div class="product-title">
        <h2>订单编号: {{ order.timestamp }}</h2>
        <p v-if="order.orderState == 1">交易成功</p>
        <!-- <span v-if="order.orderState == 0" @click="userCouponCode=true">兑换卷</span> -->
      </div>

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
    <div class="coupon" v-if="userCouponCode">
      <van-field v-model="couponCode" placeholder="请输入兑换卷" />
      <van-button
        class="foot-btn"
        color="#ff5000"
        round
        plain
        size="mini"
        @click="couponCodeHandle"
      >
        使用
      </van-button>
    </div>
    <div class="foot">
      <van-button
        class="foot-btn"
        color="#999"
        round
        plain
        size="small"
        @click="delOrder"
        :disabled="order.orderState == 1"
      >
        删除订单
      </van-button>
      <van-button
        class="foot-btn"
        type="primary"
        plain
        round
        size="small"
        color="#ff5000"
        @click="showQrcodePay = true"
        :disabled="order.orderState == 1"
      >
        扫码付款
      </van-button>
      <van-button
        class="foot-btn"
        type="primary"
        round
        size="small"
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="payOrder"
        :disabled="order.orderState == 1"
      >
        立即付款
      </van-button>
    </div>
    <template v-if="useAlipay">
      <alipayQrcode
        v-if="order.orderState == 0"
        v-model="showQrcodePay"
        :order="order"
        @paystateChange="paystateChange"
      />
    </template>
    <template v-else>
      <qrcode-pay
        v-if="order.orderState == 0"
        v-model="showQrcodePay"
        :order="order"
        @paystateChange="paystateChange"
      />
    </template>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
import { mapGetters } from "vuex";
import alipayQrcode from "./components/alipayQrcode";
import qrcodePay from "./components/biqilinQrcodePay";

export default {
  name: "Order",
  props: [""],
  data() {
    return {
      useAlipay: false,
      product: null,
      order: null,
      loading: false,
      couponCode: "",
      userCouponCode: false,
      showQrcodePay: false,
      weixinjsApi: null,
    };
  },

  components: {
    qrcodePay,
    alipayQrcode,
  },

  computed: {
    ...mapGetters({
      user: "user/user",
    }),
  },

  beforeMount() {
    this.init();
  },

  mounted() {},

  methods: {
    async init() {
      const _this = this;
      _this.order = (await api.order.get(this.$route.params.id)).result;
      _this.product = (
        await api.product.get({
          id: _this.order.productId,
        })
      ).result;

      _this.useAlipay = (await api.alipay.firstUseAlipay()).result;
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
      const isWeixin = () =>
        /micromessenger/.test(navigator.userAgent.toLowerCase());

      const aliPay = () => {
        const _this = this;
        let returnUrl = window.location.href;
        api.alipay
          .wapPay({
            id: _this.order.id,
            returnUrl: returnUrl,
          })
          .then((res) => {
            const div = document.createElement("div");
            /* 此处form就是后台返回接收到的数据 */
            div.innerHTML = res.result.alipayTradeAppPayResponse;
            document.body.appendChild(div);
            document.forms[0].submit();
          });
      };
      const weixinPay = () => {
        const _this = this;
        if (!_this.user.wxOpenId) {
          this.$toast("没有OpenId,无法使用微信支付");
          return;
        }
        const onBridgeReady = function () {
          window.WeixinJSBridge.invoke(
            "getBrandWCPayRequest",
            {
              appId: _this.weixinjsApi.jsAppId, //公众号名称，由商户传入
              timeStamp: _this.weixinjsApi.jsTimeStamp, //时间戳，自1970年以来的秒数
              nonceStr: _this.weixinjsApi.jsNonceStr, //随机串
              package: _this.weixinjsApi.jsPackages,
              signType: _this.weixinjsApi.jsSignType, //微信签名方式：
              paySign: _this.weixinjsApi.jsPaySign, //微信签名
            },
            function (res) {     
              //使用以下方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。
              if (res.err_msg == "get_brand_wcpay_request:ok") {
                //支付成功
                console.log("支付成功")
                //onSuccessMsg();
              } else {
                //弹出之后，苹果手机会卡死
                //alert(res.err_desc);
              }
            }
          );
        };
        const doPay = () => {
          if (typeof window.WeixinJSBridge == "undefined") {
            if (document.addEventListener) {
              document.addEventListener(
                "WeixinJSBridgeReady",
                onBridgeReady,
                false
              );
            } else if (document.attachEvent) {
              document.attachEvent("WeixinJSBridgeReady", onBridgeReady);
              document.attachEvent("onWeixinJSBridgeReady", onBridgeReady);
            }
          } else {
            onBridgeReady();
          }
        };
        api.biqilin
          .jsapiPay(_this.order.id, _this.user.wxOpenId)
          .then((res) => {
            console.log(res);
            _this.weixinjsApi = res.result;
            doPay();
          });
      };
      if (isWeixin()) {
        //微信支付
        weixinPay();
      } else {
        aliPay();
      }
    },

    paystateChange(val) {
      if (val) {
        this.init();
      }
    },
    couponCodeHandle() {},
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
      span {
        font-size: 11px;
        color: #e6e6e6;
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
          font-size: 13px;
          line-height: 20px;
          color: #999999;
        }
      }
    }
  }
  .coupon {
    margin-top: 20px;
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    align-items: center;
    background-color: #fff;
    padding: 10px;
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