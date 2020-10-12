<template>
  <div class="wrapper"  >
    <van-popup v-model="show" position="top" safe-area-inset-bottom class="pop">
      <div class="content">
        <img :src="require('@/assets/images/live_weixin.png')" />
      </div>
    </van-popup>
  </div>
</template>

<script>
export default {
  name: "share",
  props: {
    show: Boolean
  },
  data() {
    return {
    
    };
  },

  methods: {

  },

  watch: {

  }
};
</script>
<style lang='scss' scoped>
.wrapper {
  .pop {
       background-color: rgba(0, 0, 0, 0.1);
    .content {
     
      display: flex;
      flex-direction: column;
       align-items: center;
      height: 100vh;
      overflow: hidden;
      img {
        padding-top: 20px;
        width: 80%;
        height: auto;
      }
    }
  }
}
</style>