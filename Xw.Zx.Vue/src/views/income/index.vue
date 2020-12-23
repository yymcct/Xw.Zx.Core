



<template>
  <section>
    <search-bar @search="handleSearch" @add="handleAdd" />
    <!--列表-->
    <el-table
      :data="incomes"
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
        prop="sourceOrderId"
        label="分润订单"
        width="100px"
        sortable
      >
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.sourceOrderProducName }}</p>
          <p style="font-weight: bold">
            支付金额: {{ scope.row.sourceOrderProductAmount }}
          </p>
          <p style="color: #999999">
            单号: {{ scope.row.sourceOrderTimestamp }}
          </p>
          <p style="color: #999999">
            下单人电话: {{ scope.row.sourceOrderMemberPhone }}
          </p>
          <p style="color: #999999">
            下单时间: {{ scope.row.sourceOrderAddTime }}
          </p>
          <p style="color: #999999">
            支付通道: {{ scope.row.sourceOrderOrderPaymentTypeName }}
          </p>
        </template>
      </el-table-column>

      <el-table-column
        prop="amount"
        label="收益金额"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="memberName"
        label="收益类型"
        width="100px"
        sortable
      >
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.IncomeAccountTypeName }}</p>
          <p style="color: #999999">备注: {{ scope.row.remark }}</p>
          <p style="color: #999999">收益时间: {{ scope.row.addTime }}</p>
        </template>
      </el-table-column>

      <el-table-column prop="memberName" label="收益人" width="100px" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.memberName }}</p>
          <p style="color: #999999">{{ scope.row.memberPhone }}</p>
        </template>
      </el-table-column>

      <!-- <el-table-column label="操作" width="100px">
        <template scope="scope">
          <i
            class="el-icon-edit"
            style="margin: 0 5px; font-weight: bold; cursor: pointer"
            @click="handleEdit(scope.$index, scope.row)"
          ></i>
          <i
            class="el-icon-delete"
            style="margin: 0 5px; font-weight: bold; cursor: pointer"
            @click="handleDel(scope.$index, scope.row)"
          ></i>
        </template>
      </el-table-column> -->
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


  </section>
</template>

<script>
import api from "@/api/app";
import searchBar from "./searchBar";
export default {
  components: {
    searchBar,
  },
  data() {
    return {
      requestParams: {
        page: 1,
        pageSize: 10,
        filters: "",
        sorts: "-id",
      },
      incomes: [],
      total: 0,
      loading: false,
    };
  },
  mounted() {
    this.getIncomes();
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getIncomes();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getIncomes();
    },
    handleSearch(filterStr) {
      this.requestParams.page = 1;
      this.requestParams.filters = filterStr;
      this.getIncomes();
    },
    getIncomes() {
      this.loading = true;
      api.income.getCoupon(this.requestParams).then((respone) => {
        this.loading = false;
        this.incomes = respone.result;
        this.total = respone.total;
      });
    },
    //显示编辑界面
    handleEdit(index, row) {
      this.edit.id = row.id;
      this.edit.showEdit = true;
    },
    //显示新增界面
    handleAdd() {
      this.edit.id = 0;
      this.edit.showEdit = true;
    },

  },
};
</script>

<style scoped>
</style>