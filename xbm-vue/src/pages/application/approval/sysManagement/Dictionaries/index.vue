<template>
  <div class="menuManagement">
    <v-flex-container :leftWidth="'220px'">
      <div slot="left" class="menu-left" v-loading="loading" element-loading-text="拼命加载中">
        <h2 class="menu-title">
          数据字典列表
          <el-button
            type="primary"
            icon="el-icon-plus"
            size="mini"
            style="margin-left:10px;"
            @click="addParentNode"
          >新增</el-button>
        </h2>
        <el-tree
          ref="tree"
          :data="groupList"
          :props="defaultProps"
          node-key="NODEID"
          :default-expanded-keys="curExpanded"
          class="menu-leftTree"
          @node-click="clickNode"
          accordion
          :render-content="renderContent"
          :check-on-click-node="true"
          :expand-on-click-node="true"
        ></el-tree>
        <!-- :default-checked-keys="checked" -->
      </div>
      <div slot="right" class="menu-right">
        <div class="menu-form" v-loading="detailLoading">
          <h2 class="menu-form-title">字典配置信息</h2>
          <vDetail
            :curNodeData="detail"
            :parentType="parentType"
            @updataLevel1Node="updataLevel1Node"
            v-if="detail"
          ></vDetail>
          <!-- v-if="!detailLoading" -->
        </div>
        <!-- <bookList :curId="curId"></bookList> -->
      </div>
    </v-flex-container>
    <el-dialog
      title="新增字典"
      :visible.sync="DialogShow"
      append-to-body
      v-dialogDrag
      :close-on-click-modal="false"
      width="600px"
    >
      <vForm
        :curNodeData="curNodeData"
        :parentType="formType"
        @closeDialog="closeDialog"
        @addLevel1Node="addLevel1Node"
        @addLevel2Node="addLevel2Node"
        ref="treeForm"
        v-if="DialogShow"
      ></vForm>
      <span slot="footer" class="dialog-footer">
        <el-button @click="closeDialog">取 消</el-button>
        <el-button type="primary" @click="submitForm">确 定</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import flexContainer from "@/components/FlexContainer";
import treeForm from "./children/TreeForm";
import Detail from "./children/TreeDetail";
import * as dataService from "@/public/apiService/sysManagement/Dictionaries";
import _ from "lodash";
import { setTimeout } from "timers";
export default {
  data: function() {
    return {
      loading: false,
      detailLoading: false,
      formType: false,
      parentType: true,
      DialogShow: false,
      curExpanded: [],
      checked: [],
      groupList: [],
      defaultProps: {
        children: "children",
        label: "NODENAME"
      },
      curNodeData: null,
      detail: null,
      page: 1
    };
  },
  created() {
    this.initPageData(this.page);
  },
  methods: {
    initPageData: function(a) {
      this.loading = true;
      this.detailLoading = true;
      dataService.getDataList(a).then(res => {
        this.groupList = res;
        this.detail = _.clone(this.groupList[0]);
        this.curExpanded = [this.detail.NODEID];
        /* res.forEach(v=>{  
			console.log(v); 
			 if(v.children.length>0){
				 
			 }
		}); */

        this.detailLoading = false;
        this.loading = false;
      });
    },
    getMenuList: function() {
      this.loading = true;
      dataService.getDataList(this.page).then(res => {
        this.groupList = res;
        this.loading = false;
      });
    },
    clickNode: function(data, node) {
      this.curExpanded = [data.NODEID];
      this.parentType = node.level == 1 ? true : false;
      this.detailLoading = true;
      setTimeout(() => {
        this.detail = _.clone(data);
        this.detail.level = node.level;
        this.detail.nodePaent = node.parent.data.NODENAME;
        this.detail.nodePaentId = node.parent.data.NODEID;
        this.detailLoading = false;
      }, 100);
    },
    addParentNode: function() {
      this.formType = true;
      this.DialogShow = true;
    },
    append(data) {
      this.DialogShow = true;
      this.formType = false;
      this.curNodeData = data;
    },
    //新增一级菜单提交保存
    addLevel1Node: function(params) {
      console.log(params);
      dataService.getDataLEVEL1(params).then(res => {
        this.closeDialog();
        if (!res.success) {
          this.$message({
            type: "warning",
            message: res.msg
          });
          return;
        }
        this.$message({
          type: "success",
          message: "添加成功!"
        });
        this.getMenuList();
      });
    },
    //新增二级菜单提交保存
    addLevel2Node: function(params) {
      console.log(params);
      dataService.getDataLEVEL2(params).then(res => {
        this.closeDialog();
        if (!res.success) {
          this.$message({
            type: "warning",
            message: res.msg
          });
          return;
        }

        this.$message({
          type: "success",
          message: "添加成功!"
        });
        this.getMenuList();
      });
    },
    //更新一级菜单提交保存
    updataLevel1Node: function(params) {
      dataService.getDataEdit(params).then(res => {
        if (res.success) {
          this.$message({
            type: "success",
            message: res.msg
          });
          this.getMenuList();
        } else {
          this.$message({
            type: "warning",
            message: res.msg
          });
        }
      });
    },
    remove(node, data) {
      if (node.childNodes.length > 0) {
        this.$message({
          type: "warning",
          message: "此节点包含子节点，请先删除子节点!"
        });
        return;
      }
      let params = {
        nodeid: ""
      };
      params.nodeid = data.NODEID;
      // order 1代表一级目录 2代表二级目录 ident：一级目录编号  bizid二级目录编号；
      this.$confirm("此操作将永久删除该数据, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          dataService.getDataDEL(params).then(res => {
            if (res.success) {
              this.parentType = true;
              this.initPageData(this.page);
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
        })
        .catch(() => {});
    },
    closeDialog: function() {
      this.DialogShow = false;
    },
    submitForm: function() {
      this.$refs.treeForm.onSubmitAdd();
    },
    renderContent(h, { node, data, store }) {
      if (node.level !== 1) {
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
      } else {
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
      }
    }
  },

  components: {
    "v-flex-container": flexContainer,
    vForm: treeForm,
    vDetail: Detail
  }
};
</script>
<style lang="scss">
.menuManagement {
  height: 100%;
  .menu-left {
    height: 100%;
    .menu-title {
      background: #f5f5f5;
      padding: 8px 10px;
      font-size: 16px;
    }
    .menu-leftTree {
      font-size: 14px;
      padding-top: 10px;
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
  }
  .menu-right {
    height: 100%;
    padding: 20px;
    .menu-form {
      width: 80%;
      margin: 0 auto;
      max-width: 600px;
      .menu-form-title {
        font-weight: 400;
        color: #1f2f3d;
        font-size: 28px;
        padding-bottom: 20px;
      }
    }
  }
}
</style>