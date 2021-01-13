<template>
  <div class="app">
    <div class="down-box">
      <div class="header-box header-box5">
        <h3>成都再减减企业管理服务有限公司APP下载</h3>
      </div>

      <div class="img-box clearfix">
        <div class="right">
          <img src="../../../assets/login-erweima.png" alt />
          <p>扫码下载APP</p>
        </div>
        <div class="left">
          <img src="../../../assets/images/app1.jpg" alt />
          <img src="../../../assets/images/app2.jpg" alt />
          <img src="../../../assets/images/app3.jpg" alt />
          <img src="../../../assets/images/app4.jpg" alt />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "index",
  data() {
    return {};
  }
};
</script>

<style scoped lang='scss'>
@import "~@/assets/scss/variables";
.app {
  height: 100%;
  width: 100%;
  position: relative;
  background: url("../../../assets/down.jpg") no-repeat;
  background-size: 100% 100%;
  &::before {
    content: "";
    position: absolute;
    left: 0;
    right: 0;
    bottom: 0;
    top: 0;
    background-color: rgba(0, 0, 0, 0.3);
    z-index: 2;
  }
  .header-box {
    height: 125px;
    margin-bottom: 6px;
    h3 {
      float: left;
      padding-left: 90px;
      width: 742px;
      font-size: 40px;
      font-weight: bold;
      color: $base-color;
      text-shadow: 0px 2px 4px rgba(0, 0, 0, 0.2);
      background: url("../../../assets/logo.png") no-repeat left center;
    }
  }
  .down-box {
    width: 1000px;
    position: absolute;
    top: 100px;
    left: 50%;
    transform: translateX(-50%);
    z-index: 99;
    p {
      font-size: 32px;
      color: #fff;
      font-weight: bold;
    }
    img {
      width: 160px;
      margin-right: 20px;
    }
  }
  .img-box {
    .right {
      text-align: center;
    }
    p {
      margin-top: 15px;
    }
  }
}
</style>