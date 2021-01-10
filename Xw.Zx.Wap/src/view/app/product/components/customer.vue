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
      <div class="product" v-if="product">
        <h2>订单信息</h2>
        <div class="product-content">
          <div class="product-content-img">
            <img :src="product.images" alt="" />
          </div>
          <div class="product-content-info">
            <h1 class="product-content-info-name">
              {{ product.name }}
            </h1>
            <p class="product-content-info-price">
              <span>单价:</span>￥{{ product.price }}
            </p>

            <div class="product-content-info-total">
              <div class="product-content-info-total-amount">
                <span>合计:</span>￥{{
                  (Number(product.price) * dto.productCount).toFixed(1)
                }}
              </div>
              <div class="product-content-info-total-count">
                <span>数量:</span>
                <van-stepper v-model="dto.productCount" min="1" max="999"   button-size="22px" />
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="content">
        <h2>客户信息</h2>
        <van-field
          v-model="dto.customerName"
          required
          label="姓名"
          placeholder="请输入身份证姓名"
        />
        <van-field
          v-model="dto.customerPhone"
          required
          label="手机"
          placeholder="请输入手机号"
        />
        <van-field
          v-model="dto.remark"
          rows="2"
          autosize
          label="备注"
          type="textarea"
          maxlength="200"
          placeholder="请输入预定备注"
          show-word-limit
        />
        <van-button
          class="content-btn"
          type="primary"
          color="linear-gradient(to right, #ff7a00, #ff5000)"
          @click="pay"
          :disabled="
            dto.customerName.length < 2 || dto.customerPhone.length != 11
          "
          :loading="loading"
        >
          确定
        </van-button>
      </div>
    </van-popup>
  </div>
</template>

<script>
/*
  支付宝原生支付
*/
import api from "@/api/sqbApi";
export default {
  name: "CustomerInfo",
  props: {
    value: {
      type: Boolean,
      default: false,
    },
    product: Object,
  },
  data() {
    return {
      show: false,
      loading: false,
      dto: {
        productId: 0,
        productCount: 1,
        customerName: "",
        customerPhone: "",
        orderPaymentType: 0,
        remark: "",
      },
    };
  },

  components: {},

  computed: {},

  beforeMount() {},

  mounted() {
    if (this.$route.query.from == "coup") {
      this.dto.orderPaymentType = 4;
    }
  },

  methods: {
    pay() {
      const _this = this;
      _this.loading = true;
      this.dto.productId = this.product.id;
      api.order
        .post(_this.dto)
        .then((res) => {
          _this.loading = false;
          _this.$router.push(`/sqb/order/${res.result.id}`);
        })
        .catch(() => {
          _this.loading = false;
          this.$toast("添加订单失败!");
        });
    },
    close() {
      this.$emit("input", false);
    },
  },

  watch: {
    value: {
      handler(val) {
        this.show = val;
      },
    },
  },
};
</script>
<style lang='scss' scoped>
.product {
  margin-top: 15px;
  background-color: #fff;
  padding: 10px;
  margin-bottom: 20px;
  h2 {
    width: 100%;
    line-height: 24px;
    font-size: 18px;
    font-weight: bold;

    // margin-bottom: 10px;
    text-align: left;
  }
  &-content {
    margin-top: 15px;
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: flex-start;

    &-img {
      img {
        width: 100px;
      }
    }
    &-info {
      margin-left: 10px;
      width: calc(100%-100px);

      &-name {
        font-size: 16px;
        line-height: 20px;
        color: #333333;
        overflow: hidden;
        height: 20px;
      }
      &-price {
        margin: 8px 0px;
        font-size: 15px;
        color: #ff5000;
        width: 100%;
        span {
          color: #222222;
          font-size: 13px;
          margin-right: 5px;
        }
      }
      &-total {
        margin: 8px 0px;
        font-size: 18px;
        color: #ff5000;
        width: 100%;
        // display: flex;
        // flex-direction: row;
        // justify-content: space-between;
        // align-items: center;
        span {
          color: #222222;
          font-size: 13px;
          margin-right: 5px;
        }
        &-amount {
          font-weight: bold;
        }
        &-count {
          margin-top: 8px;
          display: flex;
          flex-direction: row;
         // justify-content: flex-end;
          align-items: center;
        }
      }
      &-time {
        display: flex;
        flex-direction: row;
        justify-content: space-between;
        align-items: flex-start;
        p {
          font-size: 13px;
          line-height: 20px;
          color: #999999;
        }
        &-btn {
          width: 60px;
        }
      }
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
  h2 {
    width: 100%;
    line-height: 24px;
    font-size: 18px;
    font-weight: bold;
    padding-left: 10px;
    // margin-bottom: 10px;
    text-align: left;
    box-sizing: border-box;
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