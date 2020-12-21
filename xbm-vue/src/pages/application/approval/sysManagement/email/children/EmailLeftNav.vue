<template>
  <div class="leftNav" ref="leftNav">
    <el-menu
      :default-active="activeSideName"
      class="el-menu-vertical-demo ln-menu"
      @open="handleOpen"
      @close="handleClose"
      @select="selectNav"
      :collapse="isCollapse"
    >
      <el-submenu index="1">
        <template slot="title">
          <i class="el-icon-menu"></i>
          <span slot="title">邮件箱</span>
        </template>
        <el-menu-item index="inBox">
          <i class="el-icon-message"></i>
          <span slot="title">收件箱</span>
          <!-- <i @click="getInBoxList('inBox')" class="el-icon-search"></i> -->
          <el-badge class="mark ln-badge" :value="num !== 0 ? num : ''"
        /></el-menu-item>
        <el-menu-item index="draft"
          ><i class="el-icon-document"></i> <span slot="title">草稿箱</span>
          <!-- <i @click="getDraftList('draft')" class="el-icon-search"></i> -->
        </el-menu-item>
        <el-menu-item index="outBox"
          ><i class="el-icon-upload"></i> <span slot="title">已发送</span>
          <!-- <i @click="getOutBoxList('outBox')" class="el-icon-search"></i> -->
        </el-menu-item>
      </el-submenu>
      <!-- <el-submenu index="2">
    <template slot="title">
      <i class="el-icon-news"></i>
      <span slot="title">邮件智能分类</span>
    </template>
     <el-menu-item index="tagsCloud">
         <i class="el-icon-star-off"></i>
        <span slot="title">关键词Tag云图</span>
       </el-menu-item>
     <el-menu-item index="2-2">
         <i class="el-icon-star-off"></i>
        <span slot="title">日程</span>
       </el-menu-item>
 </el-submenu> -->
      <i
        class="nav-right-line"
        :class="isCollapse ? 'el-icon-caret-right' : 'el-icon-caret-left'"
        @click="handleCollapse"
      ></i>
    </el-menu>
  </div>
</template>
<script>
// import {getUnreadNum} from "@/public/apiService/email/email";
export default {
  name: "email",
  data() {
    return {
      // num:0
      // isCollapse: false
    };
  },
  computed: {
    isCollapse: function() {
      return this.$store.state.email.isCollapse;
    },
    activeSideName: function() {
      return this.$store.state.email.activeSideName;
    },
    num: function() {
      return this.$store.state.email.unReadNum;
    }
  },
  created: function() {
    this.getUnReadNum();
    console.log(this.$store);
  },
  methods: {
    // getDraftList(val) {
    //   console.log(val);
    //   this.$store.commit("searchEmail", val);
    // },
    // getInBoxList(val) {
    //   console.log(val);
    //   this.$store.commit("searchEmail", val);
    // },
    // getOutBoxList(val) {
    //   console.log(val);
    //   this.$store.commit("searchEmail", val);
    // },
    handleOpen(key, keyPath) {
      // console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      // console.log(key, keyPath);
    },
    getUnReadNum: function() {
      this.$store.dispatch("getUnReadNums");
    },
    selectNav(index) {
      this.$store.commit("curSideName", index);
      this.$emit("searchClose");
    },
    handleCollapse: function() {
      this.$store.commit("changeCollapse");
    }
  }
};
</script>
<style lang="scss" scoped>
.el-menu-item {
  position: relative;
  .el-icon-search {
    position: absolute;
    margin-top: 15px;
    right: 0;
    color: red;
    z-index: 999;
  }
}

.leftNav {
  height: 100%;
  .el-menu-vertical-demo:not(.el-menu--collapse) {
    width: 220px;
  }
  .ln-menu {
    height: 100%;
    position: relative;
    overflow-y: auto;
    overflow-x: hidden;
    .nav-right-line {
      position: absolute;
      right: -8px;
      font-size: 24px;
      top: 200px;
      cursor: pointer;
    }
    .el-icon-caret-right {
      left: -8px;
    }
    .ln-badge {
      float: right;
    }
  }
}
</style>
