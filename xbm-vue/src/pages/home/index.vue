<template>
  <div class="box">
    <div class="part1 clearfix">
      <Carousel class="left"></Carousel>
      <div class="right">
        <h5>
          <p>新闻中心</p>
          <span
            class="right more"
            @click="lookMore({ path: 'news', index: '3' })"
            >查看更多</span
          >
        </h5>
        <CommonList
          :type="listType[0]"
          :list="newLists"
          @handleClick="handleClick"
          v-loading="loading"
          element-loading-text="拼命加载中"
          element-loading-spinner="el-icon-loading"
        ></CommonList>
      </div>
    </div>
    <div class="part3">
      <Part3></Part3>
    </div>
    <div class="part2 clearfix">
      <div class="left">
        <h5>
          <p>通知公告</p>
          <span
            class="right more"
            @click="lookMore({ path: 'notice', index: '2' })"
            >查看更多</span
          >
        </h5>
        <CommonList
          :type="listType[1]"
          :list="noticesLists"
          @handleClick="handleClick"
        ></CommonList>
      </div>
      <div class="notice-right">
        <h5>
          <p>公示公告</p>
          <span
            class="right more"
            @click="lookMore({ path: 'Announcement', index: '6' })"
            >查看更多</span
          >
        </h5>
        <!-- @handleClick="handleClick" -->
        <CommonList
          :type="listType[4]"
          :list="AnnounceList"
          @handleClick="handleClick"
        ></CommonList>
      </div>
      <!-- <Right2 class="right" v-on:func="getPath"></Right2> -->
    </div>

    <div class="part4">
      <div class="left">
        <h5>
          <p>政策法规</p>
          <span
            class="right more"
            @click="lookMore({ path: 'laws', index: '4' })"
            >查看更多</span
          >
        </h5>
        <CommonList
          :type="listType[2]"
          :list="lawsList"
          @handleClick="handleClick"
        ></CommonList>
      </div>
      <div class="right">
        <h5>
          <p>学习教育</p>
          <span
            class="right more"
            @click="lookMore({ path: 'study', index: '5' })"
            >查看更多</span
          >
        </h5>
        <CommonList
          :type="listType[3]"
          :list="studyList"
          @handleClick="handleClick"
        ></CommonList>
      </div>
    </div>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/home";
