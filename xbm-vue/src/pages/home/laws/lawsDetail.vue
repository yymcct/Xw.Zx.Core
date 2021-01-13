<template>
  <div id="detail" class="detail-box">
    <div class="header-box header-box10">
      <h3>成都再减减企业管理服务有限公司</h3>
      <!-- <div class="backTo" v-show="isShow">
         <el-button type="primary" plain v-on:click="back">返回</el-button>
      </div> -->
    </div>
    <div class="nav">
      <NavIn :noticievw="noticievw" :notic="notic" @clickit="getNav"></NavIn>
    </div>

    <div
      class="details-box"
      v-loading="loading"
      element-loading-text="拼命加载中"
    >
      <el-breadcrumb class="breadTop" separator-class="el-icon-arrow-right">
        <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
        <el-breadcrumb-item style="cursor:pointer">
          <a @click="backPage">政策法规</a>
        </el-breadcrumb-item>
        <el-breadcrumb-item>正文</el-breadcrumb-item>
      </el-breadcrumb>
      <div class="title">
        {{ detail.WJ_NAME }}
      </div>
      <div class="cjsj">
        发布日期：{{ detail.SCSJ }}
        <!-- <a href="javascript:window.close();" class="link-a">【关闭窗口】</a> -->
      </div>
      <div class="detail" v-html="detail.WJ_NR"></div>
      <div class="content_attachments">
        <div v-for="(item, idx) in detail.FILE" :key="idx">
          <a
            class="attachments-text"
            target="_blank"
            :href="'/jz/XBM_Service.bsp?IMAGE&Source=' + item.AC_NAME"
          >
            <span class="el-icon-paperclip"></span>附件{{ idx + 1 }}：{{
              item.SR_NAME || "null"
            }}</a
          >
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import NavIn from "@/components/Nav";
import * as dataService from "@/public/apiService/home";
export default {
  name: "right1",
  components: { NavIn },
  data() {
    return {
      detail: {},
      loading: false,
      noticievw: null,
      notic: 4
      // isShow: true
    };
  },
  watch: {
    $route(to, from) {
      // console.log(this.$route.query, "sssss");
    }
  },
  mounted() {
    this.getDetail();
  },
  methods: {
    // back(){
    //     this.$router.push('/manage/Laws')
    // },
    backPage: function() {
      this.$router.push("/");
      var data = { tag: "政策法规", path: "laws", index: 4 };
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
      dataService.checkLaws(this.$route.query.wiid).then(res => {
        this.loading = false;
        this.detail = res;
        this.detail.WJ_NR = "";
        res.data.forEach(item => {
          //  this.detail.WJ_NR+=item.WJ_NR;
          this.detail.WJ_NR += item.WJ_NR;
          //    this.detail.WJ_NR+=this.Base64.decode(item.WJ_NR);
        });
      });
    }
  }
};
</script>

<style lang="scss" scoped>
@import "~@/assets/scss/variables";
.breadTop {
  // position: absolute;
  // top: 0;
  // left: 230px;
}
.nav {
  background: $base-color;
  height: 60px;
  line-height: 60px;
}
.detail-box {
  height: 100%;
  width: 100%;
  overflow-y: auto;
  background: #f2f2f2;
  .header-box {
    // position: relative;
    // height: 70px;
    // margin-bottom: 6px;
    // padding-left: 30px;
    // border-bottom: 3px solid $base-color;
     width: 1200px;
  height: 90px;
  margin: 0 auto 6px;
  position: relative;
    h3 {
      // margin: 0 auto;
      // height: 70px;
      // line-height: 70px;
      // min-width: 480px;
      // color: $base-color;
      // font-size: 32px;
      // padding-left: 60px;
      // background: url("~@/assets/logo.png") no-repeat left center;
      // background-size: 60px 60px;
      cursor: pointer;
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
    }
    .backTo {
      position: absolute;
      right: 48px;
      top: 25px;
    }
  }
  .details-box {
    position: relative;
    width: 80%;
    margin: 0px auto;
    background: #ffffff;
    padding-left: 40px;
    padding-right: 40px;
    .title {
      margin: 40px 10px 10px;
      font-size: 24px;
      font-weight: bold;
      text-align: center;
    }
    .cjsj {
      margin-bottom: 40px;
      text-align: center;
      font-size: 14px;
      font-weight: 400;
    }
    .content_attachments {
      padding-top: 30px;
      .attachments-text {
        color: blue;
        line-height: 24px;
        font-size: 0.875em;
      }
    }
  }
  .detail {
    width: 70%;
    margin: 0 auto;
    overflow: -webkit-paged-x;
    font-size: 20px;
    p {
      width: 100% !important;
      overflow: -webkit-paged-x;
      margin: 15px 0;
    }
  }
}
</style>
