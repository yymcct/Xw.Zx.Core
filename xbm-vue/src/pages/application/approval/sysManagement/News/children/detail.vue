<template>
  <div class="laws-detail">
		<div class="breadcrumb-box">
		<el-breadcrumb separator="/">
		<el-breadcrumb-item :to="{ path: '/approval/Laws' }">首页</el-breadcrumb-item>
		<el-breadcrumb-item :to="{ path: '/Laws' }" v-if="$route.query.flag!='manage'">新闻中心</el-breadcrumb-item>
		<el-breadcrumb-item>详情</el-breadcrumb-item>
</el-breadcrumb>
</div>
 <div class="detail-content" v-loading="loading">
	 <iframe width="100%"  height="100%" src="../../../../../jz/static/template.html" id="iframe" frameborder="0"></iframe>
<!--	<div class="top-title">{{detail.WJ_NAME}}</div>
	<div class="information">上传时间：{{detail.SCSJ}}&nbsp;&nbsp;&nbsp;类型：{{$route.query.type}}&nbsp;&nbsp;&nbsp;创建人：{{detail['UR_IDENT']}}
</div> -->
<!-- <editor :defauleMsg="detail.WJ_NR" class="cont" v-if="detail.WJ_NR"></editor> -->
   <!-- <div class="cont" v-html="detail.WJ_NR"></div> -->
	</div>
</div>
</template>
<script>
import * as dataService from "@/public/apiService/home";
export default {
  data: function() {
    return {
	   detail:{},
	   loading:false
	};
  },
  computed: {},
  created() {
	  this.getDetail();
	},
	watch:{
		'$route' (to, from) {
          console.log(this.$route.query,'sssss')
		}
	},
  mounted() {},
  methods: {
   getDetail:function(){
	   this.loading=true;
	   	  dataService.checkLaws(this.$route.query.wiid).then(res => {
				 this.loading=false;
				 this.detail=res;
				 this.detail.WJ_NR='';
				 res.data.forEach(item=>{
					//  this.detail.WJ_NR+=item.WJ_NR;
					 this.detail.WJ_NR+=item.WJ_NR;
					//  this.detail.WJ_NR+=this.Base64.decode(item.WJ_NR);
				 })
				 $("#iframe").contents().find("#content").html(this.detail.WJ_NR)
				//  console.log($("#iframe").contents().find("#content"),'0000000');
				//  console.log(res,'res==');
				})
   }
  },
  components: {
	}
};
</script>

<style lang="scss" scoped>
.laws-detail{
	  height:100%;

		background:#fafbff;
		.breadcrumb-box{
			border: 1px solid #DCDFE6;
			padding: 10px;
			background-color: #f5f7fa;
		}
		.detail-content{
			// max-width:1200px;
			margin:0px auto;
			height:calc(100% - 36px);
			background:#fff;
			// padding:30px 50px;
			// overflow: auto;
			.top-title{
				font-size: 20px;
				text-align: center;
				padding: 10px;
			}
			.information{
			text-align: center;
    border-top: 1px solid #bd0000;
    font-size: 14px;
    color: #666;
    margin-bottom: 15px;
    padding-top: 5px;
			}
		}
		// td, th {
		// 	border: 1px solid #DDD;
		// }
	}
</style>
