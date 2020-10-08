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
      <div class="info">
        <div class="invite">累计收入(元)</div>
        <div class="income">
          <div class="amoount">{{ incomeInfo.incomTotal }}</div>
        </div>
        <div class="invite">当前可提现金额: {{ incomeInfo.canGet }}</div>
        <div></div>
      </div>
      <div class="list">
        <van-list>
          <van-cell title="收益详情" is-link url="/sqb/user/incomelist" />
          <van-cell
            title="提现详情"
            is-link
            url="/sqb/user/withdrawdepositlist"
          />
        </van-list>
      </div>
      <div class="btn">
        <van-button
          class="foot-btn"
          type="primary"
          round
          size="small"
          color="linear-gradient(to right, #ff7a00, #ff5000)"
          @click="tixian"
        >
          我要提现
        </van-button>
      </div>
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
      incomeInfo: null,
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    api.income.getIncomeInfo().then((res) => {
      this.incomeInfo = res.data.result;
    });
  },

  mounted() {},

  methods: {
    tixian() {
      if (this.incomeInfo.canGet < 0.1) {
        this.$toast("您还没有收益");
      }

      this.$router.push(
        `/user/withdrawdeposit?canget=${this.incomeInfo.canGet}`
      );
    },
  },

  watch: {},
};
</script>
<style lang='' scoped>
</style>