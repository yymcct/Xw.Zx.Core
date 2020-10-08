<template>
  <div>
    <hb-layout :active="1">
      <ul>
        <!-- <li><router-link :to="`/sqb/user/myteam`">我的团队</router-link></li> -->
        <li><router-link :to="`/sqb/user/income`">我的收益</router-link></li>
        <li><router-link :to="`/sqb/user/incomeaudit`">收益审核</router-link></li>
        <li><router-link :to="`/sqb/user/code`">我的兑换码</router-link></li>
        <li><router-link :to="`/sqb/user/user`">个人信息</router-link></li>
        <li @click="logout">退出登录</li>
      </ul>
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

  beforeMount() {

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
</style>