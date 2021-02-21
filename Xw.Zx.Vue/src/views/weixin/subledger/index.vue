



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
        sortable
      ></el-table-column>
      <el-table-column
        prop="out_Order_No"
        label="商户交易号"
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
        label="可分账额"
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
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column prop="subState" label="分账状态" width="100px" sortable>
        <template scope="scope">
          <p v-if="scope.row.subState == 0 && scope.row.transactionID != ''">
            待审请
          </p>
          <p v-if="scope.row.subState == 0 && scope.row.transactionID == ''">
            不能分账
          </p>
          <p v-if="scope.row.subState == 10">申请中</p>
          <p v-if="scope.row.subState == 20">分账完成</p>
          <p v-if="scope.row.subState == 30">分账失败</p>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="100px">
        <template scope="scope">
          <el-button
            type="text"
            @click="handleSub(scope.row)"
            v-if="
              scope.row.subState == 0 && user.roleName == 'Admin_CaiwuPayChange'
            "
            >申请分账
          </el-button>
          <el-button
            type="text"
            @click="showSub(scope.row)"
            v-if="scope.row.subState == 10 || scope.row.subState == 20"
            >查看分账结果
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

    <apply-dialog
      v-model="apply.show"
      :out_order_no="apply.id"
      :amount="apply.amount"
      :subCharge="apply.subCharge"
      @change="editChange"
    />

    <query-apply-dialog
      v-model="queryApply.show"
      :out_order_no="queryApply.id"
      @change="editChange"
    />
  </section>
</template>

<script>
import api from "@/api/app";
import searchBar from "./searchBar";
import applyDialog from "./apply";
import queryApplyDialog from "./queryApply";
import { mapGetters } from "vuex";
export default {
  name: "WechatOrders",
  components: {
    searchBar,
    applyDialog,
    queryApplyDialog,
  },
  computed: {
    ...mapGetters({
      user: "user/user",
    }),
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
      apply: {
        id: "0",
        show: false,
        amount: 0,
      },
      queryApply: {
        id: "0",
        show: false,
        amount: 0,
        subCharge: 0,
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
    handleSub(row) {
      this.apply.show = true;
      this.apply.id = row.out_Order_No;
      this.apply.amount = Number(row.amount);
      this.apply.subCharge = Number(row.subCharge);
    },
    showSub(row) {
      this.queryApply.show = true;
      this.queryApply.id = row.out_Order_No;
    },
    editChange(){

    }
  },
};
</script>

<style scoped>
</style>