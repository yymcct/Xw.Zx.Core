



<template>
  <section>
    <search-bar
      @search="handleSearch"
      @searchSingleOrder="handleSearchSingleOrder"
    />
    <!--列表-->
    <el-table
      :data="wechatOrderss"
      highlight-current-row
      v-loading="loading"
      style="width: 100%"
    >
      <el-table-column
        prop="id"
        label="Id"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="transactionID"
        label="微信交易号"
        width="120px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="out_Order_No"
        label="商户交易号"
        width="120px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="amount"
        label="交易额"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="subCharge"
        label="分账额"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="tranTime"
        label="交易时间"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="payState"
        label="支付状态"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="payDescription"
        label="分账描述"
        sortable
      ></el-table-column>
      <el-table-column prop="subState" label="分账状态" width="100px" sortable>
        <template scope="scope">
          <p v-if="scope.row.subState == 0">待审请</p>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="100px">
        <template scope="scope">
          <el-button      
            type="text"
            @click="handleSub(scope.row)"
            >申请分账
          </el-button>
          <!-- <i
            class="el-icon-edit"
            style="margin: 0 5px; font-weight: bold; cursor: pointer"
            @click="handleEdit(scope.$index, scope.row)"
          ></i>
          <i
            class="el-icon-delete"
            style="margin: 0 5px; font-weight: bold; cursor: pointer"
            @click="handleDel(scope.$index, scope.row)"
          ></i> -->
        </template>
      </el-table-column>
    </el-table>

    <!--工具条align='center'-->
    <el-col :span="24" class="toolbar" align="right">
      <el-pagination
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="requestParams.page"
        :page-sizes="[10, 50, 100, 500]"
        :page-size="requestParams.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="total"
        background
      ></el-pagination>
    </el-col>

    <!--TODO:删减编辑界面数据-->
    <edit-dialog
      v-model="edit.showEdit"
      :id="edit.id"
      @change="editChange"
    ></edit-dialog>
  </section>
</template>

<script>
import api from "@/api/app";
import searchBar from "./searchBar";
import editDialog from "./edit";
export default {
  components: {
    searchBar,
    editDialog,
  },
  data() {
    return {
      requestParams: {
        page: 1,
        pageSize: 10,
        filters: "",
        sorts: "-id",
      },
      wechatOrderss: [],
      total: 0,
      loading: false,
      edit: {
        id: 0,
        showEdit: false,
      },
    };
  },
  mounted() {
    this.getWechatOrderss();
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getWechatOrderss();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getWechatOrderss();
    },
    handleSearch(filterStr) {
      this.requestParams.page = 1;
      this.requestParams.filters = filterStr;
      this.getWechatOrderss();
    },
    getWechatOrderss() {
      this.loading = true;
      api.weixinSubLedger
        .getWechatOrderList(this.requestParams)
        .then((respone) => {
          this.loading = false;
          this.wechatOrderss = respone.result;
          this.total = respone.total;
        });
    },
    //显示编辑界面
    handleEdit(index, row) {
      this.edit.id = row.id;
      this.edit.showEdit = true;
    },
    //显示新增界面
    handleSearchSingleOrder(out_Order_No) {
      this.loading = true;
      api.weixinSubLedger
        .getWechatPayOrder({
          out_Order_No,
        })
        .then((respone) => {
          this.loading = false;
          this.wechatOrderss = respone.result;
          this.total = respone.total;
        })
        .catch(() => {
          this.loading = false;
        });
    },
    handleSub(row){

    },

    editChange(cancel) {
      if (cancel != "cancel") {
        this.getWechatOrderss();
      }
    },
  },
};
</script>

<style scoped>
</style>