



<template>
  <div>
    <el-col :span="24" class="toolbar" style="padding-bottom: 0px">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-input
            v-model.trim="filters.keyword"
            placeholder="姓名,电话,支付宝,备注"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-date-picker
            v-model="filters.addTimeStart"
            type="date"
            placeholder="开始时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
          <el-date-picker
            v-model="filters.addTimeEnd"
            type="date"
            placeholder="结束时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch"
            >查询</el-button
          >
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
        withdrawDepositState: 999,
        keyword: null,
        addTimeStart: null,
        addTimeEnd: null,
      },
    };
  },
  methods: {
    handleSearch() {
      let filtersStr = "";
      if (this.filters.keyword)
        filtersStr += `(Remark|RealName|Phone|AliPayAccount)@=${this.filters.keyword},`;

      if (this.filters.addTimeStart)
        filtersStr += `AddTime>=${this.filters.addTimeStart},`;

      if (this.filters.addTimeEnd)
        filtersStr += `AddTime<=${this.filters.addTimeEnd},`;

      this.$emit("search", filtersStr);
    },
  },

  mounted() {},
};
</script>

<style scoped>
</style>