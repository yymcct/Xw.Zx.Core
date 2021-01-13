<template>
  <div class="box">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>通知公告</el-breadcrumb-item>
    </el-breadcrumb>
    <div
      class="notice-box"
      v-loading="loading"
      element-loading-text="拼命加载中"
      element-loading-spinner="el-icon-loading"
    >
      <div v-if="total != 0">
        <ul>
          <li
            v-for="(item, index) in lists"
            :key="index"
            @click="handleClick(item)"
          >
            <!-- <a href> -->
            <div class="time-box">
              <p>{{ item.NT_TIME.substr(0, 4) }}</p>
              <span>{{ item.NT_TIME.substr(5) }}</span>
            </div>
            <div class="title-box">
              <h5>{{ item.NT_NAME }}</h5>
              <p>{{ item.NT_CONTENT }}</p>
              <span>发布人：{{ item.NT_SENDER }}</span>
            </div>
            <!-- </a> -->
          </li>
        </ul>
        <Pagination
          :total="total"
          :pageSize="5"
          @handleSizeChangeSub="handleSizeChangeFun"
          @handleCurrentChangeSub="handleCurrentChangeFun"
        ></Pagination>
      </div>
      <div v-else class="empty">暂无数据</div>
    </div>
  </div>
</template>

<script>
import Pagination from "@/components/pagination";
import * as dataService from "@/public/apiService/home.js";
var userInfo =
  localStorage.getItem("data") && JSON.parse(localStorage.getItem("data"));
export default {
  name: "index",
  components: {
    Pagination
  },
  data() {
    return {
      lists: [],
      option: {
        page: 1,
        uid: "",
        nt_name: "",
        nt_sender: "",
        page: 1,
        zt: ""
      },
      total: "",
      loading: false
    };
  },
  created() {
    this.getdata();
  },
  methods: {
    getdata() {
      this.loading = true;
      this.option.uid = userInfo ? userInfo.ur_ident : "";
      dataService
        .homeNotice(this.option)
        .then(res => {
          this.loading = false;
          this.lists = res.DATA;
          this.total = res.SIZE;
        })
        .catch(err => {
          this.loading = false;
          this.total = res.SIZE;
        });
    },
    handleClick(obj) {
      // let routeData = this.$router.resolve({
      //   path: "/noticeDetail",
      //   query: { wiid: obj.WIID, type: obj.NAME }
      // });
      // window.open(routeData.href, "_blank");
      this.$router.push({
        path: "/noticeDetail",
        query: { wiid: obj.WIID, type: obj.NAME }
      });
    },
    handleSizeChangeFun(v) {
      this.option.pagesize = v;
      //   this._enterpriseList(); //更新列表
    },

    handleCurrentChangeFun(v) {
      //页面点击
      this.option.page = v;
      this.getdata(); //更新列表
    }
  }
};
</script>

<style lang="scss" scoped>
.box {
  background: #fff;
}
.notice-box {
  padding-bottom: 20px;
  .empty {
    height: 100%;
    text-align: center;
    line-height: 60px;
  }
  ul {
    min-height: 300px;
  }
  li {
    padding-top: 25px;
    padding-bottom: 15px;
    border: 1px solid #ececec;
    margin-bottom: 10px;
    overflow: hidden;
    cursor: pointer;
    &:hover {
      box-shadow: 0px 1px 5px #ccc;
    }
  }
  .time-box {
    float: left;
    width: 160px;
    text-align: center;
    p {
      width: 75px;
      height: 36px;
      line-height: 36px;
      color: #fff;
      font-weight: bold;
      margin: 0 auto;
      background: -webkit-linear-gradient(left, #4b8fe2, RGBA(12, 65, 132, 1));
      background: -o-linear-gradient(right, #4b8fe2, RGBA(12, 65, 132, 1));
      background: -moz-linear-gradient(right, #4b8fe2, RGBA(12, 65, 132, 1));
      background: linear-gradient(to right, #4b8fe2, RGBA(12, 65, 132, 1));
      margin-bottom: 20px;
    }
  }
  .title-box {
    float: right;
    width: 1010px;
    h5 {
      font-size: 16px;
      font-weight: bold;
      max-width: 400px;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }
    p {
      padding-top: 20px;
      margin-bottom: 10px;
      display: -webkit-box;
      -webkit-box-orient: vertical;
      -webkit-line-clamp: 2;
      overflow: hidden;
      padding-right: 42px;
    }
    span {
      font-size: 12px;
    }
  }
}
</style>
