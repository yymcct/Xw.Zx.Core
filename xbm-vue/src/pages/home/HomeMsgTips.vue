<template>
  <div class="home-msg-tips" v-show="msgShow">
    <div class="msg-panel" :style="{ background: colorRela[themecolor] }">
      <!-- <span class="msg-panel-read" title="查看分类详情"></span>
      <span class="msg-panel-cancel" title="分类全部已阅"></span>-->
      <div class="msg-panel-left">
        <i class="iconfont ft-icon" title="消息">&#xe614;</i>
        <p>消息提醒</p>
        <span>
          共
          <b>{{ length }}</b>条提醒
        </span>
      </div>
      <div class="msg-panel-right">
        <p class="msg-panel-history" @click="history()">查看历史记录</p>
        <i class="el-icon-close" @click="closeButton"></i>
      </div>
    </div>
    <ul class="msg-list-box">
      <li class="msg-list-item" v-for="(item, idx) in msgList" :key="idx" @click="read(item, idx)">
        <i class="el-icon-info msg-panel-info"></i>
        <p class="msg-item-info">
          <span>{{ item.AT_THEME }}</span>
          <span class="msg-item-time">{{ item.AT_STIME }}</span>
        </p>
        <p class="msg-item-word" :title="item.AT_MATTER">{{ item.AT_MATTER }}</p>
      </li>
    </ul>
    <div class="msg-button">
      <el-button @click="readAll" type="primary">全部已阅</el-button>
      <el-button @click="closeButton" type="primary">关闭</el-button>
    </div>
  </div>
</template>

