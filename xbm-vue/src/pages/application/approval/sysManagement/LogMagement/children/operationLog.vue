<template>
  <div class="OperationLog">
    <div class="handle-btn">
      <el-form :model="formList" class="demo-form" :inline="true">
        <el-form-item label="用户名称">
          <el-input v-model="formList.ss_uid" style="width:180px"></el-input>
        </el-form-item>
        <el-form-item label="业务类型">
          <el-input v-model="formList.ss_mode" style="width:180px"></el-input>
        </el-form-item>
        <el-form-item label="操作类型">
          <el-input v-model="formList.ss_action" style="width:180px"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="search">查询</el-button>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="home">刷新</el-button>
        </el-form-item>
      </el-form>
    </div>
    <div class="tableParent">
      <operationList ref="operationList" :formList="formList"></operationList>
    </div>
  </div>
</template>

<script>
import operationList from "./operationList";
import * as dataService from "@/public/apiService/sysManagement/logMangement";
export default {
  name: "OperationLog",
  components: {
    operationList
  },
  data() {
    return {
      formList: {
        ss_uid: "",
        ss_mode: "",
        ss_action: "",
        page: 1
      },
      searchData: []
    };
  },
  created() {},
  mounted() {},
  computed: {},
  watch: {},
  methods: {
    search() {
      if (
        this.formList.ss_uid == "" &&
        this.formList.ss_mode == "" &&
        this.formList.ss_action == ""
      ) {
        this.$message({
          showClose: true,
          message: "请输入查询条件",
          type: "warning"
        });

        return false;
      }
      this.$nextTick(function() {
        var params = {
          ss_uid: this.formList.ss_uid,
          ss_mode: this.formList.ss_mode,
          ss_action: this.formList.ss_action,
          page: 1
        };
        this.$refs.operationList.getOperationList(params);
      });
    },
    home() {
      this.formList.ss_uid = "";
      this.formList.ss_mode = "";
      this.formList.ss_action = "";
      this.$refs.operationList.getOperationListData();
    }
  }
};
</script>

<style lang="scss">
.OperationLog {
  height: 100%;
  width: 100%;
  min-width: 930px;
  .handle-btn {
    text-align: center;
  }

  .tableParent {
    height: calc(100% - 140px);

    .cus-pagination {
      padding-top: 10px;
      text-align: center;
    }
  }

  /* overflow: hidden;
		position: relative;
		.log-search{
			position: absolute;
			left: 0;
			top: 0;




{
   "SIZE":3.0,
   "PAGE_SIZE": 1.2,
   "DATA":[

    {


      }

    ,

    {
"UR_NAME":"刘露露",
"SS_DATE":"2019-06-14  15:01",
"SS_ACTION":"修改",
"SS_MODE":"通知公告",
"SS_CONTENT":"此用户正在修改项目名称为【TZGG190614-000235】的文件"

      }

    ,

    {
"UR_NAME":"刘露露",
"SS_DATE":"2019-06-14  15:03",
"SS_ACTION":"修改",
"SS_MODE":"日程安排",
"SS_CONTENT":"此用户正在修改项目名称为【RCAP19061318】的文件"

      }

]
}














		} */
}
</style>
