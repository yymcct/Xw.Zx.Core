<template>
  <div class="emPersonSel">
    <el-tabs
      v-model="activeName"
      class="per-tabs"
      @tab-click="tabClick"
      v-loading="treeLoading"
      element-loading-text="拼命加载中"
    >
      <el-tab-pane label="已选" name="1">
        <span v-if="selPersonTags.length==0">暂无数据</span>
        <el-tag
          :key="tag.id"
          v-for="tag in selPersonTags"
          v-else
          closable
          :disable-transitions="false"
          @close="handleClose(tag)"
          class="em-tags"
        >{{tag.name}}</el-tag>
      </el-tab-pane>
      <el-tab-pane label="按部门" name="2">
        <!--按部门树结构 strat-->
        <el-input placeholder="输入关键字选择" v-model="filterText" class="per-input"></el-input>
        <span v-if="treeData.length==0">暂无数据</span>
        <el-tree
          :data="treeData"
          v-else
          class="per-tree"
          accordion
          show-checkbox
          node-key="ur_ident"
          :default-expanded-keys="[treeData[0].ur_ident]"
          :default-checked-keys="checked"
          :props="defaultProps"
          :filter-node-method="filterNode"
          ref="tree"
        ></el-tree>
        <!--按部门树结构 end-->
        <span class="peopleSel-footer">
          <!-- <el-button @click="">取 消</el-button> -->
          <el-button type="primary" @click="saveSelPeople" class="people-save-btn">确 定</el-button>
        </span>
      </el-tab-pane>
      <el-tab-pane label="自定义分组" name="3" >
        <!-- <span v-if="selPersonTags.length==0">暂无数据</span> -->
        <el-dialog
          title="分组人员选择:"
          :visible.sync="addGropPersonShow"
          width="300px"   v-dialogDrag
          append-to-body
        
          :close-on-click-modal="false"
        >
          <!-- :modal="modelShow" -->
          <div class="addSelfGroupPerson" style="height:400px;">
            <el-tree
              :data="treeData"
              class="per-tree-self" style="height:360px;overflow: auto;"
              accordion
              show-checkbox
              node-key="ur_ident"
              :default-expanded-keys="[treeData[0].ur_ident]"
              :default-checked-keys="checked"
              :props="defaultProps"
              :filter-node-method="filterNode"
              ref="treeAddSelf"
              v-if="treeData.length"
            ></el-tree>
            <span class="peopleSel-footer" style=" display:inline-block;padding:10px;width: 100%;text-align: center;">
              <!-- <el-button @click="">取 消</el-button> -->
              <el-button
                type="primary"
                @click="saveSelfGroup"
                class="people-save-btn"
                size="small"
              >确 定</el-button>
            </span>
          </div>
        </el-dialog>
        <div class="selfGroup">
          <el-button type="primary" @click="addSelfGroup" size="mini">添加分组</el-button>
          <div class="addSelfGroup" v-if="addSelfGroupShow">
            <el-form ref="form" :model="form" label-width="70px" label-position="left">
              <el-form-item label="分组名称">
                <el-input v-model="form.name" size="mini"></el-input>
                <el-button type="primary" size="mini" @click="addGrop">添加</el-button>
                <el-button type="primary" size="mini" @click="cancelAddGroup">取消</el-button>
              </el-form-item>
            </el-form>
          </div>
          <span v-if="treeSelfData.length==0">暂无数据</span>
           <el-tree
            :data="treeSelfData"
            v-else
            class="per-tree"
            accordion
            show-checkbox
            node-key="ur_ident"
            :default-expanded-keys="[treeSelfData[0].ur_ident]"
            :default-checked-keys="checked"
            :props="defaultProps"
            :filter-node-method="filterNode"
            :render-content="renderContent"
            ref="treeSelf"
          ></el-tree> 
          <span class="peopleSel-footer">
            <!-- <el-button @click="">取 消</el-button> -->
            <el-button type="primary" @click="saveSelfPerson" class="people-save-btn">确 定</el-button>
          </span>
        </div>
      </el-tab-pane>
    </el-tabs>
  </div>
