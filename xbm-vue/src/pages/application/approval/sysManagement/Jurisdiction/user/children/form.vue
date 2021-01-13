<template >
  <el-form :model="userForm" ref="userForm" label-width="80px" class="userForm">
      <el-form-item label="所在部门" prop="ur_zone">
      <el-input v-model="userForm.ur_zone" placeholder="请输入所在部门" disabled></el-input>
    </el-form-item>
    <el-form-item label="部门编号" prop="ur_node">
      <el-input v-model="userForm.ur_node"  disabled></el-input>
    </el-form-item>
    <el-form-item label="用户编号" prop="ur_ident" v-show="type!='add'">
      <el-input v-model="userForm.ur_ident" disabled></el-input>
    </el-form-item>
    <el-form-item label="用户名称" prop="ur_name"  :rules="{ required: true, message: '请输入用户名称', trigger: 'change' }">
      <el-input v-model="userForm.ur_name" placeholder="请输入用户名称" :disabled="type=='detail'"></el-input>
    </el-form-item>
    <el-form-item label="登录名称" prop="ur_login" :rules="{ required: true, message: '请输入登录名称', trigger: 'change' }">
      <el-input v-model="userForm.ur_login" placeholder="请输入登录名称" :disabled="type=='detail'"></el-input>
    </el-form-item>
   <el-form-item label="登录密码" prop="ur_crypt" v-if="type=='add'" :rules="{ required: true, message: '请输入登录密码', trigger: 'change' }">
      <el-input v-model="userForm.ur_crypt" placeholder="请输入登录密码" :disabled="type=='detail'"></el-input>
    </el-form-item>
      <!--<el-form-item label="在职状态" prop="ur_state" :rules="{ required: true, message: '请选择在职状态', trigger: 'change' }">
      <el-select v-model="userForm.ur_state" placeholder="请选择在职状态" :disabled="type=='detail'">
        <el-option label="在职" value="1"></el-option>
        <el-option label="离职" value="0"></el-option>
      </el-select>
    </el-form-item> -->
  </el-form>
</template>
<script>
export default {
  props: ["curData",'type','orgInfo'],
  data() {
    return {
      userForm: {
        ur_ident: "",
        ur_name: "",
        ur_login: "",
        ur_crypt: "",
        ur_node: "",//部门编号
        ur_zone: "",
        // ur_state:''
      },
    };
  },
  created() {
    console.log(this.orgInfo,'orgInfo');
    this.initFormData();
    // this.userForm.ur_node=this.orgInfo.OR_NAME;
    // this.userForm.ur_zone=this.orgInfo.OR_CODE;
    // this.userForm = this.type!='add'?this.curData:this.userForm;
  },
  methods: {
    initFormData:function(){
      this.userForm.ur_node=this.orgInfo.OR_CODE;
    this.userForm.ur_zone=this.orgInfo.OR_NAME;
      if(this.type!='add'){
      this.userForm={
              ur_ident: this.curData.UR_IDENT,
              ur_name: this.curData.UR_NAME,
              ur_login: this.curData.UR_LOGIN,
              // ur_crypt: this.curData.UR_CRYPT,
              ur_node: this.curData.UR_NODE,
              ur_zone: this.curData.UR_ZONE,//部门编号
              // ur_state:this.curData.UR_STATE
            }
      }
    },
    onSubmitAdd: function() {
      this.$refs["userForm"].validate(valid => {
        if (valid) {
          if(this.type=='add'){
             this.$emit("saveAddUser", this.userForm);
          }else if(this.type=='edit'){
            this.$emit("saveEditUser", this.userForm);
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