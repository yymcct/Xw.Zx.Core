<template>
  <div class="wrapper">
    <div
      class="wrapper-bg"
      :style="{backgroundImage:'url('+require('../../assets/images/computer/bg2.png')+') '}"
    >
      <div class="block">
        <div class="block-title">
          <img :src="require('@/assets/images/computer/title2.png')" alt />
        </div>
        <div class="block-content">
            
        </div>
      </div>
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
.wrapper {
  width: 100%;
  min-height: 100vh;
  background-size: 100%;
  background-repeat: no-repeat;
  background: linear-gradient(0deg, rgb(69, 69, 241) 0%, rgb(20, 28, 200) 100%);
  &-bg {
    width: 100%;
    min-height: 100vh;
    background-size: 100%;
    background-repeat: no-repeat;
    padding-top: 70px;
    padding-bottom: 20px;
    .block {
      &-title {
        margin: 0 70px;
        img {
          width: 100%;
        }
      }
    }
  }
}
</style>