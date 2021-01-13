<template>
  <div class="wrapper1">
    <a href="http://139.155.8.217:8081/zzz20201012.apk" download="zzz.apk">
      <img :src="require('@/assets/images/bk2.jpg')" alt="" />
    </a>
    <share :show="isWeiXin" />
  </div>
</template>

<script>
import share from "@/components/share";
export default {
  name: "",
  props: [""],
  data() {
    return {
      isWeiXin: false,
    };
  },

  components: { share },

  computed: {},

  beforeMount() {
    var ua = window.navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == "micromessenger") {
      this.isWeiXin = true;
    }
  },

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper1 {
  min-height: 100vh;
  background-color: rgb(233, 114, 24);
  overflow: hidden;
  a {
    img {
      width: 100%;
    }
  }
}
</style>