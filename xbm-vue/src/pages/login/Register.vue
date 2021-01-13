<template>
  <div class="home">
    <div class="main-box">
      <div class="main">
        <div class="title-box">
          <p>注册</p>
        </div>
        <div class="regist-box">
          <div class="regist">
    <el-form
      :model="ruleForm"
      :rules="rules"
      ref="ruleForm"
      label-width="100px"
      class="demo-ruleForm"
      size="mini"
    >
      <el-form-item label="用户名：" prop="username">
        <el-input class="hei" v-model="ruleForm.username"></el-input>
        <span class="input-tag">输入您的用户名称</span>
      </el-form-item>
      <el-form-item label="登录密码：" prop="pwd">
        <el-input class="hei" v-model="ruleForm.pwd"></el-input>
        <span class="input-tag">6-16个字符，区分大小写</span>
      </el-form-item>
      <el-form-item label="确认密码：" prop="repwd">
        <el-input class="hei" v-model="ruleForm.repwd"></el-input>
        <span class="input-tag">再次输入您设置的密码</span>
      </el-form-item>
      <el-form-item label="姓名：" prop="name">
        <label slot="label">姓&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;名：</label>
        <el-input class="hei" v-model="ruleForm.name"></el-input>
        <span class="input-tag">输入您的姓名</span>
      </el-form-item>
      <el-form-item label="证件类型：" prop="cardType">
        <el-select class="hei" v-model="ruleForm.cardType" placeholder="请选择">
          <el-option label="身份证" value="shanghai"></el-option>
          <el-option label="二代身份证" value="beijing"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="证件号码：" prop="IDnum">
        <el-input class="hei" v-model="ruleForm.IDnum"></el-input>
        <span class="input-tag">请输入您的证件号码</span>
      </el-form-item>
      <el-form-item label="邮箱：" prop="emial">
        <label slot="label">邮&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;箱：</label>
        <el-input class="hei" type="email" v-model="ruleForm.emial"></el-input>
      </el-form-item>
      <el-form-item label="手机号码：" prop="phone">
        <el-input class="hei" type="tell" v-model="ruleForm.phone"></el-input>
      </el-form-item>
      <el-form-item label="顾客类型：" prop="region">
        <el-select class="hei" v-model="ruleForm.region" placeholder="请选择活动区域">
          <el-option label="区域一" value="shanghai"></el-option>
          <el-option label="区域二" value="beijing"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-checkbox v-model="ruleForm.checked">
          已阅读并同意
          <span>某某科技公司使用协议</span>及
          <span>隐私协议</span>
        </el-checkbox>
      </el-form-item>
      <el-form-item>
        <button class="btn">注 册</button>
      </el-form-item>
    </el-form>
  </div>
          <!--<Regist></Regist>-->
        </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "index",
  data() {
    return {
      pageTitle: "测试主页",
      showHot: 1,
       ruleForm: {
        name: "",
        region: "",
        desc: ""
      },
      rules: {
        username: [
          { required: true, message: "请输入用户名", trigger: "blur" }
        ],
        pwd: [{ required: true, message: "请输入登录密码", trigger: "blur" }],
        repwd: [{ required: true, message: "请输入登录密码", trigger: "blur" }],
        name: [{ required: true, message: "请输入活动名称", trigger: "blur" }],
        cardType: [
          { required: true, message: "请输入活动名称", trigger: "blur" }
        ],
        IDnum: [{ required: true, message: "请输入活动名称", trigger: "blur" }],
        emial: [{ required: true, message: "请输入活动名称", trigger: "blur" }],
        phone: [{ required: true, message: "请输入活动名称", trigger: "blur" }],
        region: [
          { required: true, message: "请选择活动区域", trigger: "change" }
        ],
        desc: [{ required: true, message: "请填写活动形式", trigger: "blur" }]
      }
    };
  },
   methods: {
    submitForm(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          alert("submit!");
        } else {
          console.log("error submit!!");
          return false;
        }
      });
    },
    resetForm(formName) {
      this.$refs[formName].resetFields();
    }
  }
};
</script>
<style scoped lang='scss'>
.home {
  height: 100%;
  width: 100%;
  position: relative;
  background: url("../../assets/regist_bg.jpg") no-repeat;
  background-size: 100% 100%;
}
.main-box {
  width: 1200px;
  height: 708px;
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}
.main {
  height: 100%;
}
.title-box {
  width: 507px;
  height: 100%;
  float: left;
  background: rgba(7, 67, 139, 0.9);
  position: relative;
  p {
    position: absolute;
    left: 50%;
    top: 50%;
    transform: translate(-50%, -50%);
    color: #fff;
    font-size: 58px;
  }
}
.regist-box {
  width: 693px;
  height: 100%;
  float: right;
  background: #fff;
  .regist {
  padding: 80px 40px 70px 63px;
}
.hei {
  width: 276px;
  >>> .el-input__inner {
    height: 32px;
    line-height: 32px;
    border-radius: 0;
  }
}
.input-tag {
  color: #c8c8c8;
  font-size: 12px;
  padding-left: 19px;
}
.btn {
  width: 276px;
  height: 40px;
  background: rgba(0, 111, 226, 1);
  border-radius: 4px;
  color: #fff;
  font-size: 16px;
  line-height: 40px;
}
>>> .el-checkbox__label {
  font-size: 12px;
  color: #a3a2a2;
  span {
    color: #4b4b4b;
  }
}
>>> .el-input__suffix {
  right: 0px;
  background: #07438b;
}
}
</style>