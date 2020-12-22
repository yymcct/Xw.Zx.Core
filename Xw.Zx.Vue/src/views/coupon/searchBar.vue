



<template>
  <div>
    <!--TODO:配置查询条件-->
    <el-col :span="24" class="toolbar" style="padding-bottom: 0px">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-input
            v-model.trim="filters.memberName"
            placeholder="用户姓名,电话"
          ></el-input>
        </el-form-item>
        <el-form-item>
          <el-date-picker
            v-model="filters.startTimeStart"
            type="date"
            placeholder="领取开始时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
          <el-date-picker
            v-model="filters.startTimeEnd"
            type="date"
            placeholder="领取结束时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
        </el-form-item>

        <el-form-item>
          <el-select
            v-model="filters.couponUseState"
            placeholder="请选择"
            style="width: 120px"
          >
            <el-option
              v-for="item in filters.couponUseStateDrops"
              :key="item.value"
              :label="item.label"
              :value="item.value"
            >
            </el-option>
          </el-select>
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
        couponUseState: 999,
        couponUseStateDrops: [
          { value: 999, label: "全部" },
          { value: 0, label: "未使用" },
          { value: 1, label: "已使用" },
        ],
      },
    };
  },
  methods: {
    handleSearch() {
      let filtersStr = "";

      if (this.filters.memberName) filtersStr += `MemberName==${this.filters.memberName},`;

      if (this.filters.startTimeStart)
        filtersStr += `CreateTime>=${this.filters.startTimeStart},`;
      if (this.filters.startTimeEnd)
        filtersStr += `CreateTime<=${this.filters.startTimeEnd},`;

      if (this.filters.couponUseState !=999)
        filtersStr += `CouponUseState==${this.filters.couponUseState},`;

      
      this.$emit("search", filtersStr);
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