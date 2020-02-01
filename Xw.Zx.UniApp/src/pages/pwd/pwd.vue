<template>
  <view class="content">
    <view class="input-group">
      <view class="input-row">
        <text class="title">手机号：</text>
        <m-input type="text" focus clearable v-model="phone" placeholder="请输入手机号"></m-input>
      </view>
      <view class="input-row">
        <text class="title">新密码：</text>
        <m-input type="password" displayable v-model="password" placeholder="请输入新密码"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">验证码：</text>
        <m-input type="text" displayable v-model="smsCheck" placeholder="请输入短信验证码"></m-input>
      </view>
    </view>
    <view class="btn-row" v-show="phone!=''">
      <button type="primary" :disabled="!enableSend" hover-class="none" @tap="getCode">获取验证码</button>
    </view>
    <view class="btn-row">
      <button type="primary" hover-class="none" class="primary" @tap="findPassword">提交</button>
    </view>
  </view>
</template>

<script>
import service from "../../service.js";
import mInput from "../../components/m-input.vue";

export default {
  components: {
    mInput
  },
  data() {
    return {
      phone: "",
      password: "",
      smsCheck: "",
      enableSend: true
    };
  },
  methods: {
    getCode: function() {
      if (this.phone.length < 11) {
        uni.showToast({
          icon: "none",
          title: "请输入正确的手机号"
        });
        return;
      }
      var _this = this;
      _this.enableSend = false;
      uni.request({
        url: `${this.baseUrl}/api/Member/GetSmsCode?phone=${this.phone}`,
        method: "GET",
        success: res => {
          if (res.data.statusCode == 200) {
            uni.showToast({
              icon: "none",
              title: "验证码已发送"
            });
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
    findPassword() {
      if (this.phone.length < 11) {
        uni.showToast({
          icon: "none",
          title: "请输入正确的手机号"
        });
        return;
      }
      if (this.password.length < 6) {
        uni.showToast({
          icon: "none",
          title: "密码最短为 6 个字符"
        });
        return;
      }
      if (this.smsCheck.length < 4) {
        uni.showToast({
          icon: "none",
          title: "请填写验证码"
        });
        return;
      }
      uni.request({
        url: `${this.baseUrl}/api/Member/PostChangePasswordBySmsCode`,
        data: {
          phone: this.phone,
          NewPassword: this.password,
          smsCheck: this.smsCheck
        },
        method: "POST",
        header: {
          "Content-Type": "application/json"
        },
        success: res => {
          if (res.data.statusCode == 200) {
            uni.showModal({
              title: "提示",
              content: "密码重置成功, 请前往登录!",
              success: function(res) {
                if (res.confirm) {
                  uni.navigateTo({ url: "../login/login" });
                } else if (res.cancel) {
                  console.log("用户点击取消");
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
</style>
