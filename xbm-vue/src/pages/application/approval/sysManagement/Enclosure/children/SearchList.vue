<template>
  <div class="list">
    <el-table :data="dataList" border style="width: 100%" v-loading="loading" height="100%">
      <el-table-column type="index" width="50" label="编号" align="center"></el-table-column>
      <el-table-column prop="WIID" label="实例编号" width="200" align="center"></el-table-column>
      <!-- <el-table-column prop="AC_NAME" label="附件编号" width="150" align="center">
      </el-table-column>-->
      <el-table-column prop="SR_NAME" label="附件名称" align="center"></el-table-column>
      <el-table-column prop="SR_TIME" label="上传时间" width="180" align="center"></el-table-column>
      <el-table-column prop="UR_NAME" label="上传者" width="180" align="center"></el-table-column>
      <el-table-column fixed="right" label="操作" width="150" align="center">
        <template slot-scope="scope">
          <el-button @click="del(scope.$index)" title="删除" type="text">
            <i class="el-icon-delete common-text common-red"></i>
            <font class="common-red">删除</font>
          </el-button>
          <el-button @click="down(scope.row.AC_NAME)" title="下载" type="text">
            <i class="el-icon-download" style="color:green"></i>
            <font style="color:green">下载</font>
          </el-button>
        </template>
      </el-table-column>
    </el-table>
    <el-pagination
      background
      layout="total,prev, pager, next, jumper"
      class="cus-pagination"
      @current-change="currentChange"
      :page-size="10"
      :total="total"
    ></el-pagination>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/sysManagement/enclosure";
export default {
  name: "List",
  components: {},
  props: ["formInline"],
  data() {
    return {
      page: 1,
      dataList: [],
      tiaohao: ["否", "是"],
      zeroClearing: [
        "一直顺序递增",
        "按年自动归——",
        "按月自动归——",
        "按日自动归——"
      ],
      loading: true,
      total: 0,
      data1: []
    };
  },
  created() {},
  mounted() {
    console.log(this.formInline);
    this.getDataList(this.page);
  },
  computed: {},
  methods: {
    //获取搜索列表
    getDataList(page) {
      this.page = page;
      var data = this.formInline;
      dataService
        .getDataSearch(page, data.sr_name, data.kssj, data.jssj)
        .then(res => {
          console.log(res);
          this.dataList = res.DATA;
          this.loading = false;
          //获取类型
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
      this.getDataList(val);
    },
    del(index, row) {
      console.log(index);
      this.$confirm("此操作将永久删除该内容, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          dataService
            .getDataDel(this.dataList[index].WIID, this.dataList[index].AC_NAME)
            .then(res => {
              console.log(res);
              this.getDataList(this.page);
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
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除"
          });
        });
    },
    down(id) {
      console.log(id);
      window.open("/jz/XBM_Service.bsp?GetDoc&Source=" + id);
    }
  }
  // 		watch: {
  // 			formInline:{
  // 				handler(newValue, oldValue) {
  // 						//this.getDataList(this.page)
  // 						console.log(newValue)
  // 						this.formInline=newValue;
  // 					},
  // 					deep: true
  // 			}
  // 		},
};
</script>

<style lang="scss">
.list {
  height: 100%;
}
</style>
