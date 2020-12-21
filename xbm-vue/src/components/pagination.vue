<template>
  <div>
    <el-pagination
     @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
      :page-size="pageSize"
      layout="total, prev, pager, next, jumper"
      :total="total"
      :current-page="page"
    ></el-pagination>
  </div>
</template>

<script>
export default {
  name: "pagination",
  props: ["total", "pageSize","page"],
  data() {
    return {
      //   currentPage: 1,
    };
  },
  created() {},
  methods: {
    handleSizeChange(val) {
      this.$emit("handleSizeChangeSub", val);
    },
    handleCurrentChange(val) {
      this.$emit("handleCurrentChangeSub", val);
    }
  }
};
</script>

<style lang="scss">
.el-pagination {
  text-align: center;
  .el-pager li.active,
  .el-pager li:hover,
  button:hover {
    color: #07438b;
  }
  .el-pager li.active {
    background: #07438b;
    color: #fff;
    padding: 0;
    border-radius: 5px;
  }
}
</style>