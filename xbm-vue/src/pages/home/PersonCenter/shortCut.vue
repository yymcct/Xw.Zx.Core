<template>
	<div class="shortCut grid-inner-content">
		<div class="panel-header">常用功能
		</div>
		<div class="panel-body">
			<el-carousel indicator-position="outside" :autoplay="false" class="cus-carousel" :class="menuData.length<2?'isHiddenArrow':''">
				<el-carousel-item v-for="(item,num) in menuData" :key="num" >
				<div class="drag-item" v-for="(ele,idx) in item.list" :key="idx" :index="idx" @click="addTab(ele)">
					<a class="menu" :index="idx">
						<icon :icon="ele.icon"></icon>
						<span> {{ele.name}}</span>
					</a>
				</div>
				</el-carousel-item>
			</el-carousel>
		</div>
	</div>
</template>

<script>
	import * as dataService from "@/public/apiService/home.js";
	import {mapMutations} from "vuex";
	import icon from '@/components/CusIcon';
	import  {openDGHYApplication} from "@/public/utils";
	export default {
		name: "Home",
		data: function() {
			return {
				menuData: [
					{
					screen:1,
					list:[{
						icon:'cus-icon-receipt',
						name:'收文管理',
						path:'/jz/XBM_Service.bsp?EXEC&Source=FORM[246].[4]&token=',
						type:1
						},{
						type:1,
						icon:'cus-icon-dispatch',
						name:'行政发文',
						path:'/jz/XBM_Service.bsp?EXEC&Source=FORM[243].[5]&token='
						},{
						type:2,
						icon:'cus-icon-noticeApprove',
						name:'行政审批',
						// path:'/approval',
						path:'/jz/XBM_Service.bsp?EXEC&Source=FORM[268].[51]&token='
						},{
						type:3,
						icon:'cus-icon-portal',
						name:'窗口受理',
                        path:'/cksl/UnifiedAcceptance'
						},{
						type:1,
						icon:'cus-icon-fileManage',
						name:'电子证照',
						path:'/manage/ElectronicLicense'
						},{
						type:4,
						icon:'cus-icon-dailyRecord',
						name:'多规合一',
						path:''
						}]
					}
						],
                 // 常用功能加上收发文。
			};
		},
		computed: {
		},
		created: function() {	
		
		},
		methods: {
			addTab: function(ele) {
				if(ele.type==1||ele.type==2){
					this.checkPath(ele.type,ele);
					return
					}
					if(ele.type==4){
						openDGHYApplication()
						return
					}
				this.$router.push(ele.path);
			},
			checkPath:function(type,ele){
				let sysPath=type==1?'/manage':'/approval';
				let storeMethod=type==1?'manageMenuDefault':'changeMenuDefault';
                if (ele.path.indexOf("FORM") == -1) {
								this.$router.push(ele.path);
						}else{
							this.$router.push({ path: sysPath });
						}
				this.$store.commit(storeMethod, {BA_PATH: ele.path,Ba_Name: ele.name});
			}
		},
		components: {
			icon
		}
	};
</script>
<style lang="scss" scoped>
	@import "~@/assets/scss/iconImg";
	 .shortCut {
		 .panel-body{
			 padding:5px!important;
		 }
		/deep/ .cus-carousel{
			  height:calc(100% + 5px);
					.el-carousel__container{
					height:calc(100% - 31px);
					}
		     }
			/deep/ .isHiddenArrow{
               .el-carousel__arrow,.el-carousel__indicators{
				   display:none
			   }
			 }
		     .drag-item {
					width: 25%;
					// padding-right: 10px;
					// padding-bottom: 20px;
					float: left;
					cursor: pointer;
					position: relative;
					background: #fff;
					transition: all 1s ease;

					&:hover {
						transform: scale3d(0.9, 0.9, 0.9);
					}

					.msg-tips {
						position: absolute;
						right: 10px;
						top: 0px;
						z-index: 999;
						background: #f56c6c;
						color: #fff;
						border-radius: 14px;
						display: inline-block;
						padding: 0px 10px;
						line-height: 28px;
						height: 28px;
					}

					.menu {
						display: inline-block;
						height: 88px;
						text-align: center;
						position: relative;
						width: calc(100% - 10px);
						>span {
							width: 100%;
							position: absolute;
							left: 0px;
							bottom: 0px;
							color: #666;
							font-size: 14px;
						}
					}
				}
		.swiper-slide {
			width: 100% !important;
		}

		.swiper-container,
		.swiper-wrapper {
			height: 100%;
		}

		.h-menu-circle {
			margin: 0 auto;
			border-radius: 5px;
			text-align: center;
			float: right;

			.swiper-pagination {
				display: inline-block;
				position: inherit !important;
				vertical-align: bottom;
			}

			// position: relative;
			.h-circle {
				display: inline-block;
				width: 15px;
				height: 15px;
				border-radius: 50%;
				// background:blue;
				margin-right: 10px;
			}

			// .h-menu-edit{
			//   position:absolute;
			// }
		}

		// .h-content-box {
		//   width: 100%;
		//   height: calc(100% - 60px);
		//   position: relative;
		//   overflow-y: auto;
		//   overflow-x: hidden;
		.h-screen {
			width: 100%;
			height: 100%;

			// position: absolute;
			// transition: all 0.5s ease-out;
			.draggable-box {
				width: 100%;
				margin: 0 auto;
				height: 100%;
				text-align: center;
				overflow: auto;

				>span {
					&:nth-child(1) {
						display: inline-block;
						width: 100%;
					}
				}

				
			}
		}

		// }

		.list-complete-item {
			width: 100%;
			transition: all 1s;
			// display:none;
			// display: inline-block;
			// margin-right: 10px;
		}
	}

	/* 可以设置不同的进入和离开动画 */
	/* 设置持续时间和动画函数 */
	.slide-fade-enter-active {
		transition: all 0.3s ease;
	}

	.slide-fade-leave-active {
		transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
	}

	.slide-fade-enter,
	.slide-fade-leave-to

	/* .slide-fade-leave-active for below version 2.1.8 */
		{
		transform: translateX(-80%);
		// transform: translateX(10px);
		opacity: 0;
	}
</style>
