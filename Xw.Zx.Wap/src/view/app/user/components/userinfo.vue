<template>
  <div class="wrapper">
    <div class="left">
      <p class="name">{{ user.realName ? user.realName : "未填写姓名" }}</p>
      <p class="vip">级别: {{ user.memberVipTypeName }}</p>
      <p class="time">注册时间: {{ user.createDate }}</p>
    </div>
    <div class="right">
      <p class="first">一级: {{ user.firstChildCnt }}</p>
      <p class="second">二级: {{ user.secondChildCnt }}</p>
    </div>
  </div>
</template>

<script>
export default {
  name: "",
  props: {
    user: Object,
  },
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
    margin-top: 10px;
  padding: 15px;
  background-color: #fff;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  .left {
    width: 70%;
    .name {
      font-weight: bold;
      font-size: 18px;
    }
    .vip {
      margin-top: 15px;
      font-size: 16px;
      color: #999;
    }
    .time {
      margin-top: 10px;
      font-size: 14px;
      color: #999;
    }
  }
  .right {
    width: 30%;
    font-size: 16px;
    color: #999;
    .first{
        margin-top: 15px;
    }
    .second{
        margin-top: 10px;
    }
  }
}
</style>