<template>
	<div class="LoginLog">
		<div class="handle-btn">
			<el-form :inline="true" :model="formInline" class="demo-form-inline">
				<el-form-item label="用户名称">
					<el-input v-model="formInline.lg_user" clearable style="width:150px"></el-input>
				</el-form-item>
				<el-form-item label="登录时间">
					<el-date-picker style="width:180px" value-format="yyyy-MM-dd HH:mm" format="yyyy-MM-dd HH:mm" default-time="00:00:00" v-model="lg_time"
					 type="datetime" placeholder="选择开始时间">
					</el-date-picker>
				</el-form-item>
				<el-form-item label="离开时间">
					<el-date-picker style="width:180px"  value-format="yyyy-MM-dd HH:mm" format="yyyy-MM-dd HH:mm" v-model="lg_move" type="datetime"
					 placeholder="选择结束时间" default-time="23:59:00">
					</el-date-picker>
					<!-- <el-date-picker value-format="yyyy-MM-dd" v-model="lg_move" type="date" placeholder="选择结束时间">
					</el-date-picker> -->
				</el-form-item>
				<el-form-item>
					<el-button type="primary" @click="search">查询</el-button>
				</el-form-item>
				<el-form-item>
					<el-button type="primary" @click="home">刷新</el-button>
				</el-form-item>
			</el-form>
		</div>
		<div class="tableParent">
			<List v-if="show1" v-on:changed="zidingyi($event)"></List>
			<SearchList ref="serachList" :formInline="formInline" v-if="show2"></SearchList>
		</div>
	</div>
</template>

<script>
	import List from "./List";
	import SearchList from "./SearchList";
	export default {
		name: "CounterMangement",
		components: {
			List,
			SearchList,
		},
		data() {
			return {
				show1: true,
				show2: false,
				searchType: 0,
				formInline: {
					lg_user: '',
					lg_time: '',
					lg_move: ''
				},
				editDat: '',
				lg_time: '',
				lg_move: ''

			};
		},
		created() {},
		mounted() {

		},
		computed: {

		},
		watch: {

		},
		methods: {

			search() {
				// 查询时间
				this.formInline.lg_time = this.lg_time;
				this.formInline.lg_move = this.lg_move;
				console.log(this.formInline.lg_time, this.formInline.lg_move)
				if (this.formInline.sr_name == '' && this.formInline.kssj == '' && this.formInline.jssj == '') {
					this.$message({
						showClose: true,
						message: '请输入查询条件',
						type: 'warning'
					});

					return false;
				}

				this.show1 = false;
				this.show2 = true;
				//this.$refs.serachList.getDataList()
				// if (!this.searchType) {
				// 	this.searchType = 1
				// } else {
				//
				// }

				this.$nextTick(function() {
					this.$refs.serachList.getDataList(1)
				})

			},
			home() {
				this.lg_time='';
			 this.lg_move='';
				this.formInline = {
					lg_user: '',
					lg_time: '',
					lg_move: ''
				};
				this.searchType = 0;
				this.show1 = true;
				this.show2 = false;
				//this.$refs.serachList.getDataList(1)
			},


		}
	};
</script>

<style lang="scss">
.LoginLog {
	height: 100%;
	width: 100%;
	padding: 0px 10px;
	min-width: 930px;

	.handle-btn {
		text-align: center;
	}

	.tableParent {
		height: calc(100% - 140px);

		.cus-pagination {
			padding-top: 10px;
			text-align: center;
		}
	}
	.picker-box{
		width: 170px;
		/deep/ .el-input__inner{
			padding-right: 0;
		}
	}
		/* overflow: hidden;
		position: relative;
		.log-search{
			position: absolute;
			left: 0;
			top: 0;
		} */
	
}
</style>
