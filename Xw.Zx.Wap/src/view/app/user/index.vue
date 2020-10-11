<template>
  <div class="wrapper">
    <hb-layout :active="1">
      <div class="banner">
        <img
          :src="require('@/assets/images/home/banner.jpg')"
          style="display: block; width: 100%; height: auto"
        />
      </div>
      <div class="user">
        <div class="user-title">
          <p class="user-title-title">个人信息</p>
          <van-button
            class="user-title-edit"
            color="#8a8a8a"
            round
            plain
            size="mini"
            @click="$router.push({ path: `/sqb/user/user` })"
          >
            编辑
          </van-button>
        </div>
        <van-cell title="手机" :value="user.phone" />
        <van-cell title="姓名" :value="user.realName" />
        <van-cell title="支付宝" :value="user.aliPayAccount" />
        <van-cell title="级别" :value="memberVipType" />
      </div>
      <van-cell-group>
        <van-cell title="我的收益" is-link to="/sqb/user/income" />       
        <van-cell title="我的订单" is-link to="/sqb/user/order" />
        <!-- <van-cell title="个人信息" is-link to="/sqb/user/user" /> -->
         <van-cell v-if="isWhite" title="审核提现" is-link to="/sqb/user/incomeaudit" />
      </van-cell-group>
      <div class="foot">
        <van-button
          class="foot-btn"
          type="primary"
          round
          color="linear-gradient(to right, #ff7a00, #ff5000)"
          @click="logout"
        >
          退出登录
        </van-button>
      </div>
    </hb-layout>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
import { mapGetters } from "vuex";
import { userInfoAPI } from "@/utils/auth";
import HbLayout from "@/components/layout/hbLayout";
export default {
  name: "user",
  props: [""],
  data() {
    return {
      isWhite: false,
    };
  },

  components: {
    HbLayout,
  },

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
    api.member.isWhite().then((res) => {
      this.isWhite = res.result;
    });
  },

  mounted() {},

  methods: {
    logout: function () {
      this.$dialog.confirm({
        title: "提示",
        message: "确定退出?",
        beforeClose: (action, done) => {
          if (action === "confirm") {
            userInfoAPI.clear();
            this.$router.push(`/sqb/login`);
            done();
          } else {
            done();
          }
        },
      });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  padding-bottom: 80px;
  .user {
    background-color: #fff;
    margin-bottom: 20px;
    margin: 10px;
    padding: 10px;
    border-radius: 10px;
    &-title {
      display: flex;
      flex-direction: row;
      justify-content: space-between;
      align-items: center;
      &-title {
        font-size: 16px;
        font-weight: bold;
        color: #323233;
        margin: 10px;
        line-height: 20px;
      }
      &-edit {
        margin-right: 10px;
      }
    }
  }
  .foot {
    text-align: center;
    &-btn {
      margin-top: 20px;
      width: 80%;
    }
  }
}
</style>