<template>
  <div>
    <hb-layout :active="1">
      <div class="banner">
        <img
          :src="require('@/assets/images/home/banner.jpg')"
          style="display: block; width: 100%; height: auto"
        />
      </div>
      <van-cell-group>
        <van-cell title="我的收益" is-link to="/sqb/user/income" />
        <van-cell title="收益审核" is-link to="/sqb/user/incomeaudit" />
        <van-cell title="我的兑换码" is-link to="/sqb/user/code" />
        <van-cell title="个人信息" is-link to="/sqb/user/user`" />
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
//import api from "@/api/sqbApi";
import { userInfoAPI } from "@/utils/auth";
import HbLayout from "@/components/layout/hbLayout";
export default {
  name: "",
  props: [""],
  data() {
    return {};
  },

  components: {
    HbLayout,
  },

  computed: {},

  beforeMount() {},

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
.foot {
  text-align: center;
  &-btn {
    margin-top: 20px;
    width: 80%;
  }
}
</style>