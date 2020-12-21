<template>
  <div
    class="box"
    v-loading="loading"
    element-loading-text="拼命加载中"
    element-loading-spinner="el-icon-loading"
  >
    <ul>
      <li v-for="(item, index) in data" :key="index" @click="handleClick(item)">
        <span>{{ item.SCSJ }}</span>
        <p>{{ item.WJ_NAME }}</p>
      </li>
    </ul>
    <Pagination
      :total="total"
      :pageSize="10"
      @handleSizeChangeSub="handleSizeChangeFun"
      @handleCurrentChangeSub="handleCurrentChangeFun"
    ></Pagination>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/home";
import Pagination from "@/components/pagination";
export default {
  name: "lists",
  components: { Pagination },
  data() {
    return {
      data: [],
      type: "",
      formInline: {
        page: 1,
        pagesize: 10,
        mlid: "",
        wj_name: "",
        lg_time: "",
        lg_move: "",
        fl: 0
      },
      total: 0,
      loading: false
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
      //   path: "/lawsDetail",
      //   query: { wiid: obj.WIID, type: obj.NAME }
      // });
      // window.open(routeData.href, "_blank");
      this.$router.push({
        path: "/lawsDetail",
        query: { wiid: obj.WIID, type: obj.NAME }
      });
    },
    handleSizeChangeFun(v) {
      this.formInline.pagesize = v;
      //   this._enterpriseList(); //更新列表
    },

    handleCurrentChangeFun(v) {
      //页面点击
      this.formInline.page = v; //当前页
      this.getLawsData(); //更新列表
    }
  }
};
</script>

<style lang="scss" scoped>
.box {
  padding-left: 20px;
  ul {
    min-height: 300px;
  }
  li {
    position: relative;
    color: #666;
    margin-bottom: 8px;
    border-bottom: 1px dotted #e8e8e8;
    padding-bottom: 12px;
    margin-bottom: 12px;
    cursor: pointer;
    &:hover {
      color: #409eff;
      // box-shadow: 0px 1px 5px #ccc;
    }
    span {
      font-size: 12px;
      float: right;
      position: absolute;
      right: 30px;
      top: 5px;
    }
    p {
      width: 700px;
      white-space: nowrap;
      overflow: hidden;
      text-overflow: ellipsis;
      line-height: 28px;
      font-size: 16px;
    }
  }
}
</style>
