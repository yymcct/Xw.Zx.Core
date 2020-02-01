<template>
  <view>
    <web-view :src="loginurl"></web-view>

    <button type="primary" hover-class="none" class="gohome" v-on:click="goHome()">返回</button>
  </view>
</template>
<script>
var wv; //计划创建的webview
export default {
  data() {
    return {
      user: null,
      loginurl: null
    };
  },
  methods: {
    goHome: function() {
      uni.reLaunch({
        url: "../main/main"
      });
    }
  },
  onLoad: function() {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
    this.loginurl = `${
      this.baseUrl
    }/api/Sync/Notthing?id=${this.user.id.toString()}`;
    console.log(this.loginurl);
  },
  onReady() {
    // #ifdef APP-PLUS
    var currentWebview = this.$mp.page.$getAppWebview(); //获取当前页面的webview对象
    setTimeout(function() {
      wv = currentWebview.children()[0];

      //wv.setJsFile('static/new.js');
      wv.appendJsFile("static/new.js");
      //wv.loadURL(this.loginurl);
    }, 1000); //如果是页面初始化调用时，需要延时一下
    // #endif
  }
};
</script>
<style>
.gohome {
  margin-top: 30px;
  margin-left: 30px;
  margin-right:  30px;
}
</style>>
