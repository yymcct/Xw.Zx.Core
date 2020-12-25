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
      <van-field
        v-model="getamount"
        label="提现金额"
        placeholder="请输入提现金额,最小2.1元"
      />
      <div class="border">
        <span class="sxf"
          >重要提示: 提现手续费 = 提现金额 * 0.15% (支付宝收取)</span
        >
      </div>
    </div>
    <div class="foot">
      <van-button
        class="foot-btn"
        type="primary"
        round
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="edit"
        :loading="loading"
      >
        我要提现
      </van-button>
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
      loading: false,
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

      var charge = (this.getamount*15/10000).toFixed(2);
      var msg = `提现金额:${this.getamount}, 手续费:${charge}, 到账金额:${this.getamount-charge}`;
      this.$dialog.confirm({
        message: msg,
        beforeClose: (action, done) => {
          if (action === "confirm") {
            api.withdrawDeposit
              .withdrawDeposit({
                Amount: _this.getamount,
              })
              .then(() => {
                done();
                _this.$toast.success("提交成功!");
                _this.$router.go(-1);
              })
              .catch(() => {
                done();
              });
          } else {
            done();
          }
        },
      });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.border {
  span {
    font-size: 14px;
    color: #999;
    margin: 10px 15px;
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