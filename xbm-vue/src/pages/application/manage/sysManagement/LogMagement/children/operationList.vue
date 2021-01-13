<template>
  <div class="list">
    <el-table :data="dataList" border style="width: 100%;" v-loading="loading" height="100%">
      <el-table-column type="index" width="50" label="编号" align="center"></el-table-column>
      <el-table-column prop="UR_NAME" label="用户名称" width="100" align="center"></el-table-column>
      <el-table-column prop="SS_MODE" label="业务类型" width="220" align="center"></el-table-column>
      <el-table-column prop="SS_ACTION" label="操作类型" width="130" align="center"></el-table-column>
      <el-table-column prop="SS_DATE" label="操作时间" align="center" width="130"></el-table-column>
      <el-table-column prop="SS_CONTENT" label="具体内容" show-overflow-tooltip align="center"></el-table-column>
      <el-table-column fixed="right" label="操作" width="100" align="center">
        <template slot-scope="scope">
          <el-button @click="del(scope.$index)" type="text" >
            <i class="el-icon-delete common-text common-red"></i>
            <font class="common-red">删除</font>
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      background
      layout="total,prev, pager, next, jumper"
      @current-change="currentChange"
      :current-page="page"
      class="cus-pagination"
      :page-size="10"
      :total="total"
    ></el-pagination>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/sysManagement/logMangement";
export default {
  name: "operationList",
  components: {},
  props: ["formList"],
  data() {
    return {
      page: 1,
      dataList: [],
      loading: true,
      total: 0,
      data1: []
    };
  },
  created() {},
  mounted() {
    this.getOperationListData();
  },
  computed: {},
  methods: {
    //获取列表
    getOperationListData(a) {
      var params = {
        ss_uid: this.formList.ss_uid,
        ss_mode: this.formList.ss_mode,
        ss_action: this.formList.ss_action,
        page: a ? a : this.formList.page
      };
      this.getOperationList(params);
    },
    getOperationList(params) {
      this.page = params.page;
      dataService
        .getOperationList(params)
        .then(res => {
          this.dataList = res.DATA;
          this.loading = false;
          this.total = res.SIZE;
        })
        .catch(err => {
          console.log(err);
        });
    },
    currentChange(val) {
      console.log(val);
      this.loading = true;
      this.page = val;
      this.getOperationListData(val);
    },
    search() {},
    del(index) {
      this.$confirm("此操作将永久删除该内容, 是否继续?", "提示", {
        closeOnClickModal: false,
        cancelButtonText: "取消",
        confirmButtonText: "确定",
        type: "warning"
      })
        .then(() => {
          dataService
            .getOperationListDel(this.dataList[index].SS_ID)
            .then(res => {
              console.log(res);
              this.getOperationListData(this.page);
              this.$message({
                type: "success",
                message: "删除成功!"
              });
            })
            .catch(err => {
              console.log(err);
              this.$message({
                type: "info",
                message: "删除操作失败"
              });
            });
        })
        .catch(err => {
          console.log(err);
          this.$message({
            type: "info",
            message: "已取消删除"
          });
        });
    }
  }
};
</script>

<style lang="scss">
.list {
  height: 100%;
}

</style>
