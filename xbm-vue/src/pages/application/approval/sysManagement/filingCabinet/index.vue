<template>
  <div class="filingCabinet">
    <v-flex-container :leftMinWidth="'180px'" :leftWidth="'220px'">
      <div slot="left" class="file-left" v-loading="show">
        <h2 class="file-title">
          <img src="@/assets/images/folder.png" class="file-title-img">文档中心
        </h2>
        <div class="left-items">
          <h4 class="file-title1">
            个人文件柜
            <span title="添加" @click="addCatalog(1)" class="el-icon-plus"></span>
          </h4>
          <el-tree
            :data="data1"
            @node-contextmenu="showDel"
            :props="defaultProps"
            accordion
            node-key="id"
            @node-click="clickNode"
            :render-content="renderContent1"
          ></el-tree>
        </div>
        <div class="left-items">
          <h4 class="file-title1">
            公共文件柜
            <span @click="addCatalog(2)" title="添加" v-if="isShowAuthBtn" class="el-icon-plus"></span>
          </h4>
          <el-tree
            :props="defaultProps"
            @node-contextmenu="showDel"
            :data="data2"
            accordion
            node-key="id"
            @node-click="clickNode"
            :render-content="renderContent"
          ></el-tree>
        </div>
      </div>
      <div slot="right" class="file-right" v-loading="loading">
        <!-- <div v-if="upload">
					<el-button type="text">{{tit}}</el-button>
					<el-button type="danger" size="mini" icon="el-icon-delete" circle></el-button>
        </div>-->
        <el-breadcrumb
          style="height: 30px;line-height: 30px;font-size: 14px;font-weight: 600;padding: 5px 10px;"
          separator-class="el-icon-arrow-right"
        >
          <el-breadcrumb-item>{{tit2}}</el-breadcrumb-item>
          <el-breadcrumb-item>{{tit1}}</el-breadcrumb-item>
        </el-breadcrumb>
        <div class="file-right-box">
          <el-table :data="tableData" border style="width: 100%" height="100%">
            <el-table-column type="index" label="序号" width="80" align="center"></el-table-column>
            <el-table-column prop="DC_Ident" label="文件名" align="center"></el-table-column>
            <!-- <el-table-column prop="UR_NAME" label="发布人" width="180" align="center">
            </el-table-column>-->
            <el-table-column prop="TIME" label="发布时间" width="180" align="center"></el-table-column>
            <el-table-column fixed="right" label="操作" width="180" align="center">
              <template slot-scope="scope">
                <el-button title="下载" @click="down(scope.row.SR_IDENT)" type="text">
                  <i class="el-icon-download common-text" style="color:green"></i>
                  <font style="color:green">下载</font>
                </el-button>
                <template v-if="isPersonFile">
                  <el-button title="删除" @click="del(scope.row.DCID)" type="text">
                    <i class="el-icon-delete common-red common-text"></i>
                    <font class="common-red">删除</font>
                  </el-button>
                </template>
                <template v-else>
                  <el-button
                    title="删除"
                    v-if="isShowAuthBtn"
                    @click="del(scope.row.DCID)"
                    type="text"
                  >
                    <i class="el-icon-delete common-red common-text"></i>
                    <font class="common-red">删除</font>
                  </el-button>
                </template>
              </template>
            </el-table-column>
          </el-table>
        </div>
        <template v-if="isPersonFile">
          <el-form :inline="true" class="file-form-inline" v-if="upload">
            <el-form-item label="文件上传：">
              <el-upload
                class="upload-demo"
                action="/jz/XBM_Service.bsp?File"
                :on-preview="onPreview"
                :file-list="fileList"
                :http-request="customRequst"
                :on-remove="remove"
              >
                <el-button slot="trigger" size="small" type="primary">选取文件</el-button>
                <el-button
                  style="margin-left: 10px;"
                  size="small"
                  type="success"
                  @click="saveFile"
                >保存文件</el-button>
                <!-- <el-button type="danger" size="small" @click="showDel">删除文件夹</el-button> -->
              </el-upload>
            </el-form-item>
          </el-form>
        </template>
        <template v-else>
          <template v-if="isShowAuthBtn">
            <el-form :inline="true" class="file-form-inline" v-if="upload">
              <el-form-item label="文件上传：">
                <el-upload
                  class="upload-demo"
                  action="/jz/XBM_Service.bsp?File"
                  :on-preview="onPreview"
                  :file-list="fileList"
                  :http-request="customRequst"
                  :on-remove="remove"
                >
                  <el-button slot="trigger" size="small" type="primary">选取文件</el-button>
                  <el-button
                    style="margin-left: 10px;"
                    size="small"
                    type="success"
                    @click="saveFile"
                  >保存文件</el-button>
                  <!-- <el-button type="danger" size="small" @click="showDel">删除文件夹</el-button> -->
                </el-upload>
              </el-form-item>
            </el-form>
          </template>
        </template>
      </div>
    </v-flex-container>

    <el-dialog
      :title="tit"
      :visible.sync="dialogFormVisible"
      append-to-body
      :close-on-click-modal="false"
    >
      <el-form :model="form" ref="formadd">
        <el-form-item
          label="目录名称"
          prop="name"
          label-width="100px"
          :rules="{required: true, message: '请填写目录名称', trigger: 'blur' }"
        >
          <el-input v-model="form.name" autocomplete="off"></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="submitCatalog">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>
