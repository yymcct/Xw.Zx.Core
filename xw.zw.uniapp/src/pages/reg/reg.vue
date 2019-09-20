<template>
  <view class="content">
    <view class="input-group">
      <view class="input-row border">
        <text class="title">账号：</text>
        <m-input type="text" focus clearable v-model="account" placeholder="请输入账号"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">密码：</text>
        <m-input type="password" displayable v-model="password" placeholder="请输入密码"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">账单邮箱：</text>
        <m-input type="text" clearable v-model="email" placeholder="请输入账单邮箱"></m-input>
      </view>
      <view class="input-row">
        <text class="title">邀请人：</text>
        <m-input type="text" clearable v-model="invitePhone" placeholder="请输入邀请人电话"></m-input>
      </view>
    </view>
    <view class="btn-row">
      <button type="primary" class="primary" @tap="register">注册</button>
    </view>
  </view>
</template>

<script>
import mInput from "../../components/m-input.vue";
export default {
  components: {
    mInput
  },
  data() {
    return {
      account: "",
      password: "",
      email: "",
      invitePhone: ""
    };
  },
  methods: {
    register() {
      if (this.account.length < 5) {
        uni.showToast({
          icon: "none",
          title: "账号最短为 5 个字符"
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
      if (this.email.length < 3 || !~this.email.indexOf("@")) {
        uni.showToast({
          icon: "none",
          title: "邮箱地址不合法"
        });
        return;
      }
      if (this.invitePhone.length < 11 || this.inviteid == 0) {
        uni.showToast({
          icon: "none",
          title: "邀请人不合法"
        });
        return;
      }

      const data = {
        account: this.account,
        password: this.password,
        email: this.email
      };

      uni.request({
        url: `${this.baseUrl}/api/Member/PostRegisterUser`,
        data: {
          phone: this.account,
          password: this.password,
          mail: this.email,
          invitePhone:this.invitePhone
        },
        method: "POST",
        header: {
          "Content-Type": "application/json"
        },
        success: res => {
          if (res.data.statusCode == 200) {
            uni.showModal({
              title: "提示",
              content: "您已注册成功, 请前往登录!",
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
