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
      <div class="card" v-for="(iteam, index) in incomeDetail" :key="index">
        <div class="card-left">
          <p class="card-left-title">{{ iteam.addTime }}</p>
           <p class="card-left-content">单号: {{ iteam.timestamp }}</p>
          <p class="card-left-content">
            状态:{{ iteam.withdrawDepositStateName }}
          </p>
          <div
            v-if="iteam.withdrawDepositState == 3"
            class="uni-title uni-ellipsis"
          >
            <text>备注: {{ iteam.remark }}</text>
          </div>
        </div>
        <div class="card-right">
          <span class="amount">{{ iteam.amount }}</span>
        </div>
      </div>
      <div class="none" v-if="incomeDetail.length == 0">--您还没有提现--</div>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "ddd",
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
      this.incomeDetail = res.result;
    });
  },

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.card {
  margin-bottom: 10px;
  background-color: white;
  padding: 10px;
  font-size: 16px;
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-start;
  height: 80px;
  &-left {
    width: calc(100% - 100px);
    &-title {
      font-weight: bold;
    }
    &-content {
      margin-top: 10px;
      color: #666;
      line-height: 22px;
    }
  }
  &-right {
    width: 100px;
    height: 100%;
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    align-items: center;
    span {
      font-size: 30px;
      font-weight: bolder;
      margin-left: 5px;
      margin-right: 5px;
      color: #ff5000;
    }
  }
}

.none {
  font-size: 14px;
  text-align: center;
  color: #999999;
  margin-top: 20px;
}
</style>