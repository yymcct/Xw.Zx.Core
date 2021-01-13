<template>
  <div class="box">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item style="cursor:pointer">
        <a @click="backPage">公示公告</a>
      </el-breadcrumb-item>
      <el-breadcrumb-item v-if="operate=='detail'">正文</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="news-box clearfix" v-loading="loading"> 
      <template v-if="operate=='list'">
        <ul>
          <li
            v-for="(item, index) in catList"
            :key="index"
            :class="{ cur: cur == index }"
            @click="listHandle(item.ZZID,item.ZZMLID)"
          >
            <a>
              <h1>{{ item.XMMC }}-({{item.CZZT}})</h1>
              <em>
                <span>
                  <i>公示日期</i>
                  <span>{{ item.FZRQ }}</span>
                </span>
              </em>
            </a>
          </li>
        </ul>
        <!-- <el-pagination background
          @current-change="handleCurrentChangeFun"
          :page-size="option.LIMIT"
          :current-page="option.PAGE"
          layout="total, prev, pager, next, jumper"
          :total="total"
        ></el-pagination> -->
         <Pagination
        :total="total"
        :pageSize="option.LIMIT"
        :page="option.PAGE"
        @handleCurrentChangeSub="handleCurrentChangeFun"
      ></Pagination>
      </template>
      <transition name="el-zoom-in-center">
        <div class="transition-box" v-if="operate=='detail'">
          <router-view />
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
import Pagination from "@/components/pagination";
import * as dataService from "@/public/apiService/home";
import { apiUrl } from "@/public/apiUrl";
var userInfo =
  localStorage.getItem("data") && JSON.parse(localStorage.getItem("data"));
export default {
  name: "study",
  components: {
    Pagination,
  },
  data() {
    return {
      operate: "list",
      catList: [],
      cur: 0,
      option: {
        PAGE: 1,
        LIMIT: 6,
      },
      total: 0,
      loading: false,
      DetailData: null,
    };
  },
  created() {
    this.getAnnouncementData();
  },
  methods: {
    getAnnouncementData() {
      this.operate = "list";
      this.catList = [];
      this.$http
        .get(apiUrl.GET_HOME_ANNOUNCEMENT, { params: this.option })
        .then((res) => {
          this.catList = res.data.data;
          this.total = res.data.count;
        });
    },
    listHandle: function (ZZID, ZZMLID) {
      this.$http
        .get(apiUrl.GET_HOME_ANNOUNCEMENT_DETAIL, { params: { ZZID: ZZID } })
        .then((res) => {
          if (!res.data.data) {
            this.$message.warning(res.data.msg);
            return;
          }
          this.operate = "detail";
          this.$router.push({
            name: "announDetail",
            params: { ZZID: ZZID, ZZMLID: ZZMLID },
          });
        });
    },
    handleCurrentChangeFun(v) {
      //页面点击
      this.option.PAGE = v;
      this.getAnnouncementData(); //更新列表
    },
    backPage: function () {
      if (this.operate == "list") {
        return;
      }
      this.operate = "list";
      this.$router.push("/");
    },
  },
};
</script>

<style lang="scss" scoped>
.news-box {
  padding: 0 74px 60px 50px;
  background: #fff;
  ul {
    margin-bottom: 30px;
    min-height: 300px;
  }
  li {
    padding: 5px;
    border-bottom: 1px dashed #ccc;
    cursor: pointer;
    a {
      display: block;
      padding: 15px 0;
      position: relative;
      h1 {
        font-weight: normal;
        font-family: "microsoft yahei";
        margin: 0;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
        position: relative;
        line-height: 28px;
        height: auto;
        color: #000;
        font-size: 16px;
        padding-bottom: 10px;
      }
      &:after {
        content: ">";
        font-family: simsun;
        float: right;
        color: #999;
        top: 30px;
        right: 15px;
        font-size: 22px;
        position: absolute;
        display: block;
        clear: both;
        height: 0;
      }
    }
    em {
      display: block;
      color: #999;
      line-height: 28px;
      height: auto;
      font-size: 0.75em;
      font-style: normal;
      padding: 0;
      margin: 0;
      font-family: Arial;
      span {
        margin-left: 10px;
        &:first-child {
          margin-left: 0px;
        }
        i {
          font-size: 12px;
          border: 1px solid #e8e8e8;
          border-radius: 5px;
          padding: 2px 5px;
          line-height: 16px;
          margin-top: 3px;
          font-family: "Microsoft Yahei";
          font-style: normal;
        }
      }
    }
    // display: flex;
    // p {
    //   flex: 1;
    //   overflow: hidden;
    //   white-space: nowrap;
    //   text-overflow: ellipsis;
    // }
    // span {
    //   width: 100px;
    //   text-align: right;
    // }
  }
  li:hover {
    background: #f7f7f7;
    // color: #fff;
    &::after {
      border-left-color: #fff;
    }
  }
}
</style>
