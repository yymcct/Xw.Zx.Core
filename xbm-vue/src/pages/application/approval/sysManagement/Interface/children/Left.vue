<template>
	<div class="interfaceTree">
		<el-tree v-loading="loading" element-loading-text="加载数据中..."  :data="props1" :props="defaultProps" accordion @node-click="handleNodeClick">
		</el-tree>
	</div>
</template>

<script>
	import * as dataService from "@/public/apiService/sysManagement/interface";
	import Bus from "@/public/event";
	export default {
		name: "interfaceLeft",
		components: {

		},
		data() {
			return {
				dataList: [],
				loading: true,
				props1: [],
				defaultProps: {
					children: 'children',
					label: 'label'
				}
			};
		},
		created() {},
		mounted() {
			this.getDataList()
		},
		computed: {

		},
		methods: {
			handleNodeClick(data) {
				console.log(data);
				
				if(data.id0){
					this.getPosition(data)
					var id0=this.getPosition(data).split(',')[0]
					var id1=this.getPosition(data).split(',')[1]
					this.getDataListChild(data.id0,data.id1).then((res)=>{
						console.log(res)
						
						if(res[0].DATA.length){
							for(var i=0;i<res[0].DATA.length;i++){
								var dataChild2 = {
									label: res[0].DATA[i].BX_NAME,
									BX_BIZID: res[0].DATA[i].BX_BIZID,
									BX_ORDER: res[0].DATA[i].BX_ORDER							
								}
								this.props1[id0].children[id1].children.push(dataChild2)
							}
						}
					})													
				}
				
				if(data.BX_BIZID){
					Bus.$emit('interface-parameter',data)
					
				}
				
				
				
			},

			//获取列表
			getDataList(page) {
				dataService.getDataList().then((res) => {
					console.log(res)
					this.loading=false;
					for (var a = 0; a < res.length; a++) {
						var dataChild = {
							label: '测试数据',
							id: '',
							children: [],
						}
						for (var b = 0; b < res[a].children.length; b++) {
							var dataChild1 = {
								label: '测试数据',
								id0: '',
								id1: '',
								children: [],
							}
							dataChild1.label = res[a].children[b].BU_NAME
							dataChild1.id0 = res[a].BZ_IDENT
							dataChild1.id1 = res[a].children[b].BU_IDENT
							dataChild.children.push(dataChild1)
						}
						dataChild.label = res[a].BZ_NAME
						dataChild.id = res[a].BZ_IDENT
						this.props1.push(dataChild)
					}
				}).catch((err) => {
					console.log(err)
				})
			},
			//获取三级菜单
			getDataListChild(a,b){
				return dataService.getDataListChild(a,b)
			},
			//获取接口详情
			
			//获取数据在数组中的位置
			getPosition(data){
				for(var i=0;i<this.props1.length;i++){
					for(var j=0;j<this.props1[i].children.length;j++){
						if(this.props1[i].children[j]==data){
							console.log(i,j)
							return i+','+j
						}
					}
				}
			}


		}
	};
</script>

<style lang="scss">
	.interfaceTree {
		width: 100%;
		height: 100%;
		box-sizing: border-box;
		border-right: 1px solid #ddd;
		.el-tree{
			.el-tree-node__content{
				font-size: 16px;
				height: 30px;
				line-height: 30px;
			}
		}
	}
</style>
