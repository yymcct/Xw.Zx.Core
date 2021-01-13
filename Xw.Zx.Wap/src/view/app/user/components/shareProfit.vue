<template>
  <div>
    <van-popup
      v-model="show"
      position="bottom"
      closeable
      lazy-render
      :close-on-click-overlay="false"
      @close="close"
      :style="{ height: '80%' }"
    >
      <h2>收益提现</h2>
      <div class="incomes" v-if="incomes.length > 0">
        <van-checkbox-group v-model="checkResult">
          <template v-for="(item, index) in incomes">
            <div class="income-item" :key="index">
              <div class="income-item-content">
                <div class="income-item-content-left">
                  <div class="title">
                    <span class="subtitle">￥</span>{{ item.amount }}
                  </div>
                </div>
                <div class="income-item-content-right">
                  <div class="title">债减减返佣</div>
                  <div class="subtitle">{{ item.addTime }}</div>
                </div>
                <div class="income-item-content-end">
                  <van-checkbox :name="item.id" checked-color="#ff5000" />
                </div>
              </div>
              <div class="income-item-desc">
                <p>{{ item.remark }}</p>
              </div>
            </div>
          </template>
        </van-checkbox-group>
        <div class="btn">
          <van-button
            class="btn-btn"
            type="primary"
            round
            color="linear-gradient(to right, #ff7a00, #ff5000)"
            @click="txhandle"
            >提现 {{ btnText }} 元</van-button
          >
        </div>
      </div>
    </van-popup>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "shareProfit",
  props: {
    value: {
      type: Boolean,
      default: false,
    },
  },
  data() {
    return {
      show: false,
      loading: false,
      incomes: [],
      checkResult: [],
    };
  },

  components: {},

  computed: {
    btnText: function () {
      let amount = 0;
      var _this = this;
      _this.checkResult.map((r) => {
        let income = _this.incomes.find((i) => {
          return i.id == r;
        });
        amount += income.amount;
      });

      return amount;
    },
  },

  beforeMount() {
    this.init();
  },

  mounted() {},

  methods: {
    init() {
      api.income.getWaitWithdraws().then((res) => {
        this.incomes = res.result;
      });
    },
    txhandle() {          
      api.withdrawDeposit
        .postWithdrawDepositByShareProfitId({
          ShareProfitId: this.checkResult,
        })
        .then((res) => {
          this.$toast(res.msg);
          this.close();
        })
        .catch();
    },
    close() {
      this.incomes = [];
      this.checkResult= [];
      this.$emit("input", false);
    },
  },

  watch: {
    value: {
      handler(val) {
        if (val != this.show) this.show = val;
        if(val){
           this.init();
        }
      },
    },
  },
};
</script>
<style lang='scss' scoped>
h2 {
  text-align: center;
  font-size: 18px;
  font-weight: bold;
  margin-top: 20px;
}
.incomes {
  padding-top: 10px;
  .income-item {
    margin: 10px;
    background: #ffffff;
    display: flex;
    flex-direction: column;
    box-shadow: 0 0 4px rgba(0, 0, 0, 0.1);
    border-radius: 10px;
    &-content {
      padding: 10px;
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      &-left {
        height: inherit;
        width: 80px;
        color: #ff5000;
        text-align: center;
        display: flex;
        flex-direction: column;
        justify-content: center;
        .title {
          font-size: 24px;
          line-height: 32px;
          font-weight: bold;
        }
      }
      &-right {
        padding: 10px;
        width: 200px;
        .title {
          font-size: 16px;
          height: 24px;
          font-weight: bold;
          color: #1a1a1a;
          overflow: hidden;
        }
        .subtitle {
          line-height: 18px;
          font-size: 14px;
          color: #666;
          //word-wrap: break-all;
          overflow: hidden;
        }
      }
      &-end {
        display: flex;
        width: 30px;
        // background-color: #ff5000;
        justify-content: space-between;
        align-items: center;
      }
    }
    &-desc {
      padding: 10px;
      font-size: 12px;
      border-top: 1px dashed #ebedf0;
      line-height: 16px;
    }
  }

  .btn {
    text-align: center;
    padding: 20px;
    &-btn {
      width: 80%;
    }
  }
}
</style>