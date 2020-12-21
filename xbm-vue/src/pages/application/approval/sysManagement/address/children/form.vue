<template>
  <div class="addressFormBox">
    <h5 class="title">{{title}}</h5>
    <p class="seq-dec" v-if="type!=='add'">
      <span>
        编号:
        <a class="seq-code">{{addressForm.epid}}</a>
      </span>
    </p>
    <el-form :model="addressForm" ref="addressForm" label-width="100px" class="addressForm">
      <el-row :gutter="10">
        <el-col :span="18">
          <!-- <el-col :span="12">
						<el-form-item label="编号：" prop="ep_code">
							<el-input v-model="addressForm.ep_code" placeholder="请输入编号" disabled></el-input>
						</el-form-item>
          </el-col>-->
          <el-col :span="12">
            <el-form-item label="注册时间：">
              <el-date-picker
                type="date"
                placeholder="选择日期"
                v-model="date"
                style="width: 100%;"
                disabled
              ></el-date-picker>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="姓名："
              prop="ep_name"
              :rules="{ required: true, message: '请输入姓名', trigger: 'change' }"
            >
              <el-input
                v-model="addressForm.ep_name"
                placeholder="请输入姓名"
                :disabled="type=='detail'"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <!-- prop="ep_sexy" :rules="[{ required: true, message: '请选择性别', trigger: 'change' }]"-->
            <el-form-item label="性别：">
              <el-radio-group v-model="addressForm.ep_sexy" :disabled="type=='detail'">
                <el-radio :label="1">女</el-radio>
                <el-radio :label="0">男</el-radio>
              </el-radio-group>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="部门："
              prop="ep_group"
              :rules="[{ required: true, message: '请选择部门', trigger: 'change' }]"
            >
              <div style="display:flex">
                <el-input
                  v-model="addressForm.ep_group"
                  placeholder="请选择部门"
                  :disabled="type=='detail'"
                  style="flex:1;width:100%;padding-right: 10px;"
                ></el-input>
                <el-button
                  size="mini"
                  icon="el-icon-more"
                  @click="innerVisible=true"
                  :disabled="type=='detail'"
                ></el-button>
              </div>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <!-- prop="ep_duty" :rules="[{ required: true, message: '请输入职务', trigger: 'change' }]"-->
            <el-form-item label="职务：">
              <el-input
                v-model="addressForm.ep_duty"
                placeholder="请输入职务"
                :disabled="type=='detail'"
              ></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="出生时间："
              prop="ep_birth"
              :rules="{ message: '请选择出生时间', trigger: 'change' }"
            >
              <el-date-picker
                type="date"
                format="yyyy 年 MM 月 dd 日"
                value-format="yyyy-MM-dd"
                placeholder="选择日期"
                v-model="addressForm.ep_birth"
                style="width: 100%;"
                :disabled="type=='detail'"
              ></el-date-picker>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item
              label="学历："
              prop="ep_level"
              :rules="[{  message: '请输入学历', trigger: 'change' }]"
            >
              <el-input
                v-model="addressForm.ep_level"
                placeholder="请输入学历"
                :disabled="type=='detail'"
              ></el-input>
            </el-form-item>
          </el-col>
        </el-col>
        <el-col :span="6">
          <p class="photo-text">照片</p>
          <el-upload
            class="avatar-uploader"
            action="/jz/XBM_Service.bsp?File"
            :show-file-list="false"
            :before-upload="beforeAvatarUpload"
            :headers="{'Content-Type': 'multipart/form-data'}"
            :http-request="customRequst"
            :disabled="type=='detail'"
          >
            <img
              v-if="addressForm.ep_picture"
              :src="'/jz/XBM_Service.bsp?IMAGE&Source='+addressForm.ep_picture"
              class="avatar"
            />
            <i v-else class="el-icon-plus avatar-uploader-icon"></i>
          </el-upload>
        </el-col>
        <el-col :span="24">
          <el-form-item
            label="身份证号："
            prop="ep_certno"
            :rules="[{ message: '请输入身份证号', trigger: 'change' }]"
          >
            <el-input
              v-model="addressForm.ep_certno"
              placeholder="请输入身份证号"
              :disabled="type=='detail'"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="办公电话：" prop="ep_officephone">
            <el-input
              v-model="addressForm.ep_officephone"
              placeholder="请输入办公电话"
              :disabled="type=='detail'"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="手机号码：" prop="ep_mobile1">
            <el-input
              v-model="addressForm.ep_mobile1"
              placeholder="请输入手机号码"
              :disabled="type=='detail'"
            ></el-input>
          </el-form-item>
        </el-col>
        <!-- <el-col :span="12">
            <el-form-item label="家庭电话：" prop="ep_homephone" >
              <el-input v-model="addressForm.ep_homephone" placeholder="请输入家庭电话" :disabled="type=='detail'"></el-input>
            </el-form-item>
          </el-col>
           <el-col :span="12">
            <el-form-item label="手机号码二：" prop="ep_mobile2">
              <el-input v-model="addressForm.ep_mobile2" placeholder="请输入手机号码二" :disabled="type=='detail'"></el-input>
            </el-form-item>
        </el-col>-->
        <el-col :span="24">
          <el-form-item label="家庭住址：" prop="ur_node">
            <el-input
              v-model="addressForm.ur_node"
              placeholder="请输入家庭住址"
              :disabled="type=='detail'"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="备注：" prop="ep_remark">
            <el-input
              type="textarea"
              v-model="addressForm.ep_remark"
              placeholder="请输入备注"
              :disabled="type=='detail'"
            ></el-input>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
    <el-dialog
      width="500px"
      title="选择部门"
      :visible.sync="innerVisible"
      append-to-body
      :close-on-click-modal="false"
    >
      <div style="height:450px;" v-if="innerVisible">
        <el-input placeholder="输入关键字进行过滤" v-model="filterText" style="width:90%;margin-left:5%"></el-input>
        <el-tree
          class="filter-tree"
          :data="depart"
          @node-click="nodeClick"
          :props="defaultProps"
          default-expand-all
          :filter-node-method="filterNode"
          style="height:calc(100% - 40px);overflow:auto;padding-top:10px;"
          ref="tree2"
        ></el-tree>
      </div>
    </el-dialog>
  </div>
