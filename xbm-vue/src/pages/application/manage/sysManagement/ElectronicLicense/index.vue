<template>
  <div class="ElectronicLicense">
    <div class="top-search-form">
      <el-form :inline="true" :model="form" class="demo-form-inline" label-width="105px">
				 <el-form-item label="证照编号:">
          <el-input placeholder="请输入证照编号" v-model="form.ZZBH" class="cus-form" clearable></el-input>
        </el-form-item>
        <el-form-item label="证照名称:">
          <el-select v-model="form.ZZMC" placeholder="请选择证照名称" class="cus-form" clearable>
            <!-- ZZMLID -->
            <el-option v-for="(item,idx) in XQZZ" :label="item.ZZMC" :value="item.ZZMC" :key="idx"></el-option>
          </el-select>
        </el-form-item>
				  <el-form-item label="项目名称:">
          <el-input placeholder="请输入项目名称" v-model="form.XMMC" class="cus-form" clearable></el-input>
        </el-form-item>
        <el-form-item label="持证者名称:">
          <el-input placeholder="请输入持证者名称" v-model="form.CZZT" class="cus-form" clearable></el-input>
        </el-form-item>
        <el-form-item label="状态:">
          <el-select v-model="form.ZZZT" placeholder="请选择证照名称" class="cus-form" clearable>
            <el-option label="有效" value="11"></el-option>
            <!-- <el-option label="暂时失效" value="-10"></el-option> -->
            <!-- <el-option label="已过期" value="-4"></el-option> -->
            <el-option label="已作废" value="-5"></el-option>
          </el-select>
        </el-form-item>
		     <el-form-item label="开始时间:">
					<el-date-picker clearable  class="cus-form" value-format="yyyy-MM-dd" format="yyyy-MM-dd"  v-model="form.KSSJ" 
					 type="date" placeholder="选择开始时间">
					</el-date-picker>
				</el-form-item>
				<el-form-item label="结束时间:">
					<el-date-picker clearable   value-format="yyyy-MM-dd" format="yyyy-MM-dd" v-model="form.JSSJ" type="date" class="cus-form"
					 placeholder="选择结束时间">
					</el-date-picker>
				</el-form-item>
        <el-form-item>
          <el-button type="primary" size="medium" @click="doSearch">查询</el-button>
        </el-form-item>
      </el-form>
    </div>
    <div class="tableParent">
      <el-table :data="data" border height="calc(100% - 50px)" v-loading="loading" min-height="500">
        <el-table-column type="index" width="50" label="序号" align="center"></el-table-column>
        <el-table-column prop="CZZT" label="单位名称" align="center"></el-table-column>
        <el-table-column prop="ZZMC" label="证照名称" align="center"></el-table-column>
        <el-table-column prop="FZRQ" label="颁发日期" align="center"></el-table-column>
        <el-table-column prop="ZZBH" label="证书编号" align="center"></el-table-column>
        <el-table-column prop="ZZZT" label="状态" align="center"></el-table-column>
        <el-table-column label="操作" align="center">
          <template slot-scope="scope">
            <el-button
              @click="download(scope.row)"
              title="查看"
              type="text"
              icon="el-icon-download"
            >下载</el-button>
             <el-button :disabled="scope.row.ZZZT=='已作废'"
              @click="CancelLiense(scope.row)"
              title="证照作废" size="small"
              type="text" 
              icon="el-icon-document-delete">证照作废</el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        background
        layout="total,prev, pager, next, jumper"
        class="cus-pagination"
        @current-change="currentChange" :current-page.sync="form.PAGE"
        :page-size="form.LIMIT"
        :total="total"
      ></el-pagination>
    </div>
  </div>
</template>
<script>
import { apiUrl } from "@/public/apiUrl";
export default {
  name: "ElectronicLicense",
  components: {},
  data() {
    return {
      loading: false,
	    // time: "",
	    total:0,
      XQZZ: [],
      form: {
				ZSBH:"",
				XMMC:"",
        ZZMC: "",
        CZZT: "",
        ZZZT: "",
        KSSJ: "",
        JSSJ: "",
        LIMIT: 10,
        PAGE: 1,
      },
      data: [],
    };
  },
  created() {
    this.getCatalogDict();
    this.getLicenseList();
  },
  mounted() {},
  computed: {},
  watch: {},
  methods: {
    doSearch: function () {
      this.form.PAGE = 1;
      this.getLicenseList();
    },
    getCatalogDict: function () {
      this.XQZZ = [];
      this.$http.get(apiUrl.GET_CATALOG_INFO).then((res) => {
        this.XQZZ = res.data.data;
      });
    },
    getLicenseList: function () {
      this.data = [];
      this.$http
        .get(apiUrl.GET_LICENSEINFO_LIST, { params: this.form })
        .then((res) => {
		  this.data = res.data.data;
		  this.total=res.data.count
        });
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
            this.getLicenseList();
          }
           this.$message({
             type:res.data.succ=='0'?'success':'warning',
             message:res.data.msg
           })
        });
        }).catch(() => {
               
        });
    },
	currentChange:function(val){
		this.form.PAGE=val;
		this.getLicenseList()
	},
    download: function (row) {
      window.open(apiUrl.DOWNLOAD_LICENSEFILE + "?ZZID=" + row.ZZID);
    },
  },
};
</script>

<style lang="scss" scoped>
.ElectronicLicense {
	min-width:1200px;
  height: 100%;
	overflow-x:auto; 
	overflow-y: hidden;
//   /deep/ .el-input__inner {
//     height: 36px;
//     line-height: 36px;
//   }
  .top-search-form {
    padding:10px 10px 0px;
		.el-form-item{
			margin-bottom:10px;
			.cus-form{
				width:180px;
			}
		}
		.demo-form-inline{
			width:1200px;
		}
    // /deep/ .form-control {
    //   .input-with-select {
    //     .el-input__inner {
    //       background: #fff;
    //       border-top: 1px solid #dcdfe6;
    //       border-bottom: 1px solid #dcdfe6;
    //     }
    //   }
    //   .el-input__icon {
    //     line-height: 34px;
    //   }
    //   .form-btn {
    //     background-color: #409eff;
    //     border-color: #409eff;
    //     color: #fff;
    //     height: 34px;
    //     margin-top: -5px;
    //     border-radius: 0px;
    //   }
    // }
  }
  .tableParent {
    // border: 1px solid #e5e5e7;
    height: calc(100% - 130px);
    padding: 15px;
    /deep/ .search-form {
      padding: 0 0px 10px;
      border-bottom: 1px dashed #cccccc;
      margin-bottom: 10px;
      .search-table {
        border: 1px solid #cccccc;
        td,
        th {
          border: 1px solid #cccccc;
          padding: 5px;
          font-size: 14px;
          font-weight: normal;
        }
        th {
          background: #f9f9f9;
        }
        .el-radio-button {
          &.is-active {
            .el-radio-button__inner {
              color: #3a8ee6;
              background: transparent;
              box-shadow: none;
            }
          }
        }
        .el-radio-button__inner {
          border: none;
          font-size: 14px;
        }
      }
      .search-btn {
        padding: 0px 15px;
        vertical-align: middle;
      }
      .dec-gsp {
        padding: 0px 10px;
      }
      .dataPicker {
        margin: 0px 10px;
      }
    }
	.cus-pagination{
		padding:10px;
		text-align: center;
	}
  }
}
</style>
