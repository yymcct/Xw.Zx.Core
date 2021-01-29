



<template>
  <div>
    <!--TODO:配置查询条件-->
    <el-col :span="24" class="toolbar" style="padding-bottom: 0px">
      <el-form :inline="true" :model="filters">
        <el-form-item>
          <el-input
            v-model.trim="filters.last_Out_Order_No"
            placeholder="商家交易号"
          ></el-input>
        </el-form-item>

        <el-form-item>
          <el-input
            v-model.trim="filters.subName"
            placeholder="收益人"
          ></el-input>
        </el-form-item>

        <el-form-item>
          <!-- <el-date-picker
            v-model="filters.subTimeStart"
            type="date"
            placeholder="开始时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker>
          <el-date-picker
            v-model="filters.subTimeEnd"
            type="date"
            placeholder="结束时间"
            align="right"
            :picker-options="glpickerOptions"
            value-format="yyyy-MM-dd"
          ></el-date-picker> -->
          <date-picker @change="datepickerChange" />
        </el-form-item>
        <el-form-item>
          <el-input
            v-model.trim="filters.subState"
            placeholder="状态"
          ></el-input>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" @click="handleSearch">查询</el-button>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleAdd">添加</el-button>
        </el-form-item>
      </el-form>
    </el-col>
  </div>
</template>

<script>
import datePicker from "@/components/datePicker";
export default {
  components: {datePicker},
  data() {
    return {
      //TODO:删减查询条件
      filters: {
        id: null,
        transactionID: null,
        last_Out_Order_No: null,
        return_OrderID: null,
        subType: null,
        subAccount: null,
        subName: null,
        subAmount: null,
        subTimeStart: null,
        subTimeEnd: null,
        subState: null,
        payDescription: null,
      },
    };
  },
  methods: {
    datepickerChange(val) {

      this.filters.subTimeStart = val[0];
      this.filters.subTimeEnd = val[1];
    },
    handleSearch() {
      let filtersStr = "";

      if (this.filters.last_Out_Order_No)
        filtersStr += `Last_Out_Order_No==${this.filters.last_Out_Order_No},`;

      if (this.filters.subName)
        filtersStr += `SubName@=${this.filters.subName},`;

      if (this.filters.subAmount)
        filtersStr += `SubAmount==${this.filters.subAmount},`;

      if (this.filters.subTimeStart)
        filtersStr += `SubTime>=${this.filters.subTimeStart},`;
      if (this.filters.subTimeEnd)
        filtersStr += `SubTime<=${this.filters.subTimeEnd},`;

      if (this.filters.subState)
        filtersStr += `SubState@=${this.filters.subState},`;

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