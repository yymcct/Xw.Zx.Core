<template>
  <div class="user">
    <div class="handle-btn">
      <el-button type="primary" size="medium" @click="addDepart">新增</el-button>
      <!-- <vInputExcel @getResult="getMyExcelData" class="InputExcel"></vInputExcel> -->
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
        <el-table-column prop="OR_CODE" label="部门编号" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column prop="OR_NAME" label="部门名称" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column prop="father" label="所属单位" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column prop="OR_REMARK" label="备注" align="center" show-overflow-tooltip></el-table-column>
        <el-table-column label="操作" fixed="right" align="center" width="180">
          <template slot-scope="scope">
            <!-- <el-button  type="text" @click="handleDetail(scope.row)" title="详情"><i class="el-icon-zoom-in common-text"></i></el-button> -->
            <el-button type="text" @click="handleEdit(scope.row)" title="修改">
              <i class="el-icon-edit common-text"></i>修改
            </el-button>
            <el-button type="text" @click="handleDelete(scope.row)" title="删除">
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
        :orgInfo="orgInfo"
        :type="type"
        @closeDialog="closeDialog"
        @saveAddDepart="saveAddDepart"
        @saveEditDepart="saveEditDepart"
        ref="departForm"
        v-if="DialogShow"
      ></vForm>
      <span slot="footer" class="dialog-footer">
        <el-button @click="closeDialog">取 消</el-button>
        <el-button type="primary" @click="submitForm">确 定</el-button>
      </span>
    </el-dialog>
    <!-- <el-dialog title="导入" :visible.sync="dialogExcelShow">
			<vExcel v-if="dialogExcelShow" class="dialog-body" ref="departExcel" :excelData='excelData' :orgInfo="orgInfo" @saveAddDepart="saveAddDepart"></vExcel>
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
// import InputExcel from "../inputExcel.vue"
import * as dataService from "@/public/apiService/sysManagement/Organization";
export default {
  props: ["orgInfo"],
  data: function() {
    return {
      loading: false,
      tableData: [],
      DialogShow: false,
      curData: null,
      type: "add",
      page: 1,
      total: 0,
      excelData: [],
      dialogExcelShow: false
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
      dataService.getDepartList(orgCode, this.page).then(res => {
        this.total = res.size;
        this.tableData = res.data;
        this.loading = false;
      });
    },
    onChangePage: function(val) {
      this.page = val;
      this.getData(this.orgInfo.OR_CODE);
    },
    addDepart: function() {
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
      this.curData = row;
      this.type = "edit";
      this.DialogShow = true;
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
          dataService.delDepart(row.OR_CODE).then(res => {
            if (res.success) {
              this.getData(this.orgInfo.OR_CODE);
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
    saveAddDepart: function(params) {
      console.log(this.$parent)
      dataService.addDepart(params).then(res => {
        this.DialogShow = false;
        if (res[0].success) {
          this.$message({
            type: "success",
            message: "添加成功!"
          });
          this.getData(this.orgInfo.OR_CODE);
          this.$emit('getTree','')
          return;
        }
        this.$message({
          type: "error",
          message: "添加失败!"
        });
      });
    },
    saveEditDepart: function(params) {
      dataService.editDepart(params).then(res => {
        if (res.success) {
          this.getData(this.orgInfo.OR_CODE);
          this.$message({
            type: "success",
            message: "修改成功!"
          });
          this.$emit('getTree','')
        }
        this.DialogShow = false;
      });
    },
    submitForm: function() {
      this.$refs.departForm.onSubmitAdd();
    },
    getMyExcelData(data) {
      //处理你的数据
      // 			console.log(this.tableData)
      this.excelData = data;
      this.dialogExcelShow = true;
      //处理你的数据
      var data = JSON.parse(
        JSON.stringify(data)
          .replace(/部门名称/g, "or_name")
          .replace(/备注/g, "or_remark")
      );
      this.excelData = data;
      this.dialogExcelShow = true;

      //相同文件名可以上传多次
      document.getElementsByClassName("input-file")[0].value = "";
    },
    submitExcel() {
      this.$refs.departExcel.onSubmitAdd();
      this.dialogExcelShow = false;
    }
  },
  components: {
    vForm: Form
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
    height: calc(100% - 56px);
  }
}

</style>
