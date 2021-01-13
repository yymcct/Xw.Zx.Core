<template >

  <el-form
    :model="userForm"
    ref="userForm"
    label-width="80px"
    class="userForm" v-loading="loading" element-loading-text="正在加载中"
  >
    <el-form-item
      label="所在部门"
      prop="ur_zone"
      :rules="{ required: true, message: '请输入所在部门', trigger: 'change' }"
    >
      <el-cascader
        style="width: 100%;"
        @change="changeZone"
        :options="options"
        v-model="value1"
        :props="props" res="cascader"
        placeholder="请输入所在部门"  
         :show-all-levels="false"
      >
      </el-cascader>

    </el-form-item>
    <el-form-item
      label="用户编号"
      prop="ur_ident"
      v-show="type!='add'"
    >
      <el-input
        v-model="userForm.ur_ident"
        disabled
      ></el-input>
    </el-form-item>
    <el-form-item
      label="用户名称"
      prop="ur_name"
      :rules="{ required: true, message: '请输入用户名称', trigger: 'change' }"
    >
      <el-input
        v-model="userForm.ur_name"
        placeholder="请输入用户名称"
        :disabled="type=='detail'"
      ></el-input>
    </el-form-item>
    <el-form-item
      label="登录名称"
      prop="ur_login"
      :rules="{ required: true, message: '请输入登录名称', trigger: 'change' }"
    >
      <el-input
        v-model="userForm.ur_login"
        placeholder="请输入登录名称"
        :disabled="type=='detail'"
      ></el-input>
    </el-form-item>
         <!-- v-if="type=='add'" -->
    <el-form-item
      label="登录密码"
      prop="ur_crypt"
      :rules="{ required: true, message: '请输入登录密码', trigger: 'change' }">
      <el-input
        v-model="userForm.ur_crypt"
        placeholder="请输入登录密码"
        :disabled="type=='add'?false:true"></el-input>
    </el-form-item>
  </el-form>
</template>
<script>
import * as dataService from "@/public/apiService/sysManagement/Organization";
import {forMateData} from "@/public/utils";
export default {
  props: ["curData", 'type', 'orgInfo'],
  data () {
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
      page: 1,
      options: [],
      dataChange: [],
      props: {
        label: 'OR_NAME',
        value: 'OR_CODE',
        // expandTrigger: 'hover'
      },
      value1: [],
      loading:false
    };
  },
  created () {
    this.initFormData();
  },
  mounted () {
    this.getData()
  },
  methods: {
    initFormData: function () {
      if(this.orgInfo.OR_CODE!='410300'){
         this.userForm.ur_node = this.orgInfo.OR_CODE;
          this.userForm.ur_zone = this.orgInfo.OR_NAME;
      }
      if (this.type != 'add') {
        this.userForm = {
          ur_ident: this.curData.UR_IDENT,
          ur_name: this.curData.UR_NAME,
          ur_login: this.curData.UR_LOGIN,
          ur_crypt: this.curData.UR_CRYPT,
          ur_node: this.curData.UR_NODE,
          ur_zone: this.curData.UR_ZONE,//部门名称
        }
      }
         var arr=[this.userForm.ur_node];
      // data.push(this.userForm.ur_node)
      if(this.orgInfo.parentCode){
			arr=[this.orgInfo.parentCode,this.userForm.ur_node]
      }
      this.value1=arr;
    },
    changeZone (data) {
      for (var i = 0; i < this.options.length; i++) {
        if (data.length == 1 && data == this.options[i].OR_CODE) {
          this.userForm.ur_node = data[0]
          this.userForm.ur_zone = this.options[i].OR_NAME
        } else if (data.length == 2) {
          this.userForm.ur_node = data[1]
          this.options.map((item) => {
            if (item.children) {
              item.children.map((item) => {
                if (data[1] == item.OR_CODE) {
                  this.userForm.ur_zone = item.OR_NAME
                }
              })
            }
          })
        }
      }
    },
    handleClose (done) {
      this.$confirm('确认关闭？')
        .then(_ => {
          done();
        })
        .catch(_ => { });
    },
    getData: function (orgCode) {
      if(this.type!=='add'){this.loading = true;}
      dataService.getOrgTree().then(res => {
        // var data = [];
        // res.DATA[0].children.map((item) => {
        //   if (item.children.length == 0) {
        //     data.push({ "OR_CODE": item.OR_CODE, "OR_NAME": item.OR_NAME })
        //   } else {
        //     data.push(item)
        //   }
        // })
        this.options =  forMateData(res.data,'OR_UPER','OR_CODE');
        // this.options = data;
         if(this.type!=='add'){this.loading = false;}
      })
    },



    onSubmitAdd: function () {
      // console.log(this.$refs["userForm"])
      this.$refs["userForm"].validate(valid => {
        if (valid) {
          if (this.type == 'add') {
            this.$emit("saveAddUser", this.userForm);
          } else if (this.type == 'edit') {
            this.$emit("saveEditUser", this.userForm);
          }
        } else {
          return false;
        }
      });
    },
    resetForm () {
      this.$refs["ruleForm"].resetFields();
    }
  }
};
</script>
<style lang="scss" scoped>
.TreeForm {
  // height: 100%;
  .el-cascader-menu__wrap {
    width: calc(100%-10px);
    height: calc(100%-10px);
    margin: 0;
  }
  .el-scrollbar__wrap {
    overflow-x: hidden;
  }
}
</style>
