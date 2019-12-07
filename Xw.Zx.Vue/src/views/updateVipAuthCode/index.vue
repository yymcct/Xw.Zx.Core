
<template>
  <section>
    <!--TODO:配置查询条件-->
    <el-col :span="24" class="toolbar" style="padding-bottom: 0px;">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-select v-model="filters.uPdateVipAuthCodeState" placeholder="请选择" style="width:120px">
            <el-option
              v-for="item in uPdateVipAuthCodeStateDrops"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            ></el-option>
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-input v-model.trim="filters.keyword" placeholder="姓名,电话,备注"></el-input>
        </el-form-item>

        <el-form-item>
          <el-date-picker
            v-model="filters.expiesTimeStart"
            type="date"
            placeholder="失效起始时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
          <el-date-picker
            v-model="filters.expiesTimeEnd"
            type="date"
            placeholder="失效结束时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="getUpdateVipAuthCodeMDtos">查询</el-button>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleAdd">添加</el-button>
        </el-form-item>
      </el-form>
    </el-col>

    <!--列表-->
    <el-table
      :data="updateVipAuthCodeMDtos"
      highlight-current-row
      v-loading="listLoading"
      style="width: 100%;"
      :header-cell-style="{
                          'background-color': '#eef1f6',
                          'color': '#1f2d3d',
                      }"
    >
      <el-table-column prop="id" label="Id" width="100px" sortable></el-table-column>
      <el-table-column prop="code" label="VIP码" width="100px" sortable></el-table-column>
      <el-table-column prop="expiesTime" label="失效时间" width="120px" sortable></el-table-column>
      <el-table-column prop="uPdateVipAuthCodeStateName" label="状态" width="100px" sortable></el-table-column>
      <el-table-column prop="usedMemberName" label="使用者姓名" width="150px" sortable></el-table-column>
      <el-table-column prop="usedMemberPhone" label="使用者电话" width="150px" sortable></el-table-column>
      <el-table-column prop="usedTime" label="使用时间" width="150px" sortable></el-table-column>
      <el-table-column prop="remark" label="备注"  sortable></el-table-column>
      <el-table-column prop="addTime" label="添加时间" width="100px" sortable></el-table-column>
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
    <edit :action="editAction" :PostUpdateVipAuthCodeMDto="editForm" @change="editChange"></edit>
  </section>
</template>

<script>
//TODO: 拷贝到api文件
import {
  api_getUpdateVipAuthCodeMDtos,
  api_delUpdateVipAuthCodeMDto
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
      uPdateVipAuthCodeStateDrops: [
        { value: 999, label: "全部" },
        { value: 0, label: "待使用" },
        // { value: 1, label: "已赠送" },
        { value: 2, label: "已使用" },
        // { value: 3, label: "已过期" }
      ],
      //TODO:删减查询条件
      filters: {
        keyword: null,
        expiesTimeStart: null,
        expiesTimeEnd: null,
        uPdateVipAuthCodeState: 999
      },
      updateVipAuthCodeMDtos: [],
      total: 0,
      listLoading: false,

      //TODO:删减编辑界面数据
      editForm: null,
      editAction: "none"
    };
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getUpdateVipAuthCodeMDtos();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getUpdateVipAuthCodeMDtos();
    },
    getUpdateVipAuthCodeMDtos() {
      this.listLoading = true;
      this.page = 1;
      this.requestParams.filters = "";

      //TODO:删减查询条件

      if (this.filters.expiesTimeStart)
        this.requestParams.filters += `ExpiesTime>=${this.filters.expiesTimeStart},`;
      if (this.filters.expiesTimeEnd)
        this.requestParams.filters += `ExpiesTime<=${this.filters.expiesTimeEnd},`;

      if (this.filters.uPdateVipAuthCodeState !=999)
        this.requestParams.filters += `UPdateVipAuthCodeState==${this.filters.uPdateVipAuthCodeState},`;

      if (this.filters.keyword)
        this.requestParams.filters += `(UsedMemberName|UsedMemberPhone|Remark)@=${this.filters.keyword},`;

      api_getUpdateVipAuthCodeMDtos(this.requestParams).then(respone => {
        this.listLoading = false;
        this.updateVipAuthCodeMDtos = respone.result;
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
        api_delUpdateVipAuthCodeMDto(row.id).then(res => {
          this.listLoading = false;
          //NProgress.done();
          this.$message({
            message: "删除成功",
            type: "success"
          });
          this.getUpdateVipAuthCodeMDtos();
        });
      });
    },
    editChange(cancel) {
      this.editAction = "none";
      if (cancel != "cancel") {
        this.getUpdateVipAuthCodeMDtos();
      }
    }
  },

  mounted() {
    this.getUpdateVipAuthCodeMDtos();
  }
};
</script>

<style scoped>
</style>