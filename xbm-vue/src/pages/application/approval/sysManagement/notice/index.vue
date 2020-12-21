<template>
  <div class="Notice">
    <el-tabs v-model="activeName" @tab-click="handleClick">
      <el-tab-pane label="已发布" name="released"><v-released ref="released" v-if="activeName=='released'"></v-released></el-tab-pane>
      <el-tab-pane label="本人发布" name="unreleased"><v-unreleased ref="unreleased" v-if="activeName=='unreleased'"></v-unreleased></el-tab-pane>
      </el-tabs>
  </div>
</template>

<script>
import released from "./children/released";
import unreleased from "./children/unreleased";
import * as dataService from "@/public/apiService/PersonalAffairs/address";
export default {
  data: function() {
    return {
       activeName:'released'
    };
  },

  created() {
      this.$nextTick(()=>{
        this.$refs[this.activeName].getData();
      })
    // this.getData();
  },
  methods: {
     handleClick(tab, event) {

       this.$nextTick(()=>{
        this.$refs[tab.name].getData();
       })
      }
  },
  components: {
    'v-released':released,
    'v-unreleased':unreleased
  }
};
</script>
<style lang="scss">
.Notice {
  height: 100%;
  min-width: 900px;
  padding: 0px 10px;
  .el-tabs{
    height:100%;
    .el-tabs__content{
      height:calc(100% - 55px);
      .el-tab-pane{
        height:100%
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
    .el-button--text{
      padding:0px;
      font-weight: bolder;
    }
  }
  .el-dialog__footer{
    text-align: center;
  }
}
</style>
