<template>
  <div class="OnlineHelp">
    <v-flex-container :leftWidth="'180px'">
      <div slot="left" class="org-left" v-loading="loading" style="height:100%">
        <div class="orgTree">
          <div class="box-card" v-if="data.length">
            <div class="el-popover__title" style="margin-bottom:0px;">
              文档类别
              <el-button
                type="primary"
                @click="append"
                size="mini"
                style="margin-top:-7px;float:right"
              >新增</el-button>
            </div>
            <el-tree
              ref="tree"
              :data="data"
              :props="defaultProps"
              node-key="NODEID"
              class="help-leftTree"
              :highlight-current="true"
              @node-click="handleNodeClick"
              :default-expanded-keys="[curNodeData.NODEID]"
              :render-content="renderContent"
            ></el-tree>
          </div>
        </div>
      </div>
      <div slot="right" class="org-right">
        <div class="right-btn">
          <el-button
            type="primary"
            @click="editCont"
            v-if="type=='cont'"
            size="small"
            style="margin:10px;float:right"
          >编辑</el-button>
        </div>
        <div class="cont" v-loading="contLoading" v-if="type!='cont'">
          <add
            @onSubmit="onSubmit"
            :NRStr="contStr"
            :curNodeData="curNodeData"
            @close="dialogVisible=false"
          ></add>
        </div>
        <div class="cont" v-loading="contLoading" v-html="contStr" v-else></div>
      </div>
    </v-flex-container>
    <el-dialog
      title="新增"
      :visible.sync="DialogShow"
      v-dialogDrag
      width="500px'"
      append-to-body
      :close-on-click-modal="false"
    >
      <template>
        <el-form ref="form" label-width="80px">
          <el-form-item label="名称">
            <el-input v-model="nodename"></el-input>
          </el-form-item>
        </el-form>
        <span slot="footer" class="dialog-footer">
          <el-button @click="DialogShow=false" size="small">取 消</el-button>
          <el-button type="primary" @click="submitForm" size="small">确 定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/home";