</template>
<script>
import * as dataService from "@/public/apiService/PersonalAffairs/email";
import { forMateData } from "@/public/utils";
// import UE from "@/components/Editor.vue";
export default {
  name: "emPersonSel",
  props: ["selPerson"],
  data: function() {
    return {
      selPersonTags: [],
      SelfcheckedPerson: [],
      treeLoading: false,
      checked: [],
      activeName: "2",
      treeData: [],
      defaultProps: {
        children: "children",
        label: "name"
      },
      filterText: "",
      form: {
        name: ""
      },
      addSelfGroupShow: false,
      addGropPersonShow: false,
      selfGroupShow: true,
      treeSelfData: [],
      modelShow: false,
      addId: "",
      checkedData: [],
      SelfPerson: []
    };
  },
  mounted() {
    this.addSelfGroupList();
  },
  watch: {
    filterText: function(val) {
      this.$refs.tree.filter(val);
    },
    selPerson:function(val){
      this.setCheckedNodes(val);
      console.log(val,'val===');
    },
  },
  created: function() {
    this.getPersonData();
  },
  methods: {
    getPersonData: function() {
      this.treeLoading = true;
      let data = this.selPerson;
      dataService.getEmailPerson().then(res => {
        this.treeLoading = false;
        this.treeData = [];
        res.data.map(item => {
          item.id = item.ur_ident;
          item.name = item.ur_name;
        });
        this.treeData = forMateData(res.data, "or_uper", "ur_ident");
        this.setCheckedNodes(data);
      });
    },
    //保存按部门选择人员
    saveSelPeople: function() {
      this.getCheckedNodes();
      this.checkedData = this.selPersonTags;
      this.$emit("closeDialog", this.checkedData);
    },
    //保存自定义选择人员
    saveSelfPerson: function() {
      this.getSelfCheckedNodes();
      console.log(this.SelfPerson,this.getSelfCheckedNodes(),'ss');
      this.checkedData = this.SelfPerson;
      this.$emit("closeDialog", this.checkedData);
    },
    saveSelfGroup() {
      this.getSelfCheckedNodesAdd();
      this.addSelfGroupShow = false;
      this.selfGroupShow = true;
    },
    //点击添加分组按钮
    addSelfGroup() {
      this.addSelfGroupShow = true;
      this.selfGroupShow = false;
    },
    //添加分组
    addGrop() {
      if (this.form.name) {
        var params = {
          doc_name: this.form.name
        };
        dataService
          .addSelfGroup(params)
          .then(res => {
            this.addSelfGroupShow = false;
            this.form.name = "";
            this.addSelfGroupList();
          })
          .catch(() => {});
      }
    },
    //取消分组
    cancelAddGroup() {
      this.addSelfGroupShow = false;
    },
    //自定义分组列表
    addSelfGroupList() {
      dataService.addSelfGroupList().then(res => {
        // console.log(res);
        res.map(ele=>{
          ele.ur_ident=ele.id
        })
        this.treeSelfData = res;
      });
    },
    //添加删除分组图标
    renderContent(h, { node, data, store }) {
      return (
        <span class="custom-tree-node">
          <span class="icon-label">{node.label}</span>
          <span class="icon-group">
            <i class="el-icon-plus" title="新增" on-click={(e) => {e.stopPropagation();this.append(data)}}></i>
            <i
              class="el-icon-delete" title="删除"
              on-click={(e) => {e.stopPropagation();this.remove(node, data)}}
            ></i>
          </span>
        </span>
      );
    },
    append(data) {
      this.addGropPersonShow = true;
      this.addId = data.ur_ident;
    },
    //添加自定义分组人员
    addGropPerson() {
      var params = {
        docid: this.addId,
        DATA: this.SelfcheckedPerson
      };
      dataService.addSelfGroupPerson(params).then(res => {
        this.addSelfGroupList();
      });
    },

    //删除分组和删除分组人员
    remove(node, data) {
      let that=this;
      this.$confirm('是否确定移除该用户?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
             handleDel(node,data,that);
        }).catch(() => {
                
        });
     function handleDel(node,data,vm){
       let _this=vm;
        if (data.ur_ident) {
                var data = {
                  docid: data.ur_ident
                };
                dataService.delSelfGroup(data).then(res => {
                  _this.addSelfGroupList();
                     _this.$message({
                    type: 'success',
                    message: '删除成功!'
                  });
                });
              }
              if (data.DCID) {
                var params = {
                  dcid: data.DCID,
                  docid: node.parent.data.ur_ident
                };
                dataService.delSelfGroupPerson(params).then(res => {
                  _this.addSelfGroupList();
                    _this.$message({
                    type: 'success',
                    message: '删除成功!'
                  });
                });
              }
     }
    },
    //标签切换
    tabClick: function(tab) {
      if (tab.name == "1") {
        this.getCheckedNodes();
      }
    },
    //部门人员选择
    filterNode: function(value, data) {
      // console.log(data,'data===');
      if (!value) return true;
      return data.name && data.name.indexOf(value) !== -1;
    },
    //设置部门勾选
    setCheckedNodes: function(data) {
      this.selPersonTags = data;
      var temp = [];
      this.selPersonTags.forEach(function(item) {
        temp.push(item.id);
      });
      this.checked = temp;
      this.$refs.tree && this.$refs.tree.setCheckedKeys(temp);
    },
    //获取部门人员勾选
    getCheckedNodes: function() {
      var temp = this.$refs.tree.getCheckedNodes();
      var arr = [];
      temp.forEach(function(item) {
        if (!item.children) {
          arr.push({ name: item.name, id: item.ur_ident,ur_ident:item.ur_ident });
        }
      });
      this.selPersonTags = arr;
    },
    // setCheckedNodes: function(data) {

    // },
    //勾选增加自定义人员
    getSelfCheckedNodesAdd: function() {
      var tempSelf = this.$refs.treeAddSelf.getCheckedNodes();
      var arr = [];
      tempSelf.forEach(function(item) {
        if (!item.children) {
          arr.push({ dc_remark: item.name, dc_ident: item.ur_ident });
        }
      });
      this.SelfcheckedPerson = arr;
      this.addGropPerson();

      this.addGropPersonShow = false;
    },

    //获取自定义人员勾选,
    getSelfCheckedNodes: function() {
      var tempSelf = this.$refs.treeSelf.getCheckedNodes();
      var arr = [];
      tempSelf.forEach(function(item) {
        if (!item.children) {
          arr.push({ name: item.name, id: item.DC_Ident,ur_ident:item.DC_Ident });
        }
      });
      this.SelfPerson = arr;
    },
    //删除人员
    handleClose: function(tag) {
      this.selPersonTags.splice(this.selPersonTags.indexOf(tag), 1);
      this.setCheckedNodes(this.selPersonTags);
    }
  }
};
</script>
<style lang="scss" scoped>
.emPersonSel {
  height: 500px;
  /deep/ .per-tabs {
    height: calc(100% - 60px);
    /deep/ .el-tabs__content {
      height: 100%;
      .el-tab-pane {
        height: 100%;
        .per-input {
          margin-bottom: 10px;
        }
        
      /deep/ .per-tree {
          padding: 10px 0px;
          height: calc(100% - 105px);
          overflow: auto;
        /deep/ .el-tree-node__content{
              height:32px;
            .custom-tree-node{
              position:relative;
              display:inline-block;
              width:100%;
              &:hover{
                 .icon-group{
                   visibility:visible!important;
                 }
              }
              .icon-label{
                display:inline-block;
                width:calc(100% - 50px);
              }
              .icon-group{
                display:inline-block;
                width:50px;
                position:absolute;
                z-index:11;
                right:20px;
                text-align: right;
                visibility: hidden;
               .el-icon-plus,.el-icon-delete{
                 &:hover{
                   opacity: 0.5;
                 }
               }
              }
            }
            }
        }
        /deep/ .per-tree-self {
          height: calc(100% - 135px);
          overflow: auto;
          margin-left: 5px;
        }
       /deep/  .el-form-item {
          margin: 0;
        }
        .em-tags {
          margin-right: 10px;
        }
      }
    }
  }
   /deep/ .peopleSel-footer {
    width: 100%;
    display: inline-block;
    text-align: center;
  }
  /deep/ .selfGroup {
    width: 100%;
    height: 100%;
    .per-tree {
    /deep/  .icon-group {
        // margin-left: 10px;
        .el-icon-delete {
          color: #f00;
          margin: 0 3px;
        }
        .el-icon-plus {
          color: rgb(49, 139, 199);
          margin: 0 3px;
        }
      }
     /deep/  .el-tree-node__children {
        .el-tree-node {
          .icon-group {
            .el-icon-plus {
              display: none;
            }
          }
        }
      }
    }
  }
  // .addSelfGroup {
  //   .el-input {
  //     width: 200px;
  //   }
  // }
  .addSelfGroupPerson {
    height: 300px;
    
    .per-tree-self {
      height: 360px !important;
    }
    .peopleSel-footer {
      height: 40px;
    }
  }
}
</style>
