<template>
  <!--业务审批-->
  <div class="main-box">
    <div>
      <iframe
        :src="'/jz/form_1_2.bsp?token=' + token"
        style="display: none"
      ></iframe>
      <div class="header-box header-box2">
        <h3>成都再减减企业管理服务有限公司</h3>
        <loginBox class="login-box" :notHome="true"></loginBox>
      </div>
      <div class="nav"></div>
      <div class="main">
        <Personal></Personal>
      </div>
    </div>
  </div>
</template>

<script>
import { getToken } from "@/public/auth";
import loginBox from "@/components/loginbox";
import Personal from "@/pages/application/approval/part1/personal";
export default {
  name: "manage",
  components: {
    loginBox,
    Personal,
  },
  created() {
    //存储初始化行政审批，政务系统导航
    if (!sessionStorage.getItem("approvalMenu")) {
      this.$store.commit("changeMenuDefault", {
        BA_PATH: "/jz/XBM_Service.bsp?EXEC&Source=FORM[1].[7]&token=",
        Ba_Name: " ",
      });
    }
  },
  data() {
    return {
      token: getToken(),
    };
  },
  methods: {
    toIndex() {
      this.$router.push({ path: "/" });
    },
  },
  beforeDestroy: function () {
    sessionStorage.removeItem("approvalMenu");
  },
};
</script>

<style scoped lang="scss">
@import "~@/assets/scss/variables";
.main-box {
  width: 100%;
  min-height: 100%;
  background: #f2f2f2;
  position: absolute;
  //overflow-y: auto;
  & > div {
    width: 100%;
    height: 100%;
    position: absolute;
    left: 50%;
    -webkit-transform: translateX(-50%);
    transform: translateX(-50%);
    overflow-x: hidden;
  }
  .header-box {
    position: relative;
  }
  .header-box h3 {
    // width: 1400px;
    margin: 0 auto;
    height: 80px;
    line-height: 80px;
    color: $base-color;
    font-size: 32px;
    padding-left: 60px;
    background: url("../../../assets/logo.png") no-repeat left center;
    background-size: 60px 60px;
    cursor: pointer;
  }
  .login-box {
    position: absolute;
    right: 0;
    top: 50%;
    transform: translateY(-50%);
    // width: 200px;
    text-align: right;
  }
  .nav {
    background: $base-color;
    height: 10px;
    line-height: 10px;
  }
  .main {
    width: 100%;
    margin: 0 auto;
    background: #fff;
    height: calc(100% - 90px);
  }
}
</style>
