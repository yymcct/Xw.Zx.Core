<template>
  <div>
    <el-menu
      default-active="1"
      :default-openeds="openeds"
      class="el-menu-vertical-demo"
      @open="handleOpen"
      @close="handleClose"
    >
      <el-submenu index="1">
        <template slot="title">
          <i class="el-icon-location"></i>
          <span>个人中心</span>
        </template>
        <el-menu-item index="1-1">管理预约</el-menu-item>
        <el-menu-item index="1-2">交易记录</el-menu-item>
      </el-submenu>
      <el-menu-item index="2">
        <i class="el-icon-menu"></i>
        <span slot="title">查询信息</span>
      </el-menu-item>
      <el-menu-item index="3">
        <i class="el-icon-document"></i>
        <span slot="title">修改文字</span>
      </el-menu-item>
      <el-submenu index="4">
        <template slot="title">
          <i class="el-icon-setting"></i>
          <span>账户设置</span>
        </template>
        <el-menu-item index="4-1">企业信息设置</el-menu-item>
        <el-menu-item index="4-2">账户安全设置</el-menu-item>
        <el-menu-item index="4-3">我的银行卡</el-menu-item>
      </el-submenu>
    </el-menu>
  </div>
</template>

<script>
export default {
  name: "left",
  data() {
    return {
      openeds: ["1", "4"]
    };
  },
  methods: {
    handleOpen(key, keyPath) {
      this.openeds.push(key);
    },
    handleClose(key, keyPath) {
      //console.log(key, keyPath);
    }
  }
};
</script>

<style lang="scss" scoped>
@import "~@/assets/scss/variables";
.el-menu-item,
>>> .el-submenu__title {
  height: 34px;
  line-height: 34px;
  color: #93a0b0;
  padding-left: 5px !important;
  i {
    margin-right: 0;
    font-size: 16px;
  }
}
.el-submenu {
  .el-menu-item {
    height: 42px;
    line-height: 42px;
    color: #333333;
    padding-left: 30px !important;
  }
}
>>> .el-submenu__title {
  color: #93a0b0;
  .el-submenu__icon-arrow {
    display: none !important;
  }
}
>>> .el-submenu__title:hover,
.el-menu-item:hover,
.el-menu-item.is-active {
  background: url("../../../../assets/submenu_bg.png") no-repeat;
  background-size: cover;
  span {
    color: #fff;
  }
  i {
    color: #fff;
    margin-right: 0;
  }
}
.el-submenu {
  .el-menu-item.is-active,
  .el-menu-item:hover {
    background: transparent;
    color: $base-color;
    font-weight: 600;
  }
}
.el-submenu.is-opened.is-active {
  background: url("../../../../assets/submenu_bg.png") no-repeat;
  background-size: contain;
  >>> .el-submenu__title {
    color: #fff;
  }
  i {
    color: #fff;
    margin-right: 0;
  }
}
</style>