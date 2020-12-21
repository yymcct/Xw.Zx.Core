<template>
  <!--网上申请-->
  <div style="background:#f2f2f2;height: 100%;">
    <div class="header-box header-box3">
      <h3 @click="toIndex">成都再减减企业管理服务有限公司</h3>
      <loginBox class="login-box" :notHome="true"></loginBox>
    </div>
    <div class="nav">
      <NavIn @clickit="getNav" :navList="navList"></NavIn>
    </div>
    <div class="main">
      <Personal :is="currentView"></Personal>
    </div>
  </div>
</template>

<script>
import { getToken } from "@/public/auth";
import loginBox from "@/components/loginbox";
import NavIn from "@/components/CusNav";
import Personal from "@/pages/application/apply/part1/personal";
import axios from "axios";
export default {
  name: "manage",
  components: {
    loginBox,
    NavIn,
    Personal
  },
  data() {
    return {
      currentView: "personal",
      navList: [
        {
          tag: "待办中心",
          path: "waitting"
        },
        {
          tag: "消息中心",
          path: "notice"
        },
        {
          tag: "审文中心",
          path: "news"
        },
        {
          tag: "审批中心",
          path: "approval"
        },
        {
          tag: "报表中心",
          path: "application"
        },
        {
          tag: "任务中心",
          path: "supervise"
        },
        {
          tag: "效能中心",
          path: "efficiency"
        },
        {
          tag: "管理中心",
          path: "admin"
        }
      ],
      token: getToken()
    };
  },
  created() {},
  methods: {
    toIndex() {
      this.$router.push({ path: "/" });
    },
    getNav(data) {
      //console.log(data);
      this.currentView = data.path;
    }
  }
};
</script>

<style scoped lang='scss'>
@import "~@/assets/scss/variables";
.header-box {
  position: relative;
  h3 {
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
    width: 200px;
    text-align: right;
  }
}

.nav {
  background: $base-color;
  height: 50px;
  line-height: 50px;
}

.main {
  width: 1200px;
  margin: 0 auto;
  height: calc(100% - 131px);
}
</style>