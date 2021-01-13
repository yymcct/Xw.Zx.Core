
<template>
  <!--重点专项督办-->
  <div style="background:#f2f2f2">
    <div class="header-box header-box6">
      <h3 @click="toIndex">成都再减减企业管理服务有限公司</h3>
      <loginBox class="login-box" :notHome="true"></loginBox>
    </div>
    <div class="nav">
      <NavIn @clickit="getNav" :navList="navList" :navCur="navCur"></NavIn>
    </div>
    <div class="main">
      <Breadcrumb :title="breadcrumbItem"></Breadcrumb>
      <div class="left-menu">
        <LeftMenu :menuList="menuList" :menutitle="menutitle"></LeftMenu>
      </div>
      <div class="right-box">
        <KeyTable></KeyTable>
      </div>
    </div>
  </div>
</template>

<script>
import loginBox from "@/components/loginbox";
import NavIn from "@/components/CusNav";
import Breadcrumb from "@/components/breadcrumb";
import LeftMenu from "@/pages/application/manage/manageMenu";
import KeyTable from "@/pages/application/keyspecial/keyTable";
import { getToken } from "@/public/auth";
export default {
  name: "manage",
  components: {
    loginBox,
    NavIn,
    Breadcrumb,
    LeftMenu,
    KeyTable
  },
  data() {
    return {
      currentView: "approval",
      navList: [
        {
          tag: "个人待办",
          path: "waitting"
        },
        {
          tag: "项目审批",
          path: "notice"
        },
        {
          tag: "查询统计",
          path: "news"
        },
        {
          tag: "用地红线管理",
          path: "approval"
        }
      ],
      navCur: 0,
      breadcrumbItem: "重点事项督办",
      menuList: [
        {
          id: "1",
          text: "会员权限",
          path: ""
        },
        {
          id: "2",
          text: "消息中心",
          path: "notice"
        },
        {
          id: "3",
          text: "待处理",
          path: "notice"
        },
        {
          id: "4",
          text: "处理中",
          path: "notice"
        },
        {
          id: "5",
          text: "已结束",
          path: "notice"
        }
      ],
      menutitle: "个人代办",
      token: getToken()
    };
  },
  methods: {
    
 toIndex() {
      this.$router.push({ path: "/" });
    },
    //   this.currentView = tabText;
    getNav(data) {
      if (data.tag != "效能中心" && data.tag != "审批中心") {
        return;
      }
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
  .left-menu {
    float: left;
    width: 278px;
    box-shadow: -2px 2px 3px 0px rgba(0, 0, 0, 0.15);
    border-top: 3px solid rgba(7, 67, 139, 1);
  }
  .right-box {
    width: 900px;
    float: right;
  }
}
</style>