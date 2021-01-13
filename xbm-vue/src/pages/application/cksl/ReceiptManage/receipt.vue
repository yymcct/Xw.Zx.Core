<template>
  <div class="approval-right">
     <div class="header">
      <div class="search">
      <el-form ref="form" class="cus-form">
        <el-input placeholder="请输入内容" v-model="sxmc" class="input-with-select" clearable>
          <el-button slot="append" icon="el-icon-search" class="search-btn" type="primary" @click="onSearch" size="small">查询</el-button>
        </el-input>
        </el-form>
      </div>
    </div>
    <el-table
      ref="multipleTable"
      :data="tableData"
      tooltip-effect="dark"
      :row-style="{height:'38px'}"
      :cell-style="{padding:'0px'}" border
      header-row-class-name="tableHead" v-loading="loading"
      style="width: 100%;" height="calc(100% - 138px)">
      <el-table-column type="index" label="序号" width="80"></el-table-column>
      <!-- <el-table-column type="selection" width="55"></el-table-column> -->
      <el-table-column label="事项名称" prop="SXMC" width="" show-overflow-tooltip>
      </el-table-column>
      <el-table-column prop="SXLB" label="事项编码" width="250"></el-table-column>
      <el-table-column prop="SXLX" label="事项类别" width="100" show-overflow-tooltip></el-table-column>
        <el-table-column  label="操作" width="120">
           <template slot-scope="scope">
                  <el-button @click="AddNewBusiness(scope.row)" type="primary" size="mini">新增业务</el-button>
            </template>
        </el-table-column>
      <!-- <el-table-column prop="SXLX" label="办件类型" width="120"></el-table-column> -->
    </el-table>
    <Pagination :total="total"
      :pageSize="pagesize"
      @handleSizeChangeSub="handleSizeChangeFun"
      @handleCurrentChangeSub="handleCurrentChangeFun"></Pagination>
      <transition name="el-zoom-in-center">
        <div class="absolute-box transition-box" v-if="DialogShow">
          <vForm :detail="curData" type="add" @close="close"></vForm>
        </div>
      </transition>
  </div>
</template>
<script>
 import Pagination from "@/components/pagination";
 import {apiUrl} from '@/public/apiUrl';
 import { getToken} from '@/public/auth'
 import { getAuthIssues} from '@/public/apiService/ckcl/ckcl'
 import vForm from './children/form'
 import _ from 'lodash'
export default {
  name: "right",
  components: { Pagination,vForm},
  data() {
    return {
      sxmc:'',
      loading:false,
      DialogShow:false,
      tableData: [],
      status: false,
      page:1,
      pagesize:10,
      total:0,
      curData:null
    };
  },
  created(){
    this.getDataList()
  },
  methods: {
    onSearch:function(){
      this.page=1;
      this.getDataList()
    },
    getDataList:function(){
      this.loading=true;
        var temp={
          sxmc:this.sxmc,
          page:this.page,
          pagesize:this.pagesize
        }
      getAuthIssues(temp).then(res=>{
         this.loading=false;
          this.total=res.SIZE;
          this.tableData=res.DATA;
        })
    },
    AddNewBusiness:function(item){
      item.sxmc=item.SXMC;
      item.SERVICENAME=item.SXMC;
      item.SERVICECODE=item.SXLB;
      item.applyfrom='2';
      this.curData=_.clone(item);
      this.DialogShow=true;
    },
    close:function(){
      this.DialogShow=false;
    },
    handleSizeChangeFun(v) {
      this.pageSize = v;
      this.getDataList()
    },
    handleCurrentChangeFun(v) {
      //页面点击
      this.page = v; //当前页
       this.getDataList()
    },
    // tableColor({ row, rowIndex }) {
    //   if (rowIndex == rowIndex) {
    //     if (row.status == 0) {
    //       return "tableColor1";
    //     } else if (row.status == 1) {
    //       return "tableColor2";
    //     } else if (row.status == 2) {
    //       return "tableColor3";
    //     } else if (row.status == 3) {
    //       return "tableColor4";
    //     }
    //   }
    // },
  }
};
</script>

<style lang="scss" scoped>
.approval-right {
  position:relative;
  width: 100%;
  height:100%;
  border-top: 3px solid #07438b;
  /* box-shadow: 0px 2px 3px 0px rgba(0, 0, 0, 0.15); */
  box-shadow: 0px 12px 8px -12px rgba(0, 0, 0, 0.15);
  // padding: 0px 54px 20px;
  background: #fff;
  .absolute-box {
    position: absolute;
    left: 0px;
    top: 0px;
    width: 100%;
    height: 100%;
    background: #fff;
    border: 1px solid #dcdfe6;
    box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.12), 0 0 6px 0 rgba(0, 0, 0, 0.04);
    z-index: 9;
  }
  .header {
  // height: 10%;
  .search {
    padding-top: 5px;
    width: 100%;
    line-height: 40px;
    text-align: center;
    background-color: #f3f3f3;
   /deep/ .cus-form{
      width:600px;
      margin:0 auto;
      padding:5px;
     .el-input__inner{
            height: 35px;
      }
      .search-btn{
          background: #66b1ff;
          border-color: #66b1ff;
          color: #FFF;
          border-radius: 0px 5px 5px 0px;
          height: 35px;
          margin-bottom: -8px;
          // padding-left: 10px;
      }
    }
  }
}
}
.table-button {
  width: 50px;
  height: 24px;
  background: rgba(237, 236, 236, 1);
  border-radius: 3px;
  color: #4c4948;
}
.el-table {
  margin-bottom: 20px;
}
.el-checkbox__input.is-checked .el-checkbox__inner,
.el-checkbox__input.is-indeterminate .el-checkbox__inner {
  background-color: #07438b;
  border-color: #07438b;
}
.tableHead {
  font-size: 18px;
  color: rgba(7, 67, 139, 1);
}
.tableColor1 td:nth-last-of-type(1) span {
  color: #ff5b5b;
}
.tableColor2 td:nth-last-of-type(1) span {
  color: #07438b;
}
.tableColor3 td:nth-last-of-type(1) span {
  color: #00c989;
}
.tableColor4 td:nth-last-of-type(1) span {
  color: #999999;
}
</style>