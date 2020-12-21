<template>
  <div class="login">
    <h4>用户登录</h4>
    <el-form
      :model="loginForm"
      status-icon
      label-width="80px"
      :rules="rules2"
      ref="loginForm"
      class="demo-ruleForm"
    >
      <el-form-item prop="username" label="登录名称">
        <el-input
          type="text"
          placeholder="请输入您的账号"
          v-model="loginForm.username"
          @keyup.enter.native="handleLogin"
          autocomplete="off"
        ></el-input>
      </el-form-item>
      <!-- autocomplete="new-password" -->
      <el-form-item prop="password" label="用户密码">
        <el-input
          type="password"
          placeholder="请输入您的密码"
          v-model="loginForm.password"
          @keyup.enter.native="handleLogin"
          autocomplete="off"
        ></el-input>
      </el-form-item>
      <!-- <el-form-item label="被代理者">
        <el-input
          type="text"
          placeholder="请输入被代理者"
          v-model="loginForm.ur_dllogin"
          autocomplete="off"
          @keyup.enter.native="handleLogin"
        ></el-input>
      </el-form-item> -->
      <!-- auto-complete="new-password" -->
      <el-form-item>
        <p class="remember-select">
          <i
            :class="[checked ? 'check-icon ' : 'false check-icon']"
            @click="rememberClick()"
          ></i>
          <span>记住密码</span>
          <a
            class="print-layer"
            href="/jz/WebDriver.zip"
            download="WebDriver.zip"
            ><i class="print-icon"></i>打印插件下载</a
          >
        </p>
      </el-form-item>
      <el-form-item>
        <el-button
          @click.native.prevent="handleLogin('loginForm')"
          :loading="subLoading"
          style="cursor:pointer"
          >登录</el-button
        >
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
const Base64 = require("js-base64").Base64;
import { getToken, getLogin, removeLogin, setLogin } from "@/public/auth";
import * as dataService from "@/public/apiService/sysManagement/logMangement";
import * as dataServices from "@/public/apiService/PersonalAffairs/shortMsg";
export default {
  name: "userLogin",
  data() {
    return {
      loginForm: {
        username: "",
        password: "",
        ur_dllogin: ""
      },
      checked: false,
      rules2: {
        username: [
          {
            required: true,
            message: "请输入用户名",
            trigger: "blur"
          }
        ],
        password: [
          {
            required: true,
            message: "请输入密码",
            trigger: "blur"
          },
          {
            min: 1,
            message: "密码长度最少为2位",
            trigger: "blur"
          }
        ]
      },
      subLoading: false,
      token: getToken()
    };
  },
  created() {
    this.getLoginInfo();
  },
  methods: {
    getLoginInfo() {
      var strCookie = getLogin();
      this.checked = strCookie ? true : false;
      if (strCookie && strCookie.split(";").length > 1) {
        var arrCookie = strCookie.split(";");
        for (var i = 0; i < arrCookie.length; i++) {
          var arr = arrCookie[i].split("=");
          if (arr[0] == "account") {
            this.loginForm.username = Base64.decode(arr[1]);
          } else if (arr[0] == "password") {
            this.loginForm.password = Base64.decode(arr[1]);
          } else if (arr[0] == "ur_dllogin") {
            this.loginForm.ur_dllogin = Base64.decode(arr[1]);
          }
        }
        return;
      }
    },
    rememberClick() {
      this.checked = !this.checked;
    },
    handleLogin() {
      this.$refs.loginForm.validate(valid => {
        if (valid) {
          let params = {
            username: Base64.encode(this.loginForm.username),
            password: Base64.encode(this.loginForm.password),
            ur_dllogin: Base64.encode(this.loginForm.ur_dllogin)
            //cd_w: this.outerNet,
          };

          if (this.checked) {
            setLogin(
              "account=" +
                Base64.encode(this.loginForm.username) +
                ";password=" +
                Base64.encode(this.loginForm.password) +
                ";ur_dllogin=" +
                Base64.encode(this.loginForm.ur_dllogin)
            );
          } else {
            removeLogin();
          }

          this.$store
            .dispatch("Login", params)
            .then(res => {
              if (res.data.success) {
                //登录时存储一个状态判断消息框，关闭时移除这个状态来使消息框在登录时出现，关闭后刷新不出现
                sessionStorage.setItem("msgClose", "false");
                // localStorage.setItem('memo',{username:this.loginForm.username})
                setLogin(
                  "account=" +
                    Base64.encode(this.loginForm.username) +
                    ";password=" +
                    Base64.encode(this.loginForm.password) +
                    ";ur_dllogin=" +
                    Base64.encode(this.loginForm.ur_dllogin)
                );
                var ip = window.location.host.split(":")[0];
                var params = {
                  ur_ident: res.data.ur_ident,
                  lg_addr: window.location.href,
                  lg_host: ip
                };
                this.$message({
                  type: "success",
                  message: res.data.msg
                });
                this.getDataAdd(params);
              } else {
                this.$message({
                  type: "warning",
                  message: res.data.msg
                });
              }
            })
            .catch(res => {
              console.log(res, "err==1111111111");
            });
        }
      });
    },
    //登录时增加系统日志
    getDataAdd(params) {
      // console.log(params)
      this.subLoading = true;
      dataService
        .getDataAdd(params)
        .then(res => {
          sessionStorage.setItem("lg_code", res.LG_CODE);
          // this.$router.push({
          //   path: "/pproval"
          // });
          window.location.href='/jz/index.html#/approval';
          this.subLoading = false;
        })
        .catch(res => {
          console.log(res, "err==");
        });
    },
    submitForm(formName) {
      this.$refs[formName].validate(valid => {
        if (valid) {
          alert("submit!");
          this.$router.push("apply");
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

<style lang="scss" scoped>
.login {
  height: 397px;
  width: 482px;
  padding: 32px 35px 40px 40px;
  margin-left: 18px;
}
.print-layer {
  float: right;
  line-height: 28px;
  color: #07438b;
  font-size: 16px;
  vertical-align: baseline;
  display: inline-block;
  &:hover {
    color: red;
  }
  .print-icon {
    display: inline-block;
    width: 16px;
    height: 16px;
    background: url("~@/assets/images/print-icon.png") no-repeat;
    background-size: cover;
    vertical-align: middle;
    margin-top: -3px;
    margin-right: 5px;
  }
}
h4 {
  color: #333333;
  font-size: 28px;
  font-weight: bold;
  margin-bottom: 25px;
}

.el-form {
  >>> .el-form-item__content {
    line-height: 24px;
  }
  >>> .el-input__inner {
    height: 48px;
    line-height: 48px;
  }
  >>> .el-checkbox__label {
    color: #666666;
  }
}

.remember-select {
  .check-icon {
    display: inline-block;
    vertical-align: middle;
    width: 23px;
    height: 24px;
    border: 1px solid rgba(122, 122, 122, 1);
    box-shadow: 0px 1px 1px 0px rgba(0, 0, 0, 0.3);
    background: url("../../assets/dui.png") no-repeat center center;
  }
  .check-icon.false {
    background: none;
  }
  span {
    font-size: 18px;
    color: #666666;
    vertical-align: middle;
    margin-left: 11px;
  }
}

button {
  width: 330px;
  height: 56px;
  font-size: 24px;
  font-family: Microsoft YaHei;
  font-weight: bold;
  color: rgba(7, 67, 139, 1);
  background: #fff;
  // color: #fff;
  //   background: #07438b;
  border: 1px solid rgba(204, 204, 204, 1);
  border-radius: 5px;
}
</style>
