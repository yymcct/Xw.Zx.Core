<template>
  <div>
    <van-nav-bar
      :title="$route.meta.title"
      left-arrow
      @click-left="$router.go(-1)"
    />
    <div class="banner">
      <img
        :src="require('@/assets/images/home/banner.png')"
        style="display: block; width: 100%; height: auto"
      />
    </div>
    <kefu />
  </div>
</template>

<script>
import kefu from "@/components/kefu"
export default {
  name: "",
  props: [""],
  data() {
    return {};
  },

  components: {kefu},

  computed: {},

  beforeMount() {},

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='' scoped>
</style>