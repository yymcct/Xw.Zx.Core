<template>
	<!-- <uni-list>
		<view v-for="(vipcode,index) in vipcodeList" :key="vipcode.code">
			<uni-list-item title="标题文字" show-arrow="false">{num}</uni-list-item>
		</view>
	</uni-list> -->

	<view class="list" id="list">
		<view v-for="item in list" v-bind:key="item.code" class="card">
			<uni-swipe-action :show-arrow="true">
				<view class="uni-triplex-row crd">
					<view class="uni-triplex-left">
						<text class="uni-title uni-ellipsis">{{item.code}}</text>
						<text class="uni-text">过期时间: {{(item.expiesTime)}}</text>
						<text class="uni-text">状态: {{GetCodeState(item.uPdateVipAuthCodeState)}}</text>
					</view>
					<view class="uni-triplex-right uni-list-item__extra">
						<uni-icon :size="20" class="uni-icon-wrapper" color="#bbb" type="arrowright" />
					</view>
				</view>
			</uni-swipe-action>
		</view>
		<load-more :loadingType="loadingType" :contentText="contentText"></load-more>
	</view>

</template>

<script>
	import uniSwipeAction from "@/components/uni-swipe-action/uni-swipe-action.vue";
	import uniIcon from "@/components/uni-icon/uni-icon.vue";
	import segmentedControl from "@/components/segmented-control/segmented-control";
	import loadMore from "@/components/uni-load-more/uni-load-more";

	export default {
		components: {
			uniSwipeAction,
			uniIcon,
			loadMore,
			segmentedControl
		},
		data() {
			return {
				user: null,
				list: [],
				page: 0,
				pageSize: 10,
				loadingType: 0,
				contentText: {
					contentdown: "上拉显示更多",
					contentrefresh: "正在加载...",
					contentnomore: "我们也是有底线的~~~"
				},
				key: ""
			}
		},
		onLoad: function() {
			this.user = this.getUser("../user/user");
			console.log(this.user);
			if (!this.user) {
				return false;
			}

			const that = this;
			setTimeout(function() {
				that.loadData();
			}, 1000);
			uni.startPullDownRefresh();

		},
		onReachBottom() {
			const that = this;
			that.loadData();
		},
		onPullDownRefresh() {
			//监听下拉刷新动作的执行方法，每次手动下拉刷新都会执行一次
			const that = this;
			//that.page = 0;
			//that.list = [];
			that.loadingType = 0;
			console.log('refresh');
			setTimeout(function() {
				that.loadData();
				uni.stopPullDownRefresh(); //停止下拉刷新动画
			}, 1000);
		},
		methods: {
			GetCodeState: function(codeStateId) {
				if (codeStateId == 0) return "待使用";
				if (codeStateId == 1) return "已赠送";
				if (codeStateId == 2) return "已使用";
				if (codeStateId == 3) return "已过期";
				return codeStateId;
			},
			loadData: function() {
				const that = this;
				let list = [];
				that.page = that.page + 1;
				uni.request({
					url: `${that.baseUrl}/api/UpdateVipAuthCode/Get?filters=${that.key}&page=${that.page}&pageSize=${that.pageSize}`,
					method: "get",
					header: {
						"Content-Type": "application/x-www-form-urlencoded",
						"Authorization": `Bearer ` + that.user.token
					},
					success: res => {
						console.log(JSON.stringify(res));

						if (res.data.statusCode == "200") {
							if (res.data.result.length <= 0) {
								that.loadingType = 2;
								return false;
							}

							for (let i = 0; i < res.data.result.length; i++) {
								list.push(res.data.result[i]);
							};

							that.list = that.list.concat(list);
							that.loadingType = 0;
						} else {
							uni.showToast({
								icon: "none",
								title: res.data.msg
							});
						}
						this.text = "request success";
					},
					fail: () => {
						uni.showToast({
							icon: "none",
							title: "网络异常"
						});
					}
				});
			}
		}

	}
</script>

<style>
</style>
