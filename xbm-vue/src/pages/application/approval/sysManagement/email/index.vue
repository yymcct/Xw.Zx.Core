<template>
  <div class="email">
    <div class="top-bar">
      <div class="left-btn">
        <el-button
          type="primary"
          icon="el-icon-edit"
          size="small"
          @click="addEmail"
          >写邮件</el-button
        >
        <el-form
          v-show="serachShow"
          style="display:flex;align-item:centr;color:black"
          :model="form"
          label-width="80px"
        >
          <el-form-item v-show="activeSideName == 'inBox'" label="发件人">
            <el-input
              size="mini"
              v-model="form.fjr"
              autocomplete="off"
            ></el-input
          ></el-form-item>
          <el-form-item v-show="activeSideName == 'draft'" label="收件人">
            <el-input
              size="mini"
              v-model="form.sjr"
              autocomplete="off"
            ></el-input
          ></el-form-item>
          <el-form-item v-show="activeSideName != 'draft'" label="主题内容">
            <el-input
              size="mini"
              v-model="form.ztnr"
              autocomplete="off"
            ></el-input
          ></el-form-item>
          <el-button
            style="height:32px;margin-left:20px;margin-top:3px"
            size="small"
            type="primary"
            icon="el-icon-search"
            @click="doSearch"
            >搜索</el-button
          >
        </el-form>

        <!-- <el-button type="danger"  size="small" @click="deleteEmail">删除</el-button> -->
      </div>
    </div>
    <div class="em-content">
      <aside-left @searchClose="searchClose"></aside-left>
      <div
        class="em-inner-content"
        :style="isCollapse ? 'left:64px;width:calc(100% - 65px)' : ''"
      >
        <inBox
          @searchShow="searchEmail"
          :inboxData="form"
          @freshWiid="freshWiid"
          ref="inBox"
          v-if="activeSideName == 'inBox' || activeSideName == 'addEmail'"
        ></inBox>
        <outBox
          @searchShow="searchEmail"
          :outBoxData="outBoxData"
          @freshWiid="freshWiid"
          ref="outBox"
          v-else-if="activeSideName == 'outBox'"
        ></outBox>
        <draft
          @searchShow="searchEmail"
          :draftData="draftData"
          @freshWiid="freshWiid"
          ref="draft"
          v-else-if="activeSideName == 'draft'"
        ></draft>
        <tagsCloud v-else-if="activeSideName == 'tagsCloud'"></tagsCloud>
        <!-- <v-content></v-content> -->
      </div>
    </div>
  </div>
</template>
<script>
import { delEmail } from "@/public/apiService/PersonalAffairs/email";
import leftSide from "./children/EmailLeftNav";
import inBox from "./children/inBox";
import outBox from "./children/outBox";
import draft from "./children/draft";
import tagsCloud from "@/components/TagsCloud";
export default {
  name: "email",
  data() {
    return {
      wiid: null,
      form: {},
      outBoxData: {},
      draftData: {},
      serachShow: false
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

  methods: {
    searchClose() {
      this.serachShow = false;
    },
    searchEmail() {
      this.serachShow = true;
    },
    //搜索邮件
    doSearch() {
      console.log(this.activeSideName);
      if (this.activeSideName == "inBox") {
        this.$refs.inBox.getListData(true, this.form);
      } else if (this.activeSideName == "outBox") {
        if (Object.keys(this.form).length == 0) {
          this.outBoxData = {};
        } else {
          this.outBoxData.ztnr = this.form.ztnr;
        }
        this.$refs.outBox.getListData(true, this.outBoxData);
      } else {
        console.log(this.form);
        if (Object.keys(this.form).length == 0) {
          this.draftData = {};
        } else {
          this.draftData.sjr = this.form.sjr;
        }
        this.$refs.draft.getListData(this.draftData);
      }
    },
    handleClick() {
      alert("button click");
    },
    addEmail: function() {
      //邮件迁移过去之后开启
      this.$store.commit("curSideName", "addEmail");
      // this.$message({
      //   type: "warning",
      //   message: "邮件系统未迁移，请在原政务系统中填写和发送邮件"
      // });
    },
    freshWiid: function(wiid) {
      this.wiid = wiid;
    },
    deleteEmail: function() {
      if (!this.wiid) {
        this.$message({
          type: "warning",
          message: "请选择要删除的内容"
        });
        return;
      }
      delEmail(this.wiid).then(res => {
        if (res.success) {
          this.$message({
            type: "success",
            message: "删除成功!"
          });
          this.$refs[this.activeSideName].getListData();
          return;
        }
        this.$message({
          type: "error",
          message: res.msg
        });
      });
    }
  },
  components: {
    "aside-left": leftSide,
    // "v-content": content,
    inBox,
    outBox,
    draft,
    tagsCloud
  }
};
</script>
<style lang="scss" scoped>
.el-form-item {
  margin-bottom: 0;
}
.email {
  height: 100%;
  .top-bar {
    height: 50px;
    line-height: 50px;
    background-color: #fafafa;
    background-image: -moz-linear-gradient(top, #ffffff, #f2f2f2);
    background-image: -webkit-gradient(
      linear,
      0 0,
      0 100%,
      from(#ffffff),
      to(#f2f2f2)
    );
    background-image: -webkit-linear-gradient(top, #ffffff, #f2f2f2);
    background-image: -o-linear-gradient(top, #ffffff, #f2f2f2);
    background-image: linear-gradient(to bottom, #ffffff, #f2f2f2);
    background-repeat: repeat-x;
    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#ffffffff', endColorstr='#fff2f2f2', GradientType=0);
    border-bottom: 1px solid #d4d4d4;
    -webkit-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.065);
    -moz-box-shadow: 0 1px 4px rgba(0, 0, 0, 0.065);
    box-shadow: 0 1px 4px rgba(0, 0, 0, 0.065);
    .left-btn {
      padding-left: 20px;
      display: flex;
      align-items: center;
      .el-button-group .el-button--primary:last-child {
        border-left-color: rgb(158, 209, 249);
      }
      .em-dropdown-title {
        cursor: pointer;
        padding: 6px 10px;
        background: #e9f6ff;
        border: 1px solid #a5d9ff;
        border-radius: 3px;
        display: inline-block;
        height: 19px;
        line-height: 17px;
      }
    }
  }
  .em-content {
    height: calc(100% - 50px);
    position: relative;
    .em-inner-content {
      height: 100%;
      width: calc(100% - 220px);
      position: absolute;
      left: 220px;
      top: 0px;
      transition: all 1s ease;
      overflow: auto;
    }
  }
}
</style>
