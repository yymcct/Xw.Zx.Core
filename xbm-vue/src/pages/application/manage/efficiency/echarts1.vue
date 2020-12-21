<template>
  <div id="main" style="width: 900px;height: 360px;"></div>
</template>

<script>
import echarts from "echarts";
export default {
  name: "",
  data() {
    return {
      charts: "",
      option: {
        title: {
          text: "汇总",
          top: "10",
          left: "10",
        },
        tooltip: {
          trigger: "axis",
        },
        legend: {
          data: ["1", "2"],
          top: "10",
          right: "20",
        },
        grid: {
          left: "8%", // 与容器左侧的距离
          right: "8%", // 与容器右侧的距离
          top: "22%", // 与容器顶部的距离
          bottom: "12%", // 与容器底部的距离
        },
        toolbox: {
          show: false,
        },
        xAxis: {
          type: "category",
          boundaryGap: false,
          data: ["六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
          axisLabel: {
            textStyle: {
              color: "#999999", //坐标的字体颜色
              fontSize: "18",
            },
          },
          axisLine: {
            lineStyle: {
              color: "#E2E2E2", //左边线的颜色
            },
          },
        },
        yAxis: {
          type: "value",
          axisLabel: {
            formatter: "{value} ",
            textStyle: {
              color: "#999999", //坐标的字体颜色
              fontSize: "18",
            },
          },
          axisLine: {
            lineStyle: {
              type: "solid",
              color: "#E2E2E2", //左边线的颜色
            },
          },
          splitLine: {
            show: true,
            lineStyle: {
              color: "#E2E2E2",
            },
          },
        },
        series: [
          {
            name: "最高气温",
            type: "line",
            data: [11, 11, 15, 13, 12, 13, 10],
            symbol: "circle", //折线点设置为实心点
            symbolSize: 4, //折线点的大小
            itemStyle: {
              normal: {
                color: "#F7A63E", //折线点的颜色
                lineStyle: {
                  color: "#F7A63E", //折线的颜色
                },
              },
            },
            markPoint: {
              data: [
                { type: "max", name: "最大值" },
                { type: "min", name: "最小值" },
              ],
            },
          },
          {
            name: "最低气温",
            type: "line",
            data: [1, 2, 2, 5, 3, 2, 0],
            symbol: "circle", //折线点设置为实心点
            symbolSize: 4, //折线点的大小
            itemStyle: {
              normal: {
                color: "#3EAFF7", //折线点的颜色
                lineStyle: {
                  color: "#3EAFF7", //折线的颜色
                },
              },
            },
            markPoint: {
              data: [{ name: "周最低", value: -2, xAxis: 1, yAxis: -1.5 }],
            },
          },
        ],
      },
    };
  },
  methods: {
    drawPie(id) {
      this.charts = echarts.init(document.getElementById(id));
      this.charts.setOption(this.option);
    },
  },
  //调用
  mounted() {
    this.$nextTick(function () {
      this.drawPie("main");
    });
  },
};
</script>

<style>
</style>