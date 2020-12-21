<template>
  <div id="main1" style="width: 900px;height: 360px;"></div>
</template>

<script>
import echarts from "echarts";
export default {
  name: "echarts2",
  data() {
    return {
      charts: "",
      option: {
        color: ["#3EAFF7"],
        tooltip: {
          trigger: "axis",
          axisPointer: {
            // 坐标轴指示器，坐标轴触发有效
            type: "shadow" // 默认为直线，可选为：'line' | 'shadow'
          }
        },
        grid: {
          left: "3%",
          right: "4%",
          bottom: "3%",
          top: "10%",
          containLabel: true
        },
        xAxis: [
          {
            type: "category",
            data: [
              "1",
              "2",
              "3",
              "4",
              "5",
              "6",
              "7",
              "8",
              "9",
              "10",
              "11",
              "12",
              "13",
              "14",
              "15"
            ],
            axisTick: {
              alignWithLabel: true
            },
            axisLabel: {
              textStyle: {
                color: "#999999", //坐标的字体颜色
                fontSize: "18"
              }
            },
            axisLine: {
              lineStyle: {
                type: "solid",
                color: "#E2E2E2" //左边线的颜色
              }
            }
          }
        ],
        yAxis: [
          {
            type: "value",
            axisLabel: {
              textStyle: {
                color: "#999999", //坐标的字体颜色
                fontSize: "18"
              }
            },
            axisLine: {
              lineStyle: {
                type: "solid",
                color: "#E2E2E2" //左边线的颜色
              }
            },
            splitLine: {
              show: true,
              lineStyle: {
                color: "#E2E2E2"
              }
            }
          }
        ],
        series: [
          {
            name: "直接访问",
            type: "bar",
            barWidth: "40%",
            data: [
              10,
              52,
              200,
              334,
              390,
              330,
              220,
              10,
              52,
              200,
              334,
              223,
              100,
              120,
              300
            ]
          }
        ]
      }
    };
  },
  methods: {
    drawPie(id) {
      this.charts = echarts.init(document.getElementById(id));
      this.charts.setOption(this.option);
    }
  },
  //调用
  mounted() {
    this.$nextTick(function() {
      this.drawPie("main1");
    });
  }
};
</script>

<style>
</style>