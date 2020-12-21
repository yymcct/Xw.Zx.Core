<template>
  <div class="Address">
    <div class="handle-btn">
      <el-form :inline="true" :model="searchForm" class="search-form">
        <el-form-item label="姓名">
          <el-input v-model="searchForm.ep_name" placeholder="姓名" clearable></el-input>
        </el-form-item>
        <el-form-item label="部门">
          <el-input v-model="searchForm.ep_group" placeholder="部门" clearable></el-input>
        </el-form-item>
        <el-button type="primary" size="medium" @click="onSubmit">查询</el-button>
        <el-button type="primary" size="medium" v-if="delshow" @click="addAddress">新增</el-button>
        <el-button type="primary" size="medium" @click="refresh">刷新</el-button>
        <!-- <el-button type="primary" size="medium" @click="onExport">导出</el-button>
        <el-button type="primary" size="medium" @click="onPrint">打印</el-button>-->
      </el-form>
    </div>
    <div class="cus-common-table" v-loading="loading">
      <el-table :data="tableData" border stripe height="100%">
        <el-table-column type="index" width="80" label="序号" align="center"></el-table-column>
        <el-table-column prop="EP_NAME" label="姓名" width="100" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column prop="EP_SEXY" label="性别" width="50" align="center">
          <template slot-scope="scope">
            <div class="cell" v-if="scope.row.EP_SEXY==0">男</div>
            <div class="cell" v-else-if="scope.row.EP_SEXY==1">女</div>
            <div class="cell" v-else>-</div>
          </template>
        </el-table-column>
        <el-table-column prop="EP_GROUP" label="部门" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column prop="EP_DUTY" label="职务" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column
          prop="EP_OFFICEPHONE"
          label="办公电话"
          show-overflow-tooltip
          align="center"
          width="130"
        ></el-table-column>
        <el-table-column
          prop="EP_MOBILE1"
          label="手机号码"
          show-overflow-tooltip
          align="center"
          width="130"
        ></el-table-column>
        <!-- <el-table-column prop="EP_MOBILE2" label="手机号码二" show-overflow-tooltip></el-table-column> -->
        <el-table-column label="操作" fixed="right" :width="delshow?'200':'150'" align="center">
          <template slot-scope="scope">
            <el-button type="text" @click="handleDetail(scope.row)" title="详情">
              <i class="el-icon-zoom-in common-text"></i>详情
            </el-button>
            <el-button type="text" v-if="delshow" @click="handleEdit(scope.row)" title="修改">
              <i class="el-icon-edit common-text"></i>修改
            </el-button>
            <el-button v-if="delshow" type="text" @click="handleDelete(scope.row)" title="删除">
              <i class="el-icon-delete common-text common-red"></i>
              <font class="common-red">删除</font>
            </el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        background
        :current-page="searchForm.page"
        @current-change="onChangePage"
        layout="total,prev,pager,next,jumper"
        :total="total"
        class="cus-pagination"
      ></el-pagination>
    </div>
    <el-dialog
      :title="dialogTitle"
      :visible.sync="DialogShow"
      v-dialogDrag
      width="900px"
      append-to-body
      :close-on-click-modal="false"
    >
      <vForm
        :curData="curData"
        :title="title"
        :type="type"
        :typep="typep"
        @saveAddBook="saveAddBook"
        @saveEditBook="saveEditBook"
        ref="addressForm"
        v-if="DialogShow"
      ></vForm>
      <span slot="footer" class="dialog-footer" style="margin:0 auto;">
        <el-button type="primary" @click="submitForm" v-if="type!=='detail'">确 定</el-button>
        <el-button :type="type=='detail'?'primary':''" @click="DialogShow = false">关闭</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import Form from "./form";
import { setTimeout } from "timers";
import * as dataService from "@/public/apiService/PersonalAffairs/address";
export default {
  data: function() {
    return {
      searchForm: {
        ep_name: "",
        ep_group: "",
        page: 1,
        ur_ident: 0
      },
      // pageSize:5,
      total: 0,
      tableData: [],
      DialogShow: false,
      curData: null,
      type: "add",
      typep: "0",
      loading: false,
      title: "公共通讯录",
      uid: JSON.parse(localStorage.getItem("data")).ur_ident,
      delshow: false
    };
  },
  computed: {
    dialogTitle: function() {
      if (this.type == "add") {
        return "新增";
      } else if (this.type == "edit") {
        return "编辑";
      } else {
        return "详情";
      }
    }
  },
  created() {
    this.getData();
    this.delPublicAddress(this.uid);
  },
  methods: {
    //获取有没有公共通讯录新增修改删除权限
    delPublicAddress(uid) {
      dataService
        .delPublicAddress(uid)
        .then(res => {
          //console.log(res)
          if (res.success) {
            this.delshow = true;
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    refresh() {
      this.searchForm = {
        ep_name: "",
        ep_group: "",
        page: 1,
        ur_ident: 0
      };
      this.getData();
    },
    getData: function() {
      this.loading = true;
      dataService.getAddressList(this.searchForm).then(res => {
        this.pageSize = parseInt(res.PAGE_SIZE);
        this.total = res.SIZE;
        this.tableData = res.data;
        this.loading = false;
      });
    },
    onChangePage: function(val) {
      this.searchForm.page = val;
      this.getData();
    },
    addAddress: function() {
      this.type = "add";
      this.DialogShow = true;
    },
    onExport: function() {
      this.$message({
        type: "success",
        message: "导出成功!"
      });
    },
    onPrint: function() {
      this.$message({
        type: "success",
        message: "打印成功!"
      });
    },
    handleEdit(row) {
      this.curData = row;
      this.type = "edit";
      this.DialogShow = true;
    },
    saveAddBook: function(params) {
      dataService.addAddress(params).then(res => {
        if (res.success) {
          this.$message({
            type: "success",
            message: "添加成功!"
          });
        }
        this.getData();
        this.DialogShow = false;
      });
    },
    saveEditBook: function(params) {
      dataService.editAddress(params).then(res => {
        if (res.success) {
          this.$message({
            type: "success",
            message: "修改成功!"
          });
        }
        this.getData();
        this.DialogShow = false;
      });
    },
    handleDetail(row) {
      this.curData = row;
      this.type = "detail";
      this.DialogShow = true;
    },
    handleDelete(row) {
      this.$confirm("此操作将永久删除该条数据, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          dataService.delAddress(row.EPID).then(res => {
            if (res.success) {
              this.getData();
              this.$message({
                type: "success",
                message: "删除成功!"
              });
              return;
            }
            this.$message({
              type: "error",
              message: res.msg
            });
          });
        })
        .catch(() => {});
    },
    onSubmit: function() {
      this.searchForm.page = 1;
      this.getData(this.keyword);
    },
    submitForm: function() {
      this.$refs.addressForm.onSubmitAdd();
    }
  },
  components: {
    vForm: Form
    // vSearch:search
  }
};
</script>
<style lang="scss">
.Address {
  height: 100%;
  min-width: 930px;
  padding: 0px 10px;

  .handle-btn {
    padding: 10px 20px;
  }

  .cus-common-table {
    height: calc(100% - 160px);

    .cus-pagination {
      padding-top: 10px;
      text-align: center;
    }

    .el-button--text {
      padding: 0px;
      font-weight: bolder;
    }
  }

  .el-dialog__footer {
    text-align: center;
  }
}
</style>
