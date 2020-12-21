<template>
  <div id="app">
    <router-view />
  </div>
</template>

<script>
export default {
  name: "App",
  created(){
    
  }
};
</script>

<style lang='scss'>
@import "~@/assets/scss/base";
@import "~@/assets/scss/common";
#app {
  height: 100%;
  position: relative;
  overflow: hidden;
  color: #333333;
  font: 16px/1.4 "Roboto", "-apple-system", "BlinkMacSystemFont",
    "Helvetica Neue", "Segoe UI", "Oxygen", "Ubuntu", "Cantarell", "Open Sans",
    sans-serif;
  /* font:16px/1.5; */
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  background: #f2f2f2;
  .el-breadcrumb {
    height: 45px;
    line-height: 45px;
    font-size: 15px;
    padding-left: 10px !important;
  }
}
</style>