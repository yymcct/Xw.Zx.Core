<template>
	<div class="interfaceDetail">
		<!-- <el-table :data="dataList" height="250" border style="width: 100%">
			<el-table-column prop="date" label="日期" width="180">
			</el-table-column>
			<el-table-column prop="name" label="姓名" width="180">
			</el-table-column>
			<el-table-column prop="address" label="地址">
			</el-table-column>
		</el-table> -->
		<div v-for="(value,key,index) in dataList">
			<p>{{key}}</p>
			<p>{{value}}</p>
		</div>
	</div>
</template>

<script>
	import * as dataService from "@/public/apiService/sysManagement/interface";
	import Bus from "@/public/event";
	export default {
		name: "interfaceDetail",
		components: {

		},
		data() {
			return {
				dataList: [],
				loading: true,
				props1: [],

			};
		},
		created() {},
		mounted() {
			var that = this;
			Bus.$on('interface-parameter', function(val) {
				console.log(val);
				that.getDataDetail(val.BX_BIZID, val.BX_ORDER)
			});
		},
		computed: {

		},
		methods: {
			getDataDetail(a, b) {
				dataService.getDataDetail(a, b).then((res) => {
					console.log(res)
					this.dataList=res.data[0]
				}).catch((err) => {
					console.log(err)
				})
			},
		}
	};
</script>

<style lang="scss">
	.interfaceDetail {}
</style>
