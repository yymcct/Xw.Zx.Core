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
          <p class="product-content-info-count">
            数量: {{ order.productCount }}
          </p>
          <p class="product-content-info-price">￥{{ order.amount }}</p>
          <p class="product-content-info-time">{{ order.addTime }}</p>
        </div>
      </div>
    </div>
    <div
      class="customer"
      v-if="order.customerName || order.customerPhone || order.remark"
    >
      <div class="customer-title">
        <p>客户信息</p>
      </div>
      <div class="customer-content">
        <p>姓名: {{ order.customerName }}</p>
        <p>电话: {{ order.customerPhone }}</p>
        <p class="customer-content-remark">备注: {{ order.remark }}</p>
      </div>
    </div>
    <div class="coupon" v-if="coupon && memberIntegralChecked == false">
      <van-checkbox
        v-model="couponChecked"
        shape="square"
        checked-color="#ff5000"
        :disabled="order.orderState == 1"
        >使用{{ coupon.coupon.name }}</van-checkbox
      >
    </div>

    <div class="integral" v-if="memberIntegral && couponChecked == false">
      <div class="integral-title">
        <p>
          使用积分
          <span
            ><van-icon name="info-o" /> 现有{{
              memberIntegral.availableIntegrals
            }}积分</span
          >
        </p>
        <van-switch
          v-model="memberIntegralChecked"
          active-color="#FF5000"
          inactive-color="#dcdee0"
          size="24px"
          :disabled="
            order.orderState == 1 ||
            memberIntegral.availableIntegrals < Number(order.amount) * 10
          "
        />
      </div>
      <div
        class="integral-desc"
        v-if="memberIntegral.availableIntegrals < Number(order.amount) * 10"
      >
        <p>
          抵扣本单需 {{ Number(order.amount) * 10 }} 积分, 本单积分不足,
          无法抵扣
        </p>
      </div>
      <div class="integral-content" v-if="memberIntegralChecked">
        <p>使用 {{ Number(order.amount) * 10 }} 积分</p>
        <p>
          抵扣<span>{{ order.amount }}</span
          >元
        </p>
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
        :disabled="order.orderState == 1"
      >
        删除订单
      </van-button>
      <van-button
        v-if="couponChecked == false && memberIntegralChecked == false"
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
        v-if="couponChecked && coupon"
        class="foot-btn"
        type="primary"
        round
        size="small"
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="couponPay"
        :disabled="order.orderState == 1"
      >
        提交
      </van-button>
      <van-button
        v-if="memberIntegralChecked"
        class="foot-btn"
        type="primary"
        round
        size="small"
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="memberIntegralPay"
        :disabled="order.orderState == 1"
      >
        提交
      </van-button>
      <van-button
        v-if="
          couponChecked == false && memberIntegralChecked == false && isWeixin()
        "
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
    <!--碧麒麟 0 支付宝 1 中信 2-->
    <template v-if="useAlipay == '1'">
      <alipay-qrcode
        v-if="order.orderState == 0"
        v-model="showQrcodePay"
        :order="order"
        @paystateChange="paystateChange"
      />
    </template>
    <template v-else-if="useAlipay == '0'">
      <qrcode-pay
        v-if="order.orderState == 0"
        v-model="showQrcodePay"
        :order="order"
        @paystateChange="paystateChange"
      />
    </template>
    <template v-else-if="useAlipay == '2'">
      <citicbank-qrcode
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
import citicbankQrcode from "./components/citicbankQrcode";

