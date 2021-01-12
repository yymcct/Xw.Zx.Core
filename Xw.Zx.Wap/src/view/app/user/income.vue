<template>
  <div class="wrapper" v-if="incomeInfo">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>
    <div class="container">
      <div class="head">
        <div class="head-invite">累计收入(元)</div>
        <div class="head-income">
          <div class="head-income-amount">{{ incomeInfo.incomTotal }}</div>
        </div>
        <div class="head-invite">当前可提现金额: {{ incomeInfo.canGet }}</div>
        <div></div>
      </div>
      <div class="list">
        <van-cell-group>
          <van-cell title="收益详情" is-link url="/sqb/user/incomelist" />
          <van-cell
            title="提现详情"
            is-link
            url="/sqb/user/withdrawdepositlist"
          />
        </van-cell-group>
      </div>
      <div class="foot">
        <van-button
          class="foot-btn"
          type="primary"
          round
          color="linear-gradient(to right, #ff7a00, #ff5000)"
          @click="tixian"
        >
          我要提现
        </van-button>
      </div>
    </div>
    <share-profit v-model="showShareProfit" />
  </div>
</template>

<script>
import api from "@/api/sqbApi";
import shareProfit from "./components/shareProfit";
export default {
  name: "income",
  props: [""],
  data() {
    return {
      incomeInfo: null,
      showShareProfit: false,
    };
  },

  components: { shareProfit },

  computed: {},

  beforeMount() {
    api.income.getIncomeInfo().then((res) => {
      this.incomeInfo = res.result;
    });
  },

  mounted() {},

  methods: {
    tixian() {
      if (this.incomeInfo.canGet < 0.1) {
        this.$toast("您还没有收益");
        return;
      }
      this.showShareProfit = true;
      // this.$router.push(
      //   `/sqb/user/withdrawdeposit?canget=${this.incomeInfo.canGet}`
      // );
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.container {
  display: flex;
  flex-direction: column;
  width: 100%;
  .head {
    background-color: white;
    padding: 20px 10px;

    &-invite {
      color: #333;
    }
    &-income {
      display: flex;
      flex-direction: row;
      align-items: center;
      margin: 10px 0;
      &-amount {
        font-weight: bolder;
        font-size: 40px;
        margin-right: 30px;
        color: rgb(250, 81, 2);
      }
    }
  }
  .list {
    margin-top: 10px;
  }
}

.foot {
  text-align: center;
  &-btn {
    margin-top: 20px;
    width: 80%;
  }
}
</style>