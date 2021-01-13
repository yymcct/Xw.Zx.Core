<template>
  <div class="schedule h100">
    <div class="handle-btn">
      <el-form :inline="true" class="demo-form-inline">
        <el-form-item label="事项标题/类型">
          <el-input v-model="planSearch" clearable></el-input>
        </el-form-item>

        <el-button type="primary" size="medium" @click="onSubmit">查询</el-button>
        <el-button type="primary" size="medium" @click="addSchedule">新建</el-button>
        <el-button type="primary" size="medium" @click="refresh">刷新</el-button>
      </el-form>
    </div>
    <div class="cus-common-table" v-loading="loading">
      <el-table :data="tableData" border stripe height="100%" :cell-style="cellStyle">
        <el-table-column type="index" width="70" label="序号" align="center"></el-table-column>
        <el-table-column prop="WIID" width="180" label="实例编号" align="center"></el-table-column>
        <el-table-column prop="PLANTITLE" label="事项标题" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column prop="PLANTYPE" label="事项类型" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column prop="PLANDATE" label="创建日期" width="180" align="center"></el-table-column>
        <el-table-column prop="PLANSTARTTIME" label="开始事项时间" width="180" align="center"></el-table-column>
        <el-table-column prop="PLANENDTIME" label="结束事项时间" width="180" align="center"></el-table-column>
        <el-table-column label="操作" fixed="right" width="260" align="center">
          <template slot-scope="scope">
            <el-button type="text" @click="handleDetail(scope.row)" title="详情">
              <i class="el-icon-zoom-in common-text"></i>详情
            </el-button>
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
      width="900px"
      :close-on-click-modal="false"
    >
      <vForm
        :curData="curData"
        :type="type"
        ref="ScheduleForm"
        v-if="DialogShow"
        @getData="getData"
        @dialogShow="dialogShow"
      ></vForm>
      <span slot="footer" class="dialog-footer" style="margin:0 auto;">
        <el-button type="primary" @click="submitForm" v-if="type!=='detail'">保存</el-button>
        <el-button :type="type=='detail'?'primary':''" @click="DialogShow = false">关闭</el-button>
      </span>
    </el-dialog>
  </div>
</template>
<script>
import form from "./children/form";
import * as dataService from "@/public/apiService/PersonalAffairs/schedule";
function addDate(date, days) {
  var date = new Date(date);
  days && date.setDate(date.getDate() + days);
  var month = date.getMonth() + 1;
  var day = date.getDate();
  var hours = date.getHours();
  var minutes = date.getMinutes();
  var mm = "'" + month + "'";
  var dd = "'" + day + "'";
  var hh = "'" + hours + "'";
  var MM = "'" + minutes + "'";
  //单位数前面加0
  if (mm.length == 3) {
    month = "0" + month;
  }
  if (dd.length == 3) {
    day = "0" + day;
  }
  if (hh.length == 3) {
    hours = "0" + hours;
  }
  if (MM.length == 3) {
    minutes = "0" + minutes;
  }
  var time =
    date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes;
  return time;
}
export default {
  data: function() {
    return {
      page: 1,
      tableData: [],
      DialogShow: false,
      type: "add",
      curData: null,
      planSearch: "",
      loading: false,
      total: 0
    };
  },
  mounted() {
    this.getData();
  },
  methods: {
    getData() {
      this.loading = true;
      var params = {
        page: this.page,
        plantitle: this.planSearch
      };
      console.log(params);

      dataService.getScheduleList(params).then(res => {
        this.tableData = res.DATA;
        this.total = res.SIZE;
        console.log(res.DATA);
        this.loading = false;
      });
    },
    onChangePage: function(val) {
      this.page = val;
      this.getData();
    },
    addSchedule: function() {
      this.DialogShow = true;
      this.type = "add";
    },
    onSubmit() {
      this.page = 1;
      this.getData(this.planSearch, this.page);
    },
    refresh() {
      this.page = 1;
      this.planSearch = "";
      this.getData(this.page);
    },
    //增加
    submitForm: function() {
      this.$refs.ScheduleForm.onSubmitAdd();
    },
    dialogShow(data) {
      this.DialogShow = data;
    },
    //修改
    handleEdit(row) {
      this.DialogShow = true;
      var params = {
        plantitle: row.PLANTITLE,
        plantype: row.PLANTYPE,
        plancontent: row.PLANCONTENT,
        planstarttime: row.PLANSTARTTIME,
        planendtime: row.PLANENDTIME,
        wiid: row.WIID,
        planendtx: parseInt(row.TX)
      };
      this.curData = params;
      this.type = "edit";
    },

    //详情
    handleDetail(row) {
      console.log(row.TX);
      this.DialogShow = true;
      this.type = "detail";

      var params = {
        plantitle: row.PLANTITLE,
        plantype: row.PLANTYPE,
        plancontent: row.PLANCONTENT,
        planstarttime: row.PLANSTARTTIME,
        planendtime: row.PLANENDTIME,
        plandate: row.PLANDATE,
        wiid: row.WIID,
        planendtx: parseInt(row.TX)
      };
      this.curData = params;
    },
    //删除
    handleDelete(row) {
      var params = {
        wiid: row.WIID
      };
      this.$confirm("此操作将永久删除该内容, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          dataService
            .getScheduleDel(params)
            .then(res => {
              if (res.success) {
                this.$message({
                  type: "success",
                  message: "删除成功!"
                });
                this.getData();
              }
            })
            .catch(err => {
              console.log(err);
              this.$message({
                type: "info",
                message: "删除操作失败"
              });
            });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除"
          });
        });
    },
    cellStyle(data) {
      if (data.row.PLANENDTIME > addDate(Date.now())) {
        return "background:#f9e4f5";
      }
    }
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
  components: {
    vForm: form
  }
};
</script>
<style lang="scss">
.schedule {
  height: calc(100% - 45px);
  &.h100 {
    height: 100%;
  }
  // min-width: 930px;
  width: 100%;
  padding: 0px 10px;

  .handle-btn {
    padding: 10px 20px;
    text-align: center;
  }
  .cus-common-table {
    height: calc(100% - 160px);
    .cus-pagination {
      padding-top: 10px;
      text-align: center;
    }
    .el-button--text {
      padding: 0px;
    }
  }
  .dialog-footer {
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
  }
}
</style>