<script>
import flexContainer from "@/components/FlexContainer";
import * as dataService from "@/public/apiService/PersonalAffairs/file";
export default {
  name: "filingCabinet",
  data: function() {
    return {
      keywords: "",
      data: [],
      defaultProps: {
        label: "DOC_NAME"
      },
      tableData: [],
      userInfo: JSON.parse(localStorage.getItem("data")),
      data1: [],
      data2: [],
      dialogFormVisible: false,
      form: {
        name: ""
      },
      tit: "个人文件柜",
      zt: "个人",
      show: true,
      fileList: [],
      upload: 0,
      isShowAuthBtn: false,
      docid: "",
      loading: false,
      tit1: "",
      tit2: "",
      delFile: "",
      isPersonFile: true
    };
  },
  created: function() {},
  mounted() {
    this.getBtnAuth();
    this.getFileCatalog();
  },
  methods: {
    getBtnAuth: function() {
      dataService.getFileCatalogBtnAuth().then(res => {
        this.isShowAuthBtn = res.data;
      });
    },
    addCatalog(type) {
      if (type == 1) {
        this.tit = "个人文件柜";
        this.zt = "个人";
      } else {
        this.tit = "公共文件柜";
        this.zt = "公共";
      }

      this.dialogFormVisible = true;
    },
    submitCatalog() {
      this.dialogFormVisible = false;
      this.$refs["formadd"].validate(valid => {
        if (valid) {
          var params = {
            ur_ident: this.userInfo.ur_ident,
            zt: this.zt,
            doc_name: this.form.name
          };

          dataService
            .getFileCatalogAdd(params)
            .then(res => {
              this.$message({
                message: "目录添加成功",
                type: "success"
              });
              this.form.name = "";
              this.getFileCatalog();
              console.log(res);
            })
            .catch(err => {
              console.log(err);
              this.$message({
                message: "目录添加失败",
                type: "warning"
              });
            });
        }
      });
    },
    getFileList(a) {
      this.loading = true;
      var params = {
        docid: a
      };
      dataService
        .getFileList(params)
        .then(res => {
          // console.log(res)
          this.tableData = res.data;
          this.loading = false;
        })
        .catch(err => {
          console.log(err);
        });
    },
    getFileCatalog() {
      this.show = true;
      var params = {
        ur_ident: this.userInfo.ur_ident
      };
      dataService
        .getFileCatalog(params)
        .then(res => {
          if (res.data.length > 0) {
            this.getFileList(res.data[0].DOCID);
            this.docid = res.data[0].DOCID;
            this.tit1 = res.data[0].DOC_NAME;
            this.tit2 = "个人文件柜";
            this.upload = 1;
            this.isPersonFile = true;
          } else if (res.data2.length > 0) {
            this.getFileList(res.data2[0].DOCID);
            this.docid = res.data2[0].DOCID;
            this.tit1 = res.data2[0].DOC_NAME;
            this.tit2 = "公共文件柜";
            this.upload = 1;
            this.isPersonFile = false;
          } else {
            this.upload = 0;
          }

          this.data1 = res.data;
          this.data2 = res.data2;
          this.show = false;
        })
        .catch(err => {
          console.log(err);
        });
    },
    getFileCatalogAdd() {
      var params = {
        ur_ident: this.userInfo.ur_ident
      };
      dataService
        .getFileCatalogAdd(params)
        .then(res => {
          console.log(res);
        })
        .catch(err => {
          console.log(err);
        });
    },
    clickNode(data, node) {
      // console.log(data)
      this.getFileList(data.DOCID);
      this.docid = data.DOCID;
      this.tit1 = data.DOC_NAME;
      this.upload = 1;
      if (data.ZT == "公共") {
        this.tit2 = "公共文件柜";
        this.isPersonFile = false;
      } else {
        (this.tit2 = "个人文件柜"), (this.isPersonFile = true);
      }
    },
    down(id) {
      window.open("/jz/XBM_Service.bsp?GetDoc&Source=" + id);
    },
    del(id) {
      var params = {
        docid: this.docid,
        dcid: id
      };

      this.$confirm("此操作将永久删除该文件, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          dataService
            .getFileCatalogDelFile(params)
            .then(res => {
              console.log(res);
              if (res.success) {
                this.$message({
                  type: "success",
                  message: "删除成功!"
                });

                this.getFileList(this.docid);
              } else {
                this.$message({
                  type: "error",
                  message: "删除失败!"
                });
              }
            })
            .catch(err => {
              console.log(err);
            });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除"
          });
        });
      console.log(params);
    },
    delData(node, data) {
      // console.log(type,'type');
      var params = {
        docid: node.data.DOCID
      };
      // dataService.getFileList(params).then(res => {
      // 		if(res.data.length==0){
      // 			this.delFile=true
      // 		}else{
      // 			this.delFile=false
      // 		}
      // 	if(type.data.ZT=="公共"&&this.userInfo.ur_ident!="7"){
      // 	  	this.$confirm('你没有删除该文件夹权限!', '提示', {
      // 			confirmButtonText: '确定',
      // 		  type: 'warning'
      // 	})
      // 	}else if(this.delFile==false){
      // 		console.log()
      // 		this.$confirm('请先删除该文件夹下的所有文件!', '提示', {
      // 			confirmButtonText: '确定',
      // 		  type: 'warning'

      // 	})
      // 	}else{
      // 		  this.showDel()

      // 	}
      // 	})
    },
    showDel(node, data) {
      console.log(node, data);
      var params = {
        docid: data.DOCID
      };
      this.$confirm("此操作将删除该文件夹, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          dataService
            .getFileCatalogDel(params)
            .then(res => {
              if (res.success) {
                this.getFileCatalog();
                this.$message({
                  type: "success",
                  message: "操作成功！"
                });
                return;
              }
              this.$message({
                type: "error",
                message: res.msg
              });
            })
            .catch(err => {
              console.log(err);
            });
        })
        .catch(() => {});
    },
    onPreview: function(file) {
      window.open(file.url);
    },
    remove(file, fileList) {
      console.log(file, fileList);
      console.log(this.fileList);

      for (var i = 0; i < this.fileList.length; i++) {
        if (file.code == this.fileList[i].code) {
          console.log(i);
          this.fileList.splice(i, 1);
        }
      }
    },
    customRequst: function(file) {
      var formData = new FormData();
      var xmlhttp;
      if (window.XMLHttpRequest) {
        // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
      } else {
        // code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
      }
      var _this = this;
      xmlhttp.open("POST", "/jz/XBM_Service.bsp?FILE", true);
      xmlhttp.setRequestHeader("X-Requested-With", "XMLHttpRequest");
      formData.append("filename", file.file.name);
      formData.append("FX_0F00000000", file.file);
      formData.append("_Code_", "");
      formData.append("Submit", "提交");
      xmlhttp.send(formData);
      xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4) {
          if (xmlhttp.status == 200) {
            var data = JSON.parse(xmlhttp.responseText);
            _this.fileList.push({
              name: file.file.name,
              // url: "/jz/XBM_Service.bsp?GetDoc&Source=" + code,
              url: data.Addr,
              code: data.Code
            });
            // console.log(_this.fileList,'fileList1122');
          } else {
            console.log("上传失败" + xmlhttp.responseText);
          }
        }
      };
    },
    fileParms() {
      let arr = [];
      this.fileList.forEach(item => {
        arr.push({
          dc_ident: item.code
        });
      });
      return arr;
    },
    saveFile() {
      var params = {
        docid: this.docid,
        DATA: this.fileParms()
      };
      if (params.DATA.length == 0) {
        this.$message({
          message: "请先上传文件！",
          type: "warning"
        });

        return false;
      }
      dataService
        .getFileCatalogAddFile(params)
        .then(res => {
          if (res.success) {
            this.getFileList(this.docid);
            this.fileList = [];
            this.$message({
              message: "文件保存成功",
              type: "success"
            });
          }
        })
        .catch(err => {
          console.log(err);
        });
    },
    renderContent1(h, { node, data, store }) {
      return (
        <span class="custom-tree-node">
          <span>
            <i class="file-img" />
            {node.label}
          </span>
          <span
            on-click={e => {
              e.stopPropagation();
              this.showDel(node, data);
            }}
            class="del-action el-icon-delete"
            title="删除文件夹"
          />
        </span>
      );
    },
    renderContent(h, { node, data, store }) {
      if (this.isShowAuthBtn) {
        return (
          <span class="custom-tree-node">
            <span>
              <i class="file-img" />
              {node.label}
            </span>
            <span
              on-click={e => {
                e.stopPropagation();
                this.showDel(node, data);
              }}
              class="del-action el-icon-delete"
              title="删除文件夹"
            />
          </span>
        );
      } else {
        return (
          <span class="custom-tree-node">
            <span>
              <i class="file-img" />
              {node.label}
            </span>
          </span>
        );
      }
    }
  },
  components: {
    "v-flex-container": flexContainer
  }
};
</script>
<style lang="scss">
.filingCabinet {
  height: calc(100% - 45px);
   font-size:14px;
  .file-left {
    height: 100%;
    overflow: auto;
    .file-title {
          font-size: 14px;
        line-height: 40px;
        height: 40px;
        font-weight: normal;
      .file-title-img {
           padding: 10px;
          width: 22px;
          vertical-align: bottom;
      }
    }

    .file-title1 {
       font-size: 14px;
      padding: 10px;
      align-items: center;
     font-weight: normal;
      span {
        float: right;
        color: #21a0ff;
        font-size: 20px;
        display: none;
        cursor: pointer;
      }

      &:hover {
        span {
          display: block;
        }
      }
    }

    .custom-tree-node {
      font-size: 14px;
      display: inline-block;
      width: 100%;

      .file-img {
        display: inline-block;
        width: 16px;
        height: 16px;
        background: url("~@/assets/images/file-tree.png");
        margin-right: 5px;
      }

      .del-action {
        color: red;
        float: right;
        margin-right: 10px;
        display: none;
        line-height: 20px;
        z-index: 999999;
      }

      &:hover .del-action {
        display: inline-block;
      }
    }
    .left-items{
      padding-left:20px;
    }
  }

  .file-right {
    height: 100%;
    overflow: auto;

    .file-right-box {
      height: calc(100% - 130px);
    }

    .file-form-inline {
      padding: 10px;

      .el-form-item {
        margin-bottom: 0px;
      }
    }
  }
}
</style>
