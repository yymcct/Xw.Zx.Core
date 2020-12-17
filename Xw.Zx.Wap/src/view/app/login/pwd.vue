<template>
  <div class="wrapper">
    <div class="log">
      <img :src="require('@/assets/images/log.png')" alt />
    </div>

    <div class="login">
      <van-field
        v-model="user.phone"
        required
        :formatter="$fieldFormatter"
        label="手机"
        placeholder="请输入手机号"
      />
      <sms-code-field
        v-model="user.smsCheck"
        :formatter="$fieldFormatter"
        :phone="user.phone"
      />
      <van-field
        v-model="user.password"
        required
        :formatter="$fieldFormatter"
        type="password"
        label="密码"
        placeholder="请输入密码"
      />
      <van-field
        v-model="user.password2"
        required
        :formatter="$fieldFormatter"
        type="password"
        label="密码"
        placeholder="请再次输入密码"
      />

      <van-button
        class="login-btn"
        type="primary"
        round
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="bindLogin"
        >提交
      </van-button>
      <div class="login-foot">
        <router-link :to="`/sqb/login/reg`"> 免费注册 </router-link>
        |
        <router-link :to="`/sqb/login`"> 立即登录 </router-link>
      </div>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
import smsCodeField from "@/components/smsCodeField";
export default {
  name: "",
  props: [""],
  data() {
    return {
      user: {
        phone: "",
        password: "",
        smsCheck: "",
      },
    };
  },

  components: { smsCodeField },

  computed: {},

  beforeMount() {},

  mounted() {},

  methods: {
    bindLogin() {
      const _this = this;
      if (this.user.phone.length != 11) {
        this.$toast("手机号不正确");
        return;
      }
      if (this.user.password.length < 6) {
        this.$toast("密码最短为 6 个字符");
        return;
      }
      if (this.user.smsCheck.length != 4) {
        this.$toast("验证码不正确");
        return;
      }
      if (this.user.password != this.user.password2) {
        this.$toast("两次输入密码不一样");
        return;
      }
      api.member
        .pwd({
          phone: this.user.phone,
          NewPassword: this.user.password,
          smsCheck: this.user.smsCheck,
        })
        .then((res) => {
          if (res.statusCode == 200) {
            _this.$toast("修改成功! 请去登录");
            setTimeout(() => {
              _this.$router.push(`/sqb/login`);
            }, 2000);
          }
        });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  background-color: #fff;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  .log {
    img {
      margin-top: 100px;
      width: 200px;
    }
  }
  .login {
    width: 90%;
    margin-top: 30px;
    //box-shadow: 0.02667rem 0.02667rem 0.21333rem #666;
    border-radius: 10px;
    overflow: hidden;
    padding: 30px 5px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    &-btn {
      width: 80%;
      margin: 20px 0;
    }
    &-foot {
      font-size: 14px;
      a {
        color: #ff5000;
      }
    }
  }
}
</style>