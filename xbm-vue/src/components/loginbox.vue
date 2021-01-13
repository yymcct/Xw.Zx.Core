<template>
  <div class="user-box">
      <span class="text-dec"  v-if="user&&token">
        <i class="el-icon-s-custom"></i>
        {{user.showname}}
      </span>
      <span class="tologin-btn" @click="toChange" v-if="user && token"
      ><i class="fa fa-cog" aria-hidden="true"></i>修改密码</span
    >

     <span class="tologin-btn" @click="toLogin" v-else><i class="fa fa-sign-in"></i>登录</span>
     <span><a class="print-layer" href="/jz/WebDriver.zip" download="WebDriver.zip"><i class="fa fa-cloud-download"></i>打印插件下载</a></span>
     <span @click="loginOut"  v-if="user&&token"><i class="el-icon-switch-button" style="font-weight: bolder;"></i>退出</span>
    <span v-if="notHome" class="toindex-btn" @click="toIndex()">返回首页</span>
   
  </div>
</template>
  <script>
import * as dataService from "@/public/apiService/sysManagement/logMangement";
import { getUserInfo, removeToken,removeIfrmeLogin } from "@/public/auth";
export default {
  name: "headerIn",
  props: ["notHome"],
  data() {
    return {
       user:getUserInfo()
    };
  },
  computed:{
    token:function(){
      let temp=this.$store.state.user.token;
      this.user=getUserInfo();
      // if(this.user){
      //   this.username = this.user.showname?user.showname:user.ur_name;
      // }
      return this.$store.state.user.token
    }
  },
  mounted() {
  
  },
  methods: {
     toChange() {
      this.$router.push({
        path: "/approval/personalInformation"
      });
      this.$store.commit("changePassword");
      this.$store.commit("changeMenuDefault", {
        BA_PATH: "/approval/personalInformation",
        Ba_Name: "个人信息"
      });
     
    },
    toLogin() {
      this.$router.replace("/login");
    },
    downLoad:function(){
      this.$message.success('下载打印插件！');
    },
    loginOut: function() {
      this.$confirm("确定退出登录吗?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.getDataLeave();
          localStorage.clear();
          sessionStorage.clear();
          removeToken();
          removeIfrmeLogin();
          this.clearAllCookie();
          this.$store.commit("SET_TOKEN", "");
          this.$router.push("/");
          window.location.reload();
        })
        .catch(() => {});
    },
    getDataLeave() {
      var lg_code = sessionStorage.getItem("lg_code");
      var params = { lg_code: lg_code };
      dataService.getDataLeave(params).then(res => {
      });
    },
    toIndex() {
      this.$router.push({ path: "/" });
    },
    //清除所有cookie函数
   clearAllCookie:function () {
      var date=new Date();
      date.setTime(date.getTime()-10000);
      var keys=document.cookie.match(/[^ =;]+(?=\=)/g);
      console.log("需要删除的cookie名字："+keys);
      if (keys) {
          for (var i =  keys.length; i--;)
            document.cookie=keys[i]+"=0; expire="+date.toGMTString()+"; path=/";
      }
  }
  }
};
</script>
<style scoped lang='scss'>
@import "~@/assets/scss/variables";
.user-box {
  .text-dec{
    color: $base-color;
  }
  i {
    // color: $base-color;
    font-size: 18px;
  }
  span {
    padding: 0 6px;
    font-size: 14px;
    font-family: Microsoft YaHei;
    font-weight: 600;
    color: rgba(102, 102, 102, 1);
    cursor: pointer;
    &.tologin-btn {
      color: $base-color;
    }
    &.toindex-btn {
      font-weight: normal;
      font-size: 14px;
      text-decoration: underline;
    }
  }
  span:hover {
    color: $base-color;
  }
}

</style>

