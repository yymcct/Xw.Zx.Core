<template>
	<el-form :model="parentForm" ref="parentForm" label-width="150px" class="parentForm" v-if="parentType">
		<el-form-item label="父节点名称" prop="nodename" :rules="{ required: true, message: '请输入菜单名称', trigger: 'blur' }">
			<el-input v-model="parentForm.nodename" placeholder="请输入父节点名称"></el-input>
		</el-form-item>
		<el-form-item label="父节点ID" prop="nodeid">
			<el-input v-model="parentForm.nodeid" disabled></el-input>
		</el-form-item>
		<el-form-item>
			<el-button type="primary" @click="onSubmitEdit">确定</el-button>
			<!-- <el-button @click="resetForm('ruleForm')">取消</el-button> -->
		</el-form-item>
	</el-form>

	<el-form :model="ruleForm" ref="ruleForm" label-width="150px" class="TreeForm" v-else>
		<el-form-item label="父节点名称">
			<el-input v-model="parentNodeName" disabled></el-input>
		</el-form-item>
		<el-form-item label="子节点名称" prop="nodename" :rules="{ required: true, message: '请输入菜单名称', trigger: 'blur' }">
			<el-input v-model="ruleForm.nodename" placeholder="请输入子节点名称"></el-input>
		</el-form-item>
		<el-form-item label="子节点id" prop="nodeid" :rules="{required: true, message: '请输入菜单路径', trigger: 'blur'}">
			<el-input v-model="ruleForm.nodeid" disabled></el-input>
		</el-form-item>
		<el-form-item>
			<el-button type="primary" @click="onSubmitEdit">确定</el-button>
		</el-form-item>
	</el-form>
</template>
<script>
	export default {
		props: ["curNodeData", "parentType"],
		data() {
			return {
				ruleForm: {
					parentid:'',
					nodename:'',
					nodeid:''
				},
				parentForm: {
					parentid:'1',
					nodename:'',
					nodeid:''
				},
				parentNodeName: ''
			};
		},
		created() {
			this.initData(this.curNodeData)
		},
		watch: {
			'curNodeData': function(val) {
				this.initData(val)
			}
		},
		methods: {
			initData: function(data) {
				//  let data = this.curNodeData;
				if (this.parentType) {
					this.parentForm = {
						parentid:'1',
						nodename:data.NODENAME,
						nodeid:data.NODEID
					};
				} else {
					this.parentNodeName = data.nodePaent;
					// console.log(data.BA_DATA.split('"')[1],'000');
					this.ruleForm = {
						parentid:data.nodePaentId,
						nodename:data.NODENAME,
						nodeid:data.NODEID
					};
				}
			},
			onSubmitEdit: function() {
				let data = this.parentType ? "parentForm" : "ruleForm";
				this.$refs[data].validate(valid => {
					if (valid) {
						if (this.parentType) {
							this.$emit("updataLevel1Node", this.parentForm);
						} else {
							// this.ruleForm.ba_data = '["' + this.ruleForm.ba_data + '"]';
							this.$emit("updataLevel1Node", this.ruleForm);
						}
					} else {
						return false;
					}
				});
			},
			resetForm() {
				this.$refs["ruleForm"].resetFields();
			}
		}
	};
</script>
<style lang="scss" scoped>
	.TreeForm {
		// height: 100%;
	}
</style>
