<template>
  <div class="wrapper">
    <van-nav-bar
      :title="$route.meta.title"
      left-arrow
      @click-left="$router.go(-1)"
    />
    <img :src="user.zxQRCode" alt="">
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "",
  props: [""],
  data() {
    return {};
  },

  components: {},

  computed: {
    ...mapGetters({
      user: "user/user",
    }),
  },

  beforeMount() {},

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper{
    min-height: 100vh;
    background-color: #fff;
    img{
        width: 100%;
    }
}
</style>