<template>
  <div class="menuManagement">
    <v-flex-container :leftWidth="'220px'">
      <div slot="left" class="menu-left" v-loading="loading" element-loading-text="拼命加载中">
        <h2 class="menu-title">
          行政区列表
          <!-- <el-button
            type="primary"
            icon="el-icon-plus"
            size="mini"
            style="margin-left:10px;"
            @click="addParentNode"
          >新增</el-button>-->
        </h2>
        <el-tree
          ref="tree"
          :data="groupList"
          :props="defaultProps"
          node-key="pm_code"
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
          <h2 class="menu-form-title">行政区代码信息</h2>
          <el-form :model="regionForm" ref="regionForm" label-width="150px" class="TreeForm">
            <el-form-item label="父节点编号">
              <el-input v-model="regionForm.pm_type" disabled></el-input>
            </el-form-item>
            <!-- :rules="{ required: true, message: '请输入节点名称', trigger: 'blur' }" -->
            <el-form-item label="节点名称" prop="pm_name">
              <el-input v-model="regionForm.pm_name" placeholder="请输入节点名称"></el-input>
            </el-form-item>
            <el-form-item label="节点编号">
              <el-input v-model="regionForm.pm_code" disabled></el-input>
            </el-form-item>
            <el-form-item>
              <el-button type="primary" @click="submitForm">保存</el-button>
            </el-form-item>
          </el-form>
        </div>
        <!-- <bookList :curId="curId"></bookList> -->
      </div>
    </v-flex-container>
    <el-dialog
      title="新增"
      :visible.sync="DialogShow"
      append-to-body
      v-dialogDrag
      width="600px"
      :close-on-click-modal="false"
    >
      <el-form :model="regionForm" ref="regionForm" label-width="150px" class="TreeForm">
        <el-form-item label="父节点编号">
          <el-input v-model="regionForm.pm_type" disabled></el-input>
        </el-form-item>
        <el-form-item
          label="节点名称"
          prop="pm_name"
          :rules="{ required: true, message: '请输入节点名称', trigger: 'blur' }"
        >
          <el-input v-model="regionForm.pm_name" placeholder="请输入节点名称"></el-input>
        </el-form-item>
        <el-form-item label="节点编号">
          <el-input v-model="regionForm.pm_code"></el-input>
        </el-form-item>
      </el-form>
      <!-- <vForm
        :curNodeData="curNodeData"
        :parentType="formType"
        @closeDialog="closeDialog"
        ref="treeForm"
        v-if="DialogShow"
      ></vForm>-->
      <span
        slot="footer"
        class="dialog-footer"
        style="display:inline-block;text-align:center;width:100%;"
      >
        <el-button type="primary" @click="submitForm">确 定</el-button>
        <el-button @click="closeDialog">取 消</el-button>
      </span>
    </el-dialog>
  </div>
</template>

<script>
import flexContainer from "@/components/FlexContainer";
import treeForm from "./children/TreeForm";
import Detail from "./children/TreeDetail";
import * as dataService from "@/public/apiService/sysManagement/Region";
import { forMateData } from "@/public/utils";
import _ from "lodash";
import { setTimeout } from "timers";
export default {
  data: function () {
    return {
      loading: false,
      detailLoading: false,
      operType: "add",
      DialogShow: false,
      curExpanded: [],
      checked: [],
      groupList: [],
      defaultProps: {
        children: "children",
        label: "pm_name",
      },
      detail: null,
      regionForm: {
        pm_code: "",
        pm_name: "",
        pm_type: "",
      },
      page: 1,
    };
  },
  created() {
    this.getMenuList(true);
  },
  methods: {
    getMenuList: function (flag) {
      this.loading = true;
      this.groupList = [];
      dataService.GetDataList().then((res) => {
        this.groupList = forMateData(res.data, "pm_type", "pm_code");
        if (flag) {
          let temp = _.clone(this.groupList[0]);
          this.curExpanded = [temp.pm_code];
        }
        this.loading = false;
      });
    },
    clickNode: function (data, node) {
      this.operType = "detail";
      this.curExpanded = [data.pm_code];
      let temp = _.clone(data);
      this.regionForm = {
        pm_code: temp.pm_code,
        pm_name: temp.pm_name,
        pm_type: temp.pm_type,
      };
    },
    append(data) {
      this.operType = "add";
      this.DialogShow = true;

      this.regionForm = {
        pm_type: data.pm_code,
        pm_code: "",
        pm_name: "",
      };
      // this.curNodeData = data;
    },
    edit(data) {
      this.operType = "edit";
      this.DialogShow = true;
    },
    remove(node, data) {
      if (node.childNodes.length > 0) {
        this.$message({
          type: "warning",
          message: "此节点包含子节点，请先删除子节点!",
        });
        return;
      }
      // order 1代表一级目录 2代表二级目录 ident：一级目录编号  bizid二级目录编号；
      this.$confirm("此操作将永久删除该数据, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          dataService.DelRegionData({ pm_code: data.pm_code }).then((res) => {
            if (res.success) {
              this.curExpanded = [data.pm_type];
              this.getMenuList(false);
              this.$message({
                type: "success",
                message: "删除成功!",
              });
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
    },

    submitForm: function () {
      this.$refs["regionForm"].validate((valid) => {
        if (valid) {
          dataService.UpdateRegionData(this.regionForm).then((res) => {
            if (res.success) {
              var msg = this.operType == "add" ? "添加成功!" : "修改成功!";
              this.curExpanded = [this.regionForm.pm_code];
              this.$message.success(msg);
              this.DialogShow = false;
              this.getMenuList(false);
              return;
            }
            this.$message.error("操作失败!");
            // console.log(res,'res==');
          });
        } else {
          return false;
        }
      });
    },
    renderContent(h, { node, data, store }) {
      if (node.level !== 1) {
        return (
          <span class="custom-tree-node">
            <span class="menu-tree-label">{data.pm_name}</span>
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
  },

  components: {
    "v-flex-container": flexContainer,
    vForm: treeForm,
    vDetail: Detail,
  },
};
</script>
<style lang="scss">
.menuManagement {
  height: calc(100% - 45px);
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