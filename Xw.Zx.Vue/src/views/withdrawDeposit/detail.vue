
<template>
  <section>
    <!--TODO:删减编辑界面数据-->
    <el-dialog title="收支提现明细" :visible.sync="editFormVisible" @close="cancelSubmit">
      <el-row>
        <!--列表-->
        <el-col :span="24" class="table">
          <el-table
            :data="auditWithdrawDepositdetails.incomeDetails"
            highlight-current-row
            v-loading="listLoading"
            style="width: 100%;"
            :header-cell-style="{
                          'background-color': '#eef1f6',
                          'color': '#1f2d3d',
                      }"
          >
            <el-table-column prop="id" label="Id" width="100px" sortable></el-table-column>
            <el-table-column prop="incomeAccountTypeName" label="状态" width="100px" sortable></el-table-column>
            <el-table-column prop="amount" label="收益金额" width="100px" sortable></el-table-column>
            <el-table-column prop="remark" label="备注" sortable></el-table-column>
            <el-table-column prop="addTime" label="时间" width="100px" sortable></el-table-column>
          </el-table>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24" class="info">收入合计: {{auditWithdrawDepositdetails.incomeTotal}}</el-col>
      </el-row>

      <el-row>
        <el-col :span="24" class="table">
          <el-table
            :data="auditWithdrawDepositdetails.withdrawDepositDetails"
            highlight-current-row
            v-loading="listLoading"
            style="width: 100%;"
            :header-cell-style="{
                          'background-color': '#eef1f6',
                          'color': '#1f2d3d',
                      }"
          >
            <el-table-column prop="id" label="Id" width="100px" sortable></el-table-column>
            <el-table-column prop="amount" label="提现金额" width="100px" sortable></el-table-column>
            <el-table-column prop="withdrawDepositStateName" label="状态" width="100px" sortable></el-table-column>
            <el-table-column prop="remark" label="备注" sortable></el-table-column>
            <el-table-column prop="addTime" label="时间" width="100px" sortable></el-table-column>
          </el-table>
        </el-col>
      </el-row>
      <el-row>
        <el-col :span="24" class="info">已提现合计: {{auditWithdrawDepositdetails.withdrawDeposit}}</el-col>
      </el-row>
      <el-row>
        <el-col :span="24" class="info total">可提现合计: {{auditWithdrawDepositdetails.balance}}</el-col>
      </el-row>
    </el-dialog>
  </section>
</template>

<script>
import { api_GetAuditWithdrawDepositdetails } from "../../api/api";
import { type } from "os";

export default {
  name: "withdrawDepositDetail",
  components: {},
  props: {
    action: String, //'none' 'show'
    memberId: Number
  },
  watch: {
    action: {
      handler(val) {
        if (val == "none") {
          this.editFormVisible = false;
        } else {
          this.getauditWithdrawDepositdetails(this.memberId);
          this.editFormVisible = true;
        }
      }
    }
  },
  data() {
    return {
      auditWithdrawDepositdetails: null,
      editFormVisible: "none"
    };
  },
  methods: {
    getauditWithdrawDepositdetails(memberid) {
      this.listLoading = true;

      api_GetAuditWithdrawDepositdetails({
        memberId: memberid
      }).then(respone => {
        this.listLoading = false;
        this.auditWithdrawDepositdetails = respone.result;
      });
    },
    cancelSubmit: function() {
      this.editFormVisible = false;
      this.$emit("change", "cancel");
    }
  },
  mounted() {
    //this.getauditWithdrawDepositdetails(6);
  }
};
</script>

<style scoped>
.info {
  font-weight: bolder;
  color: darkorange;
  text-align: end;
  margin-top: 10px;
  margin-bottom: 10px;
  padding-right: 18px;
}
.total {
  font-size: 18px;
}
</style>