import Carousel from "@/pages/home/Carousel.vue";
import Right2 from "@/pages/home/right2.vue";
import Part3 from "@/pages/home/part3.vue";
import CommonList from "@/pages/home/CommomList.vue";
import { MessageBox } from "element-ui";
import _ from "lodash";
import { apiUrl } from "@/public/apiUrl";
var data = JSON.parse(localStorage.getItem("data")) || {};
export default {
  name: "home",
  components: {
    Carousel,
    Right2,
    Part3,
    CommonList
  },
  data() {
    return {
      newLists: [],
      noticesLists: [],
      AnnounceList: [],
      lawsList: [],
      studyList: [],
      listType: [0, 1, 2, 3, 4],
      formInline: {
        page: 1,
        pagesize: 10,
        mlid: "",
        wj_name: "",
        lg_time: "",
        lg_move: ""
      },
      option: {
        page: 1,
        uid: data.ur_ident || "",
        nt_name: "",
        nt_sender: "",
        page: 1,
        zt: ""
      },
      type: null,
      loading: false
    };
  },
  mounted() {
    this.getNewsData(1); //新闻
    this.getNewsData(0); //政策法规
    this.getNoticeData(); //通知公告
    this.getStudyData(); //学习教育
    this.getAnnouncementData(); //公示公告
  },
  methods: {
    handleClick(item, type) {
      var routeData = null;
      if (type == 0) {
        // routeData = this.$router.resolve({
        //   path: "/newsDetail",
        //   query: { wiid: item.WIID, type: item.NAME }
        // });
        // window.open(routeData.href, "_blank");
        this.$router.push({
        path: "/lawsDetail",
        query: { wiid: item.WIID, type: item.NAME }
      });
      } else if (type == 1) {
        // routeData = this.$router.resolve({
        //   path: "/noticeDetail",
        //   query: { wiid: item.WIID, type: item.NAME }
        // });
        // window.open(routeData.href, "_blank");
        this.$router.push({
        path: "/noticeDetail",
        query: { wiid: item.WIID, type: item.NAME }
      });
      } else if (type == 2) {
        // routeData = this.$router.resolve({
        //   path: "/lawsDetail",
        //   query: { wiid: item.WIID, type: item.NAME }
        // });
        // window.open(routeData.href, "_blank");
        this.$router.push({
        path: "/lawsDetail",
        query: { wiid: item.WIID, type: item.NAME }
      });
      } else if (type == 4) {
        // routeData = this.$router.resolve({
        //   path: "/homeAnnounDetail",
        //   query: { ZZID: item.ZZID, ZZMLID: item.ZZMLID }
        // });
        // window.open(routeData.href, "_blank");
        this.$router.push({
        path: "/homeAnnounDetail",
        query: { ZZID: item.ZZID, ZZMLID: item.ZZMLID }
      });
      } else {
        this.lookMore({ path: "study", index: "5" });
      }
    },
    getNewsData(type) {
      this.type = type;
      let temp = null;
      let list = [];
      this.loading = true;
      temp = _.clone(this.formInline);
      temp.fl = type;
      dataService.getLawsData(temp).then(res => {
        list = res.DATA;
        this.loading = false;
        type == 0 ? (this.lawsList = list) : (this.newLists = list);
      });
    },
    getNoticeData() {
      dataService
        .homeNotice(this.option)
        .then(res => {
          this.noticesLists = res.DATA;
        })
        .catch(err => {
          console.log(err);
        });
    },
    getAnnouncementData() {
      this.AnnounceList = [];
      this.$http
        .get(apiUrl.GET_HOME_ANNOUNCEMENT, { params: { LIMIT: 10, PAGE: 1 } })
        .then(res => {
          res.data.data &&
            res.data.data.forEach(item => {
              item.BT = item.XMMC;
              item.CJRQ = item.FZRQ;
              this.AnnounceList.push(item);
            });
        });
      // dataService
      //   .homeAnnouncement(10,1)
      //   .then(res => {
      //     console.log(res,'res+++');
      //     this.AnnounceList = res.data;
      //   })
      //   .catch(err => {
      //     console.log(err);
      //   });
    },
    //homeStudy
    getStudyData() {
      dataService
        .homeStudy(this.option)
        .then(res => {
          this.studyList = res.DATA;
        })
        .catch(err => {
          console.log(err);
        });
    },
    lookMore(obj) {
      sessionStorage.setItem("nav", JSON.stringify(obj));
      // 刷新页面
      let NewPage = "_empty" + "?time=" + new Date().getTime() / 500;
      this.$router.push(NewPage);
      this.$router.go(-1);
    },
    getPath(data) {
      this.$emit("func1", data, 4);
    }
  }
};
</script>

<style lang="scss" scoped>
.box {
  padding-top: 30px;
  height: 100%;
  background: #f2f2f2;
  h5 {
    height: 45px;
    line-height: 45px;
    border-left: 3px solid #07438b;
    padding-left: 15px;
    padding-right: 20px;
    font-size: 20px;
    color: #07438b;
    p {
      display: inline-block;
    }
    span.more {
      color: #999;
      font-size: 14px;
      font-weight: normal;
      cursor: pointer;
      &:hover {
        color: red;
      }
    }
  }
  .part1 {
    height: 400px;
    background: #fff;
    div.left {
      width: 690px;
      height: 100%;
    }
    div.right {
      width: 500px;
      height: 100%;
    }
  }
  .part2 {
    background: #fff;
    margin-top: 15px;
    height: 327px;
    .list-box {
      overflow: hidden;
    }
    div.left {
      width: 700px;
      height: 100%;
      overflow-y: auto;
    }
    .notice-right {
      width: 500px;
      height: 100%;
      overflow-y: auto;
    }
  }
  .part3 {
    height: 87px;
    margin-top: 15px;
  }
  .part4 {
    height: 350px;
    margin-top: 15px;
    background: #fff;
    div.left {
      width: 690px;
      height: 100%;
    }
    div.right {
      width: 500px;
      height: 100%;
    }
  }
}
</style>
