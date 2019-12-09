
<template>
  <section>
    <!--TODO:配置查询条件-->
    <el-col :span="24" class="toolbar" style="padding-bottom: 0px;">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-select v-model="filters.memberVipType" placeholder="请选择" style="width:120px">
            <el-option label="全部类型" value="999"></el-option>
            <el-option label="普通" value="0"></el-option>
            <el-option label="VIP会员" value="1"></el-option>
            <el-option label="合伙人" value="2"></el-option>
            <el-option label="服务站" value="3"></el-option>
            <el-option label="运营商" value="4"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-input v-model.trim="filters.keyword" placeholder="姓名,电话"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="getIncomeAccountMDtos">查询</el-button>
        </el-form-item>
      </el-form>
    </el-col>

    <!--列表-->
    <el-table
      :data="incomeAccountMDtos"
      highlight-current-row
      v-loading="listLoading"
      style="width: 100%;"
      :header-cell-style="{
                          'background-color': '#eef1f6',
                          'color': '#1f2d3d',
                      }"
    >
      <el-table-column prop="memberId" label="会员Id" width="100px" sortable></el-table-column>
      <el-table-column prop="memberName" label="姓名" sortable></el-table-column>
      <el-table-column prop="memberPhone" label="电话" sortable></el-table-column>
      <el-table-column prop="memberVipTypeName" label="类型" sortable></el-table-column>
      <el-table-column prop="zhijieTotla" label="直接收益" sortable></el-table-column>
      <el-table-column prop="jianjieTotla" label="间接收益" sortable></el-table-column>
      <el-table-column prop="chajiTotla" label="差级收益" sortable></el-table-column>
      <el-table-column prop="incomeTotal" label="合计收益" sortable>
        <template slot-scope="scope">
          <span style="color:salmon; font-weight:bolder;">{{ scope.row.incomeTotal }}</span>
        </template>
      </el-table-column>
      <el-table-column prop="withdrawDepositTotal" label="提现合计" sortable></el-table-column>
      <el-table-column prop="balance" label="待提现" sortable>
        <template slot-scope="scope">
          <span style="color:salmon; font-weight:bolder;">{{ scope.row.balance }}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="100px">
        <template scope="scope">
          <el-button size="mini" type="info" @click="handleShowDetails(scope.$index, scope.row)">明细</el-button>
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
    <detail :action="showDetailsAction" :memberId="shwoMemberId" @change="showDetailsChage"></detail>
  </section>
</template>

<script>
//TODO: 拷贝到api文件
import {
  api_getIncomeAccountMDtos,
  api_delIncomeAccountMDto
} from "../../api/api";
import { type } from "os";
import detail from "../withdrawDeposit/detail";
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
        sorts: "-incomeTotal"
      },
      //TODO:删减查询条件
      filters: {
        keyword: null,
        memberVipType: "999"
      },
      incomeAccountMDtos: [],
      total: 0,
      listLoading: false,

      //TODO:删减编辑界面数据
      shwoMemberId: null,
      showDetailsAction: "none"
    };
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getIncomeAccountMDtos();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getIncomeAccountMDtos();
    },
    getIncomeAccountMDtos() {
      this.listLoading = true;
      this.page = 1;
      this.requestParams.filters = "";

      //TODO:删减查询条件
      if (this.filters.keyword)
        this.requestParams.filters += `(MemberName|MemberPhone)@=${this.filters.keyword},`;

      if (this.filters.memberVipType != "999")
        this.requestParams.filters += `MemberVipType==${this.filters.memberVipType},`;

      api_getIncomeAccountMDtos(this.requestParams).then(respone => {
        this.listLoading = false;
        this.incomeAccountMDtos = respone.result;
        this.total = respone.total;
      });
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
    this.getIncomeAccountMDtos();
  }
};
</script>

<style scoped>
</style>