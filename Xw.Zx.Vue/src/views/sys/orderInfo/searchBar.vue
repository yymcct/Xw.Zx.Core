



<template>
  <div>
    <el-col :span="24" class="toolbar" style="padding-bottom: 0px">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <!-- <el-date-picker
            v-model="filters.startTime"
            type="date"
            placeholder="开始时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
          <el-date-picker
            v-model="filters.endTime"
            type="date"
            placeholder="结束时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker> -->
          <date-picker @change="datepickerChange" />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">查询</el-button>
        </el-form-item>
      </el-form>
    </el-col>
  </div>
</template>

<script>
import datePicker from "@/components/datePicker";
export default {
  components: {
    datePicker,
  },
  data() {
    return {
      filters: {
        startTime: "2020-12-01",
        endTime: "2020-12-31",
      },
    };
  },
  methods: {
    datepickerChange(val) {
      console.log(1111,val);
      this.filters.startTime = val[0];
      this.filters.endTime = val[1];
    },
    handleSearch() {
      this.$emit("search", this.filters);
    },
    handleAdd() {
      this.$emit("add");
    },
  },

  mounted() {},
};
</script>

<style scoped>
</style>
<style lang="scss" scoped>
</style>