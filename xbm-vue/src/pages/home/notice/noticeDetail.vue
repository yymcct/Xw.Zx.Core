<template>
  <div id="detail" class="detail-box">
    <div class="header-box header-box12">
      <h3>成都再减减企业管理服务有限公司</h3>
    </div>
    <div class="nav">
      <NavIn :noticievw="noticievw" :notic="notic" @clickit="getNav"></NavIn>
    </div>
    <div class="detail">
      <el-breadcrumb class="breadTop" separator-class="el-icon-arrow-right">
        <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
        <el-breadcrumb-item style="cursor:pointer">
          <a @click="backPage">通知公告</a>
        </el-breadcrumb-item>
        <el-breadcrumb-item>正文</el-breadcrumb-item>
      </el-breadcrumb>
      <h5>{{ detail.NT_NAME }}</h5>
      <p>
        <span>发布人：{{ detail.NT_SENDER }}</span>
        <span>发布时间：{{ detail.NT_TIME }}</span>
        <!-- <a href="javascript:window.close();" class="link-a">【关闭窗口】</a> -->
      </p>
      <div class="content">{{ detail.NT_CONTENT }}</div>
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
  components: {
    NavIn
  },
  data() {
    return {
      operate: "list",
      detail: {},
      loading: false,
      noticievw: null,
      notic: 2
    };
  },
  watch: {
    $route(to, from) {}
  },
  mounted() {
    this.getDetail();
  },
  methods: {
    backPage: function() {
      this.$router.push("/");
      var data = { tag: "通知公告", path: "notice", index: 2 };
      window.sessionStorage.setItem("nav", JSON.stringify(data));
    },
    getNav(data, index) {
      this.$router.push("/");
      this.noticievw = data;
      this.noticievw.index = index;
      window.sessionStorage.setItem("nav", JSON.stringify(this.noticievw));
    },
    getDetail: function() {
      this.loading = true;
      var obj = {
        uid: "2054",
        wiid: this.$route.query.wiid
      };
      dataService.checkNotice(obj).then(res => {
        this.loading = false;
        this.detail = res[0];
        this.detail.WJ_NR = "";
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
  // left: 40px;
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
  }

  .detail {
    position: relative;
    width: 80%;
    margin: 0px auto;
    background: #ffffff;
    padding-left: 40px;
    padding-right: 40px;
    h5 {
      height: 38px;
      line-height: 38px;
      text-align: center;
      font-size: 20px;
      font-weight: bold;
    }
    p {
      margin: 15px 0;
      font-size: 14px;
      color: #888;
      text-align: right;
      span {
        margin-right: 20px;
      }
    }
    .content {
      text-indent: 2em;
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
}
</style>
