
<template>
  <section>
    <!--TODO:配置查询条件-->
    <el-col :span="24" class="toolbar" style="padding-bottom: 0px;">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-input
            class="keyword"
            v-model="filters.keywords"
            placeholder="客户姓名,客户手机号"
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
          <el-button type="primary" @click="getLxComputers">查询</el-button>
        </el-form-item>
      </el-form>
    </el-col>

    <!--列表-->
    <el-table
      :data="lxComputers"
      highlight-current-row
      v-loading="listLoading"
      style="width: 100%;"
      :header-cell-style="{
                          'background-color': '#eef1f6',
                          'color': '#1f2d3d',
                      }"
    >
      <el-table-column prop="id" label="Id" width="80px" sortable></el-table-column>
      <el-table-column prop="name" label="客户姓名"  sortable></el-table-column>
      <el-table-column prop="phone" label="客户手机" width="110px" sortable></el-table-column>
      <el-table-column prop="borrowCompany" label="贷款机构"  sortable></el-table-column>
      <el-table-column prop="borrowAmount" label="到账总额" width="120px" sortable></el-table-column>
      <el-table-column prop="cycle" label="期数" width="80px" sortable></el-table-column>
      <el-table-column prop="cycleAmount" label="每期金额" width="100px" sortable></el-table-column>
      <el-table-column prop="repaymentCycle" label="已还期数" width="100px" sortable></el-table-column>
      <el-table-column prop="overdueCycle" label="逾期期数" width="100px" sortable></el-table-column>
      <el-table-column prop="minReduce" label="最小减免" width="100px" sortable></el-table-column>
      <el-table-column prop="maxReduce" label="最大减免" width="100px" sortable></el-table-column>
      <el-table-column prop="addTime" label="添加时间" width="100px" sortable></el-table-column>
      <el-table-column label="操作" width="50px">
        <!-- <template scope="scope">
          <el-button
            type="danger"
            size="mini"
            @click="handleUpdateVip(scope.$index, scope.row)"
          >升级</el-button>
          <i
            class="el-icon-edit"
            style="margin: 0 5px; font-weight:bold;cursor: pointer;"
            @click="handleEdit(scope.$index, scope.row)"
          ></i>
          <i
            class="el-icon-delete"
            style="margin: 0 5px; font-weight:bold;cursor: pointer;"
            @click="handleDel(scope.$index, scope.row)"
          ></i>
        </template> -->
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


  </section>
</template>

<script>
//TODO: 拷贝到api文件

import { api_getLxComputers } from "../../api/api";
export default {
  components: {

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
        keywords: null,
        addTimeStart: null,
        addTimeEnd: null,
      },
      lxComputers: [],
      total: 0,
      listLoading: false,
    };
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getLxComputers();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getLxComputers();
    },
    getLxComputers() {
      this.listLoading = true;
      this.page = 1;
      this.requestParams.filters = "";

      if (this.filters.keywords)
        this.requestParams.filters += `(Name|Phone)@=${this.filters.keywords},`;

      if (this.filters.addTimeStart)
        this.requestParams.filters += `AddTime>=${this.filters.addTimeStart},`;
      if (this.filters.addTimeEnd)
        this.requestParams.filters += `AddTime<=${this.filters.addTimeEnd},`;

      api_getLxComputers(this.requestParams).then(respone => {
        this.listLoading = false;
        this.lxComputers = respone.result;
        this.total = respone.total;
      });
    },
  },

  mounted() {
    this.getLxComputers();
  }
};
</script>

<style scoped>
.keyword {
  width: 400px;
}
</style>