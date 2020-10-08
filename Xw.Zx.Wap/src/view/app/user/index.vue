<template>
  <div>
    <hb-layout :active="1">
      <ul>
        <li><router-link :to="`/sqb/user/myteam`">我的团队</router-link></li>
        <li><router-link :to="`/sqb/product/`">我的收益</router-link></li>
        <li><router-link :to="`/sqb/product/`">收益审核</router-link></li>
        <li><router-link :to="`/sqb/product/`">我的兑换码</router-link></li>
        <li><router-link :to="`/sqb/product/`">我要分享</router-link></li>
        <li><router-link :to="`/sqb/product/`">个人信息</router-link></li>
        <li><router-link :to="`/sqb/product/`">退出登录</router-link></li>
      </ul>
    </hb-layout>
  </div>
</template>

<script>
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

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
</style>