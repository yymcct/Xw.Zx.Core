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
            @click="searchEmail('outbox')"
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
          发件箱列表
          <!-- <el-dropdown trigger="click" class="btn btn-small" @command="handleSort">
          <span class="el-dropdown-link">
            排序<i class="el-icon-caret-bottom el-icon--right"></i>
        </span>
					<el-dropdown-menu slot="dropdown">
						<el-dropdown-item class="clearfix" command="time">
							按时间先后
						</el-dropdown-item>
						<el-dropdown-item class="clearfix" command="state">
							按读取状态
						</el-dropdown-item>
					</el-dropdown-menu>
				</el-dropdown> -->
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
      <transition name="slide-fade" v-else>
        <v-detail :detail="detail" :isOutBox="true"></v-detail>
      </transition>
    </div>
  </v-flex-container>
</template>
<script>
import {
  getOutbox,
  checkEmailDetail,
  delEmail,
  getOutEmailList
} from "@/public/apiService/PersonalAffairs/email";
import flexContainer from "@/components/FlexContainer";
import detail from "./children/EmailDetail";
import list from "./children/EmailList";
export default {
  name: "outBox",
  props: ["outBoxData"],
  data() {
    return {
      loading: false,
      detLoading: false,
      list: [],
      pageIdx: 1,
      total: 0,
      detail: null,
      wiid: "",
      zt: "f"
    };
  },
  computed: {
    isCollapse: function() {
      return this.$store.state.email.isCollapse;
    }
  },
  created: function() {
    this.getListData(true);
  },
  methods: {
    getListData: function(flag, item) {
      this.loading = flag;
      var isNull = null;
      console.log(item);
      if (JSON.stringify(item) == "{}" || !item) {
        isNull = false;
      } else {
        isNull = true;
      }
      console.log(isNull);
      if (isNull) {
        getOutEmailList(this.pageIdx, item).then(res => {
          this.list = res.DATA;
          this.total = res.SIZE;
          this.$store.dispatch("getUnReadNums");
          if (flag) {
            this.$emit("freshWiid", "");
          }
          this.loading = false;
        });
      } else {
        getOutbox(this.pageIdx).then(res => {
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
    // getListData: function () {
    //   this.loading = true;
    //   getOutbox(this.pageIdx).then(res => {
    //     this.loading = false;
    //     this.list = res.DATA;
    //     this.$emit('freshWiid', '');
    //     this.total = res.SIZE;
    //   });
    // },
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
        zt: this.zt
      };
      //  delEmail(data).then(res => {
      //    console.log(data)

      //  })
      this.$confirm("此操作将永久删除该邮件, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          delEmail(data).then(res => {
            console.log(res, data);
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
    },
    // checkDetail: function (row) {
    //   this.detLoading = true;
    //   this.$emit('freshWiid', row.WIID);
    //   checkEmailDetail(row.WIID).then(res => {
    //     console.log(res)
    //     this.detail = res;
    //     this.detLoading = false;
    //   });
    // },
    checkDetail: function(row) {
      this.$emit("freshWiid", row.WIID);
      this.wiid = row.WIID;
      // this.backDetail();
      this.detLoading = true;
      checkEmailDetail(row.WIID).then(res => {
        // console.log(res)
        this.detail = res;
        this.detLoading = false;
        this.getListData(false);
      });
    },

    onPage: function() {
      this.getListData(true, this.inboxData);
    }
  },
  components: {
    "v-flex-container": flexContainer,
    "v-detail": detail,
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
</style>
