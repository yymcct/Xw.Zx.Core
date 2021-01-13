<template >
  <el-form :model="unitForm" ref="unitForm" label-width="80px" class="Form">
      <el-form-item label="单位名称" prop="or_name"  :rules="{ required: true, message: '请输入用户名称', trigger: 'change' }">
      <el-input v-model="unitForm.or_name" placeholder="请输入用户名称"></el-input>
    </el-form-item>
    <el-form-item label="备注" prop="or_remark">
      <el-input v-model="unitForm.or_remark"></el-input>
    </el-form-item>
  </el-form>
</template>
<script>
export default {
  props: ["curData",'type'],
  data() {
    return {
      unitForm: {
        // OR_CODE: "",
          or_name: "",
          or_remark:""
      }
    };
  },
  created() {
   // this.unitForm = this.type!='add'?this.curData:this.unitForm;
		
		if(this.type=='add'){
			this.unitForm=this.unitForm
		}else{
			this.unitForm.or_name=this.curData.OR_NAME
			this.unitForm.or_remark=this.curData.OR_REMARK
		}
  },
  methods: {
    onSubmitAdd: function() {
      this.$refs["unitForm"].validate(valid => {
        if (valid) {          					
					if(this.type=='add'){
					  this.$emit("saveAddUnit", this.unitForm);
					 }else if(this.type=='edit'){
						 this.unitForm.or_code=this.curData.OR_CODE
					    this.$emit("saveEditUnit", this.unitForm);
					 }
					
        } else {
          return false;
        }
      });
    },
    resetForm() {
      this.$refs["unitForm"].resetFields();
    }
  }
};
</script>
<style lang="scss" scoped>
.Form {
  .code-box{
    display:flex;
    .code-input{
      flex:1;
      width:100%;
    }
    .code-btn{
     width:50px;
     margin: 0px 10px;
    }
  }
  // height: 100%;
}
</style>