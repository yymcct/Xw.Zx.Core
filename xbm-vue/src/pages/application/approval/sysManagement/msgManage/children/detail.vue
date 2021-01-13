<template >
  <div class="addressFormBox">
    <h5 class="title">消息信息</h5>
    <p class="seq-dec" >
      <span>
        消息编号:
        <a class="seq-code" >{{addressForm.wiid}}</a>
      </span>
    </p>
    <el-form :model="addressForm" ref="addressForm" label-width="100px" class="addressForm">
      <el-row :gutter="10">
        <!-- <el-col :span="12">
          <el-form-item label="发布人：" prop="nt_sender">
            <el-input v-model="addressForm.nt_sender" placeholder="请输入编号" disabled></el-input>
          </el-form-item>
        </el-col> -->
        <el-col :span="24">
          <el-form-item label="所属栏目：" prop="nt_urgent" >
            <el-input v-model="addressForm.nt_name" placeholder="请输入公告主题" disabled></el-input>

          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="发布时间：" prop="nt_time" :rules="{required: true, message: '请选择发布时间',  }">
            <el-date-picker type="datetime" format="yyyy-MM-dd HH:mm" value-format="yyyy-MM-dd HH:mm" placeholder="选择日期" v-model="addressForm.nt_time" style="width: 100%;" disabled  ></el-date-picker>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="有效时间：" prop="nt_move" :rules="{required: true, message: '请选择有效时间' }">
            <el-date-picker type="datetime" format="yyyy-MM-dd HH:mm" value-format="yyyy-MM-dd HH:mm" placeholder="选择日期" v-model="addressForm.nt_move" style="width: 100%;" disabled ></el-date-picker>
          </el-form-item>
        </el-col>
        <!-- @change="endDate" -->
        <!-- <el-col :span="24">
          <el-form-item label="消息标题：" prop="nt_matter" >
            <el-input v-model="addressForm.nt_matter"  disabled></el-input>
          </el-form-item>
        </el-col> -->
        <el-col :span="24">
          <el-form-item label="消息内容：" prop="nt_content" :rules="[{ required: true,  }]">
            <el-input type="textarea" :rows="5" v-model="addressForm.nt_content" placeholder="请输入备注" disabled></el-input>
          </el-form-item>
        </el-col>
        <!-- <el-col :span="24">
          <el-form-item label="附件：">
            <el-upload class="upload-demo" action="/ly/XBM_Service.bsp?File"   disabled>
              <el-button size="small" type="primary" :disabled="true">点击上传</el-button>
            </el-upload>
          </el-form-item>
        </el-col> -->
      </el-row>
    </el-form>

  </div>
</template>
<script>
import * as dataService from "@/public/apiService/PersonalAffairs/address";
// const userInfo = JSON.parse(sessionStorage.getItem("userInfo"));
function addDate (date, days) {
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
  props: ["curData"],
  data () {
    return {
      addressForm: {},
    };
  },
  created () {
    this.getDate()

  },
  computed: {

  },
  methods: {


getDate(){
  this.addressForm={
        nt_name:this.curData.at_theme, //所属栏目
        // nt_sender: userInfo.ur_name, //发布人姓名
        nt_content:this.curData.at_matter, //消息内容
        nt_move: this.curData.at_ctime, //有效期限
        nt_user: this.curData.at_uid, //发布人编号
        nt_time: this.curData.at_stime,//发布时间
        wiid:this.curData.aid,//消息编号

  }
  console.log(this.curData)

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
