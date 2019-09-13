<template>
  <view>
    <view>
      <image style="width:100%" src="/static/img/bannerInvite20180528.png" />
    </view>
    <view class="input-group content">
      <view class="input-row border">
        <text class="title">手机号：</text>
        <m-input type="text" focus clearable v-model="account" placeholder="请输入手机号"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">密&nbsp;&nbsp;&nbsp;码：</text>
        <m-input type="password" displayable v-model="password" placeholder="请输入密码"></m-input>
      </view>
      <view class="input-row border">
        <text class="title">邮&nbsp;&nbsp;&nbsp;箱：</text>
        <m-input type="text" v-model="email" clearable   placeholder="请输入邮箱"></m-input>
      </view>
      <view class="input-row">
        <text class="title">邀请人：</text>
        <m-input type="text" disabled="disabled" :placeholder="invitePhone"></m-input>
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
      inviteid: 0,
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
        email: this.email,
        inviteid: this.inviteid
      };

      uni.request({
        url: "http://139.155.8.217/api/Member/PostRegisterUser",
        data: {
          phone: this.account,
          password: this.password,
          mail: this.email
        },
        method: "POST",
        header: {
          "Content-Type": "application/json"
        },
        success: res => {
          if (res.data.statusCode == 200) {
            uni.showModal({
              title: "提示",
              content: "您已注册成功, 请下载APP后登录!",
              success: function(res) {
                if (res.confirm) {
                  uni.navigateTo({ url: "../login/login" });//TODO 导航下载APP
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
  },
  onLoad: function(option) {
    console.log(option.inviteid);
    if (option.inviteid == undefined || option.inviteid == "") {
      uni.showToast({
        icon: "none",
        title: "邀请链接异常!无法注册,请联系开发人员!"
      });
    }
    this.inviteid = option.inviteid;
    uni.request({
      url: `${this.baseUrl}/api/Member/GetInviteUserPhone?id=${option.inviteid}`,
      method: "GET",
      header: {
        "Content-Type": "application/json"
      },
      success: res => {
        if (res.data.statusCode == 200) {
          this.invitePhone = res.data.result;
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
};
</script>

<style>
</style>
