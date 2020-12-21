<template>
  <!-- 建设工程规划核实意见书 -->
  <div class="sub-content" v-if="detail">
    <div class="big-title">{{detail.jsgcmc}}</div>
    <div class="content">
      <span class="label-text label-1">{{detail.licenseNumber}}</span>
      <span class="label-text label-2">{{detail.jsdw}}</span>
      <span class="label-text label-3">{{detail.jsgcmc}}</span>
      <span class="label-text label-4">{{detail.issueunitname}}</span>
      <span class="label-text label-5">{{detail.issueDate}}</span>
    </div>
  </div>
</template>

<script>
export default {
  name: "right1",
  components: {},
    props:['data'],
  data() {
    return {
      detail: {},
      loading: false
    };
  },
  mounted() {
    this.detail=this.data;
  },
  methods: {
  
  }
};
</script>

<style  lang="scss" scoped>
.sub-content {
  color: #666;
  font-family: simsun;
  .big-title {
    padding: 10px 0;
    font-size: 1.5em;
    text-align: center;
    color: #333;
    font-family: "Microsoft Yahei";
  }
  .content {
    position: relative;
    background: url("~@/assets/images/certificates/11410800MB18523953004.jpg")
      no-repeat;
    width: 848px;
    height: 584px;
    margin: 0 auto;
    .label-text {
      font-size: 14px;
      position: absolute;
      font-weight: bolder;
    }
    .label-1 {
      width: 100px;
      top: 124px;
      left: 204px;
      text-align: center;
    }
    .label-2 {
      width: 210px;
      top: 243px;
      left: 155px;
    }
    .label-3 {
      width: 210px;
      top: 297px;
      left: 156px;
    }
    .label-4 {
      width: 130px;
      top: 475px;
      left: 241px;
    }
    .label-5 {
      width: 127px;
      top: 510px;
      left: 243px;
    }
  }
}
</style>