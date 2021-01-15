



<template>
  <div>
    <!--TODO:配置查询条件-->
    <el-col :span="24" class="toolbar" style="padding-bottom: 0px">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-input
            v-model.trim="filters.out_Order_No"
            placeholder="请输入商家交易号"
            style="width:300px;"
          ></el-input>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="handleSearch">查询</el-button>
        </el-form-item>
      </el-form>
    </el-col>
  </div>
</template>

<script>
export default {
  components: {},
  data() {
    return {
      //TODO:删减查询条件
      filters: {
        out_Order_No: "",
      },
    };
  },
  methods: {
    handleSearch() {
      let filtersStr = "";

      if (this.filters.out_Order_No.length == 22) {
        this.$emit("searchSingleOrder", this.filters.out_Order_No);
        return;
      }

      if (this.filters.out_Order_No)
        filtersStr += `Out_Order_No@=${this.filters.out_Order_No},`;

      this.$emit("search", filtersStr);
    },
  },

  mounted() {},
};
</script>

<style scoped>
</style>


