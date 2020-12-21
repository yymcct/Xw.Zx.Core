<template>
  <div class="tabPane2">
    <div class="demo-block" v-loading="loading">
      <el-table :data="cusFileList" ref="multipleTable" @selection-change="handleSelectionChange"
    style="width: 100%" row-key="UNID" :expand-row-keys="[curExpand]">
 <el-table-column label="序号" type="selection" width="55" align="center"></el-table-column>
   <el-table-column type="expand" width="50">
      <template slot-scope="props">
          <ul class="el-upload-list el-upload-list1 el-upload-list--text" v-if="isShow">
                  <li :tabindex="idx" class="el-upload-list__item is-success" v-for="(item,idx) in props.row.FILES" :key="idx">
                    <a class="el-upload-list__item-name"  target="_blank"  @click="downLoadFile(item.DISK_PATH)">
                      <i class="el-icon-document"></i>{{item.FILENAME}}</a>
                      <label class="el-upload-list__item-status-label"><i class="el-icon-upload-success el-icon-circle-check"></i></label>
                      <!-- <span class="file-download" @click="downLoadFile(item.DISK_PATH)">下载</span> -->
                      <!-- <i class="el-icon-close" @click="handleDelete(props.$index,idx)"></i> -->
                      </li></ul>
        </template>
    </el-table-column> 
    <el-table-column
      label="材料名称"
      prop="ATTRNAME" show-overflow-tooltip>
        <template slot-scope="scope">
          <div class="cell el-tooltip" >{{scope.row.ATTRNAME}}<b style="color:red">(含{{scope.row.FILES.length}}个附件)</b></div>
           </template>
    </el-table-column>
     <el-table-column label="收取方式" prop="TAKETYPE" width="150" align="center"></el-table-column>
     <el-table-column label="是否收取" prop="ISTAKE" width="150" align="center">
        <template slot-scope="scope">
          {{scope.row.ISTAKE=='1'?'是':'否'}}
        </template>
     </el-table-column>
     <el-table-column label="收取数量" prop="AMOUNT" width="150" align="center"></el-table-column>
     <el-table-column label="收取时间" prop="CREATE_TIME" width="180" align="center"></el-table-column>
    </el-table>  
    </div>
  </div>
</template>
<script>
import * as dataService from "@/public/apiService/ckcl/jointCheck.js";
import { apiUrl } from "@/public/apiUrl";
export default {
  props: ['type','detail','tabName'],
  data() {
    return {
      loading:false,
      activeItem: 0,
      activeName: ["1",'2'],
      cusFileList:[],
      isShow:false,
      curExpand:'',
      multipleSelection:[]
    };
  },
  created() {
    // this.$nextTick(()=>{
      // this.cusFileList=this.cusFileList.length&&this.formatFile(this.attachList);
        // this.isShow=true;
    // })
    //  this.cusFileList=this.attachList.length&&this.formatFile(this.attachList);
  },
  mounted() {
    this.getGfileList();
  },
  computed: {
  },
  methods: {
    downLoadFile:function(path){
      let url=apiUrl.CHECK_DISK_PATH + "?DISK_PATH=" + encodeURIComponent(path);
       window.open(url);
    },
      getGfileList: function() {
      this.cusFileList = [];
      this.loading=true;
      let temp=[];
      this.$http
        .get(apiUrl.Get_fileList + "?PROJID=" + this.detail.projid)
        .then(res => {
          temp=res.data.data;
          this.cusFileList = res.data.data;
          this.curExpand=temp[0].FILES.length?res.data.data[0].ATTRID:[];
          this.isShow=true;
           this.loading=false;
        });
    },
    handleSelectionChange:function(val){
     
      this.multipleSelection=val;
    },
    onFold: function(n) {
      if (this.activeItem == n) {
        this.activeItem = null;
      } else {
        this.activeItem = n;
      }
    },
    handleDelete:function(index,idx){
          this.cusFileList[index].file.splice(idx,1);
            this.isShow=false;
            setTimeout(()=>{
              this.isShow=true;
            },100)
    },
    customRequst: function(e,index) {
        this.curExpand=this.cusFileList[index].FX_CLASS;
        let file=e.target.files[0];
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
      formData.append("filename", file.name);
      formData.append("FX_0F00000000", file);
      formData.append("_Code_", "");
      formData.append("Submit", "提交");
      xmlhttp.send(formData);
      xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4) {
          if (xmlhttp.status == 200) {
							var data=JSON.parse(xmlhttp.responseText);
                 _this.cusFileList[index].file.push({
               AC_IDENT:data.Code,SR_NAME:data.Name});
               _this.isShow=false;
               setTimeout(()=>{
                 _this.isShow=true;
               },100)
             $('#'+index).val('');
          } else {
            console.log("上传失败" + xmlhttp.responseText);
          }
        }
			};
	},
  },
  components: {
  }
};
</script>

