<template>
  <div id="detail" class="detail-box">
    <div class="header-box header-box8">
      <h3>成都再减减企业管理服务有限公司</h3>
    </div>
    <div class="nav">
      <NavIn :noticievw="noticievw" :notic="notic" @clickit="getNav"></NavIn>
    </div>
    <div class="detail" v-loading="loading">
      <el-breadcrumb class="breadTop" separator-class="el-icon-arrow-right">
        <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
        <el-breadcrumb-item style="cursor:pointer">
          <a @click="backPage">公示公告</a>
        </el-breadcrumb-item>
        <el-breadcrumb-item>正文</el-breadcrumb-item>
      </el-breadcrumb>
      <template v-if="detail">
        <certificate1
          :data="detail"
          v-if="$route.query.ZZMLID == '11100000000013338W064'"
        ></certificate1>
        <certificate2
          :data="detail"
          v-if="$route.query.ZZMLID == '11100000MB03271699035'"
        ></certificate2>
        <certificate3
          :data="detail"
          v-if="$route.query.ZZMLID == '11410800MB18523953004'"
        ></certificate3>
        <certificate4
          :data="detail"
          v-if="$route.query.ZZMLID == '11100000000013338W010'"
        ></certificate4>
      </template>
    </div>
  </div>
</template>

<script>
import NavIn from "@/components/Nav";
import certificate1 from "./certificates/11100000000013338W064";
import certificate2 from "./certificates/11100000MB03271699035";
import certificate3 from "./certificates/11410800MB18523953004";
import certificate4 from "./certificates/11100000000013338W010";
import { apiUrl } from "@/public/apiUrl";
export default {
  name: "right1",
  components: {
    certificate1,
    certificate2,
    certificate3,
    certificate4,
    NavIn
  },
  data() {
    return {
      detail: null,
      loading: false,
      noticievw: null,
      notic: 6
    };
  },
  mounted() {
    this.getDetail();
  },
  methods: {
    backPage: function() {
      this.$router.push("/");
      var data = { tag: "公示公告", path: "Announcement", index: 6 };
      window.sessionStorage.setItem("nav", JSON.stringify(data));
    },
    getNav(data, navIndex) {
      this.$router.push("/");
      this.noticievw = data;
      this.noticievw.index = navIndex;
      window.sessionStorage.setItem("nav", JSON.stringify(this.noticievw));
    },
    getDetail: function() {
      this.loading = true;
      this.$http
        .get(apiUrl.GET_HOME_ANNOUNCEMENT_DETAIL, {
          params: { ZZID: this.$route.query.ZZID }
        })
        .then(res => {
          this.loading = false;
          this.detail = res.data.data;
        });
    }
  }
};
</script>

<style lang="scss" scoped>
@import "~@/assets/scss/variables";
.nav {
  background: $base-color;
  height: 60px;
  line-height: 60px;
}
.detail-box {
  height: 100%;
  width: 100%;
  overflow-y: auto;
  background: #fff;
  .header-box {
    // height: 70px;
    // margin-bottom: 6px;
    // padding-left: 30px;
    // border-bottom: 3px solid $base-color;
    width: 1200px;
  height: 90px;
  margin: 0 auto 6px;
  position: relative;
    h3 {
      float: left;
    line-height: 90px;
    padding-left: 90px;
    width: 742px;
    font-size: 40px;
    font-weight: bold;
    color: $base-color;
    // color: rgba(7, 67, 139, 1);
    text-shadow: 0px 2px 4px rgba(0, 0, 0, 0.2);
    background: url("~@/assets/logo.png") no-repeat left center;
      // margin: 0 auto;
      // height: 70px;
      // line-height: 70px;
      // min-width: 480px;
      // color: $base-color;
      // font-size: 32px;
      // padding-left: 60px;
      // background: url("~@/assets/logo.png") no-repeat left center;
      // background-size: 60px 60px;
     
    }
  }

  .detail {
    width: 80%;
    margin: 0 auto;
  }
}
</style>
