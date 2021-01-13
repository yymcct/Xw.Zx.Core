<template>
  <div class="lis-box">
    <ul>
      <li v-for="(item,index) in lists" :key="index">{{item.text}}</li>

      <!-- <li>国家级法律法规</li> -->
    </ul>
  </div>
</template>

<script>
export default {
  name: "Partright",
  data() {
    return {
      lists: [
        { text: "国家级法律法规" },
        { text: "部门和地方性相关政策" },
        { text: "部门和地方性相关规章" },
        { text: "部门和地方性相关办法" },
        { text: "重要法律法规解读" },
      ],
    };
  },
};
</script>

<style lang="scss" scoped>
.lis-box {
  height: 55px;
  line-height: 55px;
  li {
    width: 49%;
    background: #f0f0f0;
    float: left;
    margin: 0 0 13px 2%;
    padding-left: 45px;
    box-sizing: border-box;
  }
  li:nth-of-type(2n + 1) {
    margin-left: 0;
  }
}
</style>