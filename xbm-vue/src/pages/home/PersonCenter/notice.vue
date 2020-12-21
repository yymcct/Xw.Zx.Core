<template>
  <div class="grid-inner-content" :style="{height:Height+'px'}">
    <div class="panel-header">
      通知公告
      <span class="right more" @click="checkMore">查看更多</span>
    </div>
    <div class="panel-body" v-loading="loading">
      <ul class="msg-list-box">
        <template v-for="(item,idx) in dataList">
          <li class="msg-list-item" :key="idx" @click="handleDetail(item)" v-if="idx<5">
            <!-- <p class="msg-item-info"> -->
            <span class="state" :style="item.ZT=='未读'?'color:#FF5722':'color:#3F51B5'">[{{item.ZT}}]</span>
            <span :title="item.NT_NAME" class="message">{{item.NT_NAME}}</span>
            <span class="date">{{item.NT_TIME}}</span>
            <!-- </p> -->
            <!-- <p class="msg-item-dec"><span>{{title[item.NT_NAME]}}</span></p> -->
          </li>
        </template>
      </ul>
    </div>
    <el-dialog title="详情" :visible.sync="DialogShow" v-dialogDrag width="900px" append-to-body>
      <vForm :curData="curData" :type="type" ref="NoticeForm" v-if="DialogShow"></vForm>
      <div slot="footer" class="dialog-footer" style="text-align:center">
        <el-button type="primary" @click="DialogShow = false" style="float:none!important">关闭</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/home.js";
import { checkNotice } from "@/public/apiService/PersonalAffairs/address";
import Form from "./form";
export default {
  name: "Home",
  props: ["Height"],
  data: function () {
    return {
      ur_ident: JSON.parse(localStorage.getItem("data")).ur_ident,
      dataList: [],
      DialogShow: false,
      curData: null,
      type: "add",
      loading: false,
    };
  },
  computed: {},
  created() {},
  mounted() {
    this.getData();
  },
  methods: {
    checkVersion: function () {
      this.$message({
        message: "暂无记录!",
        type: "warning",
      });
    },
    getData() {
      var params = {
        uid: this.ur_ident,
        nt_name: "",
        nt_sender: "",
        page: 1,
        zt: "",
      };
      dataService
        .homeNotice(params)
        .then((res) => {
          this.dataList = res.DATA;
        })
        .catch((err) => {
          console.log(err);
        });
    },
    handleDetail(row) {
      checkNotice(row.WIID).then((res) => {
        this.curData = res[0];
        this.type = "detail";
        this.DialogShow = true;
      });
    },
    checkMore: function () {
      this.$router.push({ path: "/manage/notice" });
      this.$store.commit("manageMenuDefault", {
        BA_PATH: "/manage/notice",
        Ba_Name: "公告管理",
      });
    },
  },
  components: {
    vForm: Form,
  },
};
</script>

<style lang="scss" scoped>
.msg-list-box {
  .msg-list-item {
    display: flex;
    margin: 5px;
    line-height: 28px;
    background: #fff;
    cursor: pointer;
    .state {
      flex: 1;
      min-width: 43px;
      color: rgb(63, 81, 181);
    }
    .message {
      flex: 6;
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
      color: #333;
      font-size: 14px;
      font-weight: 400;
      text-align: left;
      &:hover {
        color: #34a;
      }
    }
    .date {
      flex: 3;
      text-align: right;
      padding-left: 3px;
      padding-right: 3px;
      white-space: nowrap;
      text-overflow: ellipsis;
      overflow: hidden;
      right: 0;
      background: #fff;
      font-size: 12px;
      color: #b6b4c8;
    }
  }
}
// .version-table {

// 	tr {
// 		color: #909399;

// 		&:hover {
// 			td {
// 				background: transparent !important;
// 				color: #787878;
// 			}
// 		}
// 	}
// }
// tr{
// 	cursor: pointer;
// }
</style>
