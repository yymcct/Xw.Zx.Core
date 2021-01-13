<template>
  <div class="user">
    <div class="handle-btn">
      <el-form :inline="true" class="search-form">
        <el-form-item label="用户名称">
          <el-input v-model="keyword" placeholder="用户名称"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" size="medium" @click="onSubmit">查询</el-button>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" size="medium" @click="addUser">新增</el-button>
        </el-form-item>

        <!-- <vInputExcel @getResult="getMyExcelData" class="InputExcel"></vInputExcel> -->
        <el-form-item>
          <el-button type="primary" size="medium" @click="refreshUser">刷新</el-button>
        </el-form-item>
      </el-form>
      <!-- <vSearch @addUser="addUser" @getData="getData"></vSearch> -->
    </div>
    <div class="cus-common-table" v-loading="loading" element-loading-text="拼命加载中">
      <el-table
        :data="tableData"
        border
        stripe
        height="100%"
        :default-sort="{prop: 'UR_TIME', order: 'descending'}"
      >
        <el-table-column type="index" label="序号" width="50" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column prop="UR_IDENT" label="用户编号" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column prop="UR_NAME" label="用户名称" align="center" show-overflow-tooltip></el-table-column>
        <!-- <el-table-column prop="UR_STATE" label="在职状态" show-overflow-tooltip></el-table-column> -->
        <el-table-column prop="UR_ZONE" label="部门名称" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column prop="UR_LOGIN" label="登录名称" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column prop="UR_CRYPT" label="账号密码" show-overflow-tooltip></el-table-column>
        <!-- <el-table-column prop="UR_TIME" label="注册时间" sortable show-overflow-tooltip></el-table-column> -->
        <el-table-column label="操作" fixed="right" align="center" width="180">
          <template slot-scope="scope">
            <!-- <el-button  type="text" @click="handleDetail(scope.row)" title="详情"><i class="el-icon-zoom-in common-text"></i></el-button> -->
            <el-button type="text" @click="handleEdit(scope.row)" title="修改">
              <i class="el-icon-edit common-text"></i>修改
            </el-button>
            <el-button type="text" @click="handleDelete(scope.$index,scope.row)" title="删除">
              <i class="el-icon-delete common-text common-red"></i>
              <font class="common-red">删除</font>
            </el-button>
            <!-- <el-button icon="el-icon-zoom-in" size="mini" type="primary" circle @click="handleDetail(scope.row)" title="详情"></el-button>
            <el-button  icon="el-icon-edit" size="mini" type="primary" circle @click="handleEdit(scope.row)" title="编辑"></el-button>
            <el-button icon="el-icon-delete" size="mini" type="danger" circle @click="handleDelete(scope.$index,scope.row)" title="删除"></el-button>-->
          </template>
        </el-table-column>
      </el-table>
      <!-- <el-pagination background  @current-change="onChangePage" layout="total,prev,pager,next,jumper" :total="total" class="cus-pagination"></el-pagination> -->
    </div>
    <el-dialog
      :title="dialogTitle"
      :visible.sync="DialogShow"
      append-to-body
      v-dialogDrag
      width="600px"
      :close-on-click-modal="false"
    >
      <vForm
        :curData="curData"
        :orgInfo="orgInfo"
        :type="type"
        @saveAddUser="saveAddUser"
        @saveEditUser="saveEditUser"
        ref="userForm"
        v-if="DialogShow"
      ></vForm>
      <span slot="footer" class="dialog-footer">
        <el-button @click="closeDialog">取 消</el-button>
        <el-button type="primary" @click="submitForm">确 定</el-button>
        <el-button title="密码将被重置为1234" type="warning" v-show="type!='add'" @click="resetPassword">重置密码</el-button>
      </span>
    </el-dialog>
    <!-- <el-dialog title="导入" :visible.sync="dialogExcelShow">
			<vExcel v-if="dialogExcelShow" class="dialog-body" :excelData='excelData' :orgInfo="orgInfo" @saveAddUser="saveAddUser" ref="userExcel"></vExcel>
			<span slot="footer" class="dialog-footer">
				<el-button @click="closeDialog">取 消</el-button>
				<el-button type="primary" @click="submitExcel">确 定</el-button>
			</span>
    </el-dialog>-->
  </div>
</template>

