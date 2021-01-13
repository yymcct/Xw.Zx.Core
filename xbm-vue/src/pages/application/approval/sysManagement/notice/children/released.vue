<template>
  <div class="released">
    <div class="handle-btn">
      <el-form :inline="true" class="search-form">
        <el-form-item label="公告名称">
          <el-input v-model="keyword" placeholder="请输入关键字" clearable></el-input>
        </el-form-item>
        <el-form-item label="发布人">
          <el-input v-model="nt_sender" placeholder="请输入关键字" clearable></el-input>
        </el-form-item>
        <el-button type="primary" size="medium" @click="onSubmit">查询</el-button>
        <el-button type="primary" size="medium" @click="refresh">刷新</el-button>
      </el-form>
    </div>
    <div class="cus-common-table" v-loading="loading" element-loading-text="拼命加载中">
      <el-table
        :data="tableData"
        border
        stripe
        height="100%"
        @filter-change="handleFilterChange"
        :cell-style="cellStyle"
      >
        <el-table-column type="index" width="70" label="序号" align="center"></el-table-column>
        <el-table-column
          prop="ZT"
          label="状态"
          width="70"
          column-key="ZT"
          align="center"
          :filters="[{ text: '已读', value: '已读' }, { text: '未读', value: '未读' }]"
          filter-placement="bottom-end"
          show-overflow-tooltip
        ></el-table-column>
        <el-table-column
          prop="WIID"
          label="公告编号"
          min-width="150"
          show-overflow-tooltip
          align="center"
        ></el-table-column>
        <el-table-column
          prop="NT_NAME"
          label="公告名称"
          min-width="180"
          show-overflow-tooltip
          align="center"
        ></el-table-column>
        <el-table-column prop="NT_DEPT" label="发布部门" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column prop="NT_SENDER" label="发布人" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column prop="NT_TIME" label="发布时间" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column prop="NT_MOVE" label="有效期限" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column prop="NT_URGENT" label="紧急程度" show-overflow-tooltip align="center"></el-table-column>
        <el-table-column label="操作" fixed="right" width="100" align="center">
          <template slot-scope="scope">
            <el-button type="text" @click="handleDetail(scope.row)" title="详情">
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
        class="cus-pagination"
        :page-size="pageSize"
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
      <vForm :curData="curData" :type="type" ref="NoticeForm" v-if="DialogShow"></vForm>
      <span slot="footer" class="dialog-footer" style="margin:0 auto;">
        <el-button type="primary" @click="DialogShow = false">关闭</el-button>
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
      nt_sender: "",
      page: 1,
      pageSize: 10,
      total: 0,
      tableData: [],
      DialogShow: false,
      curData: null,
      type: "add",
      loading: false,
      state: "",
      filter: []
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
    getData: function() {
      this.loading = true;
      var data = {
        nt_name: this.keyword,
        nt_sender: this.nt_sender,
        page: this.page,
        zt: this.state
      };
      dataService.getNoticeSearchList(data).then(res => {
        // console.log(data)
        this.total = res.SIZE;
        this.tableData = res.DATA;
        this.loading = false;
      });
    },
    onChangePage: function(val) {
      this.page = val;
      this.getData();
    },
    handleDetail(row) {
      dataService.checkNotice(row.WIID).then(res => {
        this.curData = res[0];
        this.type = "detail";
        this.DialogShow = true;
        this.getData();
      });
    },
    handleFilterChange(data) {
      if (data.ZT.length == 1) {
        this.state = data.ZT[0];
      } else {
        this.state = "";
      }
      this.page = 1;
      this.getData();
    },
    onSubmit: function() {
      this.page = 1;
      this.getData(this.keyword, this.page);
    },
    refresh() {
      this.state = "";
      this.keyword = "";
      this.page = 1;
      this.nt_sender = "";
      this.getData();
    },
    cellStyle(data) {
      console.log(data.row.ZT);
      if (data.row.ZT == "未读") {
        return "background:#f9e4f5";
      }
    }
  },

  components: {
    vForm: Form
  }
};
</script>
<style lang="scss" >
.released {
  height: 100%;
}
</style>
