
<template name="loadding">
	<view>
		<view class="cu-load load-modal" v-if="loadModal">
			<!-- <view class="cuIcon-emojifill text-orange"></view> -->
			<image src="/static/logo.png" mode="aspectFit"></image>
			<view class="gray-text">加载中...</view>
		</view>
	</view>
</template>
 
<script>
export default {
	name: "loadding",
	props: {
		loadModal: true
	},
	data() {
		return {};
	},
	methods: {
		LoadModal(e) {
			this.loadModal = true;
			setTimeout(() => {
				this.loadModal = false;
			}, 2000);
		}
	}
};
</script>
 
<style></style>