<style lang="scss" scoped>
@import "~@/assets/scss/mixins";
.tabPane2 {
  .demo-block {
    margin: 10px;
    width: calc(100% - 20px);
    // margin:0 auto;
    border: 1px solid #ebebeb;
    border-radius: 3px;
    transition: 0.2s;
    .el-table__expanded-cell{
      background:#f6faff;
    }
    .el-upload-list1{
      // width:100%;
      padding:0 70px;
    }
  /deep/  .el-upload-list__item:first-child{
     margin-top:0px;
    }
    .el-collapse-item__header {
      text-indent: 10px;
      border-bottom: 1px solid #e0e3ea;
      background: #efefef;
      font-weight: bolder;
      &.is-active {
        margin-bottom: 5px;
        border-bottom: 2px solid #0088cc;
      }
      .fa-icon {
        padding-right: 5px;
        color: #8aa7a4;
      }
    }
    .el-upload-list__item-name{
     text-decoration: none;
    }
  }
  .el-table__expanded-cell[class*="cell"] {
    padding: 0;
  }
  .tree-box {
    padding-left: 0;
    width: 100%;
  }
  .el-tree {
    background: white;
    > span {
      display: inline-block;
    }
    .el-tree-node {
      &:nth-of-type(1) {
        background: #5998c8;
      }

      > .el-tree-node__content {
        border: 1px solid #a6c9e2;
        border-top: none;
      }
      .el-tree-node {
        background: transparent;
      }
    }
    .el-tree-node__content {
      height: auto !important;
      &:hover {
        .btnItem {
          visibility: visible;
        }

        .topText {
          visibility: visible;
          display: none;
          position: relative;
          z-index: 31;
          color: #000;
        }
      }
      .topText {
        visibility: hidden;
        display: block;
        position: relative;
        bottom: 40px;

        z-index: 11;
      }
      .btnItem {
        visibility: visible;
      }
      // height: 35px;
      padding: 0 !important;

      // .treeItem {
      //   font-size: 14px;
      //   display: flex;
      //   justify-content: space-between;
      //   width: 100%;
      //   height: auto!important;
      //   color: #878d99;

      //   .item-span{
      //     display:inline-block;
      //     width:10%;
      //     padding:6px 10px;
      //     border-right: 1px solid #a6c9e2;
      //   }
      //   > td {
      //     // width: 10%;
      //     text-align: center;
      //     border: 0 !important;
      //   }

      //   .taskDecribe,
      //   .treeItemTd {
      //     text-overflow: ellipsis;
      //     overflow: hidden;
      //   }
      // }
    }
  }
  .treeHeader {
    background: #5998c8;
    color: #fff !important;
    font-weight: bold;
    .item-dec {
      margin-top: 10px;
    }
  }
  .item-inner {
    width: 100%;
    padding: 6px 10px;
    background: #f7f7f7;
  }
  .tree-box {
    cursor: pointer;
    padding: 5px 10px 10px;
    .treeItem {
      font-size: 14px;
      display: flex;
      justify-content: space-between;
      width: 100%;
      height: auto !important;
      // color: #878d99;
      border-bottom: 1px solid #5998c8;
      border-left: 1px solid #5998c8;
      transition: all 1s ease;
      .item-span {
        display: inline-block;
        width: 10%;
        padding: 6px 10px;
        border-right: 1px solid #a6c9e2;
        text-align: center;
        line-height: 1.5;
        .fold-icon {
          color: #03a9f4;
        }
      }
      .item-title {
        text-align: left;
      }
      > td {
        // width: 10%;
        text-align: center;
        border: 0 !important;
      }

      .taskDecribe,
      .treeItemTd {
        text-overflow: ellipsis;
        overflow: hidden;
        display: inline-block;
        line-height: 1.5;
      }
      .tree-file-box {
        // @include hide-overflowtext(25px, 1, true);
      }
    }
    .tree-file {
      color: #08c;
      text-align: left;
      font-size: 12px;
      text-decoration: none;
    }
  }
  .file-download{
       display: inline-block;
        text-decoration: none;
        padding: 0px 20px;
        background: #009688;
        border-radius: 20px;
        color: #fff;
        transition: all 1s ease;
        &:hover{
          background: #0ab1a1;
        }
        }
}
</style>