<script>
import * as dataService from "@/public/apiService/PersonalAffairs/shortMsg";
import { colorRela } from "@/public/constant/color";
export default {
  name: "home-msg-tips",
  props: ["msgShow"],
  data: function () {
    return {
      msgList: [],
      ur_ident: "",
      length: "",
      msgLengthShow: false,
      magShow: false,
    };
  },
  watch: {
    msgShow: (n, o) => {
      console.log(n, o);
      //箭头函数  不然会发生this改变
      this.msgShow = n;
    },
  },
  created: function () {},
  methods: {
    async getMsgPushList() {
      var ur_ident = JSON.parse(localStorage.getItem("data")).ur_ident;
      this.ur_ident = ur_ident;
      var params = {
        uid: this.ur_ident,
      };
      return new Promise((resolve, reject) => {
        dataService
          .getMsgPushList(params)
          .then((res) => {
            this.msgList = res.DATA;
            this.length = res.DATA.length;
            //判断底部消息提醒数量是否出现
            if (res.DATA.length == 0) {
              this.msgLengthShow = false;
            } else {
              this.msgLengthShow = true;
            }
            var data = {
              length: this.length,
              msgLengthShow: this.msgLengthShow,
            };
            this.$emit("msgCount", data);
          })
          .catch((res) => {
            console.log(res, "err==");
          });
      });
    },
    addTab(path, name) {
      this.closeButton();
      this.$store.commit("changeMenuDefault", "/approval/msgManage");
      this.$router.push({ path: "/approval/msgManage" });
    },
    //退出消息提醒不显示
    closeButton() {
      this.$emit("closeMsg", false);
    },
    //状态单个已阅
    read(data, idx) {
      var _this = this;
      console.log(data.PATH, "data");
      var readData = [];
      var data2 = {
        aid: data.AID,
        at_uid: this.ur_ident,
        at_stime: data.AT_STIME,
        at_ctime: data.AT_CTIME,
        at_matter: data.AT_MATTER,
        at_theme: data.AT_THEME,
        at_wiid: data.AT_WIID,
      };
      readData.push(data2);
      var params = { DATA: readData };
      dataService
        .getMsgUpdateList(params)
        .then((res) => {
          this.getMsgPushList();
          var arr = [1, 2, 3, 4];
          // console.log(data.at_theme,'sss');
          //1事项通知 2消息通知 3通知公告 4 催办提醒
          if (arr.indexOf(data.at_theme) > -1) {
            //多规合一
          } else {
            if (data.AT_THEME == "通知公告") {
              routeData = this.$router.resolve({
                path: "/noticeDetail",
                query: { wiid: data.AT_WIID },
              });
              window.open(routeData.href, "_blank");
            } else {
              //事项通知
              if (data.TZLX == 1) {
                this.$router.push({ path: "/manage" });
                this.$store.commit("manageMenuDefault", {
                  BA_PATH: data.PATH,
                  Ba_Name: data.XMMC,
                });
                return;
              }
              this.$router.push({ path: "/approval" });
              this.$store.commit("changeMenuDefault", {
                BA_PATH: data.PATH,
                Ba_Name: data.XMMC,
              });
            }
          }
        })
        .catch((res) => {
          this.length = 0;
          console.log(res, "err==");
        });
      // this.addTab(data.PATH);
    },
    //状态全部已阅
    readAll() {
      var _this = this;
      var readData = [];
      this.msgList.map((item) => {
        var data1 = {
          aid: item.AID,
          at_uid: _this.ur_ident,
          at_stime: item.AT_STIME,
          at_ctime: item.AT_CTIME,
          at_matter: item.AT_MATTER,
          at_theme: item.AT_THEME,
          at_wiid: item.AT_WIID,
        };
        readData.push(data1);
        // console.log(data1)
      });
      var params = { DATA: readData };
      dataService
        .getMsgUpdateList(params)
        .then((res) => {
          console.log(res);
          this.getMsgPushList();
          this.$message({
            message: "消息全部已阅",
            type: "success",
          });
        })
        .catch((res) => {
          console.log(res, "err==");
        });
    },
    //查看历史记录跳到消息管理列表
    history() {
      this.$emit("closeMsg", false);
      this.$store.commit("changeMenuDefault", {
        BA_PATH: "/approval/msgManage",
        Ba_Name: "消息提醒",
      });
      this.$router.push({ path: "/approval" });
    },
    //监听消息提醒
    listenMsg() {
      var _this = this;
      window.clearInterval(time);
      var time = window.setInterval(function () {
        _this.getMsgPushList();
      }, 100000);
    },
  },
  mounted() {
    this.getMsgPushList();
    this.listenMsg();
  },
  computed: {
    themecolor: {
      get() {
        return this.$store.state.themecolor;
      },
      set(val) {
        this.$store.commit("setThemeColor", val);
      },
    },
    colorRela: function () {
      return colorRela;
    },
  },
};
</script>
<style lang="scss" scoped>
.home-msg-tips {
  width: 500px;
  height: 400px;
  background-color: #fff;
  position: absolute;
  top: 50%;
  left: 50%;
  margin-left: -250px; //盒子宽度的一半
  margin-top: -200px; //盒子高度的一半
  box-shadow: 0 0 10px #888;
  border: 1px solid #eaeaea;
  z-index: 9999;
  .msg-panel {
    height: 50px;
    line-height: 50px;
    font-size: 14px;
    background: #1458b3;
    font-weight: bold;
    color: #fff;
    // background: url('~@/assets/images/noc_title_bg.png') repeat-x center center;
    padding-left: 10px;
    .msg-panel-left {
      float: left;
      align-content: center;
      i {
        float: left;
      }
      p {
        float: left;

        font-size: 20px;
        margin-right: 10px;
      }
      span {
        b {
          color: #ff7403;
        }
      }
    }
    .msg-panel-right {
      float: right;
      align-content: center;
      padding-right: 10px;
      p {
        float: left;
        padding-right: 8px;
        cursor: pointer;
      }
      i {
        font-size: 18px;
        font-weight: 700;
        cursor: default;
      }
    }
  }
  .msg-list-box {
    width: 100%;
    height: 300px;
    overflow: auto;
    .msg-list-item {
      position: relative;
      background: #f7f7f7;
      border-bottom: 1px solid #dbdbdb;
      border-top: 1px solid #ffffff;
      padding: 5px;
      padding-left: 30px;
      line-height: 28px;
      cursor: pointer;
      .msg-panel-info {
        position: absolute;
        left: 10px;
        top: 10px;
        color: #6ac802;
      }
      .msg-item-info {
        color: #666666;
        font-weight: bold;
        cursor: pointer;
        .msg-item-time {
          color: #ff7403;
          float: right;
          margin-right: 10px;
          font-weight: normal;
        }
      }
      .msg-item-word {
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
        font-size: 14px;
        line-height: 20px;
      }
    }
  }
  .msg-button {
    display: flex;
    justify-content: center;
    line-height: 50px;
    align-items: center;
  }
}
</style>
