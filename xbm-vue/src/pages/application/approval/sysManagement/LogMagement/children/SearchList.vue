<template>
		<div class="list" v-loading="loading">
			<el-table :data="dataList" border style="width: 100%"  height="100%">
				<el-table-column type="index" width="50" label="编号" align="center">
				</el-table-column>
				<!-- <el-table-column prop="LG_USER" label="用户编号" width="80" align="center">
				</el-table-column> -->
				<el-table-column prop="LG_NAME" label="用户姓名" width="150" align="center">
				</el-table-column>
				<el-table-column prop="LG_ADDR" label="网络地址" align="center">
				</el-table-column>
				<el-table-column prop="LG_HOST" label="主机名称" width="200" align="center">
				</el-table-column>
				<el-table-column prop="LG_TIME" label="登录时间" width="180" align="center">
				</el-table-column>
				<el-table-column prop="LG_MOVE" label="退出时间" width="180" align="center">
				</el-table-column>
				<el-table-column fixed="right" label="操作" width="100" align="center">
					<template slot-scope="scope">
						<el-button @click="del(scope.$index)" type="text">
							<i class="el-icon-delete common-text common-red"></i>
							<font class="common-red">删除</font>
						</el-button>
					</template>
				</el-table-column>
			</el-table>
      <el-pagination background layout="total, prev, pager, next, jumper" :current-page="page" class="cus-pagination" @current-change="currentChange"
                     :page-size="10" :total="total">
      </el-pagination>
		</div>

</template>

<script>
	import * as dataService from "@/public/apiService/sysManagement/logMangement";
	export default {
		name: "List",
		components: {

		},
		props: ['formInline'],
		data() {
			return {
				page: 1,
				dataList: [],
				loading: true,
				total: 0,
				data1: []

			};
		},
		created() {},
		mounted() {
			//this.getDataList(this.page)
		},
		computed: {


		},
		methods: {
			//获取列表
			getDataList(a) {
				this.page=a;
				var data = this.formInline;
				this.loading=true;
				dataService.getDataSearch(a, data.lg_user, data.lg_time, data.lg_move).then((res) => {
					console.log(res)
					this.dataList = res.DATA;
					this.loading = false;
					//获取类型
					this.total = res.SIZE



				}).catch((err) => {
					console.log(err)
				})
			},
			currentChange(val) {
				console.log(val)
				this.loading = true;
				this.page = val;
				this.getDataList(val)
			},
			del(index, row) {
				console.log(index)
				this.$confirm('此操作将永久删除该内容, 是否继续?', '提示', {
					closeOnClickModal: false,
					confirmButtonText: '确定',
					cancelButtonText: '取消',
					type: 'warning'
				}).then(() => {
					dataService.getDataDel(this.dataList[index].LG_CODE).then((res) => {
						console.log(res)
						this.getDataList(this.page)
						this.$message({
							type: 'success',
							message: '删除成功!'
						});

					}).catch((err) => {
						console.log(err)
						this.$message({
							type: 'info',
							message: '删除操作失败'
						});
					})


				}).catch(() => {
					this.$message({
						type: 'info',
						message: '已取消删除'
					});
				});




			}

		}
	};
</script>

<style lang="scss">
  .list{
    height: 100%;
  }
</style>
