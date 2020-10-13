<template>
  <div
    class="wrapper"
    :style="{
      background:
        'url(' + require('@/assets/images/background.jpg') + ') no-repeat ',
    }"
  >
    <div class="title">
      <h1>你是否借过网贷或者小贷?</h1>
      <p>如果有, 那您的福音来了</p>
      <p>立即下载债减减, 算算你多还了多少</p>
    </div>
    <div class="qrcode">
      <vue-qr
        :logoSrc="require('@/assets/logo.png')"
        text="http://jsq.lawss360.com/sqb/user/download"
        :margin="10"
        :size="200"
      ></vue-qr>
    </div>
    <div class="desc">
      <ul>
        <li>
          8月20日,
          <span>最高人民法院</span>明确规定法律支持的利率是
          <span>年化15.4%</span>
        </li>
        <li>由此带来的结果是大量的贷款机构的利率超过了司法保护上限</li>
      </ul>
    </div>
  </div>
</template>

<script>
import vueQr from "vue-qr";
export default {
  name: "",
  props: [""],
  data() {
    return {};
  },

  components: { vueQr },

  computed: {},

  beforeMount() {},

  mounted() {
    setTimeout(() => {
      this.$toast("请截图分享,并引导下载");
    }, 1500);
  },

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  height: 100vh;
  overflow: hidden;
  .title {
    padding-top: 50px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    color: #fff;
    h1 {
      font-size: 28px;
      font-weight: bold;
      line-height: 44px;
    }
    p {
      font-size: 20px;

      line-height: 38px;
    }
  }
  .qrcode {
    padding-top: 50px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
  }
  .desc {
    margin: 50px 20px 20px 20px;
    background-color: #fff;
    border-radius: 20px;
    padding: 20px;
    font-size: 16px;
    span {
      color: red;
    }
    ul {
      padding: 10px;
      padding-left: 20px;

      li {
        list-style-type: disc;
        line-height: 24px;
      }
    }
  }
}
</style>