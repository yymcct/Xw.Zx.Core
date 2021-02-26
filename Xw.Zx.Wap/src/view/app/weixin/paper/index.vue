<template>
  <div class="wrapper">
    <div class="sucess">
      <img :src="require('@/assets/images/success.png')" alt="" />
      <p>购买成功</p>
    </div>
    <!-- <remote-script src="https://wx.gtimg.com/pay_h5/goldplan/js/jgoldplan-1.0.0.js"></remote-script> -->
    <van-button
      class="btn"
      type="primary"
      round
      color="linear-gradient(to right, #ff7a00, #ff5000)"
      @click="lookBtn"
      >查看订单</van-button
    >
  </div>
</template>

<script>
export default {
  name: "",
  props: [""],
  data() {
    return {};
  },

  components: {},

  computed: {},

  beforeMount() {
    document.querySelector("html").style.fontSize = "32px";
    if (this.$route.query.out_trade_no) {
      // SHOW_CUSTOM_PAGE  SHOW_OFFICIAL_PAGE
      // 初始化小票
      console.log("2222", this.$route.query.out_trade_no);
      let mchData = {
        action: "onIframeReady",
        displayStyle: "SHOW_CUSTOM_PAGE",
      };
      let postData = JSON.stringify(mchData);
      parent.postMessage(postData, "https://payapp.weixin.qq.com");
      // 订单查询
      //   setTimeout(() => {
      //     fetchOrderInfoByMerchantId({
      //       wtorderid: this.$route.query.out_trade_no,
      //     }).then((res) => {
      //       this.orderData = res;
      //       // 支付成功订单查询
      //       fetchOrderQuery({
      //         orderId: this.orderData.order_id,
      //       });
      //     });
      //   }, 0);
    }
  },

  mounted() {},

  methods: {
    lookBtn() {
      const url = location.origin + "/sqb/user/order";
      let mchData = { action: "jumpOut", jumpOutUrl: url };
      let postData = JSON.stringify(mchData);
      parent.postMessage(postData, "https://payapp.weixin.qq.com");
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 360px;
  //padding-top: 10px;
  .sucess {
    display: flex;
    justify-content: center;
    align-items: center;
    img {
      width: 40px;
      height: 40px;
      margin-right: 10px;
    }
    p {
      font-size: 32px;
    }
  }
  .btn {
    margin-top: 20px;
    width: 200px;
  }
}
</style>