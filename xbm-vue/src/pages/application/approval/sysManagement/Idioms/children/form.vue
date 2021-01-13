<template>
	<div class="addressFormBox">
		<h5 class="title">惯用语编辑</h5>
		<p class="seq-dec" v-if="type!=='add'">
			<span>
				编号:
				<a class="seq-code">{{addressForm.epid}}</a>
			</span>
		</p>
		<el-form :model="addressForm" ref="addressForm" label-width="100px" class="addressForm">
			<el-row :gutter="10">
				
				
				
				
				
				
				
			</el-row>
		</el-form>

	</div>
</template>
<script>
	import * as dataService from "@/public/apiService/PersonalAffairs/address";
	export default {
		props: ["curData", "type"],
		data() {
			return {
				
			};
		},
		created() {
			
		},
		watch: {
			
		},
		methods: {
			
		}
	};
</script>
<style lang="scss" scoped>
	.addressFormBox {

		// height: 100%;
		.title {
			font-weight: 400;
			color: #1f2f3d;
			font-size: 28px;
			text-align: center;
			margin-top: -10px;
			margin-bottom: 10px;
		}

		.seq-dec {
			width: 100%;
			text-align: right;

			.seq-code {
				text-decoration: underline;
				padding: 0px 10px 10px;
				margin-top: -10px;
				display: inline-block;
				color: #F44336;
			}
		}

		.addressForm {
			border: 1px solid #dbd6d6;
			padding: 10px;

			.photo-text {
				text-align: center;
				padding: 10px;
				font-size: 16px;
			}

			.avatar-uploader-icon {
				font-size: 28px;
				color: #8c939d;
				width: 100%;
				min-height: 100%;
				line-height: 140px;
				text-align: center;
			}

			.avatar {
				width: 100%;
				height: 100%;
				display: block;
			}
		}

	}
</style>
