<template>
  <div id="app">
    <router-view />
    
  </div>
</template>

<script>
export default {
  name: "app",
  components: {}
};
</script>

<style lang="scss">
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
</style>
