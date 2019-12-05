
<template>
  <section>
    <!--TODO:配置查询条件-->
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
          <el-input v-model.trim="filters.keyword" placeholder="金额,备注,姓名,电话,支付宝"></el-input>
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

    <!--列表-->
    <el-table
      :data="withdrawDepositMDtos"
      highlight-current-row
      v-loading="listLoading"
      style="width: 100%;"
      :header-cell-style="{
                          'background-color': '#eef1f6',
                          'color': '#1f2d3d',
                      }"
    >
      <el-table-column prop="id" label="Id" width="100px" sortable></el-table-column>
      <el-table-column prop="realName" label="姓名" width="100px" sortable></el-table-column>
      <el-table-column prop="phone" label="电话" width="100px" sortable></el-table-column>
      <el-table-column prop="aliPayAccount" label="支付宝" width="100px" sortable></el-table-column>
      <el-table-column prop="amount" label="金额" width="100px" sortable></el-table-column>
      <el-table-column prop="remark" label="备注" width="100px" sortable></el-table-column>
      <el-table-column prop="withdrawDepositStateName" label="状态" width="100px" sortable></el-table-column>
      <el-table-column prop="addTime" label="AddTime" width="100px" sortable></el-table-column>

      <el-table-column label="操作" width="100px">
        <template scope="scope">
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
    <edit :action="editAction" :PostWithdrawDepositMDto="editForm" @change="editChange"></edit>
  </section>
</template>

<script>
//TODO: 拷贝到api文件

import {
  api_getWithdrawDepositMDtos,
  api_delWithdrawDepositMDto
} from "../../api/api";
import { type } from "os";
import edit from "./edit";
export default {
  components: {
    edit
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
        withdrawDepositState: null,
        keyword: null,
        addTimeStart: null,
        addTimeEnd: null
      },
      withdrawDepositMDtos: [],
      total: 0,
      listLoading: false,

      //TODO:删减编辑界面数据
      editForm: null,
      editAction: "none",
      withdrawDepositStateDrops: [
        { value: "", label: "全部" },
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
      if (this.filters.withdrawDepositState)
        this.requestParams.filters += `WithdrawDepositState==${this.filters.withdrawDepositState},`;

      if (this.filters.id)
        this.requestParams.filters += `(Amount|Remark|RealName|Phone|AliPayAccount)@=${this.filters.id},`;

      api_getWithdrawDepositMDtos(this.requestParams).then(respone => {
        this.listLoading = false;
        this.withdrawDepositMDtos = respone.result;
        this.total = respone.total;
      });
    },
    //显示编辑界面
    handleEdit: function(index, row) {
      this.editForm = Object.assign({}, row);
      this.editAction = "edit";
    },
    //显示新增界面
    handleAdd: function() {
      this.editAction = "add";
    },
    //删除
    handleDel: function(index, row) {
      this.$confirm("确认删除?", "提示", { type: "warning" }).then(() => {
        this.listLoading = true;
        //NProgress.start();
        api_delWithdrawDepositMDto(row.id).then(res => {
          this.listLoading = false;
          //NProgress.done();
          this.$message({
            message: "删除成功",
            type: "success"
          });
          this.getWithdrawDepositMDtos();
        });
      });
    },
    editChange(cancel) {
      this.editAction = "none";
      if (cancel != "cancel") {
        this.getWithdrawDepositMDtos();
      }
    }
  },

  mounted() {
    this.getWithdrawDepositMDtos();
  }
};
</script>

<style scoped>
</style>