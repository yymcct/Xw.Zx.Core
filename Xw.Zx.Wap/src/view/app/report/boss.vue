<template>
  <div class="wrapper">
    <canvas id="reportMouth" class="content-canvas"></canvas>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
import F2 from "@antv/f2/lib/index-all";
export default {
  name: "Report",
  props: [""],
  data() {
    return {
      reportData: null,
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    F2.Global.setTheme({
      colors: [
        "#06a65e",
        "#D66BCA",
        "#8543E0",
        "#8E77ED",
        "#3436C7",
        "#737EE6",
        "#223273",
        "#7EA2E6",
      ],
      pixelRatio: window.devicePixelRatio,
      guide: {
        line: {
          stroke: "#06a65e",
          lineWidth: 2,
        },
      },
    });
  },

  mounted() {
    api.report.get().then((res) => {
      this.reportData = res.result;
      this.reportDataCount();
    });
  },

  methods: {
    reportDataCount: function () {
      const chart = new F2.Chart({
        id: "reportMouth",
        pixelRatio: window.devicePixelRatio,
      });
      chart.source(this.reportData.dayInfos, {
        count: {
          //    tickCount: 6,

          min: 0,
          //   max: 300,
        },
        showText: {
          type: "timeCat",
          mask: "MM-DD",
        },
      });

      chart.line().position("time*totalMoney");
      chart.point().position("time*totalMoney").style({
        stroke: "#fff",
        lineWidth: 1,
      });
      chart.scale("time", {
        // 各个属性配置
        type: "timeCat",
        sortable: false,
        formatter: function (val) {
          console.log(val);
         console.log((new Date(val)).toLocaleDateString()) 
          return (new Date(val)).Format("MM-dd");
        },
      });

      chart.render();
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.content-canvas {
  width: 100%;
  box-sizing: border-box;
}
</style>