<template>
  <div class="wrapper">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>
    <div class="content">
      <div class="input-group">
        <div class="input-row border">
          <van-field
            v-model="getamount"
            label="金额"
            placeholder="请输入提现金额,最小2.1元"
          />
        </div>
      </div>
      <div class="border">
        <span class="sxf">提现手续费: 2元/笔</span>
      </div>
      <div class="btn-row">
        <van-button
          class="foot-btn"
          type="primary"
          round
          size="small"
          color="linear-gradient(to right, #ff7a00, #ff5000)"
          @click="edit"
          :loading="loading"
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
      canget: 0,
      getamount: 0,
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    this.canget = this.$route.query.canget;
    this.getamount = this.$route.query.canget;
  },

  mounted() {},

  methods: {
    edit() {
      const _this = this;
      if (
        parseFloat(this.canget) < parseFloat(this.getamount) ||
        parseFloat(this.getamount) < parseFloat(2.1)
      ) {
        this.$toast(`最大提现金额为${this.canget}元, 最小为2.1元`);
        return;
      }
      _this.loading = true;
      api.withdrawDeposit
        .withdrawDeposit({
          Amount: _this.getamount,
        })
        .then(() => {
          _this.loading = false;
          _this.$dialog
            .alert({
              message: "我们已收到您的申请,请在提现记录中查看提现进度",
            })
            .then(() => {
              _this.$router.go(-1);
            });
        })
        .catch(() => {
          _this.loading = false;
        });
    },
  },

  watch: {},
};
</script>
<style lang='' scoped>
</style>