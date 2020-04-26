<template>
  <div id="app">
    <!-- <keep-alive include="expoCompany,expoProduct,expoShortmsg,expoLive,expoUser">
      <router-view  />
    </keep-alive>-->
    <transition name="router-fade" mode="out-in">
      <keep-alive>
        <router-view v-if="$route.meta.keepAlive"></router-view>
      </keep-alive>
    </transition>
    <transition name="router-fade" mode="out-in">
      <router-view v-if="!$route.meta.keepAlive"></router-view>
    </transition>
  </div>
</template>

<script>
export default {
  name: "app",
  components: {},
  created() {
  
  },
  mounted() {}
};
</script>

<style>
@import "common/scss/index.scss";
body {
  font-size: 16px;
  background-color: #f8f8f8;
  -webkit-font-smoothing: antialiased;
  margin: 0;
  padding: 0;
}

[v-cloak] {
  display: none;
}
.router-fade-enter-active,
.router-fade-leave-active {
  transition: opacity 0.3s;
}
.router-fade-enter,
.router-fade-leave-active {
  opacity: 0;
}
</style>
