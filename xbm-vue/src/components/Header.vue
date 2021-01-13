<template>
  <div class="header">
    <iframe :src="'/jz/form_1_2.bsp?token='+token" style="display:none"></iframe>
    <h3>
      {{title}}
      <p>{{subtitle}}</p>
    </h3>
    <!-- <div class="user-box"> -->
      <loginBox></loginBox>
    <!-- </div> -->
  </div>
</template>

<script>
import loginBox from "@/components/loginbox";
import * as dataService from "@/public/apiService/sysManagement/logMangement";
import {getToken, getUserInfo, removeToken } from "@/public/auth";
export default {
  name: "headerIn",
  components: { loginBox },
  props: ["title", "subtitle"],
  data() {
    return {
      username: "",
      token:getToken()
    };
  },
  mounted() {},
  methods: {}
};
</script>

<style scoped lang='scss'>
.header {
  height: 100%;
  width: 1200px;
  margin: 0 auto;
  position: relative;
  padding-top: 22px;
}
h3 {
  float: left;
  padding-left: 90px;
  width: 742px;
  font-size: 40px;
  font-weight: bold;
  color: rgba(7, 67, 139, 1);
  text-shadow: 0px 2px 4px rgba(0, 0, 0, 0.2);
  background: url("~@/assets/logo.png") no-repeat left center;
  p {
    font-size: 18px;
  }
}
.user-box {
  position: absolute;
  right: 0;
  top: 50%;
  transform: translateY(-50%);
  width: 200px;
  text-align: right;
}
</style>