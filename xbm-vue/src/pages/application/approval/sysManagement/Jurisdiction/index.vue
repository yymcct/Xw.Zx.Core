<template>
  <div class="orgManagement">
    <v-flex-container :leftWidth="'180px'">
      <div
        slot="left"
        class="org-left"
        v-loading="loading"
        element-loading-text="拼命加载中"
      >
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
        <!-- <div class="handle-btn">
          <el-form :inline="true" :model="formInline" class="demo-form-inline">
            <el-form-item label="用户名称">
              <el-input
                style="width:180px"
                v-model="formInline.sr_name"
                clearable
              ></el-input>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="search">查询</el-button>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="home">刷新</el-button>
            </el-form-item>
          </el-form>
        </div> -->
      <vTable ref='datatable' v-if="'vtable' == active" @back="showUser" :tableDat="tableDat">表格</vTable> 
      <vUser ref="User"  v-if="'vuser' == active" @showdedatil="showdetail" :orgInfo="orgInfo"></vUser>
      </div>
    </v-flex-container>
  </div>
</template>
<script>
import flexContainer from "@/components/FlexContainer";
import user from "./user/user";
import table from "./user/table";
import { forMateData } from "@/public/utils";
import { getOrgTree } from "@/public/apiService/sysManagement/Organization";
export default {
  data: function() {
    return {
      loading: false,
      orgInfo: null,
      data: [],
      curExpanded: [],
      defaultProps: {
        children: "children",
        label: "OR_NAME"
      },
      formInline: {
        sr_name:''
      },
      active: "vuser",
      tableDat: ""
    };
  },
  created: function() {
    this.getTreeData();
  },
  methods: {
    getTreeData: function() {
      this.loading = true;
      getOrgTree().then(res => {
        this.loading = false;
        // this.data = res.DATA;
		this.data = forMateData(res.data, "OR_UPER", "OR_CODE");
        this.orgInfo = this.data[0];
        this.curExpanded = [this.orgInfo.OR_CODE];
        this.$nextTick(() => {
          this.$refs.User.getData(this.orgInfo.OR_CODE);
        });
      });
    },
    handleNodeClick(data, node) {
      this.active = "vuser";
      this.orgInfo = data;
      this.$nextTick(() => {
        this.$refs.User.getData(data.OR_CODE);
      });
    },
    showdetail(data) {
      this.active = "vtable";
      this.tableDat = data;
    },
    showUser() {
      this.active = "vuser";
      this.$nextTick(() => {
        this.$refs.User.getData(this.orgInfo.OR_CODE);
      });
    },
  },
  components: {
    "v-flex-container": flexContainer,
    vUser: user,
    vTable: table
  }
};
</script>
<style lang="scss">
.orgManagement {
  height: 100%;
  .handle-btn {
    padding: 10px 20px;
    text-align: center;
  }
  .tableParent {
    height: calc(100% - 140px);
    .cus-pagination {
      padding-top: 10px;
      text-align: center;
    }
  }
  .org-left,
  .org-right {
    height: 100%;
  }

  .org-left {
    .orgTree {
      height: 100%;
      overflow: auto;

      .box-card {
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

    .el-button {
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
