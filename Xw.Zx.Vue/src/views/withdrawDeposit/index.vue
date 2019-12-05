
<template>
  <section>
    <!--TODO:配置查询条件-->
    <el-row>
      <el-col :span="24" class="toolbar" style="padding-bottom: 0px;">
        <el-form :inline="true" :model="filters">
          <el-form-item>
            <el-select v-model="filters.withdrawDepositState" placeholder="请选择" style="width:120px">
              <el-option
                v-for="item in withdrawDepositStateDrops"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item>
            <el-input v-model.trim="filters.keyword" placeholder="姓名,电话,支付宝,备注"></el-input>
          </el-form-item>
          <el-form-item>
            <el-date-picker
              v-model="filters.addTimeStart"
              type="date"
              placeholder="开始时间"
              align="right"
              :picker-options="glpickerOptions"
              value-format="yyyy-MM-dd"
            ></el-date-picker>
            <el-date-picker
              v-model="filters.addTimeEnd"
              type="date"
              placeholder="结束时间"
              align="right"
              :picker-options="glpickerOptions"
              value-format="yyyy-MM-dd"
            ></el-date-picker>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="getWithdrawDepositMDtos">查询</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>

    <el-row class="toolbar" style="padding-top: 20px;padding-bottom: 20px;">
      <el-col :span="24">
        <el-tag type="danger">当查询条件下合计:{{withdrawDepositMDtos.queryTotal}}</el-tag>
        <el-tag>全部提现合计:{{withdrawDepositMDtos.allTotal}}</el-tag>
        <el-tag>全部毛收入合计:{{withdrawDepositMDtos.orderTotal}}</el-tag>
        <el-tag>(全部毛收入-全部提现)合计:{{withdrawDepositMDtos.balance}}</el-tag>
      </el-col>
    </el-row>

    <!--列表-->
    <el-table
      :data="withdrawDepositMDtos.withdrawDepositMDtos"
      highlight-current-row
      v-loading="listLoading"
      style="width: 100%;"
      :header-cell-style="{
                          'background-color': '#eef1f6',
                          'color': '#1f2d3d',
                      }"
    >
      <el-table-column prop="id" label="Id" width="100px" sortable></el-table-column>
      <el-table-column prop="realName" label="姓名" width="150px" sortable></el-table-column>
      <el-table-column prop="phone" label="电话" width="150px" sortable></el-table-column>
      <el-table-column prop="aliPayAccount" label="支付宝" width="200px" sortable></el-table-column>
      <el-table-column prop="amount" label="金额" width="100px" sortable></el-table-column>
      <el-table-column prop="withdrawDepositStateName" label="状态" width="100px" sortable></el-table-column>
      <el-table-column prop="remark" label="备注" sortable></el-table-column>
      <el-table-column prop="addTime" label="时间" width="100px" sortable></el-table-column>

      <el-table-column label="操作" width="210px">
        <template scope="scope">
          <el-button
            size="mini"
            type="info"
            @click="handleShowDetails(scope.$index, scope.row)"
          >历史</el-button>
          <el-button
            v-if="scope.row.withdrawDepositState == 0"
            size="mini"
            type="warning"
            @click="handleAuditPass(scope.$index, scope.row, false)"
          >拒绝</el-button>
          <el-button
            v-if="scope.row.withdrawDepositState == 0"
            size="mini"
            type="success"
            @click="handleAuditPass(scope.$index, scope.row, true)"
          >通过</el-button>
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
//TODO: 拷贝到api文件

import {
  api_getWithdrawDepositMDtos,
  api_delWithdrawDepositMDto
} from "../../api/api";
import { type } from "os";
import { MessageBox, Message } from "element-ui";
import detail from "./detail";
export default {
  components: {
    detail
  },
  data() {
    return {
      requestParams: {
        page: 1,
        pageSize: 10,
        filters: "",
        sorts: "-id"
      },
      //TODO:删减查询条件
      filters: {
        withdrawDepositState: 999,
        keyword: null,
        addTimeStart: null,
        addTimeEnd: null
      },
      withdrawDepositMDtos: [],
      total: 0,
      listLoading: false,

      //TODO:删减编辑界面数据
      shwoMemberId: null,
      showDetailsAction: "none",
      withdrawDepositStateDrops: [
        { value: 999, label: "全部" },
        { value: 0, label: "申请中" },
        { value: 1, label: "通过" },
        { value: 2, label: "拒绝" },
        { value: 3, label: "失败" }
      ]
    };
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
    getWithdrawDepositMDtos() {
      this.listLoading = true;
      this.page = 1;
      this.requestParams.filters = "";

      //TODO:删减查询条件
      if (this.filters.withdrawDepositState != 999)
        this.requestParams.filters += `WithdrawDepositState==${this.filters.withdrawDepositState},`;

      if (this.filters.keyword)
        this.requestParams.filters += `(Remark|RealName|Phone|AliPayAccount)@=${this.filters.keyword},`;

      if (this.filters.addTimeStart)
        this.requestParams.filters += `AddTime>=${this.filters.addTimeStart},`;
      if (this.filters.addTimeEnd)
        this.requestParams.filters += `AddTime<=${this.filters.addTimeEnd},`;

      api_getWithdrawDepositMDtos(this.requestParams).then(respone => {
        this.listLoading = false;
        this.withdrawDepositMDtos = respone.result;
        this.total = respone.total;
      });
    },
    //显示编辑界面
    handleAuditPass: function(index, row, ispass) {
      this.shwoMemberId = Object.assign({}, row);
      this.showDetailsAction = "edit";
    },
    handleShowDetails: function(index, row) {
      this.showDetailsAction = "show";
      this.shwoMemberId = row.memberId;
    },
    showDetailsChage(cancel) {
      this.showDetailsAction = "none";
    }
  },

  mounted() {
    this.getWithdrawDepositMDtos();
  }
};
</script>

<style scoped>
.el-tag {
  margin-left: 10px;
}
</style>