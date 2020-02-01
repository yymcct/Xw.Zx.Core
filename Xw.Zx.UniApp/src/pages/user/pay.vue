<template>
  <view>
    <view class="uni-list">
      <radio-group @change="radioChange">
        <template v-for="item in items">
          <label
            class="uni-list-cell uni-list-cell-pd"
            :key="item.value"
            v-if="Number(item.value) > mySelf.memberVipType"
          >
            <view>
              <radio :value="item.value" :checked="curtoViptype == item.value" />
            </view>
            <view>{{item.name}}</view>
          </label>
        </template>
      </radio-group>
    </view>
    <view class="uni-padding-wrap">
      <view style="background:#FFF; padding:50upx 0;">
        <view class="uni-hello-text uni-center">
          <text>支付金额</text>
        </view>
        <view class="uni-h1 uni-center uni-common-mt">
          <text class="rmbLogo">￥</text>
          <input class="price" type="digit" :value="order.productPrice.replace(',','')" disabled />
        </view>
      </view>
      <view class="uni-btn-v uni-common-mt">
        <!-- #ifdef APP-PLUS -->
        <template v-if="providerList.length > 0">
          <button
            class="primary"
            hover-class="none"
            v-for="(item,index) in providerList"
            v-if="item.name == '支付宝'"
            :key="index"
            @click="requestPayment(item,index)"
            :loading="item.loading"
          >{{item.name}}支付</button>
        </template>
        <!-- #endif -->
      </view>
      <view class="dsq">
        <navigator url="../../pages/user/userUpgradeVip">
          <view class="userUpgradeVip">使用兑换码升级</view>
        </navigator>
      </view>
    </view>
  </view>
</template>
<script>
export default {
  data() {
    return {
      user: null,
      vipcode: "",
      order: {
        productPrice: ""
      },
      loading: false,
      price: 0.01,
      providerList: [],
      mySelf: {
        memberVipType: 2
      },
      curtoViptype: "1",
      items: [
        {
          value: "1",
          name: "会员"
        },
        {
          value: "2",
          name: "创客",
          checked: "true"
        },
        {
          value: "3",
          name: "服务站"
        },
        {
          value: "4",
          name: "运营商"
        }
      ]
    };
  },
  onLoad: function(option) {
    if (option.toViptype) {
      this.curtoViptype = option.toViptype;
    }

    this.user = this.getUser("../user/user");

    if (!this.user) {
      return false;
    }
    
    this.getMySelfInfo();
    this.getOrderInfo();

    // #ifdef APP-PLUS
    uni.getProvider({
      service: "payment",
      success: e => {      
        let providerList = [];
        e.provider.map(value => {
          switch (value) {
            case "alipay":
              providerList.push({
                name: "支付宝",
                id: value,
                loading: false
              });
              break;
            case "wxpay":
              providerList.push({
                name: "微信",
                id: value,
                loading: false
              });
              break;
            default:
              break;
          }
        });
        this.providerList = providerList;
      },
      fail: e => {
        console.log("获取支付通道失败：", e);
      }
    });
    // #endif
  },
  methods: {
    requestPayment(e, index) {
      this.providerList[index].loading = true;
      let orderInfo = this.order;
      console.log("得到订单信息", orderInfo);
      if (!orderInfo) {
        console.log("获得订单信息失败", orderInfo);
        uni.showModal({
          content: "获得订单信息失败 ",
          showCancel: false
        });
        return;
      }
      uni.requestPayment({
        provider: e.id,
        orderInfo: orderInfo.alipayTradeAppPayResponse,
        //  'app_id=2017121200619628&biz_content=%7b%22body%22%3a%22%e5%8d%87%e7%ba%a7%e4%bc%9a%e5%91%98%22%2c%22out_trade_no%22%3a%2220190919150908026814%22%2c%22product_code%22%3a%22QUICK_MSECURITY_PAY%22%2c%22subject%22%3a%22%e5%8d%87%e7%ba%a7%e4%bc%9a%e5%91%98%22%2c%22timeout_express%22%3a%2250m%22%2c%22total_amount%22%3a%220.01%22%7d&charset=UTF-8&format=json&method=alipay.trade.app.pay&notify_url=http%3a%2f%2f139.155.8.217%2fapi%2fAlipay%2fNotifyurl&sign_type=RSA2&timestamp=2019-09-19+15%3a09%3a12&version=1.0&sign=JwV4QwpHc8%2f%2bO3Udl%2bGw2uCa%2btPY%2bbbNnVwzz7c2VI%2bX5PXAxT2qeaNHys5PGd4a617G%2fNkHMPQWYceCvH1bXC5cln%2bf%2fJLg9%2fhlmstgqeBANS02dnqArHxQYs4MJMtYpLVT40beYYbfJT0eNt1stBc6Y0JvvRLteZm0H03YppgBeNgjlQj5eETPNl7qlF%2fOEKIOEIkKU47vG1dBDR1%2bF64%2bdvFgwXqBTKyBAOW1geuRSctoWytUTE%2fLx%2bgnmMgLOTnDwM4aEyy0ny3xWPhB5tBKBK3MlZRCrlJRg1lfQ8ys1N5BYGdphwzQf8nzZ5USEl%2bWRyzhRlWTpvbc9JUq6Q%3d%3d',
        success: e => {
          console.log("success", e);
          uni.showModal({
            title: "提示",
            content: "您已升级成功!",
            success: function(res) {
              uni.navigateBack({
                delta: 1
              });
            }
          });
        },
        fail: e => {
          console.log("fail", e);
          uni.showModal({
            content: "支付失败,原因为: " + e.errMsg,
            showCancel: false
          });
        },
        complete: () => {
          this.providerList[index].loading = false;
        }
      });
    },
    radioChange(e) {
      this.curtoViptype = e.detail.value;
      this.getOrderInfo();
    },

    getOrderInfo() {
      //获取订单信息
      uni.request({
        url: `${this.baseUrl}/api/Alipay/GetUpdateVip1Order?toVipType=${this.curtoViptype}`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {

            this.order = res.data.result;
          } else {
            uni.showToast({
              icon: "none",
              title: res.data.msg
            });
          }
        },
        fail: () => {
          uni.showToast({
            icon: "none",
            title: "网络异常"
          });
        }
      });
    },
    getMySelfInfo() {
      //获取个人信息
      uni.request({
        url: `${this.baseUrl}/api/Member/GetSelf`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            this.mySelf = res.data.result;
          } else {
            uni.showToast({
              icon: "none",
              title: res.data.msg
            });
          }
        },
        fail: () => {
          uni.showToast({
            icon: "none",
            title: "网络异常"
          });
        }
      });
    }
  }
};
</script>

<style>
.uni-list-cell {
  justify-content: flex-start;
}
.rmbLogo {
  font-size: 40upx;
}

button {
  background-color: #007aff;
  color: #ffffff;
}

.uni-h1.uni-center {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: flex-end;
}

.price {
  border-bottom: 1px solid #eee;
  width: 200upx;
  height: 80upx;
  padding-bottom: 4upx;
}

.ipaPayBtn {
  margin-top: 30upx;
}

.dsq {
  margin-top: 20px;
}

.dsq button {
  margin: 20px;
}

.userUpgradeVip {
  text-align: center;
  color: darkgrey;
}
</style>