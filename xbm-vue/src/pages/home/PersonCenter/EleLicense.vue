<template>
  <div class="EleLicense grid-inner-content">
      <el-table :data="data" style="100%" border height="calc(100% - 10px)" class="todo-table" v-loading="loading" min-height="500">
        <el-table-column type="index" width="50" label="序号" align="center"></el-table-column>
        <el-table-column prop="CZZT" label="单位名称" show-overflow-tooltip></el-table-column>
        <el-table-column prop="ZZMC" label="证照名称" show-overflow-tooltip></el-table-column>
        <el-table-column prop="FZRQ" label="颁发日期" show-overflow-tooltip></el-table-column>
        <el-table-column prop="ZZBH" label="证书编号" show-overflow-tooltip></el-table-column>
        <el-table-column prop="ZZZT" label="状态" align="center">
            <template slot-scope="scope">
              {{stateRela[scope.row.ZZZT]}}
            </template>
        </el-table-column>
        <el-table-column label="操作" align="center">
          <template slot-scope="scope">
            <el-button :disabled="scope.row.ZZZT=='-5'"
              @click="CancelLiense(scope.row)"
              title="证照作废" size="small"
              type="text" 
              icon="el-icon-document-delete">证照作废</el-button>
          </template>
        </el-table-column>
      </el-table>
      <!-- <el-pagination
        background
        layout="total,prev, pager, next, jumper"
        class="cus-pagination"
        @current-change="currentChange"
        :current-page.sync="form.PAGE"
        :page-size="form.LIMIT"
        :total="total"
      ></el-pagination> -->
    </div>
</template>
<script>
import {getAuthLiense} from "@/public/apiService/home.js";
import { apiUrl } from "@/public/apiUrl";
import { ElelicenseState } from "@/public/constant/constant.js";
export default {
  name: "ElectronicLicense",
  components: {},
  data() {
    return {
      loading: false,
      total: 0,
      // data: [],
      stateRela:ElelicenseState
    };
  },
  created() {
   this.loadData();
  },
  computed:{
  data(){
      return this.$store.getters.ExpireLicense
    }
  },
  methods: {
    loadData:function(){
      this.loading=true;
       this.$store.dispatch('GetExpireLicenseList').then((data)=>{
         this.loading=false;
        })
      
    },
    CancelLiense:function(row){
        let obj={ZZBH:row.ZZBH,ZZLX:row.ZZMLID}
      this.$confirm('此操作将执行作废, 是否继续?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          this.$http
        .get(apiUrl.CANCEL_LICENSEFILE, { params: obj })
        .then(res => {
          if(res.data.succ=='0'){
            this.loadData();
          }
           this.$message({
             type:res.data.succ=='0'?'success':'warning',
             message:res.data.msg
           })
        });
        }).catch(() => {
               
        });
    },
  }
};
</script>

<style lang="scss" scoped>
.EleLicense {
  width:100%;
  height:100%;
  // background: #f8f8f8;
  // padding: 10px 20px;
  margin-bottom: 10px;
  box-sizing: border-box;
  cursor: pointer;
 /deep/ .todo-table {
   width:100%;
   box-sizing: content-box;
   .el-table__body{
     width:calc(100% - 2px)!important;
   }
  
 }
  // .panel-body {
  //   color: red;
  //   /deep/ .todo-table {
  //     width:calc(100% - 10px);
  //     margin:0 auto;
  //     border: none;
  //     th {
  //       color: #3b4477;
  //       background: #f2f3fe;
  //     }
  //     th.is-leaf,
  //     td {
  //       border: none;
  //       padding: 8px 0px;
  //     }
  //     &:before {
  //       background: none;
  //     }
  //     .warning-row {
  //       background: oldlace;
  //     }
  //     .success-row {
  //       background: #f0f9eb;
  //     }
  //   }
  // }
  .cus-pagination{
    width: 100%;
    text-align: center;
    padding-top: 15px;
  }
}
</style>
