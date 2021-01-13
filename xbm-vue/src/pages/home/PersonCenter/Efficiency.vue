<template>
  <div class="EleLicense grid-inner-content">
    <div class="panel-header">
      <p class="tit">效能监管</p>
      <!-- <p class="todo-right">
        <el-radio-group v-model="activeNum" size="mini" @change="onChange">
          <el-radio-button label="SP">行政审批</el-radio-button>
          <el-radio-button label="ZW">政务管理</el-radio-button>
          <el-radio-button label="CH">项目策划</el-radio-button>
          <el-radio-button label="SC">联合审查</el-radio-button>
        </el-radio-group>
      </p>-->
      <!-- <span class="right more" @click="checkMore">查看更多</span> -->
    </div>
    <div class="panel-body" v-loading="loading">
      <div class="index-empty-data" v-if="!data">
        <img src="@/assets/images/nodata.png" class="empty-img" />
        <p class="empty-text">暂无数据</p>
      </div>
      <div id="main" style="width: 100%;height:100%;" v-else></div>
    </div>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/home.js";
import echarts from "echarts";
export default {
  name: "Home",
  data: function () {
    return {
      loading: false,
      data: null,
      activeNum: "SP",
    };
  },
  created() {},
  mounted() {
    this.getDataList();
  },
  methods: {
    onChange: function (val) {
      console.log(val);
      this.getDataList();
    },
    getDataList: function () {
      this.data = [];
      this.loading = true;
      dataService.getHomeEfficiencyCount().then((res) => {
        this.loading = false;
        this.drawChart(res);
      });
    },
    drawChart: function (data) {
      let SPData = data["SP"];
      let ZWData = data["ZW"];
      let CHData = data["CH"];
      let SCData = data["SC"];
      var option = {
        tooltip: {
          trigger: "axis",
          axisPointer: {
            // 坐标轴指示器，坐标轴触发有效
            type: "shadow", // 默认为直线，可选为：'line' | 'shadow'
          },
        },
        legend: {
          data: ["待办数", "办结数", "超期数"],
        },
        grid: {
          left: "3%",
          right: "4%",
          bottom: "3%",
          containLabel: true,
        },
        xAxis: [
          {
            type: "category",
            data: ["行政审批", "政务管理", "项目策划", "联合审查"],
          },
        ],
        yAxis: [
          {
            type: "value",
          },
        ],
        series: [
          {
            name: "待办数",
            type: "bar",
            barWidth: 15,
            itemStyle: {
              barBorderRadius: [30, 30, 0, 0],
              color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                {
                  offset: 0,
                  color: "#f9c392",
                },
                {
                  offset: 1,
                  color: "#f09b67",
                },
              ]),
            },
            label: {
              show: true,
              position: "top",
            },
            data: [
              SPData["dbnum"],
              ZWData["dbnum"],
              CHData["dbnum"],
              SCData["dbnum"],
            ],
          },
          {
            name: "办结数",
            type: "bar",
            barWidth: 15,
            itemStyle: {
              normal: {
                barBorderRadius: [30, 30, 0, 0],
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                  { offset: 0, color: "#83bff6" },
                  { offset: 0.5, color: "#188df0" },
                  { offset: 1, color: "#188df0" },
                ]),
              },
            },
            label: {
              show: true,
              position: "top",
            },
            data: [
              SPData["bjnum"],
              ZWData["bjnum"],
              CHData["bjnum"],
              SCData["bjnum"],
            ],
          },
          {
            name: "超期数",
            type: "bar",
            barWidth: 15,
            label: {
              show: true,
              position: "top",
            },
            itemStyle: {
              normal: {
                //柱形图圆角，初始化效果
                barBorderRadius: [30, 30, 0, 0],
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                  { offset: 0, color: "#f9a983" },
                  { offset: 0.5, color: "#fd736a" },
                  { offset: 1, color: "#f76263" },
                ]),
              },
            },
            data: [
              SPData["cqnum"],
              ZWData["cqnum"],
              CHData["cqnum"],
              SCData["cqnum"],
            ],
          },
        ],
      };
      let charts = echarts.init(document.getElementById("main"));
      charts.setOption(option);
      setTimeout(function () {
        window.onresize = function () {
          charts.resize();
        };
      }, 200);
    },
  },
  components: {},
};
</script>

<style lang="scss" scoped>
.EleLicense {
  background: #f8f8f8;
  // padding: 10px 20px;
  margin-bottom: 10px;
  cursor: pointer;
  .tit {
    display: inline-block;
  }
  .todo-right {
    float: right;
  }
  .panel-body {
    color: red;
    /deep/ .todo-table {
      border: none;
      th {
        color: #3b4477;
        background: #f2f3fe;
        // color:#456573;
        // background: #ebf9ff;
      }
      th.is-leaf,
      td {
        border: none;
        padding: 8px 0px;
      }
      &:before {
        background: none;
      }
      .warning-row {
        background: oldlace;
      }
      .success-row {
        background: #f0f9eb;
      }
    }
    .index-empty-data {
      width: calc(100% + 10px);
      height: calc(100% + 10px);
      background: #fbfbfb;
      margin: -10px;
      text-align: center;
      .empty-img {
        width: 120px;
        margin-top: 20%;
      }
      .empty-text {
        color: #b7b5b5;
        padding-top: 5px;
      }
    }
  }
}
</style>
