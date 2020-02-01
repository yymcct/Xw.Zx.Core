<template>
	<view class="container">
		<view class="dsq">
			您还不是VIP,立即升级VIP即可分享二维码
		</view>
		<view class="title">
			<button v-on:click="upgrade" class="primary"  hover-class="none" type="primary">立即升级</button>
		</view>
	</view>
</template>
<script>
	export default {
		data() {
			return {
				user: null,
				val: "",
				size: 400, // 二维码大小
				onval: true, // val值变化时自动重新生成二维码
				loadMake: true, // 组件加载完成后自动生成二维码
				src: "" // 二维码生成后的图片地址或base64
			};
		},
		methods: {
			upgrade: function() {
				uni.navigateTo({  url: `../user/pay`})
			}
		},
		onLoad: function() {
			this.user = this.getUser("../user/user");
			if (!this.user) {
				return false;
			}
			
		}
	};
</script>

<style>
	/* @import "../../../common/icon.css"; */
	.container {
		display: flex;
		flex-direction: column;
		padding: 20px;
	}

	.dsq {
		font-size: 18px;
		font-weight: bold;
	}

	.qrimg {
		display: flex;
		justify-content: center;
		margin-top: 30px;
	}

	.title {
		display: flex;
		justify-content: center;
		width: 100%;
	}

	.uni-title {
		display: none;
		color: #999;
	}
</style>
