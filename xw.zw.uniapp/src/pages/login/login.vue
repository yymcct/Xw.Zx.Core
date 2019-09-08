<template>
  <view class="content">
    <view class="input-group">
      <view class="input-row border">
        <text class="title">账号：</text>
        <m-input class="m-input" type="text" clearable focus v-model="account" placeholder="请输入账号"></m-input>
      </view>
      <view class="input-row">
        <text class="title">密码：</text>
        <m-input type="password" displayable v-model="password" placeholder="请输入密码"></m-input>
      </view>
    </view>
    <view class="btn-row">
      <button type="primary" class="primary" @tap="bindLogin">登录</button>
    </view>
    <view class="action-row">
      <navigator url="../reg/reg">注册账号</navigator>
      <text>|</text>
      <navigator url="../pwd/pwd">忘记密码</navigator>
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
      positionTop: 0,
      backpage: "../main/main"
    };
  },
  methods: {
    initPosition() {
      this.positionTop = uni.getSystemInfoSync().windowHeight - 100;
    },
    bindLogin() {
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
      uni.request({
        url: "http://139.199.110.116:63836/connect/token", //仅为示例，并非真实接口地址。
        data:
          "grant_type=password&client_id=App.Manager.Ro&client_secret=DEsjpJFtokIOhMKuE6BVMczYUEEyPGTOLrur3PXw26VMLNwKOfAKFZZgR2vVJDKG&username=" +
          this.account +
          "&password=" +
          this.password,
        method: "POST",
        header: {
          "Content-Type": "application/x-www-form-urlencoded"
        },
        success: res => {
          if (res.data.access_token) {
            uni.setStorageSync(
              "USERS_KEY",
              JSON.stringify({
                account: this.account,
                password: this.password,
                token: res.data.access_token
              })
            );

            uni.reLaunch({
                url:this.backpage
            });
          } else {
            uni.showToast({
              icon: "none",
              title: "用户账号或密码不正确"
            });
          }
          this.text = "request success";
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
  onReady() {
    this.initPosition();
  },
  onLoad(option) {
    if (option.backpage) {
      this.backpage = backpage;
    }
  }
};
</script>

<style>
.action-row {
  display: flex;
  flex-direction: row;
  justify-content: center;
}

.action-row navigator {
  color: #007aff;
  padding: 0 10px;
}

.oauth-row {
  display: flex;
  flex-direction: row;
  justify-content: center;
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
}

.oauth-image {
  width: 50px;
  height: 50px;
  border: 1px solid #dddddd;
  border-radius: 50px;
  margin: 0 20px;
  background-color: #ffffff;
}

.oauth-image image {
  width: 30px;
  height: 30px;
  margin: 10px;
}
</style>
