<template>
  <div class="home" ref="home" :style="{height:Height+'px'}">
    <el-row :gutter="10" class="home-row home-row-t">
      <el-col :xs="24" :sm="6" :md="6">
        <email :style="{height:Height*0.4 - 15 +'px'}"></email>
      </el-col>
      <el-col :xs="24" :sm="10" :md="10">
        <notice :style="{height:Height*0.4 - 15 +'px'}"></notice>
      </el-col>
      <el-col :xs="24" :sm="8" :md="8">
        <shortCut :style="{height:Height*0.4 - 15 +'px'}"></shortCut>
      </el-col>
    </el-row>
    <el-row :gutter="10" class="home-row home-row-b" ref="rowBtom">
      <el-col :xs="24" :sm="16" :md="16" :lg="16" :xl="16">
        <Todo ref="chart" :style="{height:Height*0.6 - 35 + 'px'}"></Todo>
      </el-col>
      <el-col :xs="24" :sm="8" :md="8" :lg="8" :xl="8">
        <Efficiency :style="{height:Height*0.6 - 35 + 'px'}"></Efficiency>
      </el-col>
    </el-row>
  </div>
</template>
<script>
import shortCut from "./shortCut";
import Efficiency from "./Efficiency";
import email from "./email";
import notice from "./notice";
// import test from "@/pages/Home/test";
import Todo from "./Todo";
import { setTimeout } from "timers";
export default {
  name: "Home",
  data: function() {
    return {
      Height: 250
    };
  },
  computed: {},
  mounted: function() {
    this.initPage();
  },
  methods: {
    initPage: function() {
      this.$nextTick(() => {
        this.resizeEleHt();
      });
    },
    resizeEleHt: function() {
      let top = this.$refs.rowBtom.$el.offsetTop;
      let height = this.$el.parentNode.clientHeight;
      this.$el.parentNode.style.width = "90%";
      if (height < 700) {
        this.Height = 702;
        return;
      }
      this.Height = height - 55;
    }
  },
  components: {
    shortCut,
    Efficiency,
    notice,
    Todo,
    email
  },
  beforeDestroy: function() {
    $(".main").css("width", "");
  }
};
</script>

<style lang="scss">
// @import url("../../../../static/css/swiper.min.css");
.home {
  width: 100%;
  height: 100%;
  overflow: hidden;
  padding: 30px 0px 0px 10px;
  background: #f2f2f2;
  .home-row {
    width: 100%;
    margin: 0px;

    .grid-inner-content {
      background: #fff;
       box-shadow: 0 0 5px 0 rgba(18, 31, 62, 0.08);
      // box-shadow: 0 1px 2px 0 rgba(0, 0, 0, 0.05);
      border-radius: 2px;
      margin-bottom: 15px;
      height: 250px;
      .panel-header {
        font-size: 20px;
        padding: 10px;
        border-bottom: 1px solid #f6f6f6;
        color: #07438b;
        font-weight: bolder;
        border-left: 2px solid #07438b;
        	.more{
          padding:5px 10px 0px;
              color: #999;
            font-size: 14px;
            font-weight: normal;
            cursor: pointer;
            &:hover{
              color:red
            }
        }
      }
      .panel-body {
        padding: 10px;
        height: calc(100% - 45px);
      }
    }
  }
  .home-row-t {
    // height: 275px;
  }
  .home-row-b {
    flex: 1;
    .el-col {
      height: 100%;
      .grid-inner-content {
        height: 100%;
        margin: 0;
      }
    }
  }
}
</style>

