<template>
  <!--电子政务-->
  <!-- v-loading="loading" -->
  <div style="background:#f2f2f2;height:100%">
    <div class="header-box header-box7">
      <h3 @click="toIndex()">成都再减减企业管理服务有限公司</h3>
      <loginBox class="login-box" :notHome="true"></loginBox>
    </div>
    <div class="nav">
    </div>
    <div class="main" v-if="iframe_src">
      <Manage :defaultMenu="defaultMenu" :navList="navList" :showMenu="showMenu"></Manage>
    </div>
  </div>
</template>

<script>
import loginBox from "@/components/loginbox";
import Manage from "@/pages/application/manage/manageBox";
import LeftMenu from "@/pages/application/approval/part1/leftMenu";
import { getManageMenuList } from "@/public/apiService/sysManagement/menu";
import { forMateData } from "@/public/utils";
import bus from "@/public/event.js";
export default {
  name: "manage",
  components: {
    loginBox,
    Manage,
    LeftMenu,
  },
  data() {
    return {
      // Number(sessionStorage.getItem("manageNav"))
      navList: [],
      navCur: null,
      defaultMenu: [],
      loading: true,
      showMenu: false,
    };
  },
  created() {
    // //存储初始化政务系统导航
    var data = JSON.parse(sessionStorage.getItem("manageMenu"));
    if (!data) {
      this.navCur = 0;
      this.$store.commit("manageMenuDefault", {
        BA_PATH: "/jz/XBM_Service.bsp?EXEC&Source=FORM[268].[50]&token=",
        Ba_Name: "待办工作",
      });
    } else if (data.BA_PATH.indexOf("FORM") == -1) {
      this.$router.push(data.BA_PATH);
    } else {
      this.$router.push({ path: "/manage" });
    }
    // this.getMenuList();
  },
  computed: {
    iframe_src() {
      return this.$store.state.approvalMenu.manageActive.BA_PATH;
    },
  },
  methods: {
    toIndex() {
      sessionStorage.removeItem("manageNav");
      this.$router.push({ path: "/" });
    },
  },
};
</script>

<style scoped lang="scss">
@import "~@/assets/scss/variables";
.header-box {
  position: relative;
  margin: 0 auto;
  height: 80px;
  overflow: hidden;
}
.header-box h3 {
  margin: 0 auto;
  height: 80px;
  line-height: 80px;
  color: $base-color;
  font-size: 32px;
  padding-left: 60px;
  background: url("../../../assets/logo.png") no-repeat left center;
  background-size: 60px 60px;
  float: left;
  cursor: pointer;
}
.login-box {
  float: right;
  line-height: 80px;
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
  height: calc(100% - 90px);
}
</style>
