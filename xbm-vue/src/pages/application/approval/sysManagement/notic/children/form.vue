<template >
  <div class="addressFormBox">
    <h5 class="title">公告信息</h5>
    <p class="seq-dec">
      <span>
        公告编号:
        <a class="seq-code" v-if="type!='add'">{{addressForm.wiid}}</a>
      </span>
    </p>
    <el-form :model="addressForm" ref="addressForm" label-width="100px" class="addressForm">
      <el-row :gutter="10">
        <el-col :span="12">
          <el-form-item label="发布人：" prop="nt_sender">
            <el-input v-model="addressForm.nt_sender" placeholder="请输入编号" disabled></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="紧急程度："
            prop="nt_urgent"
            :rules="{required: true, message: '请选择紧急程度', trigger: 'change' }"
          >
            <el-select
              v-model="addressForm.nt_urgent"
              placeholder="请选择紧急程度"
              style="width: 100%;"
              :disabled="type=='detail'"
            >
              <el-option label="一般" value="一般"></el-option>
              <el-option label="紧急" value="紧急"></el-option>
            </el-select>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="发布时间："
            prop="nt_time"
            :rules="{required: true, message: '请选择发布时间', trigger: 'change' }"
          >
            <el-date-picker
              type="datetime"
              format="yyyy-MM-dd HH:mm"
              value-format="yyyy-MM-dd HH:mm"
              placeholder="选择日期"
              v-model="addressForm.nt_time"
              style="width: 100%;"
              :disabled="type!='add'"
              :picker-options="pickerBeginDate"
              @change="changeTime"
            ></el-date-picker>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="有效时间："
            prop="nt_move"
            :rules="{required: true, message: '请选择有效时间', trigger: 'change' }"
          >
            <el-date-picker
              type="datetime"
              format="yyyy-MM-dd HH:mm"
              value-format="yyyy-MM-dd HH:mm"
              placeholder="选择日期"
              v-model="addressForm.nt_move"
              style="width: 100%;"
              :disabled="type=='detail'"
              :picker-options="pickerEndDate"
            ></el-date-picker>
          </el-form-item>
        </el-col>
        <!-- @change="endDate" -->
        <el-col :span="24">
          <el-form-item
            label="公告主题："
            prop="nt_name"
            :rules="[{ required: true, message: '请输入公告主题', trigger: 'change' }]"
          >
            <el-input
              v-model="addressForm.nt_name"
              placeholder="请输入公告主题"
              :disabled="type=='detail'"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item
            label="公告内容："
            prop="nt_content"
            :rules="[{ required: true, message: '请输入公告主题', trigger: 'change' }]"
          >
            <el-input
              type="textarea"
              :rows="5"
              v-model="addressForm.nt_content"
              placeholder="请输入备注"
              :disabled="type=='detail'"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="附件：">
            <el-upload
              class="upload-demo"
              action="/jz/XBM_Service.bsp?File"
              :on-preview="onPreview"
              :file-list="fileList"
              :http-request="customRequst"
              :disabled="type=='detail'"
            >
              <el-button size="small" type="primary" :disabled="true">点击上传</el-button>
            </el-upload>
          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>
