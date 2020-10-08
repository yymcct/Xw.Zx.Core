<template>
  <div class="wrapper">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>
    <div class="container">
      <div
        class="uni-triplex-row crd card"
        v-for="iteam in incomeDetail"
        v-bind:key="iteam.id"
      >
        <div class="uni-triplex-left">
          <div class="uni-title uni-ellipsis">
            {{ iteam.addTime }} {{ iteam.incomeAccountTypeName }}
          </div>
          <text class="uni-text"
            >状态:{{ iteam.withdrawDepositStateName }}</text
          >
          <div
            v-if="iteam.withdrawDepositState == 3"
            class="uni-title uni-ellipsis"
          >
            <text>备注: {{ iteam.remark }}</text>
          </div>
        </div>
        <div class="uni-triplex-right">
          <text class="amount">{{ iteam.amount }}</text>
        </div>
      </div>
       <div v-if="incomeDetail.length == 0">您还没有提现</div>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "",
  props: [""],
  data() {
    return {
      incomeDetail: [],
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    api.withdrawDeposit.getDetails().then((res) => {
      this.incomeDetail = res.data.result;
    });
  },

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.heard {
  height: 200px;
  background-color: white;
  border-radius: 10px;
  margin: 10px;
  display: -webkit-flex;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 10px;
}
.card {
  margin-bottom: 20px;
  background-color: white;
}
.btn {
  height: 150px;
  padding-left: 20px;
  padding-right: 20px;
}
.tbn button {
  width: 80%;
}
.title {
  font-weight: bold;
  font-size: 20px;
}
.total {
  font-weight: bold;
  font-size: 50px;
  margin-left: 50px;
  color: rgb(250, 81, 2);
}
.zhuixi {
  display: flex;
  flex-direction: row-reverse;
  justify-content: flex-start;
}
.zhuixi button {
  margin-top: 10px;
  display: block;
  margin: 0px;
}
.amount {
  font-size: 30px;
  font-weight: bolder;
  margin-left: 5px;
  margin-right: 5px;
  color: coral;
}
</style>