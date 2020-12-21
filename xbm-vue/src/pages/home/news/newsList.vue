<template>
  <div
    class="box"
    v-loading="loading"
    element-loading-text="拼命加载中"
    element-loading-spinner="el-icon-loading"
  >
    <ul v-if="total > 0">
      <li v-for="(item, index) in data" :key="index" @click="handleClick(item)">
        <!-- <h5>{{item.NAME}}</h5> -->
        <p>{{ item.WJ_NAME }}</p>
        <span>{{ item.SCSJ }}</span>
        <span>发布人：{{ item.UR_IDENT }}</span>
      </li>
      <Pagination
        :total="total"
        :pageSize="10"
        @handleSizeChangeSub="handleSizeChangeFun"
        @handleCurrentChangeSub="handleCurrentChangeFun"
      ></Pagination>
    </ul>

    <div v-else class="empty">暂无数据</div>
  </div>
</template>

<script>
import Pagination from "@/components/pagination";
import * as dataService from "@/public/apiService/home";
import axios from "axios";
export default {
  name: "newslist",
  components: {
    Pagination
  },
  data() {
    return {
      loading: false,
      data: [],
      pageNum: 1,
      formInline: {
        page: 1,
        pagesize: 10,
        mlid: "",
        wj_name: "",
        lg_time: "",
        lg_move: "",
        fl: 1
      },
      total: 0
    };
  },
  mounted() {
    this.getLawsData();
  },
  methods: {
    getLawsData(type) {
      this.loading = true;
      this.formInline.mlid = type;
      dataService.getLawsData(this.formInline).then(res => {
        this.data = res.DATA;
        this.total = res.SIZE;
        this.loading = false;
      });
    },
    handleClick(obj) {
      // let routeData = this.$router.resolve({
      //   path: "/newsDetail",
      //   query: { wiid: obj.WIID, type: obj.NAME }
      // });
      // window.open(routeData.href, "_blank");
      this.$router.push({
        path: "/newsDetail",
        query: { wiid: obj.WIID, type: obj.NAME }
      });
    },
    handleSizeChangeFun(v) {
      this.formInline.pagesize = v; //当前页
      this.getLawsData();
    },
    handleCurrentChangeFun(v) {
      //页面点击
      this.formInline.page = v; //当前页
      this.getLawsData();
    }
  }
};
</script>

<style lang="scss" scoped>
.box {
  ul {
    padding-top: 15px;
  }
  li {
    color: #666;
    margin-bottom: 10px;
    border-bottom: 1px solid #ddd;
    padding: 6px 10px;
    cursor: pointer;
    &:hover {
      box-shadow: 0px 1px 5px #ccc;
    }
    h5 {
      width: 618px;
      height: 20px;
      font-size: 16px;
      font-weight: bold;
      color: rgba(51, 51, 51, 1);
      line-height: 18px;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }
    span {
      display: inline-block;
      line-height: 36px;
      margin-right: 36px;
      font-size: 12px;
    }
    p {
      line-height: 28px;
      max-height: 56px;
      font-size: 14px;
      display: -webkit-box;
      -webkit-box-orient: vertical;
      -webkit-line-clamp: 2;
      overflow: hidden;
      font-weight: bold;
    }
  }
  li:hover {
    h5 {
      color: #07438b;
    }
  }
}
</style>
