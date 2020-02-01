<template>
	<view>
		<web-view :src="loginurl"></web-view>
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

		},
		onReady() {
			// #ifdef APP-PLUS
			
			var url = 'https://mp.weixin.qq.com/mp/homepage?__biz=MzUyNjcxODY4MA==&hid=1&sn=db5eedd6eea10f50980e2fdf67d22b46&scene=1&devicetype=android-28&version=27000834&lang=zh_CN&nettype=ctnet&ascene=7&session_us=gh_7a5817f999a0&wx_header=1';
			var currentWebview = this.$mp.page.$getAppWebview(); //获取当前页面的webview对象
			setTimeout(function() {
				wv = currentWebview.children()[0];
				wv.loadURL(url);
				
				var nwating = plus.nativeUI.showWaiting(); //显示原生等待框
				wv.addEventListener('loaded', function() {
					nwating.close(); //关闭等待框
					wv.show('slide-in-right', 150); //把新的webview窗口显示出来
				}, false);
				
			}, 500);
			
			// #endif
		}
	};
</script>
<style>
	.gohome {
		margin-top: 30px;
		margin-left: 30px;
		margin-right: 30px;
	}
</style>
