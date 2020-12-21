<template>
  <v-add v-if="activeSideName == 'addEmail'" :isReply="false"></v-add>
  <!-- <template v-else-if="activeSideName=='tagsCloud'">
      <tagsCloud></tagsCloud>
  </template> -->
  <v-flex-container :leftWidth="'300px'" v-else>
    <div slot="left" class="inBox-left">
      <div class="inbox-toolbar">
        <div class="inbox-pull-right">
          <el-button
            type="primary"
            icon="el-icon-search"
            title="查询"
            size="mini"
            plain
            @click="searchEmail('inbox')"
          ></el-button>
          <el-button
            type="primary"
            icon="el-icon-delete"
            style="color: red;"
            @click="deleteEmail"
            plain
            size="mini"
            title="删除"
          >
          </el-button>
          <el-button
            type="primary"
            icon="el-icon-refresh"
            plain
            size="mini"
            @click="getListData"
            title="刷新"
          >
          </el-button>
        </div>
        <div class="btn-group dropdown inbox-pull-left">
          收件箱列表
        </div>
      </div>
      <div
        class="email-list-wrapper"
        v-loading="loading"
        element-loading-text="拼命加载中"
      >
        <template v-if="list && list.length > 0">
          <v-list
            :emailList="list"
            :type="type"
            @checkDetail="checkDetail"
          ></v-list>

          <div class="email-readmore" style="display: block;">
            <el-pagination
              small
              layout="total,prev,pager,next,jumper"
              :total="total"
              :current-page.sync="pageIdx"
              @current-change="onPage"
            >
            </el-pagination>
          </div>
        </template>
        <template v-else>
          <div class="email-empty-tip">无内容</div>
        </template>
      </div>
    </div>
    <div
      slot="right"
      class="inBox-contont"
      v-loading="detLoading"
      element-loading-text="拼命加载中"
    >
      <div
        class="email-detail-empty-tip normal-empty"
        v-if="detail == null"
      ></div>
      <v-detail
        :detail="detail"
        @reply="showReply"
        @forWard="showForWard"
        v-else
      ></v-detail>
      <transition name="slide-fade">
        <div v-show="isShowReply" class="transition-box">
          <span @click="backDetail" class="close-reply"
            ><i class="el-icon-back"></i>返回</span
          >
          <v-add
            :replyData="detail"
            :isReply="true"
            :isForWard="isForWard"
            v-if="isShowReply"
          ></v-add>
        </div>
      </transition>
    </div>
  </v-flex-container>
