<template>
	<div class="CounterMangement">
		<div class="handle-btn">
			<el-form :inline="true" :model="formInline" class="demo-form-inline">
				<el-form-item label="附件名称">
					<el-input style="width:180px" v-model="formInline.sr_name" clearable></el-input>
				</el-form-item>
				<el-form-item label="上传时间">
					<el-date-picker style="width:180px" value-format="yyyy-MM-dd" v-model="formInline.kssj" type="date" placeholder="选择上传时间">
					</el-date-picker>
				</el-form-item>
				<el-form-item label="~">
					<el-date-picker style="width:180px" value-format="yyyy-MM-dd" v-model="formInline.jssj" type="date" placeholder="选择结束时间">
					</el-date-picker>
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
	import List from "./children/List";
	import SearchList from "./children/SearchList";
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
					sr_name: '',
					kssj: '',
					jssj: ''
				},
				editDat: ''

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
				if (!this.searchType) {
					this.searchType = 1
				} else {
					this.$refs.serachList.getDataList(1)
				}

			},
			home() {
				this.searchType=0;
				this.show1 = true;
				this.show2 = false;
				this.formInline= {
					sr_name: '',
					kssj: '',
					jssj: ''
				}
			},


		}
	};
</script>

<style lang="scss">
	.CounterMangement {
    height: calc(100% - 45px);
    width: 100%;
    //min-width: 930px;
    .handle-btn {
      padding: 10px 20px;
      text-align: center;
    }
    .tableParent{
      height: calc(100% - 140px);
      .cus-pagination {
        padding-top: 10px;
        text-align: center;
      }
    }
	}
</style>
