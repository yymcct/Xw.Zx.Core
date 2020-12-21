<template>
  <div class="dayCount">
    <div class="toolbar">
      <!-- <div class="left-button">
    <el-button type="primary" icon="el-icon-refresh" size="mini"
      >刷新</el-button>
    <el-button type="primary" icon="el-icon-printer" size="mini"
      >导出</el-button>
    </div> -->
      <el-form :inline="true" :model="formInline" class="demo-form-inline">
        <el-form-item label="年度">
          <el-date-picker
            v-model="formInline.day"
            type="year"
            placeholder="选择年"
            value-format="yyyy"
          >
          </el-date-picker>
        </el-form-item>
        <el-form-item>
          <el-button
            type="primary"
            icon="el-icon-search"
            @click="getData"
            size="mini"
            >查询</el-button
          >
        </el-form-item>
      </el-form>
    </div>
    <div
      class="data-table"
      v-loading="loading"
      element-loading-text="拼命加载中"
      element-loading-spinner="el-icon-loading"
    >
      <div id="main1" style="width: 1000px;height: 500px;"></div>
      <!-- <el-table
        :data="tableData"
        style="width: 100%;"
        show-summary
        height="calc(100% - 10px)"
      >
        <el-table-column type="index" label="序号" width="50px" align="center">
        </el-table-column>
        <el-table-column label="办件月统计" align="center">
          <el-table-column prop="time" label="月份" width="120" align="center">
          </el-table-column> -->
      <!-- <el-table-column label="承诺件" align="center"> -->
      <!-- <el-table-column
            prop="accept"
            label="受理"
            align="center"
          ></el-table-column>
          <el-table-column
            prop="back"
            label="补件"
            align="center"
          ></el-table-column> -->
      <!-- </el-table-column> -->
      <!-- </el-table-column>
      </el-table> -->
    </div>
  </div>
</template>
<script>
import echarts from "echarts";
import { apiUrl } from "@/public/apiUrl";
var year = new Date().getFullYear().toString();
export default {
  name: "mounth",
  data() {
    return {
      days: [],
      sData1: [],
      sData2: [],
      loading: false,
      formInline: {
        type: 2, //1按天 2按月
        day: "" //1按天yyyy-mm  2按月 yyyy
      },
      tableData: [
        {
          date: "2016-05-03",
          province: "上海",
          city: "普陀区"
        }
      ]
    };
  },
  created: function() {
    this.formInline.day = year;
    this.getData();
  },
  methods: {
    drawBar(id, xData, yData, sData) {
      let option = {
        tooltip: {
          // trigger: 'axis',
          axisPointer: {
            // 坐标轴指示器，坐标轴触发有效
            type: "shadow" // 默认为直线，可选为：'line' | 'shadow'
          }
        },
        xAxis: {
          type: "category",
          data: xData,
          axisLabel: {
            interval: 0,
           
          },
          axisline: {
            size: "18px",
            color: "#333333",
            width: 2
          }

        },
        yAxis: {
          type: "value"
          // data: yData,
        },
        legend: {
          data: ["受理", "补件"]
        },
        series: [
          {
            name: "受理",
            type: "bar",
            data: yData
          },
          {
            name: "补件",
            type: "bar",
            data: sData
          }
        ]
      };
      this.charts = echarts.init(document.getElementById(id));
      this.charts.setOption(option);
    },
    getData: function() {
      this.days = [];
      this.sData1 = [];
      this.sData2 = [];
      this.loading = true;
      this.$http
        .get(apiUrl.GET_ACCEPT_COUNT, { params: this.formInline })
        .then(res => {
          var temp = res.data.resultmsg;
          this.tableData = [];
          temp.N.forEach((ele, idx) => {
            var time = idx + 1;
            if (idx >= 0 && idx <= 8)
            {
                time = this.formInline.day +"-0" + time;
            }else if(idx>=9&&idx<=11){
               time = this.formInline.day +"-" + time;
            }
            this.tableData.push({
              time: time + "月",
              accept: temp.Y[idx],
              back: ele
            });
          });
          console.log(this.tableData);
          this.tableData.forEach(ele => {
            this.days.push(ele.time);
            this.sData1.push(ele.accept);
            this.sData2.push(ele.back);
          });
          this.drawBar("main1", this.days, this.sData1, this.sData2);
          // this.FormatJsonData(res.data.data);
          // this.total=res.data.sum;
          this.loading = false;
        });
    },
    onSeach: function() {
      this.getData();
    }
  }
};
</script>

<style lang="scss" scoped>
.dayCount {
  height: 100%;
  .demo-form-inline {
    float: right;
  }
  .left-button {
    display: inline-block;
    margin-top: 6px;
  }
  >>> .data-table {
    height: calc(100% - 38px);
    .el-table--border td,
    .el-table--border th,
    .el-table__body-wrapper
      .el-table--border.is-scrolling-left
      ~ .el-table__fixed {
      border-right: 1px solid #87b9f5;
    }
    .el-table--border th,
    .el-table__fixed-right-patch {
      border-bottom: 1px solid #3a8ee6;
    }
    & td,
    & th {
      padding: 8px 0px;
    }
  }
  >>> .el-form--inline .el-form-item {
    display: inline-block;
    margin-right: 10px;
    vertical-align: top;
    margin-bottom: 10px;
    .el-date-editor--year {
      width: 120px;
    }
    .el-input__inner {
      width: 120px;
      height: 28px;
      line-height: 28px;
    }
  }
  //  .el-table__header{
  //  }
}
//  .el-table td,.el-table th{
//   padding: 6px 0;
// }
.toolbar {
  height: 38px;
  margin-bottom: 5px;
}
</style>
