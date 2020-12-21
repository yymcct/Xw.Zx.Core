<template>
  <div class="box">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item :to="{ path: '/' }">首页</el-breadcrumb-item>
      <el-breadcrumb-item>新闻动态</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="news-box clearfix">
      <div class="left">
        <div class="smal-title">新闻动态</div>
        <div class="left-list">
          <ul>
            <li
              v-for="(item, index) in catList"
              :key="index"
              :class="{ cur: cur == index }"
              @click="listHandle(index, item)"
            >
              {{ item.NAME }}
            </li>
          </ul>
        </div>
      </div>
      <div class="right">
        <Newslist ref="child"></Newslist>
      </div>
    </div>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/home";
import Newslist from "./newsList";
export default {
  name: "news",
  components: { Newslist },
  data() {
    return {
      catList: [],
      cur: null
    };
  },
  created() {
    this.getCatList();
  },
  methods: {
    backPage() {
      this.$router.push("/");
    },
    getCatList: function() {
      dataService.getLawsCat(1).then(res => {
        this.catList = res.data;
      });
    },
    listHandle(index, item) {
      this.cur = index;
      this.$refs.child.getLawsData(item.MLID);
    }
  }
};
</script>

<style lang="scss" scoped>
.news-box {
  padding: 0 74px 60px 50px;
  background: #fff;
}
.left {
  width: 230px;
  text-align: center;
  float: left;
  margin-right: 57px;
  .smal-title {
    height: 65px;
    line-height: 65px;
    background: rgba(7, 67, 139, 1);
    font-size: 24px;
    font-family: Microsoft YaHei;
    font-weight: bold;
    color: rgba(255, 255, 255, 1);
    background: url("~@/assets/images/index/news_title.png") no-repeat;
    margin-bottom: 13px;
  }
  .left-list {
    background: #eaeaea;
    padding: 8px 4px 1px;
    li {
      margin: 0 0 8px;
      background: #fff;
      color: #333333;
      font-size: 14px;
      height: 40px;
      line-height: 40px;
      position: relative;
      cursor: pointer;
    }
    li:after {
      content: "";
      height: 0;
      width: 0;
      border: 6px solid transparent;
      border-left-color: #07438b;
      position: absolute;
      top: 10px;
      right: 8px;
    }
    li.cur {
      background: #07438b;
      color: #fff;
      &::after {
        border-left-color: #fff;
      }
    }
  }
}
.right {
  float: right;
  width: 778px;
}
</style>
