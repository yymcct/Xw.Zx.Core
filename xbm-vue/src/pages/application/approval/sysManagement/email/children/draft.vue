<template>
  <v-flex-container :leftWidth="'300px'">
    <div slot="left" class="inBox-left">
      <div class="inbox-toolbar">
        <div class="inbox-pull-right">
          <el-button
            type="primary"
            icon="el-icon-search"
            title="查询"
            size="mini"
            plain
            @click="searchEmail('draft')"
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
          草稿箱列表
        </div>
      </div>
      <div
        class="email-list-wrapper"
        v-loading="loading"
        element-loading-text="拼命加载中"
      >
        <template v-if="list.length > 0">
          <v-list :emailList="list" @checkDetail="checkDetail"></v-list>
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
    <div slot="right" class="inBox-contont">
      <div
        class="email-detail-empty-tip normal-empty"
        v-if="detail == null"
      ></div>
      <transition name="slide-fade" v-else>
        <v-add v-if="deShow" :replyData="detail" :isDraft="isDraft"></v-add>
      </transition>
    </div>
  </v-flex-container>
</template>
<script>
import {
  getDraftData,
  checkEmailDetail,
  delEmail,
  getDraftEmailList
} from "@/public/apiService/PersonalAffairs/email";
import flexContainer from "@/components/FlexContainer";
import list from "./children/EmailList";
import addEmail from "./children/EmailAdd";
export default {
  name: "outBox",
  props: ["draftData"],
  data() {
    return {
      loading: false,
      list: [],
      pageIdx: 1,
      total: 0,
      detail: null,
      isDraft: true,
      deShow: false,
      wiid: ""
    };
  },
  computed: {
    isCollapse: function() {
      return this.$store.state.email.isCollapse;
    }
  },
  created: function() {
    this.getListData();
  },
  methods: {
    checkDetail: function(row) {
      this.$emit("freshWiid", row.WIID);
      this.wiid = row.WIID;
      this.deShow = false;
      checkEmailDetail(row.WIID).then(res => {
        this.deShow = true;
        this.detail = res;
      });
    },
    getListData: function(item) {
      // var _this = this;
      this.loading = true;
      var isNull = null;
      console.log(item);
      if (JSON.stringify(item) == "{}" || !item) {
        isNull = false;
      } else {
        isNull = true;
      }
      if (isNull) {
        console.log(item);
        getDraftEmailList(this.pageIdx, item).then(res => {
          this.loading = false;
          this.list = res.DATA;
          this.total = res.SIZE;
          this.$emit("freshWiid", "");
        });
      } else {
        getDraftData(this.pageIdx).then(res => {
          this.loading = false;
          this.list = res.DATA;
          this.total = res.SIZE;
          this.$emit("freshWiid", "");
        });
      }
    },
    onPage: function() {
      this.getListData(this.draftData);
    },
    // refresh: function() {
    //     this.getListData();
    // }
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
        wiid: this.wiid,
        zt: "f"
      };
      this.$confirm("此操作将永久删除该邮件, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          delEmail(data).then(res => {
            console.log(data);
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
    // "v-detail": detail,
    "v-add": addEmail,
    "v-list": list
  }
};
</script>
<style lang="scss" scoped>
.inBox-left {
  height: 100%;
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
  height: 100%;
  background: #f5f5f5;
  padding: 10px;
  overflow: auto;
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

// }
</style>