import flexContainer from "@/components/FlexContainer";
import add from "./Form/AddHelp";
import { setTimeout } from "timers";
export default {
  name: "timeLine",
  data: function() {
    return {
      loading: false,
      contLoading: false,
      DialogShow: false,
      dialogVisible: false,
      contStr: "",
      nodename: "",
      type: "cont",
      curNodeData: null,
      data: [],
      defaultProps: {
        children: "children2",
        label: "NODENAME"
      }
    };
  },
  created() {
    this.getinitHelpCat();
  },
  computed: {},
  watch: {
    curNodeData: function(val) {
      this.$nextTick(() => {
        this.$refs.tree.setCurrentKey(val.NODEID); // treeBox 元素的ref   value 绑定的node-key
      });
    }
  },
  methods: {
    getinitHelpCat(id) {
      this.loading = true;
      dataService
        .getHelpCat({})
        .then(res => {
          this.data = res.data;
          this.dialogVisible = true;
          this.curNodeData = this.data[0];
          this.loading = false;
          if (id) {
            this.curNodeData.NODEID = id;
          }
          this.getHelpCont(this.curNodeData.NODEID);
        })
        .catch(err => {
          console.log(err);
        });
    },
    addCont: function() {
      if (!this.curNodeData) {
        this.$message({
          type: "warning",
          message: "请先选择左侧目录"
        });
        return;
      }
      this.type = "contAdd";
    },
    editCont: function() {
      this.type = "contEdit";
    },
    onSubmit: function(param) {
      this.type = "cont";
      this.curNodeData.NODEID = param;
      this.getHelpCont(param);
      this.$nextTick(() => {
        this.$refs.tree.setCurrentKey(param); // treeBox 元素的ref   value 绑定的node-key
      });
    },
    getHelpCont: function(id) {
      this.contStr = "";
      this.contLoading = true;
      dataService.getHelpCont(id).then(res => {
        res.data &&
          res.data.forEach(item => {
            this.contStr += item.WJ_NR;
          });
        this.contLoading = false;
        if (this.contStr == "" && this.type != "contEdit") {
          this.type = "contAdd";
        } else {
          this.type = "cont";
        }
      });
    },
    handleNodeClick(data, node) {
      this.curNodeData = data;
      this.getHelpCont(data.NODEID);
    },
    append(data) {
      this.DialogShow = true;
      this.nodename = "";
      this.curNodeData = data;
    },
    remove: function(node, data) {
      if (node.childNodes.length > 0) {
        this.$message({
          type: "warning",
          message: "此节点包含子节点，请先删除子节点!"
        });
        return;
      }
      this.$confirm("此操作将永久删除该文件, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      }).then(() => {
        this.onSubmitDelData(data.NODEID);
      });
    },
    onSubmitDelData: function(id) {
      dataService.delHelpCat(id).then(res => {
        if (res.success) {
          this.getinitHelpCat();
          this.$message({
            type: "success",
            message: "删除成功!"
          });
        } else {
          this.$message({
            type: "warning",
            message: res.msg
          });
        }
      });
    },
    submitForm: function() {
      if (!this.nodename) {
        this.$message({
          type: "warning",
          message: "名称不能为空!"
        });
        return;
      }
      let parentid = this.curNodeData.NODEID || 1;
      dataService.addHelpCat(this.nodename, parentid).then(res => {
        if (res.success) {
          this.DialogShow = false;
          this.curNodeData.NODEID = res.data[0].NODEID;
          this.getinitHelpCat(this.curNodeData.NODEID);
          this.$message({
            type: "success",
            message: "添加成功!"
          });
        }
      });
    },
    renderContent(h, { node, data, store }) {
      if (node.level == 1 || node.level == 2) {
        return (
          <span class="custom-tree-node">
            <span class="menu-tree-label">{node.label}</span>
            <span class="menu-tree-icon">
              <i
                size="mini"
                type="text"
                class="el-icon-plus"
                on-click={e => {
                  e.stopPropagation();
                  this.append(data);
                }}
              />
              <i
                size="mini"
                type="text"
                class="el-icon-delete"
                on-click={e => {
                  e.stopPropagation();
                  this.remove(node, data);
                }}
              />
            </span>
          </span>
        );
      } else {
        return (
          <span class="custom-tree-node">
            <span class="menu-tree-label">{data.NODENAME}</span>
            <span class="menu-tree-icon">
              <i
                size="mini"
                type="text"
                class="el-icon-delete"
                on-click={e => {
                  e.stopPropagation();
                  this.remove(node, data);
                }}
              />
            </span>
          </span>
        );
      }
    }
  },

  components: {
    "v-flex-container": flexContainer,
    add
  }
};
</script>

<style lang="scss">
.OnlineHelp {
  height: 100%;
  .org-left,
  .orgTree,
  .box-card {
    height: 100%;
  }
  .org-right {
    height: 100%;
    .right-btn {
      width: 100%;
      height: auto;
      overflow: hidden;
      border-bottom: 1px solid #efefef;
    }
    .cont {
      height: calc(100% - 48px);
      padding: 10px 20px;
      overflow: auto;
      // overflow: auto;
      // padding: 10px 20px;
    }
  }
  .el-popover__title {
    font-size: 14px;
  }
  .help-leftTree {
    font-size: 14px;
    // padding-top: 10px;
    height: calc(100% - 44px);
    overflow: auto;
    .el-tree-node {
      // padding:5px;
      .el-tree-node__content {
        height: 36px;
        line-height: 36px;
        position: relative;
        .custom-tree-node {
          .menu-tree-label {
          }
          .menu-tree-icon {
            display: none;
            padding: 0px 5px;
          }
        }
        &:hover {
          .menu-tree-icon {
            display: inline-block;
            position: absolute;
            right: 0px;
            background: #e8eef7;
            padding: 0px 10px;
            .el-icon-plus,
            .el-icon-delete,
            .el-icon-edit {
              font-size: 16px;
            }
            .el-icon-delete {
              color: red;
              &:hover {
                color: darken(red, 20%);
              }
            }
            .el-icon-plus {
              padding-right: 10px;
              &:hover {
                color: blue;
              }
            }
          }
        }
      }
    }
  }
  // 	td, th {
  // 	border: 1px solid #DDD;
  // }
}
</style>
