<template>
  <div>
    <van-popup
      v-model="show"
      position="bottom"
      closeable
      lazy-render
      :close-on-click-overlay="false"
      @close="close"
      :style="{ height: '55%' }"
    >
      <div class="content">
        <h1>客户信息</h1>
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
    productId: String,
  },
  data() {
    return {
      show: false,
      loading: false,
      dto: {
        productId: 0,
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
      this.dto.productId = this.productId;
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