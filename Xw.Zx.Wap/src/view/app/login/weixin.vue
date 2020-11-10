<template>
  <div class="wapper">
    <van-loading class="loading" type="spinner" size="24px"
      >登录中...</van-loading
    >
  </div>
</template>

<script>
import { userInfoAPI } from "@/utils/auth";
import api from "@/api/sqbApi";
export default {
  name: "WeixinLogin",
  data() {
    return {};
  },
  computed: {},
  created() {
    this.logIn();
  },
  methods: {
    logIn() {
      const { code, state } = this.getUrlCode();
      if (!code || state !== "weixin") {
        const appid = "wx87734a5a656fc8cb";
        const scope = "snsapi_userinfo";
        const redirectUri = location.href;
        window.location.href = `https://open.weixin.qq.com/connect/oauth2/authorize?appid=${appid}&redirect_uri=${encodeURIComponent(
          redirectUri
        )}&response_type=code&scope=${scope}&state=weixin#wechat_redirect`;
      } else {
        api.member.weixinLogin(code).then((res) => {
          if (res.msg) {
            this.$router.push(`/sqb/login/bind?id=${res.msg}`);
          } else {
            userInfoAPI.set(res.result);
            this.$store.commit("user/setUser", res.result.member);
            this.$router.push(`/sqb/home`);
          }
        });
      }
    },
    getUrlCode() {
      // 截取url中的code方法
      var url = location.search;
      this.winUrl = url;
      var theRequest = new Object();
      if (url.indexOf("?") != -1) {
        var str = url.substr(1);
        var strs = str.split("&");
        for (var i = 0; i < strs.length; i++) {
          theRequest[strs[i].split("=")[0]] = strs[i].split("=")[1];
        }
      }
      return theRequest;
    },
  },
};
</script>
<style lang='scss' scoped>
.wapper {
  display: flex;
  justify-content: center;
  .loading {
    top: 20px;
  }
}
</style>