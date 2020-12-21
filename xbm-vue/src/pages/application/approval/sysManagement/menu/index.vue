<template>
  <div class="menuManagement">
    <v-flex-container :leftWidth="'220px'">
      <div slot="left" class="menu-left" v-loading="loading" element-loading-text="拼命加载中">
        <h2 class="menu-title">
          菜单项列表
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
          node-key="BZ_IDENT"
          :default-expanded-keys="curExpanded"
          class="menu-leftTree"
          @node-click="clickNode"
          accordion
          :render-content="renderContent"
          :check-on-click-node="true"
        ></el-tree>
        <!-- :default-checked-keys="checked" -->
      </div>
      <div slot="right" class="menu-right">
        <div class="menu-form" v-loading="detailLoading">
          <h2 class="menu-form-title">菜单项配置信息</h2>
          <vDetail
            :curNodeData="detail"
            :parentType="parentType"
            @updataLevel1Node="updataLevel1Node"
            @updataLevel2Node="updataLevel2Node"
            v-if="detail"
          ></vDetail>
          <!-- v-if="!detailLoading" -->
        </div>
        <!-- <bookList :curId="curId"></bookList> -->
      </div>
    </v-flex-container>
    <el-dialog
      title="新增"
      :visible.sync="DialogShow"
      v-dialogDrag
      width="600px"
      append-to-body
      :close-on-click-modal="false"
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
import treeForm from "@/pages/sysManagement/menu/children/TreeForm";
import Detail from "@/pages/sysManagement/menu/children/TreeEdit";
import * as dataService from "@/public/apiService/sysManagement/menu";
import _ from "lodash";
import { setTimeout } from "timers";
export default {
  data: function () {
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
        label: "BZ_NAME",
      },
      curNodeData: null,
      detail: null,
    };
  },
  created() {
    this.initPageData();
  },
  methods: {
    initPageData: function () {
      this.loading = true;
      this.detailLoading = true;
      dataService.getMenuList().then((res) => {
        this.groupList = res;
        this.detail = _.clone(this.groupList[0]);
        // this.checked = [this.detail.BZ_IDENT];
        console.log(this.detail, 99999);
        this.curExpanded = [this.detail.BZ_IDENT];
        this.detailLoading = false;
        this.loading = false;
      });
    },
    getMenuList: function () {
      this.loading = true;
      // this.detailLoading = true;
      this.$store.dispatch("navTabs/getMenuList");
      dataService.getMenuList().then((res) => {
        this.groupList = res;
        // console.log(this.groupList);
        this.loading = false;
        // this.detailLoading = false;
        // if (this.detail.level == 1) {
        //   this.groupList.forEach(item => {
        //     if (item.BZ_IDENT == this.checked[0]) {
        //       this.detail = item;
        //       return;
        //     }
        //   });
        // } else if (this.detail.level == 2) {
        //   this.groupList.forEach(item => {
        //     item.children &&
        //       item.children.forEach(ele => {
        //         if (ele.BA_IDENT == this.checked[0]) {
        //           this.detail = ele;
        //           return;
        //         }
        //       });
        //   });
        // }
      });
    },
    renderContent(h, { node, data, store }) {
      if (node.level !== 1) {
        return (
          <span class="custom-tree-node">
            <span class="menu-tree-label">{data.BA_NAME}</span>
            <span class="menu-tree-icon">
              <i
                size="mini"
                type="text"
                class="el-icon-delete"
                on-click={(e) => {
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
                on-click={(e) => {
                  e.stopPropagation();
                  this.append(data);
                }}
              />
              <i
                size="mini"
                type="text"
                class="el-icon-delete"
                on-click={(e) => {
                  e.stopPropagation();
                  this.remove(node, data);
                }}
              />
            </span>
          </span>
        );
      }
    },
    clickNode: function (data, node) {
      // this.curExpanded=data.BZ_IDENT
      this.curExpanded =
        node.level == 1 ? [data.BZ_IDENT] : [node.parent.data.BZ_IDENT];
      // this.checked = node.level == 1 ? [data.BZ_IDENT] : [data.BA_IDENT];
      this.parentType = node.level == 1 ? true : false;
      this.detailLoading = true;
      setTimeout(() => {
        this.detail = _.clone(data);
        this.detail.level = node.level;
        this.detailLoading = false;
      }, 100);
    },
    addParentNode: function () {
      this.formType = true;
      this.DialogShow = true;
    },
    append(data) {
      this.DialogShow = true;
      this.formType = false;
      this.curNodeData = data;
    },
    //新增一级菜单提交保存
    addLevel1Node: function (params) {
      dataService.addLevel1Menu(params).then((res) => {
        this.closeDialog();
        if (!res.success) {
          this.$message({
            type: "warning",
            message: res.msg,
          });
          return;
        }
        this.$message({
          type: "success",
          message: "添加成功!",
        });
        this.getMenuList();
      });
    },
    //新增二级菜单提交保存
    addLevel2Node: function (params) {
      dataService.addLevel2Menu(params).then((res) => {
        this.closeDialog();
        if (!res.success) {
          this.$message({
            type: "warning",
            message: res.msg,
          });
          return;
        }

        this.$message({
          type: "success",
          message: "添加成功!",
        });
        this.getMenuList();
      });
    },
    //更新一级菜单提交保存
    updataLevel1Node: function (params) {
      dataService.updateLevel1Menu(params).then((res) => {
        if (res.success) {
          this.$message({
            type: "success",
            message: res.msg,
          });
          this.getMenuList();
        } else {
          this.$message({
            type: "warning",
            message: res.msg,
          });
        }
      });
    },
    //更新二级菜单提交保存
    updataLevel2Node: function (params) {
      dataService.updateLevel2Menu(params).then((res) => {
        if (res.success) {
          this.$message({
            type: "success",
            message: res.msg,
          });
          this.getMenuList();
        } else {
          this.$message({
            type: "warning",
            message: res.msg,
          });
        }
      });
    },
    remove(node, data) {
      // const parent = node.parent;
      // console.log(data);
      if (node.childNodes.length > 0) {
        this.$message({
          type: "warning",
          message: "此节点包含子节点，请先删除子节点!",
        });
        return;
      }
      let params = {
        order: node.level,
        bizid: data.BA_IDENT || "",
      };
      params.ident = node.level == 1 ? data.BZ_IDENT : data.BA_BIZID;
      // order 1代表一级目录 2代表二级目录 ident：一级目录编号  bizid二级目录编号；
      this.$confirm("此操作将永久删除该菜单目录, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          dataService.delMenu(params).then((res) => {
            if (res.success) {
              this.parentType = true;
              if (node.level == 1) {
                this.initPageData(); //如果删除一级节点默认显示数组第一条数据信息
              } else {
                //如果删除二级节点默认显示二级节点所属父级数据信息
                this.loading = true;
                this.detailLoading = true;
                this.$store.dispatch("navTabs/getMenuList");
                dataService.getMenuList().then((res) => {
                  this.groupList = res;
                  this.loading = false;
                  this.detail = node.parent.data;
                  this.detailLoading = false;
                });
              }
              // this.getMenuList();
              this.$message({
                type: "success",
                message: "删除成功!",
              });
              this.getMenuList();
            } else {
              this.$message({
                type: "warning",
                message: res.msg,
              });
            }
          });
        })
        .catch(() => {});
    },
    closeDialog: function () {
      this.DialogShow = false;
      // this.operaType = "detail";
    },
    //一级二级菜单新增保存提交
    submitForm: function () {
      // console.log(this.operaType, "this.operaType");
      // if (this.operaType == "add") {
      this.$refs.treeForm.onSubmitAdd();
      // } else {
      //   this.$refs.treeForm.onSubmitEdit();
      // }
    },
  },

  components: {
    "v-flex-container": flexContainer,
    vForm: treeForm,
    vDetail: Detail,
  },
};
</script>
<style lang="scss">
// @import "~@/assets/scss/iconImg";
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
