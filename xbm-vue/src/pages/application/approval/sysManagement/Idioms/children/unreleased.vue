<template>
  <div class="Notice">
    <div class="handle-btn">
      <el-form :inline="true" class="search-form">
        <el-form-item label="公告名称">
          <el-input v-model="keyword" placeholder="请输入关键字" clearable></el-input>
        </el-form-item>
        <el-button type="primary" size="medium" @click="onSubmit">查询</el-button>
        <el-button type="primary" size="medium" @click="addNotice">新建</el-button>
      </el-form>
    </div>
    <div class="cus-common-table" v-loading="loading" element-loading-text="拼命加载中">
      <el-table :data="tableData" border stripe height="100%">
        <el-table-column type="index" width="70" label="序号"></el-table-column>
        <el-table-column
          prop="NT_FBZT"
          label="发布状态"
          width="90"
          :filters="[{ text: '未发布', value: 0 }, { text: '已发布', value: 1 }]"
          :filter-method="filterTag"
          filter-placement="bottom-end"
        >
          <template slot-scope="scope">
            <div class="cell" v-if="scope.row.NT_FBZT==0">未发布</div>
            <div class="cell" v-else-if="scope.row.NT_FBZT==1">已发布</div>
          </template>
        </el-table-column>
        <el-table-column prop="WIID" label="公告编号" min-width="150" show-overflow-tooltip></el-table-column>
        <el-table-column prop="NT_NAME" label="公告名称" min-width="180" show-overflow-tooltip></el-table-column>
        <el-table-column prop="NT_DEPT" label="发布部门" show-overflow-tooltip></el-table-column>
        <el-table-column prop="NT_SENDER" label="发布人" show-overflow-tooltip></el-table-column>
        <el-table-column prop="NT_TIME" label="发布时间" show-overflow-tooltip></el-table-column>
        <el-table-column prop="NT_MOVE" label="有效期限" show-overflow-tooltip></el-table-column>
        <el-table-column prop="NT_URGENT" label="紧急程度" show-overflow-tooltip></el-table-column>
        <el-table-column label="操作" fixed="right" width="260">
          <template slot-scope="scope">
            <el-button type="text" @click="handleDetail(scope.row)" title="详情">
              <i class="el-icon-zoom-in common-text"></i>详情
            </el-button>
            <el-button type="text" @click="handleEdit(scope.row)" title="修改">
              <i class="el-icon-edit common-text"></i>修改
            </el-button>
            <el-button type="text" @click="handleReleased(scope.row)" title="发布">
              <i class="el-icon-refresh common-text"></i>发布
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
    <!--  @saveAddNotice="saveAddNotice"-->
    <el-dialog
      :title="dialogTitle"
      :visible.sync="DialogShow"
      v-dialogDrag
      append-to-body
      width="900px"
      :close-on-click-modal="false"
    >
      <vForm :curData="curData" :type="type" ref="NoticeForm" v-if="DialogShow" @getData="getData"></vForm>
      <span slot="footer" class="dialog-footer" style="margin:0 auto;">
        <el-button type="primary" @click="submitForm(0)" v-if="type!=='detail'">保存</el-button>
        <el-button type="primary" @click="submitForm(1)" v-if="type!=='detail'">保存并发布</el-button>
        <el-button :type="type=='detail'?'primary':''" @click="DialogShow = false">关闭</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import Form from "./form";
import * as dataService from "@/public/apiService/PersonalAffairs/address";
export default {
  data: function() {
    return {
      keyword: "",
      page: 1,
      // pageSize:5,
      total: 0,
      tableData: [],
      DialogShow: false,
      curData: null,
      type: "add",
      loading: false
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
    // this.getData();
  },
  methods: {
    getData: function() {
      this.loading = true;
      dataService.getUnreleasedNotice(this.page, this.keyword).then(res => {
        this.total = res.SIZE;
        this.tableData = res.DATA;
        this.loading = false;
        this.DialogShow = false;
      });
    },
    onChangePage: function(val) {
      this.page = val;
      this.getData();
    },
    addNotice: function() {
      this.type = "add";
      this.DialogShow = true;
    },
    handleEdit(row) {
      dataService.checkNotice(row.WIID).then(res => {
        this.curData = res[0];
        this.type = "edit";
        this.DialogShow = true;
      });
    },
    handleReleased: function(row) {
      if (row.NT_FBZT == 1) {
        this.$message({
          type: "warning",
          message: "当前已发布,不能重复发布!"
        });
        return;
      }
      dataService.updateNoticeState(row.WIID).then(res => {
        if (res.success) {
          this.$message({
            type: "success",
            message: "发布成功!"
          });
          this.getData();
        }
      });
    },
    handleDetail(row) {
      dataService.checkNotice(row.WIID).then(res => {
        this.curData = res[0];
        this.type = "detail";
        this.DialogShow = true;
      });
    },
    handleDelete(row) {
      this.$confirm("此操作将永久删除该条数据, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          dataService.delNotice(row.WIID).then(res => {
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
    filterTag(value, row) {
      return row.NT_FBZT === value;
    },
    onSubmit: function() {
      this.getData(this.keyword);
    },
    submitForm: function(num) {
      // num 0未发布 1已发布
      this.$refs.NoticeForm.onSubmitAdd(num);
    }
  },
  components: {
    vForm: Form
    // vSearch:search
  }
};
</script>
<style lang="scss">
.Notice {
  height: 100%;
}
</style>
