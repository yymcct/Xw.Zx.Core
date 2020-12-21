<template>
  <div class="CounterMangement">
    <el-tabs v-model="activeName">
      <el-tab-pane label="登录日志" name="loginLog">
        <v-loginLog ref="loginLog" v-if="activeName=='loginLog'"></v-loginLog>
      </el-tab-pane>
      <el-tab-pane label="操作日志" name="operationLog">
        <v-operationLog ref="operationLog" v-if="activeName=='operationLog'"></v-operationLog>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script>
import lopginLog from "./children/loginLog";
import operationLog from "./children/operationLog";

export default {
  data: function() {
    return {
      activeName: "loginLog"
    };
  },

  created() {},
  methods: {},
  components: {
    "v-loginLog": lopginLog,
    "v-operationLog": operationLog
  }
};
</script>
<style lang="scss">
.CounterMangement {
  height: calc(100% - 45px);
  min-width: 900px;
  .el-tabs {
    height: 100%;
    .el-tabs__content {
      height: calc(100% - 55px);
      .el-tab-pane {
        height: 100%;
      }
    }
  }
  .cus-common-table {
    height: calc(100% - 160px);
    .cus-pagination {
      padding-top: 10px;
      text-align: center;
    }
    .el-button--text {
      padding: 0px;
      font-weight: bolder;
    }
  }
  .el-dialog__footer {
    text-align: center;
  }
}
</style>
