<template>
  <el-form
    :model="parentForm"
    ref="parentForm"
    label-width="80px"
    class="parentForm"
    v-if="parentType"
  >
    <el-form-item
      label="名称"
      prop="nodename"
      :rules="{ required: true, message: '请输入菜单名称', trigger: 'blur' }"
    >
      <el-input v-model="parentForm.nodename" placeholder="请输字典名称"></el-input>
    </el-form-item>
  </el-form>

  <el-form :model="dynamicValidateForm" ref="ruleForm" label-width="100px" class="TreeForm" v-else>
    <el-form-item label="所属父级">
      <el-input v-model="ba_data" disabled></el-input>
    </el-form-item>

    <el-form-item
      v-for="(domain, index) in dynamicValidateForm.DATA"
      :label="'子节点' + (index+1)"
      :key="domain.key"
      :prop="'DATA.' + index + '.nodename'"
      :rules="{
      required: true, message: '子节点不能为空', trigger: 'blur'
    }"
    >
      <el-input v-model="domain.nodename">
        <el-button slot="append" @click.prevent="remove(index)">删除</el-button>
      </el-input>
    </el-form-item>

    <div style="text-align: center;">
      <el-button type="primary" plain @click="addDomain">添加子节点</el-button>
    </div>
  </el-form>
</template>
<script>
export default {
  props: ["curNodeData", "parentType"],
  data() {
    return {
      parentForm: {
        nodename: ""
      },
      dynamicValidateForm: {
        DATA: [
          {
            nodename: ""
          }
        ],

        parentid: ""
      },
      ba_data: ""
    };
  },
  created() {
    let data = this.curNodeData;
    console.log(this.curNodeData);
    // console.log(data,'data==');
    if (this.parentType) {
    } else {
      this.dynamicValidateForm.parentid = data.NODEID;
      this.ba_data = data.NODENAME;
      // this.ruleForm.ba_bizid=data.BZ_BizID || 1;
    }
  },
  methods: {
    onSubmitAdd: function() {
      let data = this.parentType ? "parentForm" : "ruleForm";
      this.$refs[data].validate(valid => {
        if (valid) {
          if (this.parentType) {
            this.$emit("addLevel1Node", this.parentForm);
          } else {
            //this.ruleForm.ba_data = '["' + this.ruleForm.ba_data + '"]';
            this.$emit("addLevel2Node", this.dynamicValidateForm);
          }
        } else {
          return false;
        }
      });
    },
    resetForm() {
      this.$refs["ruleForm"].resetFields();
    },
    addChild() {
      var child = {
        nodename: ""
      };
      this.ruleForm.DATA.push(child);
    },
    remove(index) {
      this.dynamicValidateForm.DATA.splice(index, 1);
    },
    addDomain() {
      this.dynamicValidateForm.DATA.push({
        nodename: "",
        key: Date.now()
      });
    }
  }
};
</script>
<style lang="scss" scoped>
.TreeForm {
  // height: 100%;
}
</style>
