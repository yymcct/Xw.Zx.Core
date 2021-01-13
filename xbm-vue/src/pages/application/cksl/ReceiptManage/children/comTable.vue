<template>
  <div class="approval-right">
    <div class="header">
      <div class="search">
      <el-form ref="form" :model="searchForm" class="cus-form">
        <el-input placeholder="请输入内容" v-model="searchForm[cxtj]" class="input-with-select" clearable>
          <el-select v-model="cxtj" slot="prepend" placeholder="请选择" style="width:120px">
            <el-option label="申报号" value="PROJID"></el-option>
						<el-option label="项目名称" value="PROJECTNAME"></el-option>
						<el-option label="事项类型" value="SERVICENAME"></el-option>
          </el-select>
          <el-button slot="append" icon="el-icon-search" class="search-btn" type="primary" @click="onSearch" size="small">查询</el-button>
        </el-input>
        </el-form>
      </div>
    </div>
    <div class="table-box">
      <el-table
        ref="multipleTable1"
        :data="tableData"
        tooltip-effect="dark"
        :row-style="{height:'38px'}" border
        :cell-style="{padding:'0px'}" highlight-current-row
        header-row-class-name="tableHead"
        @current-change="handleCurrentChange"
        style="width: 100%;height:100%" height="calc(100% - 50px)"
         v-loading="loading" element-loading-text="拼命加载中"
    element-loading-spinner="el-icon-loading">
         <el-table-column label="序号" type="index" width="70" align="center">
        </el-table-column>
        <el-table-column label="项目名称" prop="PROJECTNAME" show-overflow-tooltip>
        </el-table-column>
        <el-table-column prop="PROJID" label="申报号" width="120"></el-table-column>
        <el-table-column prop="APPLYNAME" label="申请对象" show-overflow-tooltip></el-table-column>
        <el-table-column prop="SERVICENAME" label="事项类型" show-overflow-tooltip></el-table-column>
        <el-table-column prop="APPLYFROM" label="申报来源" show-overflow-tooltip>
            <template slot-scope="scope">
                 <div class="cell el-tooltip state-text" >{{SBLYRela[scope.row.APPLYFROM]}}</div>
            </template>
        </el-table-column>
        <!-- <el-table-column prop="from" label="办件类型" show-overflow-tooltip></el-table-column> -->
        <el-table-column prop="RECEIVETIME" label="申报时间" show-overflow-tooltip></el-table-column>
         <el-table-column label="项目状态" min-width="105" show-overflow-tooltip align="left">
           <template slot-scope="scope">
                 <div class="cell el-tooltip state-text" >{{stateRela[scope.row.state]}}</div>
            </template>
        </el-table-column>
        <el-table-column fixed="right" label="操作" width="100">
         <template slot-scope="scope">
            <!-- <el-button @click="HandleDetail(scope.row)" type="primary" size="mini">详情</el-button> -->
            <el-button @click="HandleDetail(scope.row)" type="primary" size="mini">{{scope.row.STATE=='0'?'办理':'详情'}}</el-button>
            <!-- <el-dialog title="详情" :visible.sync="dialogFormVisible" append-to-body>
              <BaseInfo></BaseInfo>
            </el-dialog> -->
          </template>
        </el-table-column>
      </el-table>
      <Pagination
        :total="total"
        :pageSize="10"
        @handleSizeChangeSub="handleSizeChangeFun"
        @handleCurrentChangeSub="handleCurrentChangeFun"
      ></Pagination>
    </div>
    <transition name="el-zoom-in-center">
			<div class="create-pro transition-box" v-if="dialogFormVisible">
				<vForm :detail="curData" type="detail" tabName="受理" @close="dialogFormVisible=false" @updateState="updateState" @giveBack="giveBack" @onrefres="getTableData" ref="vForm" v-if="dialogFormVisible"></vForm>
			</div>
		</transition>
  </div>
</template>

<script>
import Pagination from "@/components/pagination";
// import BaseInfo from "@/pages/application/cksl/ReceiptManage/BaseInfo";
 import {apiUrl} from '@/public/apiUrl';
 import vForm from '../form/form_index';
  import { SBLYRela,ckclState } from "@/public/constant/constant.js";
//  import vForm from './form/form_index';
 import _ from 'lodash'
