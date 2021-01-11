<template>
  <div class="wrapper">
    <a href="http://139.155.8.217:8081/zzz20201012.apk" download="zzz.apk">
    <img :src="require('@/assets/images/bk2.jpg')" alt="">
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
.wrapper {
  height: 100vh;
  overflow: hidden;
  .title {
    padding-top: 50px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    color: #fff;
    h1 {
      font-size: 28px;
      font-weight: bold;
      line-height: 44px;
    }
    p {
      font-size: 20px;

      line-height: 38px;
    }
  }
  .download {
    padding-top: 50px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    a {
      background-color: #fff;
      padding: 20px;
      border-radius: 50px;
      color: #ff5000;
      font-weight: bold;
    }
  }
  .desc {
    margin: 50px 20px 20px 20px;
    background-color: #fff;
    border-radius: 20px;
    padding: 20px;
    font-size: 16px;
    span {
      color: red;
    }
    ul {
      padding: 10px;
      padding-left: 20px;

      li {
        list-style-type: disc;
        line-height: 24px;
      }
    }
  }
}
</style>