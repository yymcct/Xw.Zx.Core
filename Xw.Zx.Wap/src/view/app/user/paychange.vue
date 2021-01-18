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
      <van-radio-group v-model="radio">
        <van-radio name="1" class="radio-item" checked-color="#FF5000"
          >支付宝</van-radio
        >
        <van-radio name="2" class="radio-item" checked-color="#FF5000"
          >碧麒麟</van-radio
        >
      </van-radio-group>
    </div>

    <div class="btn">
      <van-button
        class="btn"
        type="primary"
        round
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="post"
        >提交</van-button
      >
    </div>
  </div>
</template>

<script>
import manageApi from "@/api/manageApi";
export default {
  name: "",
  props: [""],
  data() {
    return { radio: "0" };
  },

  components: {},

  computed: {},

  beforeMount() {
    manageApi.sysParam.getValue("FirstUseAlipay").then((res) => {
      this.radio = res.result;
    });
  },

  mounted() {},

  methods: {
    post() {
      manageApi.sysParam.setValue("FirstUseAlipay", this.radio).then(() => {
        this.$toast.success("切换成功!");
      });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  background-color: #fff;
  height: 100vh;
  .container {
    padding: 10px;
    font-size: 16px;

    .radio-item {
      margin-bottom: 20px;
    }
  }

  .btn {
    margin-top: 15px;
    padding: 10px;
    text-align: center;
    .btn {
      width: 80%;
    }
  }
}
</style>