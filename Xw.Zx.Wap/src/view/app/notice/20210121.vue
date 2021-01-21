<template>
  <div class="banner">
    <van-nav-bar
      :title="$route.meta.title"
      left-arrow
      @click-left="$router.go(-1)"
    />
    <div>
      <img src="/UpLoad/jpg/2020-01-20/1.jpg" alt="" />
    </div>
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

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.banner{
    min-height: 100vh;
    img{
        width: 100%;
    }
}
</style>