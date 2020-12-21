<template>
	<div class="interfaceMagement">
		<div class="left">
			<Left></Left>
		</div>
		<div class="detail">
			<Detail></Detail>
		</div>
	</div>
</template>

<script>
	import * as dataService from "@/public/apiService/sysManagement/interface";
	import Left from "./children/Left";
	import Detail from "./children/Detail";
		export default {
			name: "interface",
			components: {
				Left,
				Detail
			},
			props: ['formInline'],
			data() {
				return {
					page: 1,
					dataList: [],
					
	
				};
			},
			created() {},
			mounted() {
				
			},
			computed: {
				
			},
			methods: {
				
			}

		};
</script>

<style lang="scss">
	.interfaceMagement{
		height: 100%;
		width: 1200px;
		margin: 0 auto;
		.left{
			width: 250px;
			height: 100%;
			overflow: auto;
			float: left
		}
		.detail{
			width:950px;
			float:right;
		}
		
	}
</style>