export default {
  name: "right",
  components: { Pagination,vForm},
  props:['tabName'],
  data() {
    return {
      loading:false,
      cxtj:'PROJECTNAME',
      curSelId:'',
      searchForm: {
        PROJID:'',//办件标识
				SERVICENAME:'',//审批事项名称
        PROJECTNAME:'',//申请项目的具体名称
        APPLYFROM:'',//办件来源0 1 2
        TRANSACT:'',//获取已办理
				STATE:'',
				start: 1,
				count: 10,
			},
      tableData: [],
      stateRela:ckclState,
      SBLYRela:SBLYRela,
      curData: null,
			isContainPlan:'',
      dialogFormVisible: false,
      total:0
    };
  },
  created:function(){
    this.getTableData();
  },
  methods: {
    getTableData:function(){
        this.loading=true;
        let _this=this;
        let p=this.searchForm;
      let APPLYFROM=this.tabName=='WorkTable'?'0':'1';
      let baseUrl=apiUrl.Get_Acceptance_List;
       if(this.tabName=='BackBusiness'){
          p.STATE=2
          // return baseUrl+'&STATE='+p.state+'&APPLYFROM=&TRANSACT=0'
        }else if(this.tabName=='DoneBusiness'){
          //  p.STATE=1;
            p.TRANSACT=0;
        }else if(this.tabName=='WorkTable'){
            p.STATE=0;
          p.APPLYFROM=0
        }else if(this.tabName=='powerOperation'){
             p.STATE=0;
             p.APPLYFROM=1
        }
      this.$http.get(baseUrl,{params:p})
			.then(res=>{
					this.FormatJsonData(res.data.data);
          this.total=res.data.sum;
           this.loading=false;    
			})
    },
    // onSwitchCaseUrl:function(tabName,baseUrl){
    //     if(this.tabName=='BackBusiness'){
    //       this.TRANSACT
    //       // return baseUrl+'&STATE='+p.state+'&APPLYFROM=&TRANSACT=0'
    //     }else if(this.tabName=='DoneBusiness'){
    //       return baseUrl+'&STATE='+p.state+'&APPLYFROM=&TRANSACT=0'
    //     }
    // },
    FormatJsonData:function(array){
        array.map(item=>{
						// item['xmlx']=item['APPLY_PROPERTIY'];
						item['lxlx']=item['BUS_TYPE'];
						item['bjlx']=item['INFOTYPE'];
						item['sldw']=item['DEPTNAME'];
						item['sxmc']=item['SERVICENAME'];
						item['jsdwmc']=item['APPLYNAME'];
						item['jsdwzjlx']=item['APPLY_CARDTYPE'];
						item['jsdwzjhm']=item['APPLY_CARDNUMBER'];
						item['frdb']=item['LEGALMAN'];
						item['wtdlr']=item['CONTACTMAN'];
						item['wtrdlrzjh']=item['CONTACTMAN_CARDNUMBER'];
            item['wtdlrdh']=item['TELPHONE'];
            // if(this.tabName!='UnifiedAcceptance'){
            //   item['state']='1'
            // }
						item['xmzt']=item['state'];
					 var arr=Object.keys(item);
							arr.forEach(k=>{
								item[k.toLowerCase()]=item[k];
							})
				})
          this.tableData=array;
    },
    giveBack:function(STATE,PROJID){
	    this.$http({
					url:apiUrl.Update_Acceptance_State+'?STATE='+STATE+'&PROJID='+PROJID,
					method: 'get',
				})
				.then(respanse=>{
						this.DialogShow = false;
						this.$message.success('操作成功!');
						this.getTableData()
				})
		},
		
		updateState:function(STATE,PROJID){
			let data={STATE:STATE,PROJID:PROJID};
	    this.$http({
					url:apiUrl.Update_Acceptance_State+'?STATE='+STATE+'&PROJID='+PROJID,
					method: 'get',
					// data: data,
				})
				.then(respanse=>{
						this.$message.success('操作成功!')
				})
    },
    onSearch: function() {
			this.searchForm.start = 1;
			this.getTableData()
		},
    handleCurrentChange:function(val){
      this.curSelId=val&&val.PROJID;
    },
    handleSizeChangeFun(v) {
      this.searchForm.searchForm = v;
       this.getTableData();
    },
    handleCurrentChangeFun(v) {
      //页面点击
      this.searchForm.start = v; //当前页
      this.getTableData();
    },
    HandleDetail(item) {
      // console.log(item,'item');
      this.curData=_.clone(item);
      // if(this.tabName!='WorkTable'){
      //       this.curData.xmzt='1';
      // }
        this.curData.isContainPlan='20601';
      this.dialogFormVisible = true;
    },
   
  }
};
</script>

<style lang="scss" scoped>
.approval-right {
  position:relative;
  width: 100%;
  height:100%;
  border-top: 3px solid #07438b;
  box-shadow: 0px 12px 8px -12px rgba(0, 0, 0, 0.15);
  // padding: 0px 54px 20px;
  background: #fff;
  .table-box{
    height:calc(100% - 90px)
  }
  	.create-pro {
		position: absolute;
		left: 0px;
		top: 0px;
		width: calc(100% - 0px);
		height: 100%;
		background: #fff;
		border: 1px solid #dcdfe6;
		box-shadow: 0 2px 4px 0 rgba(0, 0, 0, 0.12), 0 0 6px 0 rgba(0, 0, 0, 0.04);
		z-index: 9;
	}
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
          margin-bottom: -7px;
          // padding-left: 10px;
      }
    }
  }
}
.el-form-item {
  margin-bottom: 0px !important;
}
.table-box {
  height: calc(100% - 86px);
  // height: 320px;
}

.table-button {
  width: 50px;
  height: 24px;
  background: rgba(237, 236, 236, 1);
  border-radius: 3px;
  color: #4c4948;
}
.el-table {
  overflow-y: scroll;
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