<template >
  <el-form :model="departForm" ref="departForm" label-width="80px" class="departForm">
    <el-form-item label="上级部门" prop="ur_node">
      <el-input v-model="parentDep" placeholder="请输入所在部门" disabled></el-input>
    </el-form-item>
    <!-- <el-form-item label="部门编号" prop="or_code">
      <el-input v-model="departForm.or_code" disabled></el-input>
    </el-form-item> -->
    <el-form-item
      label="部门名称"
      prop="or_name"
      :rules="{ required: true, message: '请输入用户名称', trigger: 'change' }"
    >
      <el-input v-model="departForm.or_name" placeholder="请输入用户名称" :disabled="type=='detail'"></el-input>
    </el-form-item>
    <el-form-item label="备注" prop="or_remark">
      <el-input v-model="departForm.or_remark" placeholder="请输入备注" :disabled="type=='detail'"></el-input>
    </el-form-item>
  </el-form>
</template>
<script>
export default {
  props: ["curData", "type", "orgInfo"],
  data() {
    return {
      parentDep:'',
      departForm: {
        or_code: "",
        or_name: "",
        or_remark: ""
      }
    };
    
  },
  created() {
    this.initFormData();
    
    // this.departForm = this.type != "add" ? this.curData : this.departForm;
  },
  methods: {
    initFormData:function(){
      if(this.type != "add"){
        this.parentDep=this.curData.father;
        this.departForm ={
          or_code: this.curData.OR_CODE,
          or_name: this.curData.OR_NAME,
          or_remark: this.curData.OR_REMARK
        }
       
      }else{
        this.parentDep=this.orgInfo.OR_NAME;
      }
    },
    onSubmitAdd: function() {
      this.$refs["departForm"].validate(valid => {
        if (valid) {
           if(this.type=='add'){
             let param = {
                or_uper:this.orgInfo.OR_CODE,
                or_name:this.departForm.or_name,
                or_remark:this.departForm.or_remark
             }
            this.$emit("saveAddDepart", param);
            }else if(this.type=='edit'){
               this.$emit("saveEditDepart", this.departForm);
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