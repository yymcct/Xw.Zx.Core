<template>
  <div class="countChart grid-inner-content todo">
    <div class="panel-header">
      <p class="tit">待办业务</p>
      <p class="todo-right">
        <el-radio-group v-model="activeNum" size="mini" @change="onChange">
          <!-- <el-radio-button label="1">行政审批</el-radio-button> -->
          <el-radio-button label="1">
            行政办公(
            <b class="red-text">{{total1}}</b>)
          </el-radio-button>
          <el-radio-button label="2">
            项目策划(
            <b class="red-text">{{total2}}</b>)
          </el-radio-button>
          <el-radio-button label="3">
            联合审查(
            <b class="red-text">{{total3}}</b>)
          </el-radio-button>
          <el-radio-button label="4">
            窗口受理(
            <b class="red-text">{{total4}}</b>)
          </el-radio-button>
          <el-radio-button label="5">
            电子证照(
            <b class="red-text">{{expireLicense?expireLicense.length:0}}</b>)
          </el-radio-button>
        </el-radio-group>
      </p>
    </div>
    <div class="panel-body" v-loading="loading">
      <template v-if="activeNum=='1'">
        <el-table
          :data="ZWdata"
          stripe
          style="width: 100%"
          height="100%"
          class="todo-table"
          :cell-style="cellStyle"
        >
          <el-table-column prop="WI_CSTATE" label="督办" width="80">
            <template slot-scope="scope">
              <div style="color:red">{{scope.row.WI_CSTATE==1?'督办':''}}</div>
            </template>
          </el-table-column>
          <el-table-column prop="WI_XNAME" label="项目名称" show-overflow-tooltip></el-table-column>
          <el-table-column prop="YWLX" label="任务类型" show-overflow-tooltip></el-table-column>
          <el-table-column prop="WT_NAME" label="当前环节" show-overflow-tooltip></el-table-column>
          <el-table-column prop="WA_START" label="创建时间" show-overflow-tooltip></el-table-column>
          <el-table-column prop="JZSJ" label="超期时间" show-overflow-tooltip></el-table-column>
          <el-table-column label="操作" width="80">
            <template slot-scope="scope">
              <el-button
                @click="handleClick(scope.row)"
                title="办理"
                icon="el-icon-thumb"
                type="primary"
                circle
                size="small"
              ></el-button>
            </template>
          </el-table-column>
        </el-table>
      </template>
      <template v-else-if="activeNum=='2'">
        <el-table
          :data="project"
          stripe
          style="width: 100%"
          height="100%"
          class="todo-table table2"
        >
          <el-table-column label="项目警示" width="80" align="center">
            <template slot-scope="scope">
              <span class="dec-block" title="督办:请抓紧时间办理" v-if="scope.row.dbzt==1">督</span>
              <span
                :title="YJBS_TEXT[scope.row.rwicon]"
                class="pro-warning"
                :class="YJrela[scope.row.rwicon]"
              ></span>
            </template>
          </el-table-column>
          <el-table-column prop="xmmc" label="项目名称" show-overflow-tooltip></el-table-column>
          <el-table-column prop="lcmc" label="项目状态" show-overflow-tooltip>
            <template slot-scope="scope">{{scope.row.lcmc?scope.row.lcmc:'未发起'}}</template>
          </el-table-column>
          <el-table-column prop="ur_name" label="创建人" show-overflow-tooltip></el-table-column>
          <el-table-column prop="cjsj" label="创建时间" show-overflow-tooltip></el-table-column>
          <el-table-column label="操作" width="80">
            <template slot-scope="scope">
              <el-button
                @click="handleClick(scope.row)"
                title="办理"
                icon="el-icon-thumb"
                type="primary"
                circle
                size="small"
              ></el-button>
            </template>
          </el-table-column>
        </el-table>
      </template>
      <template v-else-if="activeNum=='3'">
        <el-table :data="approvePro" stripe style="width: 100%" height="100%" class="todo-table">
          <el-table-column prop="xmmc" label="项目名称" width="180"></el-table-column>
          <el-table-column prop="lcmc" label="当前状态"></el-table-column>
          <el-table-column prop="sxmc" label="类型"></el-table-column>
          <el-table-column prop="ur_name" label="创建人"></el-table-column>
          <el-table-column prop="jjrq" label="创建时间"></el-table-column>
          <el-table-column label="操作" width="80">
            <template slot-scope="scope">
              <el-button
                @click="handleClick(scope.row)"
                title="办理"
                icon="el-icon-thumb"
                type="primary"
                circle
                size="small"
              ></el-button>
            </template>
          </el-table-column>
        </el-table>
      </template>
      <template v-else-if="activeNum=='4'">
        <el-table
          :data="Accpetance"
          stripe
          style="width: 100%"
          height="100%"
          class="todo-table"
          v-if="!loading"
        >
          <el-table-column label="项目名称" prop="PROJECTNAME" show-overflow-tooltip></el-table-column>
          <el-table-column prop="PROJID" label="申报号" show-overflow-tooltip></el-table-column>
          <el-table-column prop="APPLYNAME" label="申请对象" show-overflow-tooltip></el-table-column>
          <el-table-column prop="SERVICENAME" label="事项类型" show-overflow-tooltip></el-table-column>
          <el-table-column prop="APPLYFROM" label="申报来源" show-overflow-tooltip>
            <template slot-scope="scope">
              <div class="cell el-tooltip state-text">{{SBLYRela[scope.row.APPLYFROM]}}</div>
            </template>
          </el-table-column>
          <el-table-column prop="RECEIVETIME" label="申报时间" show-overflow-tooltip></el-table-column>
          <el-table-column label="操作" width="80">
            <template slot-scope="scope">
              <el-button
                @click="handleClick(scope.row)"
                title="办理"
                icon="el-icon-thumb"
                type="primary"
                circle
                size="small"
              ></el-button>
            </template>
          </el-table-column>
        </el-table>
      </template>
      <template v-else-if="activeNum=='5'">
        <EleLicense @checkPath="checkPath"></EleLicense>
      </template>
      <el-pagination
        class="cus-pagination"
        v-if="activeNum!='5'"
        @current-change="handleCurrentChange"
        :current-page="page"
        background
        layout="total,prev, pager, next"
        :total="total"
      ></el-pagination>
    </div>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/home.js";
