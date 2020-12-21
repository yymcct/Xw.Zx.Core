<template>
  <el-carousel height="400px">
    <el-carousel-item v-for="(item,index) in items" :key="index">
      <img :src="item.src" alt />
    </el-carousel-item>
  </el-carousel>
</template>

<script>
export default {
  name: "carousel",
  data() {
    return {
      items: [
        {
          src: require("@/assets/images/index/img1.png")
        },
        {
          src: require("@/assets/images/index/img2.png")
        },
        {
          src: require("@/assets/images/index/img3.png")
        },
        {
          src: require("@/assets/images/index/img4.png")
        }
      ]
    };
  }
};
</script>

<style lang="scss" scoped>
img {
  width: 100%;
  height: 100%;
}
</style>