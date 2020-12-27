<template>
  <div class="login-container">
    <el-form
      :model="logininfo"
      :rules="rules2"
      status-icon
      ref="logininfo"
      label-position="left"
      label-width="0px"
      class="demo-ruleForm login-page"
    >
      <h3 class="title">债减减APP管理后台</h3>
      <el-form-item prop="username">
        <el-input type="text" v-model="logininfo.username" auto-complete="off" placeholder="用户名"></el-input>
      </el-form-item>
      <el-form-item prop="password">
        <el-input type="password" v-model="logininfo.password" auto-complete="off" placeholder="密码"></el-input>
      </el-form-item>

      <el-form-item style="width:100%;">
        <el-button type="primary" style="width:100%;" @click="handleSubmit" :loading="logining">登录</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
import { requestLogin, getUser } from "../../api/api";
import { setToken } from "../../utils/auth";
export default {
  data() {
    return {
      logining: false,
      logininfo: {
        username: "",
        password: ""
      },
      rules2: {
        username: [
          { required: true, message: "请输入用户名", trigger: "blur" }
        ],
        password: [{ required: true, message: "请输入密码", trigger: "blur" }]
      },
      checked: false
    };
  },
  methods: {
    handleSubmit() {
      var _this = this;
      requestLogin(this.logininfo.username, this.logininfo.password).then(
        res => {
          setToken(res.access_token);
          getUser().then(res => {
            var user = res.result;
            sessionStorage.setItem("user", JSON.stringify(user));
            console.log('123',user);
            _this.$store.commit("user/setUser", user);
            _this.$router.push({ path: "/member" });
          });
        }
      );
    }
  }
};
</script>

<style scoped>
.login-container {
  width: 100%;
  height: 100%;
}
.login-page {
  -webkit-border-radius: 5px;
  border-radius: 5px;
  margin: 180px auto;
  width: 350px;
  padding: 35px 35px 15px;
  background: #fff;
  border: 1px solid #eaeaea;
  box-shadow: 0 0 25px #cac6c6;
}
label.el-checkbox.rememberme {
  margin: 0px 0px 15px;
  text-align: left;
}
</style>