import { setTimeout } from "timers";
import { getToken, getLogin } from "@/public/auth";
import { openDGHYApplication } from "@/public/utils";
import { apiUrl } from "@/public/apiUrl";
import EleLicense from "./EleLicense";
import { SBLYRela } from "@/public/constant/constant.js";
export default {
  name: "Home",
  props: ["Height"],
  data: function () {
    return {
      page: 1,
      total1: 0,
      total2: 0,
      total3: 0,
      total4: 0,
      // total5:0,
      activeNum: "1",
      token: getToken(),
      SBLYRela: SBLYRela,
      project: [],
      approvePro: [],
      ZWdata: [],
      Accpetance: [],
      loading: false,
      YJrela: { 50: "state-normal", 51: "state-warning", 52: "state-error" },
      YJBS_TEXT: { 50: "正常", 51: "预警", 52: "超期" },
    };
  },
  created() {
    this.loadAllDataList();
  },
  computed: {
    total: function () {
      if (this.activeNum == "1") {
        return this.total1;
      } else if (this.activeNum == "2") {
        return this.total2;
      } else if (this.activeNum == "3") {
        return this.total3;
      } else if (this.activeNum == "4") {
        return this.total4;
      }
    },
    expireLicense: function () {
      return this.$store.getters.ExpireLicense;
    },
  },
  methods: {
    handleClick: function (row) {
      // console.log(row);
      if (this.activeNum == "1") {
        this.checkPath(row);
      } else if (this.activeNum == "2" || this.activeNum == "3") {
        openDGHYApplication();
      } else {
        let path =
          row.APPLYFROM == "0"
            ? "/cksl/UnifiedAcceptance"
            : "cksl/powerOperation";
        this.$router.push(path);
      }
    },
    cellStyle: function (data) {
      if (data.row.WI_CSTATE == 1) {
        return "background:#f9e4f5;border-bottom:1px solid rgb(252, 220, 233)";
      }
    },
    checkPath: function (ele) {
      let sysPath = ele.XTLX == "ZW" ? "/manage" : "/approval";
      let storeMethod =
        ele.XTLX == "ZW" ? "manageMenuDefault" : "changeMenuDefault";
      if (ele.CDPATH.indexOf("FORM") == -1) {
        this.$router.push(ele.CDPATH);
      } else {
        this.$router.push({ path: sysPath });
      }
      this.$store.commit(storeMethod, {
        BA_PATH: ele.CDPATH,
        Ba_Name: ele.CDM,
      });
    },
    onChange: function (val) {
      this.page = 1;
      this.loadActiveData(val);
    },
    handleCurrentChange: function (val) {
      this.page = val;
      this.loadActiveData(this.activeNum);
    },
    loadAllDataList: function () {
      this.getHomeOffice();
      this.getReserverPro();
      this.getJointApproval();
      this.getWindowAcceptance();
      this.$store.dispatch("GetExpireLicenseList");
    },
    loadActiveData: function (tab) {
      if (tab == "1") {
        this.getHomeOffice();
        return;
      }
      if (tab == "2") {
        this.getReserverPro();
        return;
      }
      if (tab == "3") {
        this.getJointApproval();
        return;
      }
      if (tab == "4") {
        this.getWindowAcceptance();
      }
      // if(tab=='5'){

      // }
    },
    getWindowAcceptance: function () {
      this.loading = true;
      this.Accpetance = [];
      let p = {
        PROJID: "", //办件标识
        SERVICENAME: "", //审批事项名称
        PROJECTNAME: "", //申请项目的具体名称
        APPLYFROM: "", //办件来源0 1 2
        TRANSACT: "", //获取已办理
        STATE: 0,
        start: this.page,
        count: 10,
      };
      // this.$http.get(apiUrl.GET_TO_LIST_WINDOW)
      this.$http.get(apiUrl.Get_Acceptance_List, { params: p }).then((res) => {
        this.loading = false;
        this.total4 = res.data.sum;
        res.data.data.forEach((item) => {
          this.Accpetance.push(item);
        });
      });
    },
    getHomeOffice: function () {
      this.ZWdata = [];
      this.loading = true;
      dataService["getHomeOfficePend"](this.page).then((res) => {
        this.loading = false;
        this.ZWdata = res.data;
        this.total1 = res.size;
      });
    },
    getJointApproval: function () {
      this.approvePro = [];
      this.loading = true;
      dataService["GetPendJiontList"](this.page).then((res) => {
        this.loading = false;
        this.approvePro = res.DATA;
        this.total3 = res.SIZE;
      });
    },
    getReserverPro: function () {
      this.project = [];
      this.loading = true;
      dataService["getProjectList"](this.page).then((res) => {
        this.loading = false;
        this.project = res.DATA;
        this.total2 = res.SIZE;
      });
    },
  },
  components: {
    EleLicense,
  },
};
</script>

<style lang="scss" scoped>
.todo {
  .panel-header {
    .tit {
      display: inline-block;
    }
    .todo-right {
      float: right;
      .red-text {
        color: #f31313;
        font-size: 14px;
      }
    }
  }
  .panel-body {
    /deep/ .todo-table {
      width: 100%;
      border: none;
      height: calc(100% - 50px) !important;
      th {
        color: #3b4477;
        background: #f2f3fe;
      }
      th.is-leaf,
      td,
      td.is-leaf {
        border: none;
        padding: 8px 0px;
      }
      &:before,
      &:after {
        background: none;
      }
    }
    .table2 {
      .pro-warning {
        display: inline-block;
        width: 16px;
        height: 16px;
      }
      .state-normal {
        background: url("~@/assets/images/SmallIcon.png");
        background-position-x: -1px;
      }
      .state-warning {
        background: url("~@/assets/images/SmallIcon.png");
        background-position-x: 36px;
      }
      .state-error {
        background: url("~@/assets/images/SmallIcon.png");
        background-position-x: 20px;
      }
    }

    .cus-pagination {
      text-align: center;
      margin-top: 15px;
    }
  }
}
</style>
