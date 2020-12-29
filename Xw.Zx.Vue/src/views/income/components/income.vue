<template>
  <section>
    <search-bar @search="handleSearch" />
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
      <el-table-column prop="memberName" label="收益人" width="180px" sortable>
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

      <el-table-column prop="amount" label="收益金额" width="100px" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold; color: #ff5000; font-size: 18px">
            {{ scope.row.amount }}
          </p>
        </template>
      </el-table-column>
      <el-table-column prop="memberName" label="分润状态" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold">
            {{ scope.row.incomeAccountStateName }}
          </p>
          <p style="color: #999999" v-if="scope.row.auditMemberName">
            审核人: {{ scope.row.auditMemberName }}
          </p>
          <p style="color: #999999" v-if="scope.row.auditMemberName">
            审核时间: {{ scope.row.auditime }}
          </p>
          <p style="color: #999999">分润时间: {{ scope.row.addTime }}</p>
          <p style="color: #999999">备注: {{ scope.row.remark }}</p>
        </template>
      </el-table-column>
      <el-table-column
        prop="sourceOrderId"
        label="分润订单"
        width="260px"
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
      <el-table-column label="操作" width="210px">
        <template scope="scope">
          <el-button
            size="mini"
            type="info"
            @click="handleShowDetails(scope.row)"
            >历史</el-button
          >
          <el-button
            v-if="menu == 'waitAudit' && user.roleName == 'Admin_Tongjibu'"
            size="mini"
            type="warning"
            @click="handleAuditFail(scope.row)"
            >拒绝</el-button
          >
          <el-button
            v-if="menu == 'waitAudit' && user.roleName == 'Admin_Tongjibu'"
            size="mini"
            type="success"
            @click="handleAudit(scope.row)"
            >通过
          </el-button>
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
    <parent-tree
      v-model="memberParentTree.show"
      :memberId="memberParentTree.memberId"
    />
    <detail
      :action="showDetailsAction"
      :memberId="shwoMemberId"
      @change="showDetailsChage"
    ></detail>
  </section>
</template>

<script>
import api from "@/api/app";
import searchBar from "./searchBar";
import detail from "@/views/withdrawDeposit/components/detail";
import { mapGetters } from "vuex";
import parentTree from "@/components/parentTree";
export default {
  name: "income",
  components: {
    detail,
    searchBar,
    parentTree,
  },
  props: {
    menu: String,
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
      incomes: [],
      total: 0,
      loading: false,
      memberParentTree: {
        show: false,
        memberId: 0,
      },
      shwoMemberId: null,
      showDetailsAction: "none",
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
      if (this.menu == "waitAudit") {
        this.requestParams.filters += `IncomeAccountState==0,`;
      } else if (this.menu == "sucess") {
        this.requestParams.filters += `IncomeAccountState==10,`;
      } else if (this.menu == "fail") {
        this.requestParams.filters += `IncomeAccountState==20,`;
      }

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

    handleAudit: function (row) {
      const _this = this;
      _this.$confirm("操作不可逆, 确认通过吗？", "提示", {}).then(() => {
        api.income.auditSucess(row.id).then(() => {
          _this.$message({
            message: "审核通过",
            type: "success",
          });
          _this.getIncomes();
        });
      });
    },

    handleAuditFail: function (row) {
      const _this = this;
      this.$prompt("请输入拒绝的原因", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
      })
        .then(({ value }) => {
          api.income
            .auditFail(row.id, {
              Remark: value,
            })
            .then(() => {
              _this.$message({
                message: "已拒绝",
                type: "error",
              });
              _this.getIncomes();
            });
        })
        .catch(() => {});
    },
    handleShowDetails: function (row) {
      this.showDetailsAction = "show";
      this.shwoMemberId = row.memberId;
    },
    showDetailsChage() {
      this.showDetailsAction = "none";
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