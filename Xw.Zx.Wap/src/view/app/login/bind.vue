<template>
  <div class="wrapper">
    <div class="log">
      <img :src="require('@/assets/images/log.png')" alt />
    </div>

    <div class="login" v-if="!hasAccount">
      <van-field
        v-model="user.realName"
        required
        :formatter="$fieldFormatter"
        label="姓名"
        placeholder="请输入身份证姓名"
      />
      <van-field
        v-model="user.phone"
        required
        :formatter="$fieldFormatter"
        label="手机"
        placeholder="请输入手机号"
      />
      <sms-code-field
        v-model="user.smsCheck"
        :formatter="$fieldFormatter"
        :phone="user.phone"
      />
      <van-field
        v-model="user.password"
        required
        :formatter="$fieldFormatter"
        type="password"
        label="密码"
        placeholder="请输入密码"
      />
      <van-field
        v-model="user.password2"
        required
        :formatter="$fieldFormatter"
        type="password"
        label="密码"
        placeholder="请再次输入密码"
      />
      <van-field
        v-model="user.invitePhone"
        required
        :formatter="$fieldFormatter"
        label="邀请人"
        placeholder="请输入邀请人手机号"
      />
      <van-button
        class="login-btn"
        type="primary"
        round
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="regLogin"
        >注册</van-button
      >
      <van-button
        style="maring-top: 10px; width: 80%"
        type="primary"
        plain
        round
        color="#ff5000"
        @click="hasAccount = true"
        >已有账号</van-button
      >
    </div>

    <div class="login" v-if="hasAccount">
      <van-field
        v-model="bindUser.phone"
        label="手机"
        placeholder="请输入手机号"
      />
      <van-field
        v-model="bindUser.smsCheck"
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
      <van-button
        class="login-btn"
        type="primary"
        round
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="bindAccount"
        >提交
      </van-button>
      <van-button
        style="maring-top: 10px; width: 80%"
        type="primary"
        plain
        round
        color="#ff5000"
        @click="hasAccount = false"
        >新建账号</van-button
      >
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
import smsCodeField from "@/components/smsCodeField";
export default {
  name: "",
  props: [""],
  data() {
    return {
      hasAccount: false,
      user: {
        realName: "",
        phone: "",
        password: "",
        invitePhone: "",
        openId: "",
        smsCheck: "",
      },
      bindUser: {
        phone: "",
        smsCheck: "",
        openId: "",
      },
    };
  },

  components: { smsCodeField },

  computed: {},

  beforeMount() {
    this.user.openId = this.$route.query.id;
    this.bindUser.openId = this.$route.query.id;
    console.log(this.$route.query);
  },

  mounted() {},

  methods: {
    regLogin() {
      const _this = this;
      if (!this.user.openId) {
        this.$toast("没有OpenID");
        return;
      }
      if (this.user.realName.length < 2) {
        this.$toast("姓名不正确");
        return;
      }
      if (this.user.phone.length != 11) {
        this.$toast("手机号不正确");
        return;
      }
      if (this.user.smsCheck.length != 4) {
        this.$toast("验证码不正确");
        return;
      }
      if (this.user.password.length < 6) {
        this.$toast("密码最短为 6 个字符");
        return;
      }
      if (this.user.invitePhone.length != 11) {
        this.$toast("邀请人电话不正确");
        return;
      }
      if (this.user.password != this.user.password2) {
        this.$toast("两次输入密码不一样");
        return;
      }
      api.member.reg(_this.user).then((res) => {
        if (res.statusCode == 200) {
          _this.$router.push(`/sqb/login/weixin`);
        }
      });
    },
    bindAccount() {
      const _this = this;
      if (!this.bindUser.openId) {
        this.$toast("没有OpenID");
        return;
      }
      if (this.bindUser.phone.length != 11) {
        this.$toast("手机号不正确");
        return;
      }
      if (this.bindUser.smsCheck.length != 4) {
        this.$toast("验证码不正确");
        return;
      }
      api.member.weixinBind(_this.bindUser).then((res) => {
        if (res.statusCode == 200) {
          _this.$router.push(`/sqb/login/weixin`);
        }
      });
    },
    sendSms() {
      const _this = this;
      api.member
        .smscode({
          phone: _this.bindUser.phone,
        })
        .then(() => {
          this.$toast("验证码已发送");
        });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  background-color: #fff;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  .log {
    img {
      margin-top: 100px;
      width: 200px;
    }
  }
  .login {
    width: 90%;
    margin-top: 30px;
    //box-shadow: 0.02667rem 0.02667rem 0.21333rem #666;
    border-radius: 10px;
    overflow: hidden;
    padding: 30px 5px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    &-btn {
      width: 80%;
      margin: 20px 0;
    }
    &-foot {
      font-size: 14px;
      a {
        color: #ff5000;
      }
    }
  }
}
</style>