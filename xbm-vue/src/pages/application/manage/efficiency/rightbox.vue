<template>
  <div class="box">
    <div class="echerts-box top">
      <Echarts1></Echarts1>
    </div>
    <div class="echerts-box bottom">
      <Echarts2></Echarts2>
    </div>
  </div>
</template>

<script>
import Echarts1 from "@/pages/application/manage/efficiency/echarts1";
import Echarts2 from "@/pages/application/manage/efficiency/echarts2";
export default {
  name: "rightbox",
  components: {
    Echarts1,
    Echarts2
  },
  data() {
    return {};
  },
  methods: {}
};
</script>

<style lang="scss" scoped>
.box {
  width: 100%;
  .echerts-box {
    border-top: 3px solid #07438b;
    box-shadow: 0px 2px 3px 0px rgba(0, 0, 0, 0.15);
    height: 363px;
    margin-bottom: 21px;
  }
}
</style>