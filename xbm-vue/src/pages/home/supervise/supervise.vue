<template>
  <!-- 效能监管 -->
  <div class="supervisebox" v-loading="loading">
    <Breadcrumb :title="breadcrumbItem"></Breadcrumb>
    <div class="super-inner clearfix">
      <div
        id="chart"
        style="transion:all 1s ease;width: 100%;height:500px"
      ></div>
      <transition name="el-zoom-in-top">
        <div class="chart-table transition-box" v-if="detailData.length">
          <el-table
            :data="detailData"
            stripe
            height="600"
            class="todo-table"
            v-loading="tableLoading"
          >
            <el-table-column
              type="index"
              label="序号"
              width="70px"
              align="center"
            ></el-table-column>
            <template v-if="barindex == 4">
              <el-table-column
                label="项目名称"
                prop="PROJECTNAME"
                show-overflow-tooltip
              >
              </el-table-column>
              <el-table-column
                prop="PROJID"
                label="申报号"
                width="120"
              ></el-table-column>
              <el-table-column
                prop="APPLYNAME"
                label="申请对象"
                show-overflow-tooltip
              ></el-table-column>
              <el-table-column
                prop="SERVICENAME"
                label="事项类型"
                show-overflow-tooltip
              ></el-table-column>
              <el-table-column
                prop="RECEIVETIME"
                label="申报时间"
                show-overflow-tooltip
              ></el-table-column>
              <el-table-column
                label="项目状态"
                min-width="105"
                show-overflow-tooltip
                align="left"
              >
                <template slot-scope="scope">
                  <div
                    class="cell el-tooltip"
                    :style="{
                      color:
                        active == '待办数'
                          ? '#f09b67'
                          : active == '超期数'
                          ? '#f76263'
                          : '#188df0'
                    }"
                  >
                    {{ stateRela[scope.row.STATE] }}
                  </div>
                </template>
              </el-table-column>
            </template>
            <template v-else>
              <el-table-column
                prop="BH"
                label="项目编号"
                show-overflow-tooltip
              ></el-table-column>
              <el-table-column
                prop="XNAME"
                label="项目名称"
                show-overflow-tooltip
              ></el-table-column>
              <el-table-column prop="YWLX" label="业务类型"></el-table-column>
              <el-table-column
                prop="UNAME"
                label="用户名"
                v-if="active == '待办数'"
                width="120px"
              ></el-table-column>
              <el-table-column
                prop="BJSJ"
                label="办结时间"
                v-if="active == '办结数'"
                width="100px"
              ></el-table-column>
              <el-table-column label="状态" width="80px">
                <template slot-scope="scope">
                  <span
                    :style="{
                      color:
                        active == '待办数'
                          ? '#f09b67'
                          : active == '超期数'
                          ? '#f76263'
                          : '#188df0'
                    }"
                    >{{ active.split("数")[0] }}</span
                  >
                </template>
              </el-table-column>
            </template>
          </el-table>
          <el-pagination
            class="cus-pagination"
            @current-change="handleCurrentChange"
            :current-page="page"
            :page-size="10"
            background
            layout="total,prev, pager, next"
            :total="total"
          ></el-pagination>
        </div>
      </transition>
    </div>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/home.js";
