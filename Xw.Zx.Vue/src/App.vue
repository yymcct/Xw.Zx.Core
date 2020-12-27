<template>
  <div id="app">
    <router-view />
  </div>
</template>

<script>
export default {
  name: "app",
  created() {
    var user = sessionStorage.getItem("user");
    console.log('123',user);
    if (user) {
      user = JSON.parse(user);
      this.$store.commit("user/setUser", user);
    }
  },
};
</script>
<style lang="scss">
body {
  margin: 0px;
  padding: 0px;
  /*background: url(assets/bg1.jpg) center !important;
		background-size: cover;*/
  // background: #1F2D3D;
  font-family: Helvetica Neue, Helvetica, PingFang SC, Hiragino Sans GB,
    Microsoft YaHei, SimSun, sans-serif;
  font-size: 14px;
  -webkit-font-smoothing: antialiased;
}

#app {
  position: absolute;
  top: 0px;
  bottom: 0px;
  width: 100%;
}

.el-submenu [class^="fa"] {
  vertical-align: baseline;
  margin-right: 10px;
}

.el-menu-item [class^="fa"] {
  vertical-align: baseline;
  margin-right: 10px;
}

.toolbar {
  background-color: #ff51000a;
  padding: 10px;
  margin: 10px 0px;
  .el-form-item {
    margin-bottom: 5px;
  }
}

.fade-enter-active,
.fade-leave-active {
  transition: all 0.2s ease;
}

.fade-enter,
.fade-leave-active {
  opacity: 0;
}
</style>
<style  >
.el-table--enable-row-hover .el-table__body tr:hover > td {
  background-color: #ff510015 !important;
}
/* .el-pagination .el-pager li:not(.active) {
  background-color: #06a65e10 !important;
} */
/* .el-pagination .btn-prev {
  background-color: #06a65e10 !important;
} */
/* .el-pagination .btn-next {
  background-color: #06a65e10 !important;
} */
.el-image-viewer__close .el-icon-circle-close {
  color: #fff;
}
</style>