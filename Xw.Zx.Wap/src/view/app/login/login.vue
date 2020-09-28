<template>
  <div class="view-content">
    <div class="view-content-body">
      <div class="login-view border">
        <div class="input-title">账号：</div>
        <m-input
          type="text"
          class="login-input"
          clearable
          focus
          v-model="account"
          placeholder="请输入账号"
        ></m-input>
      </div>
      <div class="login-view border">
        <div class="input-title">密码：</div>
        <m-input
          type="password"
          class="login-input"
          displayable
          v-model="password"
          placeholder="请输入密码"
        ></m-input>
      </div>
      <div class="btn-row">
        <button
          type="primary"
          hover-class="none"
          class="primary"
          @tap="bindLogin"
        >
          登录
        </button>
      </div>
      <div class="action-row">
        <navigator url="../reg/reg">注册账号</navigator>
        <text>|</text>
        <navigator url="../pwd/pwd">忘记密码</navigator>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "",
  props: [""],
  data() {
    return {
      account: "",
      password: "",
      positionTop: 0,
      backpage: "../main/main",
    };
  },

  components: {},

  computed: {},

  beforeMount() {},

  mounted() {},

  methods: {
    bindLogin() {
      if (this.account.length < 5) {
        uni.showToast({
          icon: "none",
          title: "账号最短为 5 个字符",
        });
        return;
      }
      if (this.password.length < 6) {
        uni.showToast({
          icon: "none",
          title: "密码最短为 6 个字符",
        });
        return;
      }
      uni.request({
        url: `${this.baseUrl}/connect/token`, //仅为示例，并非真实接口地址。
        data:
          "grant_type=password&client_id=App.Manager.Ro&client_secret=DEsjpJFtokIOhMKuE6BVMczYUEEyPGTOLrur3PXw26VMLNwKOfAKFZZgR2vVJDKG&username=" +
          this.account +
          "&password=" +
          this.password,
        method: "POST",
        header: {
          "Content-Type": "application/x-www-form-urlencoded",
        },
        success: (res) => {
          if (res.data.access_token) {
            uni.setStorageSync(
              "USERS_KEY",
              JSON.stringify({
                id: res.data.id,
                account: this.account,
                password: this.password,
                token: res.data.access_token,
              })
            );

            uni.reLaunch({
              url: this.backpage,
            });
          } else {
            uni.showToast({
              icon: "none",
              title: "用户账号或密码不正确",
            });
          }
          this.text = "request success";
        },
        fail: () => {
          uni.showToast({
            icon: "none",
            title: "网络异常",
          });
        },
      });
    },
  },

  watch: {},
};
</script>
<style lang='' scoped>
</style>