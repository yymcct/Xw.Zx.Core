<template>
  <div class="OnlineHelp">
    <v-flex-container :leftWidth="'180px'">
      <div slot="left" class="org-left">
        <div class="orgTree" v-loading="loading" element-loading-text="拼命加载中">
          <!-- <el-button type="primary" @click="append" size="small" style="margin:10px;">新增</el-button> -->
          <div class="box-card" v-if="data.length">
            <!-- <div class="el-popover__title" style="margin-bottom:0px;">文档类别</div> -->
            <el-tree
              ref="tree"
              :data="data"
              :props="defaultProps"
              node-key="NODEID"
              class="help-leftTree"
              :highlight-current="true"
              @node-click="handleNodeClick"
              :default-expanded-keys="[data[0].NODEID]"
              :render-content="renderContent"
            ></el-tree>
          </div>
        </div>
      </div>
      <div slot="right" class="org-right">
        <!-- v-html="contStr" -->
        <div class="contIDX" v-loading="contLoading">
          <!-- <iframe width="100%"  height="100%" src="../../../../jz/static/helpCont.html" id="iframe1" frameborder="0"></iframe>  -->
          <iframe
            width="100%"
            height="100%"
            src="../../../../jz/static/template.html"
            id="iframe1"
            frameborder="0"
          ></iframe>
        </div>
      </div>
    </v-flex-container>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/home";
import flexContainer from "@/components/FlexContainer";
import add from "./Form/AddHelp";
export default {
  name: "timeLine",
  data: function() {
    return {
      loading: false,
      contLoading: false,
      contStr: "",
      nodename: "",
      type: "leftAdd",
      curNodeData: null,
      data: [],
      defaultProps: {
        children: "children2",
        label: "NODENAME"
      }
    };
  },
  mounted() {
    // this.contStr=this.data[0].cont[0]
    this.getHelpCat();
  },
  computed: {},
  watch: {},
  methods: {
    getHelpCat() {
      this.loading = true;
      dataService
        .getHelpCat({ cd_w: "w" })
        .then(res => {
          this.data = res.data;
          this.curNodeID = this.data[0].NODEID;
          this.dialogVisible = true;
          this.loading = false;
          this.getHelpCont(this.data[0].NODEID);
          this.$nextTick(() => {
            this.$refs.tree.setCurrentKey(this.data[0].NODEID); // treeBox 元素的ref   value 绑定的node-key
          });
        })
        .catch(err => {
          console.log(err);
        });
    },
    // addData:function(){
    //    this.type='leftAdd';
    // 	this.dialogVisible=true;
    // },
    getHelpCont: function(id) {
      this.contStr = "";
      this.contLoading = true;
      //  var contEle=$("#iframe1").contents().find("#helpCont");
      var contEle = $("#iframe1")
        .contents()
        .find("#content");
      dataService.getHelpCont(id).then(res => {
        res.data &&
          res.data.forEach(item => {
            this.contStr += item.WJ_NR;
            //  this.contStr+=this.Base64.decode(item.WJ_NR);
          });
        contEle.html(this.contStr);
        contEle.addClass("helpCont");
        this.contLoading = false;
      });
    },
    handleNodeClick(data, node) {
      // this.contStr=data.cont[0];
      this.getHelpCont(data.NODEID);
    },

    renderContent(h, { node, data, store }) {
      //  console.log(node.level,data);
      if (node.level == 1 || node.level == 2) {
        return (
          <span class="custom-tree-node">
            <span class="menu-tree-label">{node.label}</span>
          </span>
        );
      } else {
        return (
          <span class="custom-tree-node">
            <span class="menu-tree-label">{data.NODENAME}</span>
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
  .orgTree {
    height: 100%;
    overflow: auto;
  }
  .org-right {
    height: 100%;
    .contIDX {
      height: 100%;
      // overflow: auto;
      // padding: 10px;
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
}
</style>
