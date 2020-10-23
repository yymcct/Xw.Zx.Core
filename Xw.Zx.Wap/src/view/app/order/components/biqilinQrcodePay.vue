<template>
  <div>
    <van-popup
      v-model="show"
      position="bottom"
      closeable
      lazy-render
      :close-on-click-overlay="false"
      @close="close"
      :style="{ height: '75%' }"
    >
      <div class="change" v-if="payType == null">
        <h1>请选择支付方式</h1>
        <van-cell @click="selectChanl(1)">
          <template #title>
            <div class="change-cell">
              <img
                class="change-cell-aiplaylog"
                :src="require('@/assets/images/alpay.png')"
                alt=""
              />
              <span class="change-cell-aiplaytext">支付宝</span>
            </div>
          </template>
        </van-cell>
        <van-cell @click="selectChanl(0)">
          <!-- 使用 title 插槽来自定义标题 -->
          <template #title>
            <div class="change-cell">
              <img
                class="change-cell-weixinlog"
                :src="require('@/assets/images/weixin.jpg')"
                alt=""
              />
              <span class="change-cell-weixintext">微信</span>
            </div>
          </template>
        </van-cell>
      </div>
      <div class="content" v-if="payType != null && payUrl">
        <h1>{{ order.producName }}</h1>
        <vue-qr
          :logoSrc="require('@/assets/logo.png')"
          :text="payUrl"
          :margin="10"
          :size="200"
        ></vue-qr>
        <p class="content-amount">
          金额<span>{{ order.amount }}</span
          >元
        </p>
        <p class="content-desc">请用<span>{{payType==1?"支付宝":"微信"}}扫码</span>支付</p>
        <van-button
          class="content-btn"
          type="primary"
          color="linear-gradient(to right, #ff7a00, #ff5000)"
          @click="payOrderBack"
          :loading="loading"
        >
          支付完成
        </van-button>
      </div>
    </van-popup>
  </div>
</template>

<script>
/*
  支付宝原生支付
*/
import vueQr from "vue-qr";
import api from "@/api/sqbApi";
export default {
  name: "qrcodepay",
  props: {
    value: {
      type: Boolean,
      default: false,
    },
    order: Object,
  },
  data() {
    return {
      show: true,
      payUrl: "",
      loading: false,
      payType: null,
    };
  },

  components: { vueQr },

  computed: {},

  beforeMount() {},

  mounted() {
    //const _this = this;
    // api.alipay.scanCodeGen(_this.order.id).then((res) => {
    //   _this.payUrl = res.result;
    // });
  },

  methods: {
    selectChanl(val) {
      this.payType = val;
      this.refPayUrl();
    },
    refPayUrl() {
      const _this = this;

      api.biqilin
        .scanCodeGen({
          Biqilin_PayType: _this.payType,
          OrderId: _this.order.id,
        })
        .then((res) => {
          _this.payUrl = res.result;
        })
        .catch(() => {
          api.biqilin
            .scanCodeGen({
              Biqilin_PayType: _this.payType,
              OrderId: _this.order.id,
            })
            .then((res) => {
              _this.payUrl = res.result;
            });
        });
    },
    payOrderBack() {
      const _this = this;
      _this.loading = true;
      api.order.get(_this.order.id).then((res) => {
        let _order = res.result;
        _this.loading = false;
        if (_order.orderState == 1) {
          //支付完成
          _this.show = false;
          _this.$emit("paystateChange", true);
        } else {
          this.$dialog
            .confirm({
              message: "未完成支付,是否放弃本次付款?",
              confirmButtonText: "继续付款",
              cancelButtonText: "放弃",
            })
            .then(() => {              
            })
            .catch(() => {
              _this.show = false;
              _this.$emit("paystateChange", false);
            });
        }
      });
    },
    close() {
      this.payType = null;
      this.payUrl = "";
      this.$emit("input", false);
    },
  },

  watch: {
    value: {
      handler(val) {
        //this.refPayUrl();
        this.show = val;
      },
    },
  },
};
</script>
<style lang='scss' scoped>
.change {
  padding: 20px;
  h1 {
    text-align: center;
    margin-top: 80px;
    margin-bottom: 30px;
  }
  &-cell {
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: center;
    font-size: 16px;

    &-aiplaylog {
      width: 40px;
      margin-left: 5px;
    }
    &-weixinlog {
      width: 50px;
    }
    &-aiplaytext {
      font-weight: bold;
      margin-left: 10px;
    }
    &-weixintext {
      font-weight: bold;
      margin-left: 5px;
    }
  }
}
.content {
  margin-top: 15px;
  text-align: center;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  h1 {
    width: 70%;
    line-height: 24px;
    font-size: 18px;
    font-weight: bold;
    margin-bottom: 20px;
  }
  &-amount {
    color: #999999;
    margin-top: 10px;
    font-size: 16px;
    span {
      font-weight: bold;
      color: #ff5000;
      font-size: 20px;
      margin: 0 3px;
    }
  }
  &-desc {
    color: #999999;
    margin-top: 10px;
    font-size: 16px;
    span {
      font-weight: bold;
      font-size: 20px;
      margin: 0 3px;
    }
  }
  &-btn {
    margin-top: 40px;
    width: 80%;
  }
}
</style>