</template>
<script>
import * as dataService from "@/public/apiService/PersonalAffairs/address";
import { forMateData } from "@/public/utils";
export default {
  props: ["curData", "group", "type", "typep", "title"],
  data() {
    return {
      innerVisible: false,
      date: new Date(),
      addressForm: {
        ep_name: "",
        ep_code: "",
        ep_sexy: "",
        ep_group: "",
        ep_duty: "",
        ep_birth: "",
        ep_level: "",
        ep_certno: "",
        ep_officephone: "",
        ep_homephone: "",
        ep_mobile1: "",
        ep_mobile2: "",
        ep_address: "",
        ep_remark: "",
        ep_picture: "",
        ur_ident: ""
      },
      filterText: "",
      depart: [],
      defaultProps: {
        children: "children",
        label: "name"
      }
      // imageUrl: ""
    };
  },
  created() {
    this.initFormData();
    // console.log(this.typep);
    this.addressForm.ur_ident = this.typep;
  },
  watch: {
    filterText(val) {
      this.$refs.tree2.filter(val);
    }
  },
  methods: {
    initFormData: function() {
      if (this.type != "add") {
        this.date = this.curData.EP_TIME;
        let arr = Object.keys(this.curData);
        arr.forEach(key => {
          if (key !== "EP_TIME") {
            let newKey = key.toLowerCase();
            this.addressForm[newKey] = this.curData[key];
          }
        });
      } else {
        this.addressForm.ep_group = this.group;
      }
      this.getDepartData();
    },
    filterNode(value, data) {
      if (!value) return true;
      return data.name.indexOf(value) !== -1;
    },
    getDepartData: function() {
      dataService.selAddressDep().then(res => {
        res.data.map(ele => {
          ele.name = ele.or_name;
        });
        this.depart = forMateData(res.data, "or_uper", "or_code");
      });
    },
    nodeClick: function(data) {
      this.addressForm.ep_group = data.name;
      this.innerVisible = false;
    },
    onSubmitAdd: function() {
      this.$refs["addressForm"].validate(valid => {
        if (valid) {
          if (this.type == "add") {
            this.$emit("saveAddBook", this.addressForm);
          } else if (this.type == "edit") {
            this.$emit("saveEditBook", this.addressForm);
          }
        } else {
          return false;
        }
      });
    },
    customRequst: function(file) {
      var formData = new FormData();
      var xmlhttp;
      if (window.XMLHttpRequest) {
        // code for IE7+, Firefox, Chrome, Opera, Safari
        xmlhttp = new XMLHttpRequest();
      } else {
        // code for IE6, IE5
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
      }
      var _this = this;
      xmlhttp.open("POST", "/jz/XBM_Service.bsp?File", true);
      xmlhttp.setRequestHeader("X-Requested-With", "XMLHttpRequest");
      formData.append("filename", file.file.name);
      formData.append("FX_0F00000000", file.file);
      formData.append("_Code_", "");
      formData.append("Submit", "提交");
      xmlhttp.send(formData);
      xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4) {
          if (xmlhttp.status == 200) {
            var data = JSON.parse(xmlhttp.responseText);
            _this.addressForm.ep_picture = data.Code;
          } else {
            console.log("上传失败" + xmlhttp.responseText);
          }
        }
      };
    },
    beforeAvatarUpload(file) {
      const isJPG = file.type === "image/jpeg" || file.type === "image/png";
      const isLt2M = file.size / 1024 / 1024 < 2;
      if (!isJPG) {
        this.$message.error("上传头像图片只能是 JPG或png 格式!");
      }
      if (!isLt2M) {
        this.$message.error("上传头像图片大小不能超过 2MB!");
      }
      return isJPG && isLt2M;
    },
    resetForm() {
      this.$refs["addressForm"].resetFields();
    }
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
      color: #f44336;
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
    .avatar-uploader {
      width: 100%;
      height: 100%;
      text-align: center;
      /deep/ .el-upload {
        border: 1px dashed #d9d9d9;
        border-radius: 6px;
        cursor: pointer;
        position: relative;
        overflow: hidden;
        width: 140px;
        height: 140px;
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
}
</style>
