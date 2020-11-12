
<template>
  <section>
    <!--TODO:配置查询条件-->
    <el-row>
      <el-col :span="24" class="toolbar" style="padding-bottom: 0px">
        <el-form :inline="true" :model="filters">
          <el-form-item>
            <el-input
              v-model.trim="filters.keyword"
              placeholder="单号,姓名,电话"
            ></el-input>
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
            <el-button type="primary" @click="getOrderMDtos">查询</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
    <el-row class="toolbar" style="padding-top: 20px; padding-bottom: 20px">
      <el-col :span="24">
        <el-tag type="danger"
          >当查询条件下合计:{{ orderMDtos.queryTotal }}</el-tag
        >
        <el-tag>全部毛收入合计:{{ orderMDtos.allOrderTotal }}</el-tag>
        <el-tag>全部提现合计:{{ orderMDtos.withdrawDepositsTotal }}</el-tag>
        <el-tag>(全部毛收入-全部提现)合计:{{ orderMDtos.balance }}</el-tag>
      </el-col>
    </el-row>
    <!--列表-->
    <el-table
      :data="orderMDtos.orderMDtos"
      highlight-current-row
      v-loading="listLoading"
      style="width: 100%"
      :header-cell-style="{
        'background-color': '#eef1f6',
        color: '#1f2d3d',
      }"
    >
      <el-table-column prop="id" label="Id" width="100px"></el-table-column>
      <el-table-column prop="timestamp" label="单号"></el-table-column>
      <el-table-column prop="realName" label="姓名"></el-table-column>
      <el-table-column prop="memberPhone" label="电话"></el-table-column>
      <el-table-column prop="producName" label="订单名"></el-table-column>
      <el-table-column prop="amount" label="金额">
        <template slot-scope="scope">
          <span style="color: #ff5000;font-weight: bold;">{{ scope.row.amount }}</span>
        </template>
      </el-table-column>
      <el-table-column prop="addTime" label="时间"></el-table-column>
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
//TODO: 拷贝到api文件

import { api_getOrderMDtos, api_delOrderMDto } from "../../api/api";
export default {
  components: {},
  data() {
    return {
      requestParams: {
        page: 1,
        pageSize: 10,
        filters: "",
        sorts: "-id",
      },
      //TODO:删减查询条件
      filters: {
        keyword: null,
        addTimeStart: null,
        addTimeEnd: null,
      },
      orderMDtos: null,
      total: 0,
      listLoading: false,
    };
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getOrderMDtos();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getOrderMDtos();
    },
    getOrderMDtos() {
      this.listLoading = true;
      this.page = 1;
      this.requestParams.filters = "";

      //TODO:删减查询条件

      if (this.filters.keyword)
        this.requestParams.filters += `(Timestamp|RealName|MemberPhone)@=${this.filters.keyword},`;

      if (this.filters.addTimeStart)
        this.requestParams.filters += `AddTime>=${this.filters.addTimeStart},`;
      if (this.filters.addTimeEnd)
        this.requestParams.filters += `AddTime<=${this.filters.addTimeEnd},`;

      api_getOrderMDtos(this.requestParams).then((respone) => {
        this.listLoading = false;
        this.orderMDtos = respone.result;
        this.total = respone.total;
      });
    },
  },

  mounted() {
    this.getOrderMDtos();
  },
};
</script>

<style scoped>
.el-tag {
  margin-left: 10px;
}
</style>