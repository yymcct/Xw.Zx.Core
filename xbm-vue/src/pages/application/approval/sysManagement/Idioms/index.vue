<template>
  <div class="Address">
    <div class="handle-btn">
      <el-button type="primary" size="medium" @click="addIdioms">新增</el-button>
      <el-button type="primary" size="medium" @click="refresh">刷新</el-button>
    </div>
    <div class="cus-common-table" v-loading="loading">
      <el-table :data="tableData" border stripe height="100%">
        <el-table-column align="center" type="index" width="50" label="序号"></el-table-column>
        <el-table-column align="center" prop="WIID" label="ID" width="100" show-overflow-tooltip></el-table-column>
        <el-table-column align="center" prop="GYYNR" label="惯用语内容" show-overflow-tooltip></el-table-column>
        <el-table-column
          align="center"
          prop="GYYCJSJ"
          width="150"
          label="创建时间"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          align="center"
          prop="GYYSYPL"
          width="100"
          label="使用次数"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          align="center"
          prop="GYYZJSYSJ"
          width="150"
          label="最后使用时间"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column align="center" prop="GYYFL" width="100" label="分类" show-overflow-tooltip></el-table-column>
        <el-table-column align="center" prop="GYYBT" width="100" label="标题" show-overflow-tooltip></el-table-column>
        <el-table-column align="center" label="操作" width="180">
          <template slot-scope="scope">
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
        :current-page="page"
        :total="total"
        class="cus-pagination"
      ></el-pagination>
    </div>
    <el-dialog
      :title="title"
      append-to-body
      :visible.sync="dialogFormVisible"
      label-width="100px"
      :close-on-click-modal="false"
    >
      <el-form :model="form" status-icon ref="Form" label-width="100px" class="demo-ruleForm">
        <el-form-item
          label="惯用语内容"
          prop="gyynr"
          :rules="{ required: true, message: '请输入惯用语内容', trigger: 'blur' }"
        >
          <el-input type="textarea" placeholder="请输字惯用语内容" v-model="form.gyynr"></el-input>
        </el-form-item>
        <el-form-item label="惯用语分类" prop="gyyfl">
          <el-select style="width: 100%;" v-model="form.gyyfl" placeholder="请选择惯用语分类">
            <el-option label="常用" value="常用"></el-option>
            <el-option label="办公" value="办公"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="惯用语标题" prop="gyybt">
          <el-input placeholder="请输字惯用语标题" v-model="form.gyybt"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="submit()">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import Form from "./children/form";
import { setTimeout } from "timers";
import * as dataService from "@/public/apiService/PersonalAffairs/idioms";
export default {
  data: function() {
    return {
      total: 0,
      tableData: [],
      dialogFormVisible: false,
      type: "add",
      loading: false,
      userInfo: JSON.parse(localStorage.getItem("data")),
      page: 1,
      title: "",
      form: {
        yy_user: JSON.parse(localStorage.getItem("data")).ur_ident,
        gyynr: "",
        gyyfl: "",
        gyybt: ""
      }
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
  },
  methods: {
    getData() {
      this.loading = true;
      var params = {
        yy_user: this.userInfo.ur_ident,
        page: this.page
      };
      dataService.getMy(params).then(res => {
        console.log(res);
        this.pageSize = parseInt(res.PAGE_SIZE);
        this.total = res.SIZE;
        this.tableData = res.data;
        this.loading = false;
      });
    },
    onChangePage: function(val) {
      this.page = val;
      this.getData();
    },
    addIdioms() {
      this.form = {
        yy_user: this.userInfo.ur_ident,
        gyynr: "",
        gyyfl: "",
        gyybt: ""
      };
      this.type = "add";
      this.title = "新增惯用语";
      this.dialogFormVisible = true;
    },
    handleEdit(row) {
      this.type = "edit";
      this.form = {
        yy_user: this.userInfo.ur_ident,
        gyynr: row.GYYNR,
        gyyfl: row.GYYFL,
        gyybt: row.GYYBT,
        wiid: row.WIID
      };
      this.title = "修改惯用语";
      this.dialogFormVisible = true;
    },
    handleDetail(row) {
      this.curData = row;
      this.type = "detail";
      this.DialogShow = true;
    },
    handleDelete(row) {
      this.$confirm("此操作将永久删除该条数据, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var params = {
            wiid: row.WIID
          };
          dataService.getDel(params).then(res => {
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
    submit() {
      this.$refs["Form"].validate(valid => {
        if (valid) {
          if (this.type == "add") {
            dataService
              .getAdd(this.form)
              .then(res => {
                console.log(res);
                this.$message({
                  message: "添加成功！",
                  type: "success"
                });
                this.dialogFormVisible = false;
                this.getData();
              })
              .catch(err => {
                console.log(err);
              });
          } else {
            dataService
              .getEdit(this.form)
              .then(res => {
                console.log(res);
                this.$message({
                  message: "修改成功！",
                  type: "success"
                });
                this.dialogFormVisible = false;
                this.getData();
              })
              .catch(err => {
                console.log(err);
              });
          }
        } else {
          return false;
        }
      });
    },
    refresh() {
      this.page = 1;
      this.getData();
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
  height: calc(100% - 45px);
  min-width: 930px;
  padding: 0px 10px;

  .handle-btn {
    padding: 10px 20px;
  }

  .cus-common-table {
    height: calc(100% - 120px);

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