import Breadcrumb from "@/components/breadcrumb";
import { apiUrl } from "@/public/apiUrl";
import echarts from "echarts";
export default {
  name: "supervise",
  components: {
    Breadcrumb
  },
  data() {
    return {
      stateRela: {
        "0": "未受理",
        "1": "已受理",
        "2": "补齐补正告知",
        "3": "办理中",
        "4": "已办结",
        "5": "挂起"
      },
      SBLYRela: { "0": "工改窗口", "1": "综合窗口", "2": "内网录入" },
      breadcrumbItem: "效能监管",
      loading: false,
      tableLoading: false,
      page: 1,
      active: "待办数",
      detailData: [],
      // chartData: null,
      barindex: 0,
      total: 0,
      barChart: null,
      CKDetaildata: []
    };
  },
  created() {
    this.getCKData().then(res => {
      console.log(res.data.data, "aaaaa");
      this.getDataList(res.data.data);
    });
  },
  methods: {
    getCKData: function() {
      return new Promise((resolve, reject) => {
        this.$http
          .get(apiUrl.GET_LEADER_CKEFFICIENCY_COUNT)
          .then(response => {
            resolve(response);
          })
          .catch(error => {
            reject(error);
          });
      });
    },
    getDataList: function(ckObj) {
      this.data = [];
      this.loading = true;
      dataService.getLeaderEfficiencyCount().then(res => {
        console.log(res, "bbbbbb");
        this.loading = false;
        // this.chartData = res;
        res.CK = ckObj;
        // this.page=1;
        console.log(res, "cccccc");
        this.$nextTick(() => {
          this.drawChart(res);
        });
      });
    },
    drawChart: function(data) {
      let SPData = data["SP"];
      let ZWData = data["ZW"];
      let CHData = data["CH"];
      let SCData = data["SC"];
      let CKData = data["CK"];
      var clickIndex;
      var option = {
        tooltip: {
          trigger: "axis",
          axisPointer: {
            // 坐标轴指示器，坐标轴触发有效
            type: "shadow" // 默认为直线，可选为：'line' | 'shadow'
          }
        },
        legend: {
          data: ["待办数", "办结数", "超期数"]
        },
        grid: {
          left: "3%",
          right: "4%",
          bottom: "3%",
          containLabel: true
        },
        xAxis: [
          {
            type: "category",
            data: [
              "行政审批",
              "政务管理",
              "项目策划",
              "联合审查",
              "窗口受理",
              
            ]
          }
        ],
        yAxis: [
          {
            type: "value",
            min: "dataMin", // 最小
            splitNumber: 6
          }
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
                  color: "#f9c392"
                },
                {
                  offset: 1,
                  color: "#f09b67"
                }
              ])
            },
            label: {
              show: true,
              position: "top"
            },
            // data: [10, 5, 6,3]
            data: [
              SPData["dbnum"],
              ZWData["dbnum"],
              CHData["dbnum"],
              SCData["dbnum"],
              CKData["notAcceptCount"],
             
            ]
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
                  { offset: 1, color: "#188df0" }
                ])
              }
            },
            label: {
              show: true,
              position: "top"
            },
            // data: [2, 5, 6,8]
            data: [
              SPData["bjnum"],
              ZWData["bjnum"],
              CHData["bjnum"],
              SCData["bjnum"],
              CKData["finishCount"],
              10
            ]
          },
          {
            name: "超期数",
            type: "bar",
            barWidth: 15,
            label: {
              show: true,
              position: "top"
            },
            itemStyle: {
              normal: {
                //柱形图圆角，初始化效果
                barBorderRadius: [30, 30, 0, 0],
                color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                  { offset: 0, color: "#f9a983" },
                  { offset: 0.5, color: "#fd736a" },
                  { offset: 1, color: "#f76263" }
                ])
              }
            },
            // data: [5, 8, 6,7]
            data: [
              SPData["cqnum"],
              ZWData["cqnum"],
              CHData["cqnum"],
              SCData["cqnum"],
              CKData["overdueCount"],
              1000
            ]
          }
        ]
      };
      let charts = echarts.init(document.getElementById("chart"));
      this.barChart = charts;
      charts.setOption(option);
      charts.resize();
      charts.on("click", params => {
        if (params.value) {
          this.page = 1;
          this.active = params.seriesName;
          this.barindex = params.dataIndex;
          this.detailData = [];
          if (params.dataIndex == 4) {
            this.getCKTableList(params.seriesName);
            return;
          }
          this.getTableList(params.seriesName, params.dataIndex);
        }
      });
      charts.getZr().on("click", params => {
        let pointInPixel = [params.offsetX, params.offsetY];
        if (charts.containPixel("grid", pointInPixel)) {
          let xIndex = charts.convertFromPixel(
            { seriesIndex: 0 },
            pointInPixel
          );

          console.log(xIndex, option.series[xIndex], "xIndex");
        }
      });
      setTimeout(function() {
        window.onresize = function() {
          charts.resize();
        };
      }, 200);
    },
    getCKTableList: function(type) {
      let params = { 待办数: "0", 办结数: "4", 超期数: "6" };
      this.tableLoading = true;
      this.$http
        .get(apiUrl.CHECK_CK_EFFICIENCY_DETAIL, {
          params: { type: params[type] }
        })
        .then(res => {
          this.tableLoading = false;
          this.CKDetaildata = res.data.data;
          this.detailData = this.CKDetaildata.slice(0, 10);
          this.total = res.data.count;
          $("#chart").height(350);
          this.barChart.resize();
        });
    },
    getTableList: function(type, index) {
      let severName = [
        "CheckSPEfficiencyDetail",
        "CheckZWEfficiencyDetail",
        "CheckCHEfficiencyDetail",
        "CheckSCEfficiencyDetail"
      ];
      let params = { 待办数: "db", 办结数: "bj", 超期数: "cq" };
      // this.detailData = [];
      this.tableLoading = true;
      dataService[severName[index]](this.page, params[type]).then(res => {
        this.tableLoading = false;
        if (!res.data.length) {
          this.$message.warning("暂未查到数据!");
          return;
        }
        this.detailData = res.data;
        this.total = res.size;
        $("#chart").height(350);
        this.barChart.resize();
      });
    },
    handleCurrentChange: function(val) {
      this.page = val;
      if (this.barindex == 4) {
        this.tableLoading = true;
        setTimeout(() => {
          this.tableLoading = false;
          var num = (val - 1) * 10;
          this.detailData = this.CKDetaildata.slice(num, num + 10);
        }, 300);
        return;
      }
      this.getTableList(this.active, this.barindex);
    }
  }
};
</script>

<style lang="scss" scoped>
.supervisebox {
  background: #fff;
  min-height: 600px !important;
  .super-inner {
    width: 90%;
    height: calc(100% - 50px);
    margin: 0 auto;
    padding-top: 20px;
    .chart-table {
      width: calc(100% - 30px);
      height: 400px;
      margin: 0 auto;
      /deep/ .todo-table {
        border: none;
        height: calc(100% - 80px) !important;
        th {
          color: #3b4477;
          background: #f2f3fe;
        }
        th.is-leaf,
        td {
          border: none;
          padding: 8px 0px;
        }
        &:before {
          background: none;
        }
      }
      .cus-pagination {
        width: 100%;
        text-align: center;
        padding-top: 15px;
      }
    }
  }
  // .left-menu {
  //   float: left;
  //   width: 278px;
  //   box-shadow: -2px 2px 3px 0px rgba(0, 0, 0, 0.15);
  //   border-top: 3px solid rgba(7, 67, 139, 1);
  // }
  // .right-box {
  //   width: 900px;
  //   float: right;
  // }
}
</style>