<script>
import * as dataService from "@/public/apiService/PersonalAffairs/address";
import { saveFile } from "@/public/apiService/sysManagement/enclosure";
const userInfo = JSON.parse(localStorage.getItem("data"));
function addDate(date, days) {
  var date = new Date(date);
  days && date.setDate(date.getDate() + days);
  var month = date.getMonth() + 1;
  var day = date.getDate();
  var hours = date.getHours();
  var minutes = date.getMinutes();
  var mm = "'" + month + "'";
  var dd = "'" + day + "'";
  var hh = "'" + hours + "'";
  var MM = "'" + minutes + "'";
  //单位数前面加0
  if (mm.length == 3) {
    month = "0" + month;
  }
  if (dd.length == 3) {
    day = "0" + day;
  }
  if (hh.length == 3) {
    hours = "0" + hours;
  }
  if (MM.length == 3) {
    minutes = "0" + minutes;
  }
  var time =
    date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + minutes;
  return time;
}
export default {
  props: ["curData", "type"],
  data() {
    return {
      addressForm: {
        nt_name: "", //公告名称
        nt_sender: userInfo.ur_name, //发布人姓名
        nt_dept: userInfo.ur_node, //发布部门编号
        nt_content: "", //发布内容
        nt_move: addDate(Date.now(), 3), //有效期限
        nt_user: userInfo.ur_ident, //发布人编号
        nt_urgent: "", //紧急程度
        nt_time: addDate(Date.now()) //发布时间
      },
      fileList: [],
      pickerBeginDate: {
        disabledDate(time) {
          return time.getTime() < Date.now() - 8.64e7;
        }
      },
      pickerEndDate: {
        disabledDate(time) {
          return time.getTime() < new Date(addDate(Date.now(), 3)) - 8.64e7;
        }
      }
    };
  },
  created() {
    this.initFormData();
  },
  computed: {
    fileParms: function() {
      let arr = [];
      this.fileList.forEach(item => {
        arr.push({ ac_name: item.code });
      });
      return arr;
    }
  },
  methods: {
    initFormData: function() {
      if (this.type != "add") {
        this.fileList = [];
        this.curData.FILE &&
          this.curData.FILE.forEach(item => {
            this.fileList.push({
              name: item.SR_NAME,
              url: "/jz/XBM_Service.bsp?IMAGE&Source=" + item.AC_NAME,
              code: item.AC_NAME
            });
          });
        let arr = Object.keys(this.curData);
        arr.forEach(key => {
          if (key !== "FILE") {
            let newKey = key.toLowerCase();
            if (newKey == "ntid") {
              this.addressForm["wiid"] = this.curData[key];
            } else if (newKey == "nt_move" || newKey == "nt_time") {
              this.addressForm[newKey] = addDate(this.curData[key]);
            } else {
              this.addressForm[newKey] = this.curData[key];
            }
          }
        });
      }
    },
    //选择开始时间，清空结束时间
    changeTime(val) {
      if (val) {
        this.addressForm.nt_move = addDate(val, 3);
        this.pickerEndDate = {
          disabledDate(time) {
            return time.getTime() <= new Date(addDate(val, 3)) - 8.64e7;
          }
        };
      } else {
        this.pickerEndDate = {
          disabledDate(time) {
            return time.getTime() < Date.now() - 8.64e7;
          }
        };
      }
    },
    saveAddNotice: function(params) {
      dataService.addNotice(params).then(res => {
        if (res.success) {
          saveFile(res.WIID, this.fileParms);
          this.$message({
            type: "success",
            message: "添加成功!"
          });
          this.$emit("getData");
        }
        this.DialogShow = false;
      });
    },
    saveEditNotice: function(params) {
      dataService.editNotice(params).then(res => {
        if (res.success) {
          saveFile(res.data[0].WIID, this.fileParms);
          this.$message({
            type: "success",
            message: "修改成功!"
          });
          this.$emit("getData");
        }
        this.DialogShow = false;
      });
    },
    onSubmitAdd: function(num) {
      this.$refs["addressForm"].validate(valid => {
        if (valid) {
          this.addressForm.nt_fbzt = num;
          if (this.type == "add") {
            this.saveAddNotice(this.addressForm);
          } else if (this.type == "edit") {
            var params = {
              wiid: this.addressForm.wiid,
              nt_name: this.addressForm.nt_name,
              nt_content: this.addressForm.nt_content,
              nt_move: this.addressForm.nt_move,
              nt_urgent: this.addressForm.nt_urgent,
              nt_fbzt: num
            };
            this.saveEditNotice(params);
          }
        } else {
          return false;
        }
      });
    },
    onPreview: function(file) {
      window.open(file.url);
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
            // var code = xmlhttp.responseText
            //   .split("<body>")[1]
            //   .split("</body>")[0]
            //   .split("/>")[0]
            //   .split("value=")[1]
            //   .split('"')[1];
            _this.fileList.push({
              name: file.file.name,
              url: data.Addr,
              code: data.Code
            });
          } else {
            console.log("上传失败" + xmlhttp.responseText);
          }
        }
      };
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
    margin-top: -10px;
    padding-bottom: 10px;
    .seq-code {
      text-decoration: underline;
      padding: 0px 10px;
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