<template>
	<view>
		<web-view src="https://ui.ptlogin2.qq.com/cgi-bin/login?style=9&appid=522005705&daid=4&s_url=https%3A%2F%2Fw.mail.qq.com%2Fcgi-bin%2Flogin%3Fvt%3Dpassport%26vm%3Dwsk%26delegate_url%3D%26f%3Dxhtml%26target%3D&hln_css=http%3A%2F%2Fmail.qq.com%2Fzh_CN%2Fhtmledition%2Fimages%2Flogo%2Fqqmail%2Fqqmail_logo_default_200h.png&low_login=1&hln_autologin=%E8%AE%B0%E4%BD%8F%E7%99%BB%E5%BD%95%E7%8A%B6%E6%80%81&pt_no_onekey=1" @message="handleMessage"></web-view>//https://w.mail.qq.com/cgi-bin/loginpage?f=xhtml
	</view>
</template>
<script>
	var wv; //计划创建的webview
	export default {
		methods: {
			handleMessage(evt) {
				console.log('接收到的消息：' + JSON.stringify(evt.detail.data));
			}
		},
		onReady() {
			console.log('读取storage'+uni.getStorageSync('12333'));
			// #ifdef APP-PLUS
			var currentWebview = this.$mp.page.$getAppWebview() //获取当前页面的webview对象
			setTimeout(function() {
				wv = currentWebview.children()[0];
	
				wv.setJsFile('static/new.js'); 
	
			}, 1000); //如果是页面初始化调用时，需要延时一下
			// #endif
		}
	};
</script>
