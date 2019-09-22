<template>
	<view>
		后台同步中,请点击左上角返回按钮返回
		<web-view :src="loginurl" ></web-view>
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
		},
		onLoad:function() {
			this.user = this.getUser("../user/user");
			if (!this.user) {
				return false;
			}
			this.loginurl = `${this.baseUrl}/#/pages/reg/invitereg?id=${this.user.id.toString()}`;
			console.log(this.loginurl);
		},
		onReady() {

			// #ifdef APP-PLUS
			var currentWebview = this.$mp.page.$getAppWebview() //获取当前页面的webview对象
			setTimeout(function() {
				wv = currentWebview.children()[0];

				wv.setJsFile('static/new.js');
				//wv.loadURL(this.loginurl);
			}, 1000); //如果是页面初始化调用时，需要延时一下
			// #endif
		}
	};
</script>