<script>
import Form from "./children/form";
// import Excel from "./children/excel";
// import search from "./children/SearchForm";
// import InputExcel from "../inputExcel.vue"
import * as dataService from "@/public/apiService/sysManagement/Organization";
export default {
  props: ["orgInfo"],
  data: function() {
    return {
      keyword: "",
      tableData: [],
      DialogShow: false,
      curData: {},
      type: "add",
      loading: false,
      page: 1,
      // total: 0,
      excelData: [],
      dialogExcelShow: false
    };
  },
  computed: {
    dialogTitle: function() {
      return this.type == "add" ? "新增" : "编辑";
    }
  },
  created() {
    // this.getData(this.orgInfo.OR_CODE)
  },
  methods: {
    getData: function(orgCode) {
      this.loading = true;
      dataService.getUserList(this.keyword, orgCode, this.page).then(res => {
        //console.log(res);
        // this.total = res.SIZE;
        this.tableData = res.DATA;
        this.loading = false;
      });
    },
    // onChangePage: function (val) {
    // 	this.page = val;
    // 	this.getData(this.orgInfo.OR_CODE);
    // },
    addUser: function() {
      if (!this.orgInfo) {
        this.$message({
          type: "warning",
          message: "请先选择左边的组织机构"
        });
        return;
      }
      this.type = "add";
      this.DialogShow = true;
    },

    closeDialog: function() {
      this.DialogShow = false;
      this.dialogExcelShow = false;
    },
    handleEdit(row) {
      this.curData = Object.assign({}, row);
      this.type = "edit";
      this.DialogShow = true;
    },
    saveAddUser: function(params) {
      // console.log(params,'zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz')
      dataService.addUser(params).then(res => {
        //console.log(res);
        this.DialogShow = false;
        this.dialogExcelShow = false;
        if (res[0].success != true) {
          this.$message({
            type: "error",
            message: "添加失败!"
          });
          return;
        }
        this.getData(this.orgInfo.OR_CODE);
        this.$message({
          type: "success",
          message: "添加成功!"
        });
      });
    },
    saveEditUser: function(params) {
      dataService.editUser(params).then(res => {
        this.DialogShow = false;
        if (res.indexOf("true") == -1) {
          this.$message({
            type: "error",
            message: "修改失败!"
          });
          return;
        }
        this.getData(this.orgInfo.OR_CODE);
        this.$message({
          type: "success",
          message: "修改成功!"
        });
      });
    },
    handleDetail(row) {
      this.curData = row;
      this.type = "detail";
      this.DialogShow = true;
    },
    handleDelete(index, row) {
      this.$confirm("此操作将永久删除该条数据, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          dataService.delUser(row.UR_IDENT).then(res => {
            if (res.indexOf("true") == -1) {
              this.$message({
                type: "error",
                message: "删除失败!"
              });
              return;
            }
            this.getData(this.orgInfo.OR_CODE);
            this.$message({
              type: "success",
              message: "删除成功!"
            });
          });
        })
        .catch(() => {});
    },
    onSubmit: function() {
      this.getData(this.orgInfo.OR_CODE);
    },
    submitForm: function() {
      //console.log(this.$refs.userForm);
      this.$refs.userForm.onSubmitAdd();
    },
    resetPassword() {
      var params = {
        npass: "1234",
        ur_ident: this.curData.UR_IDENT
      };

      dataService
        .resetPassword(params)
        .then(res => {
          //console.log(res);
          if (res.success) {
            this.$message({
              message: "恭喜你，密码重置为：" + res.data[0].UR_LOGIN,
              type: "success"
            });
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    refreshUser() {
      this.keyword = "";
      this.getData(this.orgInfo.OR_CODE);
    },

    getMyExcelData(data) {
      //处理你的数据
      var data = JSON.parse(
        JSON.stringify(data)
          .replace(/用户编号/g, "UR_IDENT")
          .replace(/用户名称/g, "UR_LOGIN")
          .replace(/部门名称/g, "UR_ZONE")
          .replace(/登录名称/g, "UR_NAME")
          .replace(/登录密码/g, "UR_CRYPT")
      );
      this.excelData = data;

      this.dialogExcelShow = true;

      //相同文件名可以上传多次
      document.getElementsByClassName("input-file")[0].value = "";
    },
    submitExcel() {
      //console.log(this.$refs.userExcel);
      this.$refs.userExcel.onSubmitAdd();
      // 				console.log(data)
      // data.map(function(item){
      // 	console.log(item)
      // 	this.saveEditUser(item)
      // })
    }
  },
  components: {
    vForm: Form
    // vSearch:search
    // vInputExcel: InputExcel,
    // vExcel: Excel
  }
};
</script>
<style lang="scss" scoped>
.user {
  height: 100%;

  .handle-btn {
    padding: 10px 20px;
  }

  .cus-common-table {
    height: calc(100% - 30px);
  }
}
.InputExcel {
  width: 70px;
  height: 40px;

  display: inline-block;
  border-radius: 5px;
  margin-right: 10px;
}
.excel {
  position: fixed;
  top: 10px;
}

</style>
