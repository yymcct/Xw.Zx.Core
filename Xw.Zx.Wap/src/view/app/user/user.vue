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
          v-model="user.phone"
          label="手机"
          placeholder="手机号"
          disabled
        />
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
          v-model="dto.aliPayAccountName"
          label="支付宝姓名"
          placeholder="支付宝绑定的真实姓名,填错无法提现"
        />
        <van-field
          v-model="memberVipType"
          label="级别"
          placeholder="级别"
          disabled
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
      <div
        class="coupon-use"
        v-if="!userCouponCode"
        @click="userCouponCode = true"
      >
        使用兑换卷
      </div>
      <div class="coupon" v-if="userCouponCode">
        <van-field v-model="couponCode" placeholder="请输入兑换卷" />
        <van-button
          class="coupon-btn"
          color="#ff5000"
          round
          plain
          size="mini"
          @click="couponCodeHandle"
          :disabled="couponCode.length < 2"
        >
          使用
        </van-button>
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
import { userInfoAPI } from "@/utils/auth";
export default {
  name: "",
  props: [""],
  data() {
    return {
      dto: {
        realName: "",
        aliAccount: "",
        aliPayAccountName: "",
        smsCheck: "",
      },
      aliAccountReadonly: false,
      couponCode: "",
      userCouponCode: false,
    };
  },

  components: {},

  computed: {
    ...mapGetters({
      user: "user/user",
    }),
    memberVipType: function () {
      switch (this.user.memberVipType) {
        case 0:
          return "普通";
        case 10:
          return "会员";
        case 20:
          return "合伙人";
        case 30:
          return "运营中心";
        default:
          return "";
      }
    },
  },

  beforeMount() {
    api.member.getSelf().then((res) => {
      this.dto.realName = res.result.realName;
      if (res.result.aliPayAccount) {
        this.dto.aliAccount = res.result.aliPayAccount;
        this.dto.aliPayAccountName = res.result.aliPayAccountName;
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
      if (this.dto.aliPayAccountName.length == 0) {
        this.$toast("请填写支付宝绑定姓名");
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
    couponCodeHandle() {
      api.updateVipAuthCode.use(this.couponCode).then((res) => {
        userInfoAPI.updateMember(res.result);
        this.$store.commit("user/setUser", res.result);
        this.$toast("兑换成功! 您的级别已更新");
        this.userCouponCode = false;
      });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.coupon {
  margin-top: 20px;
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  align-items: center;
  background-color: #fff;
  padding: 10px;
  &-use {
    margin: 10px;
    font-size: 14px;
    color: #cdcdcd;
    text-align: right;
  }
  &-btn {
    width: 80px;
    margin-left: 10px;
  }
}
.foot {
  text-align: center;
  &-btn {
    margin-top: 20px;
    width: 80%;
  }
}
</style>