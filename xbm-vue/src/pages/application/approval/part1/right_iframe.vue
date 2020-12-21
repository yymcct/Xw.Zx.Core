<template>
  <div class="iframe-box">
    <!-- 'http://192.168.1.254:9001'+ -->
    <iframe
      class="iframe-content"
      :src="iframe_src+token"
      width="100%"
      height="100%"
      frameborder="0"
      style="display: block;"
    ></iframe>
    <!-- 
      解决mousemove事件遇见iframe卡顿问题
      1.在iframe上层加透明盒子，当mousedown 的时候显示透明盒子，mouseup的事件隐藏 
      2.给iframe加样式pointer-events:none;适用于iframe中没有任何鼠标事件 
    -->
    <div class="transparent-box" :class="ismouseDown?'':'none'"></div>
  </div>
</template>

<script>
import { getToken } from "@/public/auth";
export default {
  name: "right",
  props: ["iframe_src", "ismouseDown"],
  data() {
    return {
      token: getToken()
    };
  },
  computed: {},
  created() {},
  methods: {}
};
</script>

<style lang="scss" scoped>
.iframe-box {
  height: calc(100% - 45px);
  width: 100%;
  position: relative;
  .transparent-box {
    position: absolute;
    top: 0;
    width: 100%;
    height: 100%;
  }
  .transparent-box.none {
    display: none;
  }

  .iframe-content {
    width: 100%;
    // height: calc(100% - 45px);
  }
}
</style>