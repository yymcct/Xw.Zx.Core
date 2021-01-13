<template>
  <div class="user">
    <div class="handle-btn">
      <el-button type="primary" size="medium" @click="add">新增单位</el-button>
    </div>
    <div class="cus-common-table">
      <el-table :data="tableData" border stripe height="100%">
        <el-table-column type="index" label="序号" width="50" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column prop="OR_CODE" label="单位编号" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column prop="OR_NAME" label="单位名称" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column prop="OR_REMARK" label="备注" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column label="操作" align="center" width="180">
          <template slot-scope="scope">
            <el-button type="text" @click="handleEdit(scope.row)" title="修改">
              <i class="el-icon-edit common-text"></i>修改
            </el-button>
            <el-button type="text" @click="handleDelete(scope.$index,scope.row)" title="删除">
              <i class="el-icon-delete common-text common-red"></i>
              <font class="common-red">删除</font>
            </el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        background
        @current-change="onChangePage"
        layout="total,prev,pager,next,jumper"
        :total="total"
        class="cus-pagination"
      ></el-pagination>
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
        :type="type"
        @saveAddUnit="saveAddUnit"
        @saveEditUnit="saveEditUnit"
        ref="userForm"
        v-if="DialogShow"
      ></vForm>
      <span slot="footer" class="dialog-footer">
        <el-button @click="closeDialog">取 消</el-button>
        <el-button type="primary" @click="submitForm">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import Form from "./children/form";
import * as dataService from "@/public/apiService/sysManagement/Organization";
export default {
  data: function() {
    return {
      tableData: [],
      DialogShow: false,
      curData: null,
      type: "add",
      page: 1,
      total: 0
    };
  },
  computed: {
    dialogTitle: function() {
      return this.type == "add" ? "新增" : "编辑";
    }
  },
  methods: {
    getData: function(orgCode) {
      this.loading = true;
      dataService.getUnitList(this.page).then(res => {
        this.total = res.SIZE;
        this.tableData = res.DATA;
        this.loading = false;
      });
    },
    onChangePage: function(val) {
      this.page = val;
      this.getData();
    },
    add: function() {
      this.type = "add";
      this.DialogShow = true;
    },
    closeDialog: function() {
      this.DialogShow = false;
    },
    handleEdit(row) {
      this.curData = row;
      this.type = "edit";
      this.DialogShow = true;
      console.log(row);
    },
    saveAddUnit: function(params) {
      dataService.addUnit(params).then(res => {
        console.log(res, "res==");
        this.DialogShow = false;
        if (res.success) {
          this.getData();
          this.$message({
            type: "success",
            message: "添加成功!"
          });
          this.$emit('getTree','')
        } else {
          this.$message({
            type: "error",
            message: "添加失败!"
          });
        }
      });
    },
    saveEditUnit: function(params) {
      dataService.editDepart(params).then(res => {
        if (res.success) {
          this.getData();
          this.$message({
            type: "success",
            message: "修改成功!"
          });
          this.$emit('getTree','')
        }
        this.DialogShow = false;
      });
    },
    // handleDetail(row) {
    //   this.curData=row;
    //   this.type='detail';
    //    this.DialogShow=true;
    // },
    handleDelete(index, row) {
      console.log(index, row);

      this.$confirm("此操作将永久删除该条数据, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          dataService.delDepart(row.OR_CODE).then(res => {
            if (res.success) {
              this.getData();
              this.$message({
                type: "success",
                message: "删除成功!"
              });
              this.$emit('getTree','')
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
    submitForm: function() {
      this.$refs.userForm.onSubmitAdd();
    }
  },
  components: {
    vForm: Form
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
    height: calc(100% - 56px);
  }
}
// el-dialog: 左删除 右取消
>>>.el-dialog__footer {
  padding-bottom: 100px !important;
}
// /deep/ .el-dialog .el-dialog__footer {
//   padding-bottom: 60px !important;
// }
// .el-dialog__footer .el-button {
//   float: right !important;
// }
// .el-dialog__footer button:nth-child(2) {
//   margin-right: 10px !important;
//   margin-left: 0 !important;
// }
// //messageBox button: 左删除 右取消
// .el-message-box__btns .el-button--small {
//   float: right !important;
// }
// .el-message-box__btns button:nth-child(2) {
//   margin-right: 10px !important;
//   margin-left: 0 !important;
// }
</style>
