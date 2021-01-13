<template>
  <div class="Notice h100">
    <el-tabs v-model="activeName" @tab-click="handleClick">
      <el-tab-pane label="公共名片" name="public">
        <v-public ref="public" v-if="activeName=='public'"></v-public>
      </el-tab-pane>
      <el-tab-pane label="个人创建" name="person">
        <v-person ref="person" v-if="activeName=='person'"></v-person>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>

<script>
import public1 from "./children/public";
import person from "./children/person";
import * as dataService from "@/public/apiService/PersonalAffairs/address";
export default {
  data: function() {
    return {
      activeName: "public"
    };
  },

  created() {
    this.$nextTick(() => {
      this.$refs[this.activeName].getData();
    });
    // this.getData();
  },
  methods: {
    handleClick(tab, event) {
      this.$nextTick(() => {
        this.$refs[tab.name].getData();
      });
    }
  },
  components: {
    "v-public": public1,
    "v-person": person
  }
};
</script>
<style lang="scss">
.Notice {
  height: calc(100% - 45px);
  &.h100{
    height: 100%;
  }
  min-width: 900px;
  padding: 0px 10px;
  .el-tabs {
    height: 100%;
    .el-tabs__content {
      height: calc(100% - 55px);
      .el-tab-pane {
        height: 100%;
      }
    }
  }
  .handle-btn {
    padding: 10px 20px;
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
