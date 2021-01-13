<template>
  <div class="msgManage">
    <div class="handle-btn">
      <el-form :inline="true" class="search-form">
        <el-form-item label="消息内容">
          <el-input v-model="at_matter" placeholder="消息内容" clearable></el-input>
        </el-form-item>
        <el-form-item label="发表时间">
          <el-date-picker
            v-model="searchDate"
            value-format="yyyy-MM-dd"
            type="daterange"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
            format="yyyy 年 MM 月 dd 日"
          ></el-date-picker>
        </el-form-item>
        <el-button type="primary" size="medium" @click="onSubmit">查询</el-button>
        <el-button type="primary" size="medium" @click="refresh">刷新</el-button>
        <!-- <el-button type="primary" size="medium" @click="addAddress">新增</el-button> -->
      </el-form>
    </div>
    <div class="cus-common-table" v-loading="loading">
      <el-table :data="tableData" border stripe height="100%" :cell-style="cellStyle">
        <el-table-column type="index" width="80" label="序号" align="center"></el-table-column>
        <el-table-column prop="AID" label="消息编号" width="150" align="center"></el-table-column>
        <el-table-column prop="AT_MATTER" label="消息内容" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column prop="AT_THEME" label="所属栏目" width="200" align="center"></el-table-column>
        <el-table-column prop="AT_STIME" label="发布日期" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column prop="AT_CTIME" label="有效期限" show-overflow-tooltip align="center"></el-table-column>
        <!-- <el-table-column prop="AT_UID" label="发表人" show-overflow-tooltip></el-table-column> -->
        <el-table-column prop="AT_STATE" label="状态" width="100" align="center"></el-table-column>
        <el-table-column label="操作" fixed="right" width="100" align="center">
          <template slot-scope="scope">
            <el-button type="text" @click="handleDetail(scope.row)">
              <i class="el-icon-zoom-in common-text"></i>详情
            </el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        background
        @current-change="onChangePage"
        layout="total,prev,pager,next,jumper"
        :total="total"
        :current-page="searchForm.page"
        class="cus-pagination"
      ></el-pagination>
    </div>

    <el-dialog
      title="详情"
      :visible.sync="DialogShow"
      v-dialogDrag
      width="900px"
      append-to-body
      :close-on-click-modal="false"
    >
      <vDetail :curData="curData" v-if="DialogShow"></vDetail>
      <span slot="footer" class="dialog-footer" style="margin:0 auto;">
        <el-button type="primary" @click="closeDetail">关闭</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/PersonalAffairs/shortMsg";
import Detail from "./children/detail";
export default {
  data: function() {
    return {
      searchForm: {
        at_matter: "",
        at_stime: "", // 开始时间
        at_ctime: "", //结束时间
        page: 1
      },
      total: 0,
      tableData: [],
      loading: false,
      DialogShow: false,
      curData: null,
      searchDate: "",
      at_matter: ""
    };
  },
  created() {
    this.getData();
  },
  methods: {
    getData: function() {
      this.loading = true;
      var ur_indent = JSON.parse(localStorage.getItem("data")).ur_ident;
      var params = {
        uid: ur_indent,
        at_matter: this.searchForm.at_matter,
        at_stime: this.searchForm.at_stime,
        at_ctime: this.searchForm.at_ctime,
        page: this.searchForm.page
      };
      dataService.getMsgAllList(params).then(res => {
        this.pageSize = parseInt(res.PAGE_SIZE);
        this.total = res.SIZE;
        this.tableData = res.DATA;
        this.loading = false;
      });
    },
    onChangePage: function(val) {
      this.searchForm.page = val;
      this.getData();
    },
    handleDetail(row) {
      this.DialogShow = true;
      console.log(row);
      var data = {
        aid: row.AID,
        at_uid: row.AT_UID,
        at_stime: row.AT_STIME,
        at_ctime: row.AT_CTIME,
        at_matter: row.AT_MATTER,
        at_theme: row.AT_THEME,
        at_wiid: row.AT_WIID
      };
      this.curData = data;
      var readData = [];
      readData.push(data);
      var params = { DATA: readData };

      dataService
        .getMsgUpdateList(params)
        .then(res => {
          // console.log(res)
          this.getData();
        })
        .catch(res => {
          console.log(res, "err==");
        });
    },
    handleDelete(row) {
      this.$confirm("此操作将永久删除该条数据, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.getData();
          this.$message({
            type: "success",
            message: "删除成功!"
          });
        })
        .catch(() => {});
    },
    closeDetail() {
      this.DialogShow = false;
      //  this.$message({
      //     message: '消息已阅',
      //     type: 'success'
      //   });
    },
    onSubmit: function() {
      if (this.searchDate) {
        this.searchForm.at_stime = this.searchDate[0] + " 00:00";
        this.searchForm.at_ctime = this.searchDate[1] + " 23:59";
      } else {
        this.searchForm.at_stime = "";
        this.searchForm.at_ctime = "";
      }
      this.searchForm.at_matter = this.at_matter;
      this.searchForm.page = 1;
      // console.log(this.searchForm)
      this.getData();
    },
    refresh() {
      this.at_matter = "";
      this.searchDate = "";
      this.searchForm = {
        at_matter: "",
        at_stime: "", // 开始时间
        at_ctime: "", //结束时间
        page: 1
      };
      // this.searchForm.page=1;
      this.getData();
    },
    cellStyle(data) {
      if (data.row.AT_STATE == "未读") {
        return "background:#f9e4f5";
      }
    }
  },
  components: {
    vDetail: Detail
  }
};
</script>
<style lang="scss">
.msgManage {
  height: calc(100% - 45px);
  min-width: 930px;
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
