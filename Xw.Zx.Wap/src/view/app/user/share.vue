<template>
  <div class="wrapper">
    <img :src="require('@/assets/images/bk1.jpg')" alt="" />
  </div>
</template>

<script>
export default {
  name: "",
  props: [""],
  data() {
    return {};
  },

  components: {},

  computed: {},

  beforeMount() {},

  mounted() {
    setTimeout(() => {
      this.$toast("请截图分享,并引导下载");
    }, 1500);
  },

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  overflow: hidden;
  img {
    width: 100%;
  }
  min-height: 100vh;
  background-color: rgb(233, 114, 24);
}
</style>