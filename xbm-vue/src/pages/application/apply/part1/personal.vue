<template>
  <div class="personal">
    <div class="left">
      <Left></Left>
    </div>
    <div class="right-box">
      <Right></Right>
    </div>
  </div>
</template>

<script>
import Left from "@/pages/application/apply/part1/left";
import Right from "@/pages/application/apply/part1/right";
export default {
  name: "Personal",
  components: {
    Left,
    Right
  },
  data() {
    return {};
  },
  methods: {}
};
</script>

<style lang="scss" scoped>
div.personal {
  height: 100%;
  padding-top: 30px;
  padding-bottom: 60px;
  .left {
    width: 160px;
    float: left;
    overflow: hidden;
    padding-top: 20px;
    padding-bottom: 60px;
    background: #fff;
  }

  .right-box {
    width: 1010px;
    height: 100%;
    float: right;
    background: #fff;
  }
}
</style>