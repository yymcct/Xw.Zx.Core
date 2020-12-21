<template >
 <el-row :gutter="10">
      <el-form :inline="true" :model="searchForm" class="search-form">
      <el-col :span="6">
         <el-form-item label="用户名称">
          <el-input v-model="searchForm.UR_NAME" placeholder="用户名称"></el-input>
        </el-form-item>
      </el-col>
       <el-col :span="6">
        <el-form-item label="登录名称">
          <el-input v-model="searchForm.UR_LOGIN" placeholder="登录名称"></el-input>
        </el-form-item>
        </el-col>
       <el-col :span="6">
        <el-form-item label="在职状态">
          <el-select v-model="searchForm.UR_STATE" placeholder="在职状态">
            <el-option label="在职" value="1"></el-option>
            <el-option label="离职" value="0"></el-option>
          </el-select>
        </el-form-item>
         </el-col>
         <el-col :span="6">
        <el-form-item label="手机号码">
          <el-input v-model="searchForm.UR_PHONE" placeholder="手机号码"></el-input>
        </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-button type="primary" size="medium" @click="onSubmit">查询</el-button>
          <el-button type="primary" size="medium"  @click="addUser">新增</el-button>
         </el-col>
      </el-form>
     
      </el-row>
</template>
<script>
export default {
  data() {
    return {
       searchForm: {
        UR_NAME: "",
        UR_LOGIN: "",
        UR_STATE: '1',
        UR_PHONE: ""
      },
    };
  },
  created() {
  },
  methods: {
    onSubmit: function() {
      this.$emit('getData')
    },
    addUser: function() {
    this.$emit('addUser');
    },
    resetForm() {
      this.$refs["ruleForm"].resetFields();
    }
  }
};
</script>
<style lang="scss">
.search-form{
  width:95%;
  margin:0 auto;
  .el-form-item{
    margin-bottom:10px;
     .el-form-item__content{
      width: calc(100% - 68px);
    }
  }
}
</style>