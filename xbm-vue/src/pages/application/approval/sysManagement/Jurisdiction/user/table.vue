<template>
	<div class="Jurisdiction">
		<v-flex-container :leftWidth="'calc(100% - 180px)'" :leftMaxWidth="'calc(100% - 400px)'" style="height:100%">
			<div slot="left" class="org-left">
				<!-- <el-button type="primary">主要按钮</el-button> -->
				<el-breadcrumb style="height: 30px;line-height: 30px;font-size: 18px;font-weight: 600;padding: 5px 10px;" separator-class="el-icon-arrow-right">
					<el-breadcrumb-item style="cursor: pointer;" @click.native="back">{{tableDat.UR_ZONE}}</el-breadcrumb-item>
					<el-breadcrumb-item>{{tableDat.UR_NAME}}
					</el-breadcrumb-item>
				</el-breadcrumb>
				<div class="Jurisdiction-right">
					<div class="cus-common-table" v-loading="loadingTable">
						<div style="float: left;width:100%;height:40px">
							<el-button type="primary" size="small" style="float: left;padding:10px 15px;" @click="handleAllDelete">批量删除</el-button>
						</div>
						<div style="height:calc(100% - 50px)">
							<el-table :data="tableData" border stripe height='100%' @selection-change="handleSelectionChange">
								<el-table-column type="selection" width="55">
								</el-table-column>
								<el-table-column align="center" type="index" width="50" label="序号"></el-table-column>
								<el-table-column align="center" prop="Ru_BizID" label="业务编号" width="100" show-overflow-tooltip></el-table-column>
								<el-table-column align="center" prop="Ru_Role" label="权限编号" width="100" show-overflow-tooltip></el-table-column>
								<el-table-column align="center" prop="Bz_Name" width="150" label="业务名称" show-overflow-tooltip></el-table-column>
								<el-table-column align="center" prop="Br_Name" width="150"  label="角色名称" show-overflow-tooltip></el-table-column>
								<el-table-column align="center" prop="Br_Remark" label="角色描述" show-overflow-tooltip></el-table-column>
								<el-table-column align="center" label="操作" width="70" fixed="right">
									<template slot-scope="scope">
										<el-button type="text" @click="handleDelete(scope.row)" title="删除">
											<i class="el-icon-delete common-text common-red"></i>
											<font class="common-red">删除</font>

										</el-button>
									</template>
								</el-table-column>
							</el-table>

							<el-pagination background @current-change="onChangePage" layout="total,prev,pager,next,jumper" :total="total" class="cus-pagination"></el-pagination>
						</div>
					</div>
				</div>
			</div>
			<div slot="right" class="org-right" v-loading="loading" element-loading-text="拼命加载中">
				<h2 class="menu-title">业务权限
					<el-button type="primary" icon="el-icon-plus" size="mini" style="margin-right:10px;float: right;" @click="addJur" :disabled='dis'>新增</el-button>
				</h2>
				<div style="height: calc(100% - 40px);overflow: auto;" v-loading="loadingTree">
					<el-tree class='tree' :data="treeData" :props="defaultProps" ref="tree" @node-click="handleNodeClick" show-checkbox @check-change="handleCheckChange" lazy :load="load"></el-tree>
				</div>

			</div>
		</v-flex-container>
	</div>
</template>

