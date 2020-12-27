
<template>
  <section>
    <search-bar @search="handleSearch" />

    <!--列表-->
    <el-table
      :data="withdrawDepositMDtos.withdrawDepositMDtos"
      highlight-current-row
      v-loading="listLoading"
      style="width: 100%"
      :header-cell-style="{
        'background-color': '#eef1f6',
        color: '#1f2d3d',
      }"
    >
      <el-table-column
        prop="id"
        label="Id"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column prop="realName" label="申请人" width="260px" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold">
            {{ scope.row.realName }}
          </p>
          <p style="color: #999999; font-weight: bold">
            {{ scope.row.memberVipTypeName }}
          </p>
          <p style="color: #999999; font-weight: bold">
            {{ scope.row.phone }}
          </p>
          <p
            style="color: #999999; font-weight: bold"
            v-if="scope.row.businessCode"
          >
            编码: {{ scope.row.businessCode }}
          </p>
          <p style="color: #999999; font-weight: bold" v-if="scope.row.address">
            {{ scope.row.address }}
          </p>
          <p style="color: #999999; font-weight: bold">
            支付宝: {{ scope.row.aliPayAccount }}
          </p>
        </template>
      </el-table-column>
      <el-table-column prop="amount" label="提现金额" width="200px" sortable>
        <template slot-scope="scope">
          <p style="color: #999999; font-weight: bold">
            <span
              style="color: #ff5000; font-size: 22px"
              :class="{ fail: menu == 'fail' }"
              >{{ scope.row.amount }}</span
            >
          </p>
          <p style="color: #999999; font-weight: bold">
            手续费: {{ scope.row.withdrawCharge }}
          </p>
          <p style="color: #999999; font-weight: bold">
            到账金额: {{ scope.row.realityAmount }}
          </p>
        </template>
      </el-table-column>
      <el-table-column
        prop="withdrawDepositStateName"
        label="状态"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column prop="remark" label="备注" sortable></el-table-column>
      <el-table-column
        prop="addTime"
        label="时间"
        width="100px"
        sortable
      ></el-table-column>

      <el-table-column label="操作" width="210px">
        <template scope="scope">
          <el-button
            size="mini"
            type="info"
            @click="handleShowDetails(scope.row)"
            >历史</el-button
          >
          <el-button
            v-if="
              (menu == 'tongjibuAudit' && user.roleName == 'Admin_Tongjibu') ||
              (menu == 'caiwuAudit' && user.roleName == 'Admin_Caiwu') ||
              (menu == 'caiwuManagerAudit' &&
                user.roleName == 'Admin_CaiwuManager')
            "
            size="mini"
            type="warning"
            @click="handleAuditFail(scope.row)"
            >拒绝</el-button
          >
          <el-button
            v-if="menu == 'tongjibuAudit' && user.roleName == 'Admin_Tongjibu'"
            size="mini"
            type="success"
            @click="handleAudit(scope.row)"
            >通过
          </el-button>
          <el-button
            v-if="menu == 'caiwuAudit' && user.roleName == 'Admin_Caiwu'"
            size="mini"
            type="success"
            @click="handleAudit(scope.row)"
            >通过
          </el-button>
          <el-button
            v-if="
              menu == 'caiwuManagerAudit' &&
              user.roleName == 'Admin_CaiwuManager'
            "
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

    <!--TODO:删减编辑界面数据-->
    <detail
      :action="showDetailsAction"
      :memberId="shwoMemberId"
      @change="showDetailsChage"
    ></detail>
  </section>
</template>

<script>
import api from "@/api/app";
import detail from "./detail";
import searchBar from "./searchBar";
import { mapGetters } from "vuex";
export default {
  name: "withdrawDeposit",
  components: {
    detail,
    searchBar,
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

      withdrawDepositMDtos: [],
      total: 0,
      listLoading: false,

      shwoMemberId: null,
      showDetailsAction: "none",
    };
  },
  mounted() {
    this.getWithdrawDepositMDtos();
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getWithdrawDepositMDtos();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getWithdrawDepositMDtos();
    },
    handleSearch(filterStr) {
      this.requestParams.page = 1;
      this.requestParams.filters = filterStr;
      this.getWithdrawDepositMDtos();
    },
    getWithdrawDepositMDtos() {
      this.listLoading = true;

      if (this.menu == "tongjibuAudit") {
        this.requestParams.filters += `WithdrawDepositState==0,`;
      } else if (this.menu == "caiwuAudit") {
        this.requestParams.filters += `WithdrawDepositState==5,`;
      } else if (this.menu == "caiwuManagerAudit") {
        this.requestParams.filters += `WithdrawDepositState==10,`;
      } else if (this.menu == "sucess") {
        this.requestParams.filters += `WithdrawDepositState==20,`;
      } else if (this.menu == "fail") {
        this.requestParams.filters += `WithdrawDepositState==30,`;
      }

      api.withdraw.get(this.requestParams).then((respone) => {
        this.listLoading = false;
        this.withdrawDepositMDtos = respone.result;
        this.total = respone.total;
      });
    },
    handleAudit: function (row) {
      let auditApi = "";
      if (this.menu == "tongjibuAudit") {
        auditApi = api.withdraw.tongjibuAudit;
      } else if (this.menu == "caiwuAudit") {
        auditApi = api.withdraw.caiwuAudit;
      } else if (this.menu == "caiwuManagerAudit") {
        auditApi = api.withdraw.pay;
      }
      const _this = this;
      _this.$confirm("操作不可逆, 确认通过吗？", "提示", {}).then(() => {
        auditApi(row.id).then(() => {
          _this.$message({
            message: "审核通过",
            type: "success",
          });
          _this.getWithdrawDepositMDtos();
        });
      });
    },

    handleAuditFail: function (row) {
      const _this = this;
      this.$confirm("操作不可逆, 确认拒绝吗？", "提示", {}).then(() => {
        api.withdraw.fail(row.id).then(() => {
          _this.$message({
            message: "已拒绝",
            type: "error",
          });
          _this.getWithdrawDepositMDtos();
        });
      });
    },
    handleShowDetails: function (row) {
      this.showDetailsAction = "show";
      this.shwoMemberId = row.memberId;
    },
    showDetailsChage(cancel) {
      this.showDetailsAction = "none";
    },
  },
};
</script>

<style scoped>
.el-tag {
  margin-left: 10px;
}
p {
  padding: 0px;
  margin: 0px;
}
.fail {
  color: #666 !important;
}
</style>