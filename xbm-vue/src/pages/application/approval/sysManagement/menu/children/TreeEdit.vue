<template>
  <el-form
    :model="parentForm"
    ref="parentForm"
    label-width="50px"
    class="parentForm"
    v-if="parentType"
  >
    <el-form-item
      label="名称"
      prop="bz_name"
      :rules="{ required: true, message: '请输入菜单名称', trigger: 'change' }"
    >
      <el-input v-model="parentForm.bz_name" placeholder="请输入菜单名称"></el-input>
    </el-form-item>
    <el-form-item label="备注" prop="bz_remark">
      <el-input v-model="parentForm.bz_remark" placeholder="请输入备注"></el-input>
    </el-form-item>
    <el-form-item label="序号" prop="ba_order">
      <el-input-number v-model="parentForm.bz_order" :min="1" label="描述文字"></el-input-number>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="onSubmitEdit">确定</el-button>
      <!-- <el-button @click="resetForm('ruleForm')">取消</el-button> -->
    </el-form-item>
  </el-form>

  <el-form :model="ruleForm" ref="ruleForm" label-width="100px" class="TreeForm" v-else>
    <el-form-item label="所属父级">
      <el-input v-model="parentNodeName" disabled></el-input>
    </el-form-item>
    <el-form-item
      label="名称"
      prop="ba_name"
      :rules="{ required: true, message: '请输入菜单名称', trigger: 'change' }"
    >
      <el-input v-model="ruleForm.ba_name" placeholder="请输入菜单名称"></el-input>
    </el-form-item>
    <el-form-item
      label="路径"
      prop="path"
      :rules="{required: true, message: '请输入菜单路径', trigger: 'change'}"
    >
      <el-input v-model="ruleForm.path" placeholder="请输入菜单路径"></el-input>
    </el-form-item>
    <el-form-item
      label="图标"
      prop="ba_icon"
      :rules="{ required: true, message: '请输入菜单图标', trigger: 'change' }"
    > 
    <el-select  v-model="ruleForm.ba_icon" clearable placeholder="可选择默认图标,或输入格式如fa-xx" filterable allow-create style="width:80%">
     <el-option-group
      v-for="group in menuList"
      :key="group.name"
      :label="group.name">
      <el-option
        v-for="item in group.children"
        :key="item.id"
        :label="item.icon+'('+item.name+')'"
        :value="item.icon">
        <div class="option-item" style="height:34px;line-height:34px;">
         <span style="float: left;display:inline-block;width:60px">
             <icon :icon="item.icon" style="font-size:24px;padding:0px;vertical-align: bottom;"></icon>
             {{ item.icon }}
           </span>
         <span style="float: right; color: #8492a6; font-size: 13px">{{ item.name }}</span>
        </div>
      </el-option>
    </el-option-group>
  </el-select>
 <el-popover placement="top-start" title="帮助说明" width="400" trigger="hover">
        <div>用户可搜索选择默认内置的图标，若查询不到或没有找到匹配的图标，可访问<a style="color:#21A0FF" href="http://fontawesome.dashgame.com/" target="_blank">http://fontawesome.dashgame.com/</a>链接，查找对应图标的名称，如图标的名称为address-book，则输入fa-address-book进行创建新图标</div>
        <el-button type="text" slot="reference">帮助说明</el-button>
      </el-popover>
      <!-- <el-input v-model="ruleForm.ba_icon" placeholder="请输入菜单图标名称,格式如cus-icon-xx"></el-input> -->
    </el-form-item>
       <el-form-item label="备注" prop="bz_remark">
      <el-input v-model="ruleForm.ba_remark" placeholder="请输入备注"></el-input>
    </el-form-item>
    <el-form-item label="序号" prop="ba_order">
      <el-input-number v-model="ruleForm.ba_order" :min="1" label="描述文字"></el-input-number>
    </el-form-item>
    <el-form-item>
      <el-button type="primary" @click="onSubmitEdit">确定</el-button>
    </el-form-item>
  </el-form>
</template>
<script>
import {menuList} from '@/public/constant/menu'
import icon from '@/components/CusIcon'
export default {
  props: ["curNodeData", "parentType"],
  data() {
    return {
      menuList:menuList,
      ruleForm: {
        ba_bizid: "",
        ba_order: "",
        // ba_owner: 1,
        ba_name: "",
        ba_data: [],
        ba_ware: "",
        ba_icon: "",
        path: "",
        ba_remark: "",
        ba_ident: ""
      },
      parentForm: {
        bz_ident: "",
        bz_name: "",
        bz_remark: "",
        bz_order: ""
      },
      parentNodeName:''
    };
  },
  created() {
   this.initData(this.curNodeData)
  },
  watch:{
    'curNodeData':function(val){
      this.initData(val)
    }
  },
  methods: {
    initData:function(data){
    //  let data = this.curNodeData;
    if (this.parentType) {
      this.parentForm = {
        bz_ident: data.BZ_IDENT,
        bz_name: data.BZ_NAME,
        bz_remark: data.BZ_REMARK || "",
        bz_order: data.BZ_ORDER
      };
    } else {
      this.parentNodeName=data.BA_DATA.split('"')[1];
      // console.log(data.BA_DATA.split('"')[1],'000');
      this.ruleForm = {
        ba_bizid: data.BA_BIZID,
        ba_ident: data.BA_IDENT,
        ba_order: data.BA_ORDER,
        ba_name: data.BA_NAME,
        ba_data: data.BA_DATA,
        ba_ware: data.BA_WARE,
        ba_icon: data.BA_ICON,
        ba_remark: "",
        path: data.BA_PATH
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
            this.$emit("updataLevel2Node", this.ruleForm);
          }
        } else {
          return false;
        }
      });
    },
    resetForm() {
      this.$refs["ruleForm"].resetFields();
    }
  },
   components: {
     icon
   }
};
</script>
<style lang="scss" scoped>
.TreeForm {
  // height: 100%;
}
</style>