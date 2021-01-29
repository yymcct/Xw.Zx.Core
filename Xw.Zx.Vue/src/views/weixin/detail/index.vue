



<template>
  <section>
    <search-bar @search="handleSearch" @add="handleAdd" />
    <p class="info">
      当前条件下合计: <span> {{ wechatSubDetail.totalAmount }}</span>
    </p>
    <!--列表-->
    <el-table
      :data="wechatSubDetail.details"
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
      <!-- <el-table-column prop="transactionID" label="TransactionID" width="100px" sortable></el-table-column> -->
      <el-table-column
        prop="last_Out_Order_No"
        label="商家交易号"
        width="200px"
        sortable
      ></el-table-column>
      <!-- <el-table-column prop="return_OrderID" label="Return_OrderID" width="100px" sortable></el-table-column> -->
      <!-- <el-table-column prop="subType" label="SubType" width="100px" sortable></el-table-column> -->
      <el-table-column
        prop="subAccount"
        label="OpenId"
        width="200px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="subName"
        label="收益人"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="subAmount"
        label="收益金额"
        sortable
      ></el-table-column>
      <el-table-column
        prop="subTime"
        label="分账时间"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="subState"
        label="分账状态"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column
        prop="payDescription"
        label="描述"
        width="100px"
        sortable
      ></el-table-column>
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
  </section>
</template>

<script>
//TODO: 拷贝到api文件
/*
wechatSubDetail:{
    get: (id) => request.get(`/manager/1000/WechatSubDetail/${id}`),
    gets: (params) => request.get('/manager/1000/WechatSubDetail', { params: params }),
    post: (params) => request.post('/manager/1000/WechatSubDetail', params),
    put: (id,params) => request.put(`/manager/1000/WechatSubDetail?id=${id}`, params),
    del: (id) => request.post(`/manager/1000/WechatSubDetail/Delete?id=${id}`),
}
*/
import api from "@/api/app";
import searchBar from "./searchBar";

export default {
  components: {
    searchBar,
  },
  data() {
    return {
      requestParams: {
        page: 1,
        pageSize: 10,
        filters: "",
        sorts: "-id",
      },

      wechatSubDetail: null,
      total: 0,
      loading: false,
      edit: {
        id: 0,
        showEdit: false,
      },
    };
  },
  mounted() {
    this.getWechatSubDetails();
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getWechatSubDetails();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getWechatSubDetails();
    },
    handleSearch(filterStr) {
      this.requestParams.page = 1;
      this.requestParams.filters = filterStr;
      this.getWechatSubDetails();
    },
    getWechatSubDetails() {
      this.loading = true;
      api.weixinSubLedger
        .getWechatOrderDetails(this.requestParams)
        .then((respone) => {
          this.loading = false;
          this.wechatSubDetail = respone.result;
          this.total = respone.total;
        });
    },
    //显示编辑界面
    handleEdit(index, row) {
      this.edit.id = row.id;
      this.edit.showEdit = true;
    },
    //显示新增界面
    handleAdd() {
      this.edit.id = 0;
      this.edit.showEdit = true;
    },
    //删除
    handleDel(index, row) {
      this.$confirm("确认删除?", "提示", { type: "warning" }).then(() => {
        this.loading = true;
        api.wechatSubDetail
          .del(row.id)
          .then((res) => {
            this.loading = false;
            this.$message({
              message: "删除成功",
              type: "success",
            });
            this.getWechatSubDetails();
          })
          .catch(() => {
            this.loading = false;
          });
      });
    },
    editChange(cancel) {
      if (cancel != "cancel") {
        this.getWechatSubDetails();
      }
    },
  },
};
</script>


<style lang="scss" scoped>
.info{
  font-size: 20px;
  span{
    color: #ff5000;
    font-weight: bolder;
    font-size: 24px;
  }
}
</style>