export default {
  name: "Order",
  props: [""],
  data() {
    return {
      useAlipay: null,
      product: null,
      order: null,
      coupon: null,
      memberIntegral: null,
      couponChecked: false,
      memberIntegralChecked: false,
      loading: false,
      showQrcodePay: false,
      weixinjsApi: null,
    };
  },

  components: {
    qrcodePay,
    alipayQrcode,
    citicbankQrcode,
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

    //TODO
     // if(_this.user.)
      //_this.useAlipay = 2;

      _this.coupon = (
        await api.coupon.getCouponByProductId(_this.order.productId)
      ).result;

      if (_this.product.canUseMemberIntegral) {
        _this.memberIntegral = (await api.member.getMemberIntegral()).result;
      }

      //下单时选择了优惠券方式,且有名下有优惠券
      if (_this.coupon && _this.order.orderPaymentType == 4) {
        _this.couponChecked = true;
      }
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
    couponPay() {
      const _this = this;
      api.order
        .couponPay(_this.order.id, _this.coupon.couponReceiveId)
        .then(() => {
          this.$toast("支付成功");
          this.$router.push({ path: `/sqb/user/order` });
        });
    },
    memberIntegralPay() {
      const _this = this;
      api.order.memberIntegralPay(_this.order.id).then(() => {
        this.$toast("支付成功");
        this.$router.push({ path: `/sqb/user/order` });
      });
    },
    isWeixin() {
      return /micromessenger/.test(navigator.userAgent.toLowerCase());
    },

    payOrder() {
      let useAlipay = this.useAlipay;
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
      const biqilinWeixinPay = () => {
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
                window.location.href =
                  "http://jsq.lawss360.com/sqb/order/" + _this.order.id;
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
      const citicbankWeixinPay = () => {
        const _this = this;
        if (!_this.user.wxOpenId) {
          this.$toast("没有OpenId,无法使用微信支付");
          return;
        }
        const onBridgeReady = function () {
          window.WeixinJSBridge.invoke(
            "getBrandWCPayRequest",
            {
              // appId: _this.weixinjsApi.jsAppId, //公众号名称，由商户传入
              // timeStamp: _this.weixinjsApi.jsTimeStamp, //时间戳，自1970年以来的秒数
              // nonceStr: _this.weixinjsApi.jsNonceStr, //随机串
              // package: _this.weixinjsApi.jsPackages,
              // signType: _this.weixinjsApi.jsSignType, //微信签名方式：
              // paySign: _this.weixinjsApi.jsPaySign, //微信签名
              appId: _this.weixinjsApi.appId, //公众号名称，由商户传入
              timeStamp: _this.weixinjsApi.timeStamp, //时间戳，自1970年以来的秒数
              nonceStr: _this.weixinjsApi.nonceStr, //随机串
              package: _this.weixinjsApi.package,
              signType: _this.weixinjsApi.signType, //微信签名方式：
              paySign: _this.weixinjsApi.paySign, //微信签名
            },
            function (res) {
              //使用以下方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回    ok，但并不保证它绝对可靠。
              if (res.err_msg == "get_brand_wcpay_request:ok") {
                window.location.href =
                  "http://jsq.lawss360.com/sqb/order/" + _this.order.id;
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
        api.citicbank.jsapiPay(_this.order.id).then((res) => {
          _this.weixinjsApi = JSON.parse(res.result);
          console.log("jsapi", _this.weixinjsApi);
          doPay();
        });
      };
      if (isWeixin()) {
        //微信支付//中信的
        if (useAlipay == 0 || useAlipay == 1) {
          biqilinWeixinPay();
        } else if (useAlipay == 2) {
          citicbankWeixinPay();
        }
      } else {
        aliPay();
      }
    },

    paystateChange(val) {
      if (val) {
        this.init();
      }
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
          height: 20px;
        }
        &-count {
          margin-top: 5px;
          font-size: 15px;
          color: #333333;
        }
        &-price {
          margin: 14px 0px;
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
  .customer {
    margin-top: 20px;
    background-color: #fff;
    padding: 10px;
    color: #999999;
    font-size: 15px;
    line-height: 26px;
    &-title {
      p {
        color: #333333;
        font-size: 15px;
        font-weight: bold;
      }
    }
    &-content {
      margin: 5px 10px 0px 10px;
      &-remark {
        font-size: 14px;
      }
    }
  }
  .coupon {
    margin-top: 20px;
    background-color: #fff;
    padding: 10px;
    font-size: 16px;
  }
  .integral {
    margin-top: 20px;

    background-color: #fff;
    padding: 10px;
    font-size: 16px;
    &-title {
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;

      p {
        color: #333333;
        font-weight: bold;
        span {
          font-size: 13px;
        }
      }
    }
    &-desc {
      margin-top: 10px;
      p {
        color: #333333;
        font-size: 13px;
      }
    }
    &-content {
      margin-top: 15px;
      padding: 20px 0;
      color: #333333;
      font-size: 13px;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      border-top: 1px dashed #999999;
      span {
        margin: 0 5px;
        font-size: 16px;
        color: #ff5000;
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