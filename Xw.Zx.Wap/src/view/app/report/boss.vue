<template>
  <div class="wrapper">
    <van-nav-bar
      :title="$route.meta.title"
      left-arrow
      @click-left="$router.go(-1)"
    />
    <div class="canvas">
      <p class="title">交易趋势</p>
      <canvas id="reportMouth" class="content-canvas"></canvas>
    </div>

    <div class="detail" v-if="reportDataReverse">
      <p class="title">交易明细</p>
      <ul>
        <li>
          <p class="table-data th">日期</p>
          <p class="table-app th">APP</p>
          <p class="table-wechat th">公众号</p>
          <p class="table-total th" style="color: #000">交易合计</p>
        </li>
        <li v-for="(item, index) in reportDataReverse" :key="index">
          <p class="table-data">{{ new Date(item.time).Format("MM-dd") }}</p>
          <p class="table-app">{{ item.appMoney }}</p>
          <p class="table-wechat">{{ item.xtsuoMoney }}</p>
          <p class="table-total">{{ item.totalMoney }}</p>
        </li>
      </ul>
    </div>
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
      reportDataReverse: null,
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    F2.Global.setTheme({
      colors: [
        "#FF5000",
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
          stroke: "#FF5000",
          lineWidth: 2,
        },
      },
    });
  },

  mounted() {
    api.report.get().then((res) => {
      this.reportData = res.result;
      this.reportDataReverse = res.result.dayInfos.concat();
      this.reportDataReverse.reverse();
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
        // count: {
        //   //    tickCount: 6,
        //   min: 0,
        //   //   max: 300,
        // },
        // showText: {
        //   type: "timeCat",
        //   mask: "MM-DD",
        // },
      });

      chart.line().position("time*totalMoney");
      chart.point().position("time*totalMoney").style({
        stroke: "#fff",
        lineWidth: 1,
      });
      chart.area().position("time*totalMoney").style({});
      chart.scale("time", {
        // 各个属性配置
        type: "timeCat",
        sortable: false,
        formatter: function (val) {
          return new Date(val).Format("MM-dd");
        },
      });

      chart.render();
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  background-color: #fff;
  .canvas {
    p {
      padding: 10px 10px 0 10px;
      font-size: 18px;
      font-weight: bolder;
    }
  }
  .detail {
    .title {
      padding: 20px 10px 10px 10px;
      font-size: 18px;
      font-weight: bolder;
    }
    ul {
      padding: 10px;
      li {
        display: flex;
        justify-content: space-around;
        //  padding-bottom: 6px;
        border-bottom: 1px #e8e8e8 solid;
        p {
          font-size: 15px;
          font-weight: normal;
          color: #333;
          padding: 10px 0px;
        }
        .th {
          font-weight: bold;
        }
        .table-data {
          width: 20%;
        }
        .table-app {
          width: 26%;
        }
        .table-wechat {
          width: 26%;
        }
        .table-total {
          width: 28%;
          font-weight: bold;
          color: #ff5000;
        }
      }
    }
  }
}
.content-canvas {
  width: 100%;
  box-sizing: border-box;
}
</style>