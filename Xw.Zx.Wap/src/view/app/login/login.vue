<template>
  <div class="wrapper">
    <div class="log">
      <img :src="require('@/assets/images/log.png')" alt />
    </div>

    <div class="login">
      <van-field
        v-model="account"
        :formatter="$fieldFormatter"
        label="手机"
        placeholder="请输入手机号"
      />
      <van-field
        v-model="password"
        :formatter="$fieldFormatter"
        type="password"
        label="密码"
        placeholder="请输入密码"
      />

      <van-button
        class="login-btn"
        type="primary"
        round
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="bindLogin"
        >登录</van-button
      >
      <div class="login-foot">
        <router-link :to="`/sqb/login/reg`"> 免费注册 </router-link>

        |
        <router-link :to="`/sqb/login/pwd`"> 忘记密码 </router-link>
      </div>
    </div>
  </div>
</template>

<script>
import { userInfoAPI } from "@/utils/auth";
import api from "@/api/sqbApi";
export default {
  name: "",
  props: [""],
  data() {
    return {
      account: "",
      password: "",
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    const isWeixin = () =>
      /micromessenger/.test(navigator.userAgent.toLowerCase());

    if (isWeixin()) {
      this.$router.push("/sqb/login/weixin");
    }
  },

  mounted() {},

  methods: {
    bindLogin() {
      if (this.account.length != 11) {
        this.$toast("手机号不正确");
        return;
      }
      if (this.password.length < 6) {
        this.$toast("密码最短为 6 个字符");
        return;
      }
      api.member
        .login({
          account: this.account,
          password: this.password,
        })
        .then((res) => {
          userInfoAPI.set(res.result);
          this.$store.commit("user/setUser", res.result.member);
          let url = this.$globalFun.userInfoAPI.getLoginFrom() ;
          if(!url){
            url = "/sqb/home";
          }
          this.$router.push(url);
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