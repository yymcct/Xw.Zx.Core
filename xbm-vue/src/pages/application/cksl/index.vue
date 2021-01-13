
<template>
  <!--窗口受理-->
  <div class="cksl" style="background:#f2f2f2">
    <div class="header-box header-box4">
      <h3 @click="toIndex">成都再减减企业管理服务有限公司</h3>
      <loginBox class="login-box" :notHome="true"></loginBox>
    </div>
    <div class="nav box">
      <ul class="ckcl-box">
      <li
        v-for="(item, index) in navList"
        :class="{ active: navCur == index }"
        :key="index"
        @click="getNav(index, item)">
        <span>{{ item.Ba_Name }}</span>
      </li>
    </ul>
    </div>
    <div class="main">
      <div class="left-menu">
        <ul class="el-menu-vertical-demo el-menu">
          <li
            class="el-menu-item"
            :class="idx==curMenuItem.id?'is-active':''"
            v-for="(item,idx) in currentView.children"
            :key="idx"
            @click="ChangePage(item)"
          >
            <span>{{item.Ba_Name}}</span>
          </li>
        </ul>
        <!-- <LeftMenu :menuList="menuList" :menutitle="menutitle"></LeftMenu> -->
      </div>
      <div class="right-box">
        <router-view/>
      </div>
    </div>
  </div>
</template>
<script>
// import HeaderIn from "@/components/Header";
import { getToken } from "@/public/auth";
import loginBox from "@/components/loginbox";
import Breadcrumb from "@/components/breadcrumb";
import LeftMenu from "@/pages/application/manage/manageMenu";
// import ComTable from "@/pages/application/cksl/children/comTable";
// import bus from "@/public/event.js";
// import KeyTable from "@/pages/application/keyspecial/keyTable";
// import Home from "@/components/nav/home";
export default {
  name: "manage",
  components: {
    // HeaderIn,
    loginBox,
    // NavIn,
    Breadcrumb,
    LeftMenu
    // ComTable
    // KeyTable
  },
  data() {
    return {
      title: "自然资源和规划局窗口受理",
      subtitle:
        "Key special supervision system of natural resources and Planning Bureau",
      // currentView: "approval",
      navList: [
        {
          Ba_Name: "收件管理",
          path: "/cksl/receiptManage",
          children: [
            // {
            //   id: 0,
            //   Ba_Name: "收件",
            //   path: "/cksl/receipt"
            // },
            {
              id: 0,
              Ba_Name: "工改事项接件",
              path: "/cksl/UnifiedAcceptance"
            }, {
              id: 1,
              Ba_Name: "其他事项接件",
              path: "/cksl/powerOperation"
            }
          ]
        },
        {
          Ba_Name: "办结管理",
          path: "/cksl/DoneBusiness",
          children: [
            {
              id: 0,
              Ba_Name: "已办业务",
              path: "/cksl/DoneBusiness"
            },
            {
              id: 1,
              Ba_Name: "补齐补正业务",
              path: "/cksl/BackBusiness"
            }
          ]
        },
        {
          Ba_Name: "统计分析",
          path: "/cksl/dayCount",
          children: [
            {
              id: 0,
              Ba_Name: "办件日统计",
              path: "/cksl/dayCount"
            },
            {
              id: 1,
              Ba_Name: "办件月统计",
              path: "/cksl/monthCount"
            }
          ]
        }
      ],
      navCur: 0,
      currentView: null,
      curMenuItem: null
    };
  },
  created() {
    this.reloadPage();
  },
  watch:{
    '$route':function(val){
      this.initPage(val.path);
    }
  },
  methods: {
    reloadPage:function(){
      var path=window.location.href.split('#')[1];
      this.initPage(path);
    },
    initPage:function(path){
       this.navList.forEach((item,idx)=>{
        item.children.forEach(child=>{
          if(child.path==path){
            this.navCur=idx;
             this.currentView = item;
             this.curMenuItem =child
          }
        })
      })
    },
    toIndex() {
      this.$router.push({ path: "/" });
    },
    getNav(index,item) {
       this.navCur=index;
       this.currentView = item;
       this.curMenuItem = item.children[0];
       this.$router.push({
        path: item.path
      });
    },
    ChangePage: function(item) {
      this.curMenuItem = item;
      this.$router.push({
        path: item.path
      });
    }
  }
};
</script>

<style lang='scss' scoped>
@import "~@/assets/scss/variables";
.cksl {
  width:100%;
  height:100%;
  // overflow: auto;
  .ckcl-box {
    width: 70%;
    margin: 0 auto;
  }
  .nav {
  background: $base-color;
  height: 50px;
  line-height: 50px;
}
  .box {
  // height: 100%;
  color: #fdfefe;
  font-size: 16px;
  font-weight: 600;
  ul {
    width: 100%;
    padding: 0 20px;
    margin: 0 auto;
    height: 100%;
    display: flex;
    justify-content: space-between;
  }
  li {
    text-align: center;
    cursor: pointer;
    flex: 1;
    font-size: 18px;
    span {
      display: inline-block;
      height: 24px;
      line-height: 24px;
      width: 100%;
      border-right: 1px solid #f2f2f2;
    }
  }
  li.active {
    background: #e1e3f1;
    // background: #f2f2f2;
    color: $base-color;
    span {
      border-right: 0;
    }
  }
  li:last-of-type span {
    border-right: 0;
  }
}
}
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

>>> .main {
  width: 100%;
  margin: 0 auto;
  height: calc(100% - 130px);
  overflow: hidden;
  background: #fff;
  padding: 10px 20px;
  .left-menu {
    float: left;
    width: 278px;
    // margin-top:20px;
    box-shadow: -2px 2px 3px 0px rgba(0, 0, 0, 0.15);
    border-top: 3px solid rgba(7, 67, 139, 1);
     .el-menu-item {
      text-align: center;
      font-size: 16px;
      color: #4c4948;
      border-bottom: 1px solid #edecec;
      &.is-active {
        color: #07438b;
        font-weight: bold;
      }
    }
  }

  .right-box {
    width: calc(100% - 300px);
    float: right;
    height:100%;
  }
  .breadcrumb-box {
    padding-left: 15px;
  }
  .el-breadcrumb {
    line-height: 60px;
    font-size: 14px;
  }
}
</style>



