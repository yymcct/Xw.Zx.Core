<template >
  <div class="ScheduleFormBox">
    <h5 class="title">日程安排</h5>

    <el-form
      :model="scheduleForm"
      ref="scheduleForm"
      label-width="100px"
      class="scheduleForm"
    >
      <el-row :gutter="10">
        <el-col :span="12">
          <el-form-item
            label="事项类型："
            prop="plantype"
            :rules="{required: true, message: '请输入事项类型', trigger: 'change' }"
          >
            <el-input
              v-model="scheduleForm.plantype"
              placeholder="请输入事项类型"
              :disabled="type=='detail'"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="当前时间："
            :rules="{required: true, message: '请选择发布时间', trigger: 'change' }"
          >
            <el-date-picker
              type="datetime"
              format="yyyy-MM-dd HH:mm"
              value-format="yyyy-MM-dd HH:mm"
              placeholder="选择日期"
              v-model="scheduleForm.plandate"
              style="width: 100%;"
              disabled
            ></el-date-picker>
          </el-form-item>

        </el-col>
        <el-col :span="24">
          <el-form-item
            label="事项标题："
            prop="plantitle"
            :rules="[{ required: true, message: '请输入事项标题', trigger: 'change' }]"
          >
            <el-input
              v-model="scheduleForm.plantitle"
              placeholder="请输入事项标题"
              :disabled="type=='detail'"
            ></el-input>
          </el-form-item>
        </el-col>

        <el-col :span="24">
          <el-form-item
            label="事项内容："
            prop="plancontent"
            :rules="[{ required: true, message: '请输入事项内容', trigger: 'change' }]"
          >
            <el-input
              type="textarea"
              :rows="5"
              v-model="scheduleForm.plancontent"
              placeholder="请输入事项内容"
              :disabled="type=='detail'"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="开始时间："
            prop="planstarttime"
            :rules="{required: true, message: '请选择发布时间', trigger: 'change' }"
          >
            <el-date-picker
              type="datetime"
              format="yyyy-MM-dd HH:mm"
              value-format="yyyy-MM-dd HH:mm"
              placeholder="选择日期"
              v-model="scheduleForm.planstarttime"
              style="width: 100%;"
              :disabled="type=='detail'"
              :picker-options="pickerBeginDate"
            ></el-date-picker>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="结束时间："
            prop="planendtime"
            :rules="{required: true, message: '请选择有效时间', trigger: 'change' }"
          >
            <el-date-picker
              type="datetime"
              format="yyyy-MM-dd HH:mm"
              value-format="yyyy-MM-dd HH:mm"
              placeholder="选择日期"
              v-model="scheduleForm.planendtime"
              style="width: 100%;"
              :disabled="type=='detail'"
              :picker-options="pickerEndDate"
            ></el-date-picker>
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="提醒：">
            <el-radio-group v-model="scheduleForm.planendtx">
              <el-radio :label="0">开始时</el-radio>
              <el-radio :label="5">5分钟前</el-radio>
              <el-radio :label="15">15分钟前</el-radio>
              <el-radio :label="30">30分钟前 </el-radio>
              <el-radio :label="1">1小时前</el-radio>
              <el-radio :label="2">1天前</el-radio>
            </el-radio-group>

          </el-form-item>
        </el-col>
      </el-row>
    </el-form>
  </div>
</template>
<script>
import * as dataService from "@/public/apiService/PersonalAffairs/schedule";
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
  props: ["curData", "type"],
  data () {
    return {
      scheduleForm: {
        plantitle: "",
        plantype: "",
        plancontent: "",
        plandate: addDate(Date.now()),
        planstarttime: addDate(Date.now()),
        planendtime: '',
        planendtx: null,
      },
      //结束时间大于开始时间
      pickerBeginDate: this.limitDateStart(),
      pickerEndDate: this.limitDateEnd(),
    };
  },
  created () {
    this.initFormData();
  },
  computed: {

  },
  mounted () {

  },
  methods: {
    limitDateStart: function () {
      let _this = this
      return {
        disabledDate (time) {
          if (_this.scheduleForm.planstarttime >= addDate(Date.now())) {
            return time.getTime() < Date.now() - 8.64e7;
          }
        }
      }
    },
    limitDateEnd: function () {
      let _this = this
      return {
        disabledDate (time) {
          return time.getTime() < new Date(_this.scheduleForm.planstarttime).getTime() - 1 * 24 * 60 * 60 * 1000
        }
      }
    },
    initFormData () {
      if (this.type == 'add') {
        this.scheduleForm = this.scheduleForm;
      } else if (this.type == 'edit') {
        this.scheduleForm = this.curData;
        this.scheduleForm.plandate = addDate(Date.now());
      } else {
        this.scheduleForm = this.curData;
        console.log(this.scheduleForm)
        //  this.scheduleForm.planendtx=5
      }

    },

    saveAddSchedule: function (params) {
      console.log(params)
      dataService.getScheduleAdd(params).then(res => {
        console.log(res)
        if (res.success) {
          this.$message({
            type: "success",
            message: "添加成功!"
          });
          this.$emit("getData");
        }
        this.$emit('dialogShow', false)
      });
    },
    saveEditSchedule: function (params) {

      dataService.getScheduleEdit(params).then(res => {
        console.log(params, res)
        if (res.success) {
          this.$message({
            type: "success",
            message: "修改成功!"
          });
          this.$emit("getData");
        }
        this.$emit('dialogShow', false)
      });
    },
    onSubmitAdd: function () {
      this.$refs["scheduleForm"].validate(valid => {
        if (valid) {
          if (this.type == "add") {
            this.saveAddSchedule(this.scheduleForm);
          } else if (this.type == "edit") {

            var params = {
              plantitle: this.scheduleForm.plantitle,
              plantype: this.scheduleForm.plantype,
              plancontent: this.scheduleForm.plancontent,
              planstarttime: this.scheduleForm.planstarttime,
              planendtime: this.scheduleForm.planendtime,
              plandate: this.scheduleForm.plandate,
              planendtx: this.scheduleForm.planendtx,
              wiid: this.curData.wiid
            };
            this.saveEditSchedule(params);
          }
        } else {
          return false;
        }
      });
    },


  }
};
</script>
<style lang="scss" scoped>
.ScheduleFormBox {
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
  .scheduleForm {
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
