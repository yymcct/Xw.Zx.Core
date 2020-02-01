<template>
	<view class="download">
		<view v-if="isweiixn">
			<a href="#" download="sqb.apk" @click="ckdownload">已注册完成点此直接下载APP</a>
		</view>
		<view v-else>
			<a href="http://139.155.8.217:8081/sqb.apk" download="sqb.apk" @click="ckdownload">已注册完成，在浏览器点此直接下载APP</a>
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
		isweiixn: false
    };
  },
  methods: {
	  isWeiXin: function() {
	  	var ua = window.navigator.userAgent.toLowerCase();
	  	if (ua.match(/MicroMessenger/i) == "micromessenger") {
	  		return true; // 是微信端
	  	} else {
	  		return false;
	  	}
	  },
	  ckdownload: function() {
	  		if (this.isWeiXin()) {
	  			uni.showModal({
	  				title: "提示",
	  				content: "请点击右上角,在浏览器中打开,并安装应用!",
	  				success: function(res) {
	  					if (res.confirm) {} else if (res.cancel) {}
	  				}
	  			});
	  		} else {
	  			uni.showToast({
	  				icon: "none",
	  				title: "后台下载中,请稍后...!"
	  			});
	  		}
	  },
		onLoad: function(option) {
			if (this.isWeiXin()) {
				this.isweiixn = true;
			}
		}
  }
};
</script>
<style>
	.download {
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		width: 100%;
	}
	
	.download view {
		color: #c8c7cc;
	}
	
	.download a {
		color: #f16613;
	}
</style>>