</template>
<script>
import {
  getInbox,
  checkEmailDetail,
  delEmail,
  getInxEmailList
} from "@/public/apiService/PersonalAffairs/email";
import flexContainer from "@/components/FlexContainer";
import detail from "./children/EmailDetail";
import list from "./children/EmailList";
import addEmail from "./children/EmailAdd";
export default {
  name: "inBox",
  props: ["inboxData"],
  data() {
    return {
      loading: false,
      list: [],
      pageIdx: 1,
      total: 0,
      detail: null,
      isShowReply: false,
      replyData: null,
      detLoading: false,
      wiid: "",
      isForWard: false,
      type: "inBox",
      form: {}
    };
  },
  computed: {
    isCollapse: function() {
      return this.$store.state.email.isCollapse;
    },
    activeSideName: function() {
      return this.$store.state.email.activeSideName;
    }
  },
  created: function() {
    this.getListData(true);
  },
  methods: {
    checkDetail: function(row) {
      this.$emit("freshWiid", row.WIID);
      this.wiid = row.WIID;
      this.backDetail();
      this.detLoading = true;
      checkEmailDetail(row.WIID).then(res => {
        this.detail = res;
        this.detLoading = false;
        this.getListData(false);
      });
    },
    // searchInboxList(item) {
    //   getInxEmailList(1, item).then(res => {});
    // },
    getListData: function(flag, item) {
      this.loading = flag;
      var isNull = null;
      if (JSON.stringify(item) == "{}" || !item) {
        isNull = false;
      } else {
        isNull = true;
      }
      console.log(isNull);
      if (isNull) {
        getInxEmailList(this.pageIdx, item).then(res => {
          this.list = res.DATA;
          this.total = res.SIZE;
          this.$store.dispatch("getUnReadNums");
          if (flag) {
            this.$emit("freshWiid", "");
          }
          this.loading = false;
        });
      } else {
        getInbox(this.pageIdx).then(res => {
          this.list = res.DATA;
          this.total = res.SIZE;
          this.$store.dispatch("getUnReadNums");
          if (flag) {
            this.$emit("freshWiid", "");
          }
          this.loading = false;
        });
      }
    },
    onPage: function() {
      this.getListData(true, this.inboxData);
    },
    showReply: function() {
      this.isShowReply = true;
    },
    showForWard() {
      this.isShowReply = true;
      this.isForWard = true;
    },
    //关闭回复页面
    backDetail: function() {
      this.isShowReply = false;
      this.isForWard = false;
    },
    handleSort: function(command) {},
    searchEmail(val) {
      this.$emit("searchShow", val);
    },
    deleteEmail() {
      console.log(this.wiid);
      if (!this.wiid) {
        this.$message({
          type: "warning",
          message: "请选择要删除的内容"
        });
        return;
      }
      var data = {
        wiid: this.wiid
        // zt:'f'
      };
      this.$confirm("此操作将永久删除该邮件, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          delEmail(data).then(res => {
            if (res.success) {
              this.$message({
                type: "success",
                message: "删除成功!"
              });
              this.getListData();
              return;
            }
            this.$message({
              type: "error",
              message: res.msg
            });
          });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除"
          });
        });
    }
  },
  components: {
    "v-flex-container": flexContainer,
    "v-detail": detail,
    "v-add": addEmail,
    "v-list": list
    // tagsCloud
  }
};
</script>
<style lang="scss" scoped>
.inBox-left {
  height: 100%;
}
.el-input .el-input__inner {
  width: 150px;
}
.el-form-item {
  margin-bottom: 0;
}

.inbox-toolbar {
  padding: 0 5px;
  height: 40px;
  line-height: 35px;
  border-top: 1px solid #fff;
  border-bottom: 1px solid #ddd;
  width: 100%;
  background: #f5f7fa;
  z-index: 999;
  position: relative;

  .inbox-pull-right {
    float: right;
  }

  .inbox-pull-left {
    float: left;
  }
}

.email-list-wrapper {
  height: calc(100% - 40px);

  .email-readmore {
    height: 40px;
    text-align: center;

    .el-pagination {
      padding-top: 10px;
    }
  }
}

.inBox-contont {
  position: relative;
  height: 100%;
  background: #f5f5f5;
  padding: 10px;

  .email-detail-empty-tip {
    width: 100%;
    height: 80px;
    position: absolute;
    left: 0;
    top: 35%;
    background: #f5f5f5 url("~@/assets/images/email_empty.png") no-repeat center
      center;
    z-index: 10;
  }

  .transition-box {
    position: absolute;
    top: 0px;
    left: 0px;
    width: 100%;
    height: 100%;
    overflow: auto;
    background: #f5f5f5;

    .close-reply {
      color: #4899d6;
      padding: 10px 0px 0px 10px;
      display: inline-block;
      cursor: pointer;

      &:hover {
        opacity: 0.8;
      }
    }
  }

  .slide-fade-enter-active {
    transition: all 0.3s ease;
  }

  .slide-fade-leave-active {
    transition: all 0.8s cubic-bezier(1, 0.5, 0.8, 1);
  }

  .slide-fade-enter,
  .slide-fade-leave-active {
    padding-left: 10px;
    opacity: 0;
  }
}
</style>
