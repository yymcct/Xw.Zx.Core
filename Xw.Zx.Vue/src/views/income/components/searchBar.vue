



<template>
  <div>
    <!--TODO:配置查询条件-->
    <el-col :span="24" class="toolbar" style="padding-bottom: 0px">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-input
            v-model.trim="filters.memberName"
            placeholder="收益人姓名,电话"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-date-picker
            v-model="filters.startTimeStart"
            type="date"
            placeholder="收益开始时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
          <el-date-picker
            v-model="filters.startTimeEnd"
            type="date"
            placeholder="收益结束时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
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
        memberName: null,
        startTimeStart: null,
        startTimeEnd: null,
      },
    };
  },
  methods: {
    handleSearch() {
      let filtersStr = "";

      if (this.filters.memberName)
        filtersStr += `MemberName==${this.filters.memberName},`;

      if (this.filters.startTimeStart)
        filtersStr += `addTime>=${this.filters.startTimeStart},`;
      if (this.filters.startTimeEnd)
        filtersStr += `addTime<=${this.filters.startTimeEnd},`;

      this.$emit("search", filtersStr);
    },
  },

  mounted() {},
};
</script>

<style scoped>
</style>