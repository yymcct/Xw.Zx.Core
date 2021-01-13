<template>
	<div class="user">
		  <div class="handle-btn">
          <el-form :inline="true" :model="formInline" class="demo-form-inline">
            <el-form-item label="用户名称">
              <el-input
                style="width:180px"
                v-model="formInline.sr_name"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="search">查询</el-button>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="onRefres">刷新</el-button>
            </el-form-item>
          </el-form>
        </div>
		<div class="cus-common-table" v-loading="loading" element-loading-text="拼命加载中">
			<el-table :data="tableData" border stripe height="100%" :default-sort="{prop: 'UR_TIME', order: 'descending'}">

				<el-table-column type="index" label="序号" width="50" align="center" show-overflow-tooltip></el-table-column>
				<el-table-column prop="UR_IDENT" label="用户编号" align="center" show-overflow-tooltip></el-table-column>
				<el-table-column prop="UR_NAME" label="用户名称" align="center" show-overflow-tooltip></el-table-column>
				<!-- <el-table-column prop="UR_STATE" label="在职状态" show-overflow-tooltip></el-table-column> -->
				<el-table-column prop="UR_ZONE" label="部门名称" align="center" show-overflow-tooltip></el-table-column>
				<el-table-column prop="UR_LOGIN" label="登录名称" align="center" show-overflow-tooltip></el-table-column>
				<el-table-column label="操作" fixed="right" align="center" width="120">
					<template slot-scope="scope">
						<!-- <el-button  type="text" @click="handleDetail(scope.row)" title="详情"><i class="el-icon-zoom-in common-text"></i></el-button> -->
						<el-button type="text" @click="handleEdit(scope.row)" title="修改">
							<i class="el-icon-edit common-text"></i>修改
						</el-button>
					</template>
				</el-table-column>
			</el-table>
		</div>
	</div>
</template>

<script>
import * as dataService from "@/public/apiService/sysManagement/Organization";
export default {
	props: ['orgInfo'],
	data: function() {
		return {
			formInline: {
				sr_name:''
			},
			tableData: [],
			loading: false,
			page: 1,
			total: 0
		};
	},
	computed: {

	},
	created() {

	},
	methods: {
		getData: function(orgCode) {
			this.loading = true;
			dataService.getUserList(this.formInline.sr_name, orgCode, this.page).then(res => {
				this.total = res.SIZE;
				this.tableData = res.DATA;
				this.loading = false;
			})
		},
		search() {
			if (this.formInline.sr_name == "" ) {
				this.$message({
				showClose: true,
				message: "请输入查询条件",
				type: "warning"
				});
				return false;
			}
			this.getData(this.orgInfo.OR_CODE);
			},
		handleEdit(row) {
			this.$emit('showdedatil', row)
		},
		onRefres:function(){
			this.formInline.sr_name='';
           this.getData(this.orgInfo.OR_CODE);
		},
	},
	components: {}
};
</script>
<style lang="scss" scoped>
.user {
	height: 100%;

	.handle-btn {
		padding: 10px 20px;
	}

	.cus-common-table {
		height: calc(100% - 93px);
	}
}
</style>
