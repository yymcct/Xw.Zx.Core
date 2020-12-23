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
      <el-table-column prop="memberName" label="收益人" width="200px" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.memberName }}</p>
          <p style="color: #999999">{{ scope.row.memberPhone }}</p>
          <p>
            <el-link type="primary" @click="showMemberParentTree(scope.row)"
              >查看收益人团队树</el-link
            >
          </p>
        </template>
      </el-table-column>

      <el-table-column prop="amount" label="收益金额" width="130px" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold; color: #ff5000; font-size: 18px">
            {{ scope.row.amount }}
          </p>
        </template>
      </el-table-column>
      <el-table-column prop="memberName" label="收益类型" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.incomeAccountTypeName }}</p>
          <p style="color: #999999">备注: {{ scope.row.remark }}</p>
          <p style="color: #999999">收益时间: {{ scope.row.addTime }}</p>
        </template>
      </el-table-column>
      <el-table-column
        prop="sourceOrderId"
        label="分润订单"
        width="300px"
        sortable
      >
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.sourceOrderProducName }}</p>
          <p style="color: #999999">
            订单金额: {{ scope.row.sourceOrderProductAmount }}
          </p>
          <p style="color: #999999">
            支付通道: {{ scope.row.sourceOrderOrderPaymentTypeName }}
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
          <p>
            <el-link
              type="primary"
              @click="showOrderMemberParentTree(scope.row)"
              >查看下单人团队树</el-link
            >
          </p>
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
    <parent-tree
      v-model="memberParentTree.show"
      :memberId="memberParentTree.memberId"
    />
  </section>
</template>

<script>
import api from "@/api/app";
import searchBar from "./searchBar";
import parentTree from "@/components/parentTree";
export default {
  components: {
    searchBar,
    parentTree,
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
      memberParentTree: {
        show: false,
        memberId: 0,
      },
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
    showMemberParentTree(row) {
      this.memberParentTree.memberId = row.memberId;
      this.memberParentTree.show = true;
    },

    showOrderMemberParentTree(row) {
      this.memberParentTree.memberId = row.sourceOrderMemberId;
      this.memberParentTree.show = true;
    },
  },
};
</script>

<style scoped>
p {
  padding: 0px;
  margin: 0px;
}
</style>