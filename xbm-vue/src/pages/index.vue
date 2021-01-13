<template>
  <div class="main-box">
    <div>
      <div class="header-box header-box1">
        <h3>成都再减减企业管理服务有限公司</h3>
        <div class="user-box">
          <loginBox></loginBox>
        </div>
      </div>
      <div class="nav">
        <NavIn @clickit="getNav"></NavIn>
      </div>
      <div class="main">
        <home :is="currentView"></home>
        <vFooter></vFooter>
      </div>
      <!-- <msgTips :msgShow="msgShow" @closeMsg="closeMsg" @msgCount="msgCount"></msgTips> -->
    </div>
  </div>
</template>

<script>
import loginBox from "@/components/loginbox";
import * as dataService from "@/public/apiService/sysManagement/logMangement";
import { getUserInfo, removeToken } from "@/public/auth";
import NavIn from "@/components/Nav";
import Home from "@/pages/home/index";
import Notice from "@/pages/home/notice/index";
import News from "@/pages/home/news/news";
import Laws from "@/pages/home/laws/laws";
import Study from "@/pages/home/study/study";
import Application from "@/pages/application/index";
import Supervise from "@/pages/home/supervise/supervise";
import msgTips from "@/pages/home/HomeMsgTips";
import vFooter from "@/components/footer";
import PersonCenter from "@/pages/home/PersonCenter/index";
import Announcement from "@/pages/home/Announcement/index";
// import Home from "@/components/nav/home";
export default {
  name: "index",
  components: {
    loginBox,
    NavIn,
    Home,
    Notice,
    News,
    Laws,
    Study,
    Application,
    Announcement,
    Supervise,
    msgTips,
    vFooter,
    PersonCenter
  },
  data() {
    return {
      title: "成都再减减企业管理服务有限公司",
      username: "",
      currentView: "home",
      msgShow: false,
      msglength: 0,
      msgClose: "",
      msgLengthShow: ""
    };
  },
  created() {
    var navinfo = JSON.parse(sessionStorage.getItem("nav"));
    console.log(navinfo);
    if (navinfo) {
      this.currentView = navinfo.path;
    } else {
      this.currentView = "home";
    }
  },
  methods: {
    //   this.currentView = tabText;
    getNav(data, navIndex) {
      this.currentView = data.path;
      // console.log(navIndex);
    },
    closeMsg(data) {
      this.msgShow = data;
      sessionStorage.setItem("msgClose", true);
    },
    //判断消息提醒是否出现
    msgTipsShow() {
      //消息提醒数量存在则出现
      var msgClose = sessionStorage.getItem("msgClose");
      // if (this.msglength != 0 && msgClose == "false") {
      if (this.msglength != 0) {
        this.msgShow = true;
      } else {
        this.msgShow = false;
      }
      // console.log(this.msglength != 0, this.msgClose == 1);
    },
    msgCount(data) {
      this.msgClose = data.msgLengthShow;
      // console.log(data);
      this.msglength = data.length;
      this.msgLengthShow = data.msgLengthShow;
      this.msgTipsShow();
    }
  }
};
</script>

<style scoped lang="scss">
@import "~@/assets/scss/variables";
.main-box {
  width: 100%;
  min-height: 100%;
  background: #f2f2f2;
  position: absolute;
  overflow-y: scroll;
  overflow-x: hidden;
}
.main-box > div {
  width: 100%;
  height: 100%;
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
}
.header-box {
  width: 1200px;
  height: 90px;
  margin: 0 auto 6px;
  position: relative;
  h3 {
    float: left;
    line-height: 90px;
    padding-left: 90px;
    width: 742px;
    font-size: 40px;
    font-weight: bold;
    color: $base-color;
    // color: rgba(7, 67, 139, 1);
    text-shadow: 0px 2px 4px rgba(0, 0, 0, 0.2);
    background: url("~@/assets/logo.png") no-repeat left center;
  }
  .user-box {
    position: absolute;
    right: 0;
    top: 50%;
    transform: translateY(-50%);
    width: 380px;
    text-align: right;
  }
}
.nav {
  background: $base-color;
  height: 60px;
  line-height: 60px;
}
.main {
  width: 1200px;
  margin: 0 auto;
  min-height: calc(100% - 160px);
  background: #fff;
  padding-bottom: 96px;
  position: relative;
  & > div:nth-of-type(1) {
    width: 100%;
    min-height: 100%;
  }
}
</style>
