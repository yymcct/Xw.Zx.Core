<template>
  <view class="container">
    <view class="dsq">
      <view class="title">使用兑换码</view>
      <input class="uni-input" v-model="vipcode" focus placeholder="输入您的VIP兑换码" />
    </view>
    <view class="enter">
      <button v-on:click="doUpgradeSubmit" class="primary" hover-class="none" type="primary">确定</button>
    </view>
  </view>
</template>
<script>
export default {
  data() {
    return {
      vipcode: "",
      user: null
    };
  },
  methods: {
    doUpgradeSubmit: function() {
      if (this.vipcode.length < 5) {
        uni.showToast({
          icon: "none",
          title: "请输入VIP兑换码"
        });
        return;
      }

      uni.request({
        url: `${this.baseUrl}/api/UpdateVipAuthCode/Use`,
        data: {
          code: this.vipcode
        },
        method: "get",
        header: {
          "Content-Type": "application/x-www-form-urlencoded",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            uni.showModal({
              title: "提示",
              content: "兑换成功",
              showCancel: false,
              success: function(res) {
                if (res.confirm) {
                  uni.navigateBack({
                    delta: 2
                  });
                }
              }
            });
          } else {
            uni.showToast({
              icon: "none",
              title: res.data.msg
            });
          }
        },
        fail: e => {
          console.log("fail", e);
          uni.showToast({
            icon: "none",
            title: "网络异常"
          });
        }
      });
    }
  },
  onLoad: function() {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
  }
};
</script>

<style>
/* @import "../../../common/icon.css"; */
.container {
  display: flex;
  flex-direction: column;
  padding: 20px;
}

.dsq {
  font-size: 18px;
  font-weight: bold;
}

.qrimg {
  display: flex;
  justify-content: center;
  margin-top: 30px;
}

.title {
  display: flex;
  justify-content: center;
  width: 100%;
}
.enter {
  margin-top: 2rem;
}

.uni-title {
  display: none;
  color: #999;
}
</style>