<script>
import flexContainer from "@/components/FlexContainer";
import * as dataServices from "@/public/apiService/sysManagement/Jurisdiction";
export default {
	props: ['tableDat'],
	data() {
		return {
			loading: false,
			userInfo: JSON.parse(sessionStorage.getItem("userInfo")),
			ur_ident: '',
			page: 1,
			tableData: [],
			total: 0,
			defaultProps: {
				label: 'Bz_Name',
				children: 'children',
				isLeaf: 'leaf'
			},
			treeData: [],
			pageAll: 1,
			loadingTable: false,
			dis: true,
			checkDat: [],
			loadingTree: false,
			multipleSelection: []

		};
	},
	created() {
		this.ur_ident = this.tableDat.UR_IDENT
		console.log('123')
	},
	mounted() {
		this.getData()
		this.getBusiness()
	},
	methods: {
		getData() {
			var params = {
				page: this.page,
				ur_ident: this.ur_ident,
				
			}
			this.loadingTable = true
			dataServices.getDataList(params).then(res => {
				// console.log(res)
				this.loadingTable = false;
				this.tableData = res.data;
				this.total = res.SIZE;
			}).catch(err => {
				console.log(err)
			})
		},
		getBusiness() {
			this.loadingTree = true;
			dataServices.getDataPersonList({}).then(res => {
				// console.log(res)
				this.treeData = res.data;
				this.loadingTree = false;
				// console.log(this.treeData)
			}).catch(err => {
				console.log(err)
			})
		},
		getAll(id) {
			var params = {
				page: this.pageAll,
				ru_bizid: id
			}
			return dataServices.getDataAllList(params);
		},
		add() {

		},
		handleAllDelete() {
			var _this = this;
			var data = []
			this.multipleSelection.map((item) => {
				var data1 = {
					ru_bizid: item.Ru_BizID,
					ru_role: item.Ru_Role,
					ru_user: _this.ur_ident
				}
				data.push(data1)

			})
			var params = {
				DATA: data,

			};
			this.$confirm('此操作将永久删除该文件, 是否继续?', '提示', {
				closeOnClickModal:false,
				confirmButtonText: '确定',
				cancelButtonText: '取消',
				type: 'warning'
			}).then(() => {

				dataServices.getDataDel(params).then(res => {
					this.getData()
					this.$message({
						type: 'success',
						message: '删除成功!'
					});
				}).catch(err => {
					console.log(err)
					this.$message({
						type: 'error',
						message: '删除失败！'
					});
				})

			}).catch(() => {
				this.$message({
					type: 'info',
					message: '已取消删除'
				});
			});


		},
		handleDelete(val) {
			// console.log(val)
			var data = [{
				ru_bizid: val.Ru_BizID,
				ru_role: val.Ru_Role,
				ru_user: this.ur_ident
			}]
			var params = {
				DATA: data,

			};
			// console.log(params)
			this.$confirm('此操作将永久删除该文件, 是否继续?', '提示', {
				closeOnClickModal: false,
				confirmButtonText: '确定',
				cancelButtonText: '取消',
				type: 'warning'
			}).then(() => {

				dataServices.getDataDel(params).then(res => {
					// console.log(res)
					this.getData()
					this.$message({
						type: 'success',
						message: '删除成功!'
					});
				}).catch(err => {
					// console.log(err)
					this.$message({
						type: 'error',
						message: '删除失败！'
					});
				})

			}).catch(() => {
				this.$message({
					type: 'info',
					message: '已取消删除'
				});
			});


		},
		onChangePage(val) {
			this.page = val;
			this.getData();
		},
		handleNodeClick(data, node, c) {
			// console.log(data, node, c)
			if (node.level == 1) {
				// console.log(this.treeData.indexOf(data))
			}

		},
		load(node, resolve) {
			if (node.level == 1) {
				this.getAll(node.data.Ru_BizID).then(res => {
					for (var i = 0; i < res.data.length; i++) {

						res.data[i].Parent = res.data[i].Bz_Name;
						res.data[i].leaf = true;
						res.data[i].Bz_Name = res.data[i].Br_Name;
					}
					return resolve(res.data)
				}).catch(err => {
					// console.log(err)
				})

			}

		},
		addJur() {
			var params = {
				DATA: this.checkDat
			}
			dataServices.getDataAdd(params).then(res => {
				this.$message({
					showClose: true,
					message: '权限添加成功！',
					type: 'success'
				});
				this.getData()

			}).catch(err => {
				console.log(err)
			})


		},
		handleCheckChange(a, b, c) {
			this.haha();
			if (this.checkDat.length > 0) {
				this.dis = false;
			} else {
				this.dis = true;
			}


		},
		haha() {
			//验证是否选择
			var checkData = this.$refs.tree.getCheckedNodes();
			var data = [];
			for (var i = 0; i < checkData.length; i++) {
				var child = {
					ru_bizid: '',
					ru_role: '',
					ru_user: ''
				}
				if (checkData[i].leaf) {
					child = {
						RU_BIZID: checkData[i].Ru_BizID,
						RU_ROLE: checkData[i].Ru_Role,
						RU_USER: this.ur_ident
					}
					data.push(child)
				}
			}

			this.checkDat = data;
		},
		test() {
			this.$store.commit("navTabs/changeTab", '1007');
		},
		back() {
			this.$emit('back')
		},
		handleSelectionChange(val) {
			this.multipleSelection = val;
		}
	},
	components: {
		"v-flex-container": flexContainer,
	}
};
</script>
<style lang="scss" >
 .Jurisdiction {
	height: 100%;
	// /deep/ .el-breadcrumb__item{
    //    cursor: pointer;
	// }
	.flex-left{
		height:100%;
	}
	/deep/  .org-right,.org-left,.flex-left{
      height: 100%;
	}
		.Jurisdiction-right {
			height: 100%;
			// min-width: 930px;
			padding: 0px 10px;
			.handle-btn {
				padding: 10px 20px;
			}
		/deep/	.cus-common-table {
				height: calc(100% - 80px);
			/deep/	.cus-pagination {
					padding-top: 10px;
					text-align: center;
				}

				/deep/ .el-button--text {
					padding: 0px;
					font-weight: bolder;
				}
			}

			/deep/ .el-dialog__footer {
				text-align: center;
			}
		}

	/deep/ .org-right {
		.menu-title {
			background: #f5f5f5;
			padding: 8px 10px;
			font-size: 16px;
		}

		.tree {
			height: 100%;
			overflow: auto;

			.box-card {
				height: calc(100% - 3px);
				margin: 0px 1px 1px;
				border: 1px solid #ebeef5;
			}
		}
	}

	.cus-common-table {
		width: 98%;
		margin: 0 auto;
		text-align: center;

	/deep/	.el-button {
			padding: 0px;
		}

	/deep/	.cus-pagination {
			padding: 10px;
		}
	}
}
</style>
