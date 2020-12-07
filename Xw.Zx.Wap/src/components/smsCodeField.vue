<template>
  <van-field
    v-model="smsCode"
    @input="smsCodeInputHandle"
    label="验证码"
    placeholder="请输入验证码"
    required
  >
    <template #button>
      <van-button
        size="mini"
        type="primary"
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="sendSms"
        :disabled="phone.length < 11 || disabledCodeBtn"
        >{{ codeText }}</van-button
      >
    </template>
  </van-field>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "",
  props: {
    value: String,
    phone: String,
  },
  data() {
    return {
      codeText: "发送验证码",
      smsCode: "",
      disabledCodeBtn: false,
    };
  },

  components: {},
  computed: {},
  beforeMount() {},

  mounted() {},

  methods: {
    sendSms() {
      const _this = this;
      api.member
        .smscode({
          phone: _this.phone,
        })
        .then(() => {
          this.$toast("验证码已发送");
          this.countDown(60);
        });
    },
    countDown(time) {
      if (time === 0) {
        this.disabledCodeBtn = false;
        this.codeText = "发送验证码";
        return;
      } else {
        this.disabledCodeBtn = true;
        this.codeText = "重新发送(" + time + ")";
        time--;
      }
      setTimeout(() => {
        this.countDown(time);
      }, 1000);
    },
    smsCodeInputHandle(val) {
      this.$emit("input", val);
    },
  },

  watch: {},
};
</script>
<style lang='' scoped>
</style>