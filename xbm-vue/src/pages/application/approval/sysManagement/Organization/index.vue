<template>
  <div class="orgManagement">
    <v-flex-container :leftWidth="'180px'">
      <div slot="left" class="org-left" v-loading="loading" element-loading-text="拼命加载中">
        <div class="orgTree">
          <div class="box-card">
            <div class="el-popover__title">组织机构</div>
            <el-tree
              :data="data"
              :props="defaultProps"
              node-key="OR_CODE"
              :default-expanded-keys="curExpanded"
              accordion
              @node-click="handleNodeClick"
            ></el-tree>
          </div>
        </div>
        <!-- <v-org @NodeClick="NodeClick"></v-org> -->
      </div>
      <div slot="right" class="org-right">
        <el-tabs
          v-model="activeName"
          @tab-click="handleClick"
          type="border-card"
          class="cus-tabs"
          style="height:100%"
        >
          <el-tab-pane label="单位管理" name="Unit" class="cus-pane">
            <vUnit ref="Unit" @getTree="getTreeData" v-if="activeName=='Unit'"></vUnit>
          </el-tab-pane>
          <el-tab-pane label="部门管理" name="Depart" class="cus-pane">
            <vDepart
              ref="Depart"
              :orgInfo="orgInfo"
              v-if="activeName=='Depart'"
              @getTree="getTreeData"
            ></vDepart>
          </el-tab-pane>
          <el-tab-pane label="用户管理" name="User" class="cus-pane">
            <vUser ref="User" :orgInfo="orgInfo" v-if="activeName=='User'"></vUser>
          </el-tab-pane>
        </el-tabs>
      </div>
    </v-flex-container>
  </div>
</template>

<script>
import flexContainer from "@/components/FlexContainer";
import user from "./user/user";
import Unit from "./unit/Unit";
import Depart from "./depart/Depart";
import { getOrgTree } from "@/public/apiService/sysManagement/Organization";
import { forMateData } from "@/public/utils";
export default {
  data: function () {
    return {
      loading: false,
      activeName: "User",
      orgInfo: null,
      curExpanded: [],
      data: [],
      defaultProps: {
        children: "children",
        label: "OR_NAME",
      },
    };
  },

  created: function () {
    console.log(111);
    this.getTreeData();
  },
  methods: {
    getTreeData: function () {
      this.loading = true;
      console.log(222);
      getOrgTree().then((res) => {
        console.log(333);
        this.loading = false;
        this.data = forMateData(res.data, "OR_UPER", "OR_CODE");
        this.orgInfo = this.data[0];
        this.curExpanded = [this.orgInfo.OR_CODE];
        this.$nextTick(() => {
          this.$refs[this.activeName].getData(this.orgInfo.OR_CODE);
        });
      });
    },
    handleClick(tab, event) {
      if (!this.orgInfo) {
        this.$message({
          type: "warning",
          message: "请等待左侧列表加载完后点击",
        });
        return;
      }
      this.activeName = tab.name;
      this.$nextTick(() => {
        this.$refs[this.activeName].getData(this.orgInfo.OR_CODE);
      });
    },
    handleNodeClick(data, node) {
      // console.log(data, node);
      if (node.isLeaf) {
        data.parentCode = node.parent.data.OR_CODE;
      }
      this.orgInfo = data;
      this.$refs[this.activeName].getData(data.OR_CODE);
    },
  },
  components: {
    "v-flex-container": flexContainer,
    vUser: user,
    vUnit: Unit,
    vDepart: Depart,
  },
};
</script>
<style lang="scss" scoped>
.orgManagement {
  height: 100%;
  /deep/ .org-right {
    height: 100%;
    .cus-tabs {
      height: 100%;
      .el-tabs__content {
        height: calc(100% - 40px);
      }
    }
  }
  .org-left {
    height: 100%;
    /deep/ .orgTree {
      height: 100%;
      overflow: auto;
      /deep/ .box-card {
        height: calc(100% - 3px);
        margin: 0px 1px 1px;
        border: 1px solid #ebeef5;
      }
    }
  }

  .cus-common-table {
    width: 98%;
    margin: 0 auto;
    text-align: center;
    /deep/ .el-button {
      padding: 0px;
    }
    .cus-pagination {
      padding: 10px;
    }
  }
  .cus-pane {
    height: calc(100% - 50px);
  }
}
</style>
