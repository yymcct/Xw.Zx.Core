<template>
  <div class="box">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item>首页</el-breadcrumb-item>
      <el-breadcrumb-item>学习教育</el-breadcrumb-item>
    </el-breadcrumb>
    <div class="news-box clearfix">
      <ul v-loading="loading">
        <li
          v-for="(item, index) in catList"
          :key="index"
          :class="{ cur: cur == index }"
          @click="listHandle(index, item)"
        >
          <p>{{ item.BT }}</p>
          <span>{{ item.CJRQ }}</span>
        </li>
      </ul>
      <Pagination
        :total="total"
        :pageSize="10"
        @handleSizeChangeSub="handleSizeChangeFun"
        @handleCurrentChangeSub="handleCurrentChangeFun"
      ></Pagination>
    </div>
    <template>
      <el-dialog
        title="学习教育信息"
        :visible.sync="dialogVisible"
        append-to-body
        :close-on-click-modal="false"
      >
        <el-form
          :model="detailForm"
          status-icon
          label-width="75px"
          class="demo-ruleForm"
          label-position="left"
        >
          <el-row>
            <el-col :span="8">
              <el-form-item label="创建人:" prop="CJR">
                <el-input v-model="detailForm.CJR" :disabled="dis"></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="创建科室:" prop="CJKS">
                <el-input v-model="detailForm.CJKS"></el-input>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="创建日期:" prop="CJRQ">
                <el-input v-model="detailForm.CJRQ" :disabled="dis"></el-input>
              </el-form-item>
            </el-col>
          </el-row>
          <el-row>
            <el-form-item label="标题:" prop="BT">
              <el-input type="textarea" v-model="detailForm.BT" :disabled="dis"></el-input>
            </el-form-item>
          </el-row>
          <el-row>
            <el-form-item label="附件列表:" prop="checkPass">
              <ul>
                <li v-for="(item,index) in detailForm.FILE" :key="index">
                 <a style="color:blue;line-height:24px;font-size: .875em;" target="_blank" :href="'/jz/XBM_Service.bsp?IMAGE&Source='+item.AC_NAME">
                   <span class="el-icon-paperclip"></span>{{item.SR_NAME||'null'}}</a>
                  </li>
              </ul>
            </el-form-item>
          </el-row>
        </el-form>
        <div slot="footer" class="dialog-footer">
          <el-button @click="dialogVisible = false">关闭</el-button>
        </div>
      </el-dialog>
    </template>
  </div>
</template>

<script>
import Pagination from "@/components/pagination";
import * as dataService from "@/public/apiService/home";
var userInfo=localStorage.getItem("data")&&JSON.parse(localStorage.getItem("data"));
export default {
  name: "study",
  components: {
    Pagination
  },
  data() {
    return {
      catList: [],
      cur: 0,
      option: {
        uid: '',
        nt_name: "",
        nt_sender: "",
        page: 1,
        zt: ""
      },
      dialogVisible: false,
      detailForm: {},
      dis: false,
      total: 0,
      loading: true
    };
  },
  created() {
    this.getStudyData();
  },
  methods: {
    submitPass() {},
    getStudyData() {
      this.option.uid=userInfo?userInfo.ur_ident:'';
      dataService
        .homeStudy(this.option)
        .then(res => {
          this.loading = false;
          this.catList = res.DATA;
          this.total = res.SIZE;
        })
        .catch(err => {
          console.log(err);
        });
    },
    listHandle(index, item) {
      this.cur = index;
      this.dialogVisible = true;
      this.detailForm = item;
    },
    handleSizeChangeFun(v) {
      this.option.pagesize = v;
      //   this._enterpriseList(); //更新列表
    },

    handleCurrentChangeFun(v) {
      //页面点击
      this.option.page = v;
      this.getStudyData(); //更新列表
    }
  }
};
</script>

<style lang="scss" scoped>
.news-box {
  padding: 0 74px 60px 50px;
  background: #fff;
  ul {
    margin-bottom: 30px;
    min-height: 300px;
  }
  li {
    padding: 20px;
    border-bottom: 1px dashed #ccc;
    cursor: pointer;
    display: flex;
    p {
      flex: 1;
      overflow: hidden;
      white-space: nowrap;
      text-overflow: ellipsis;
    }
    span {
      width: 100px;
      text-align: right;
    }
  }
  li:hover {
    background: #d4e4f7;
    color: #fff;
    &::after {
      border-left-color: #fff;
    }
  }
}
.demo-ruleForm {
  >>> .el-input__inner {
    padding: 0;
    border: 0 none;
  }
}
</style>
