<template>
  <div class="app">
    <div class="main-box">
      <div class="main">
        <div class="title-box">
          <p>成都再减减企业管理服务有限公司</p>
        </div>
        <div class="login-box">
          <userLogin></userLogin>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import userLogin from "@/pages/login/login";

export default {
  name: "index",
  components: { userLogin },
  data() {
    return {
      pageTitle: "测试主页",
      showHot: 1
    };
  }
};
</script>

<style scoped lang='scss'>
.app {
  height: 100%;
  width: 100%;
  position: relative;
  background: url("../../assets/home_bg.jpg") no-repeat;
  background-size: 100% 100%;
}
.main-box {
  width: 100%;
  height: 236px;
  position: absolute;
  top: 50%;
  transform: translateY(-50%);
  background: rgba(7, 67, 139, 0.9);
}
.main {
  width: 1200px;
  height: 100%;
  margin: 0 auto;
}
.title-box {
  width: 615px;
  color: #fff;
  float: left;
  margin-top: 71px;
  p {
    font-size: 36px;
    font-family: SimSun;
    font-weight: 400;
    margin-bottom: 22px;
  }
  span {
    font-size: 12px;
    font-family: FZBaoSong-Z04;
  }
}
.login-box {
  width: 568px;
  height: 565px;
  margin-top: -80px;
  float: right;
  background: url("../../assets/login_bg.png") no-repeat;
  background-size: 100% 100%;
}
</style>