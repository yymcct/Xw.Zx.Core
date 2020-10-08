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
        <van-field
          v-model="dto.realName"
          label="姓名"
          placeholder="请输入真实姓名"
        />
        <van-field
          v-model="dto.aliAccount"
          label="支付宝"
          :disabled="aliAccountReadonly"
          placeholder="提现账户,设置后不能修改"
        />
        <van-field
          v-model="dto.smsCheck"
          label="验证码"
          placeholder="请输入验证码"
        >
          <template #button>
            <van-button
              size="small"
              type="primary"
              color="linear-gradient(to right, #ff7a00, #ff5000)"
              @click="sendSms"
              >发送验证码</van-button
            >
          </template>
        </van-field>
      </div>
      <div class="foot">
        <van-button
          class="foot-btn"
          type="primary"
          round
          color="linear-gradient(to right, #ff7a00, #ff5000)"
          @click="post"
        >
          提交
        </van-button>
      </div>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
import { mapGetters } from "vuex";
export default {
  name: "",
  props: [""],
  data() {
    return {
      dto: {
        realName: "",
        aliAccount: "",
        smsCheck: "",
      },
      aliAccountReadonly: false,
    };
  },

  components: {},

  computed: {
    ...mapGetters({
      user: "user/user",
    }),
  },

  beforeMount() {
    api.member.getSelf().then((res) => {
      this.dto.realName = res.result.realName;
      if (res.result.aliPayAccount) {
        this.dto.aliAccount = res.result.aliPayAccount;
        this.aliAccountReadonly = true;
      }
    });
  },

  mounted() {},

  methods: {
    sendSms() {
      const _this = this;
      api.member
        .smscode({
          phone: _this.user.phone,
        })
        .then(() => {
          this.$toast("验证码已发送");
        });
    },
    post() {
      const _this = this;
      if (this.dto.realName.length < 2) {
        this.$toast("请填写真实姓名");
        return;
      }
      if (this.dto.aliAccount.length == 0) {
        this.$toast("请填写支付宝账号");
        return;
      }
      if (this.dto.smsCheck.length != 4) {
        this.$toast("验证码不正确");
        return;
      }
      api.member.edit(_this.dto).then(() => {
        this.$toast("修改成功!");
      });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.foot {
  text-align: center;
  &-btn {
    margin-top: 20px;
    width: 80%;
  }
}
</style>