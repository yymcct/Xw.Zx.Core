<template>
  <div class="grid-inner-content home-laws" ref="laws-home">
    <!-- <v-flex-container :leftWidth="'180px'"> -->
    <!-- <div slot="left" class="org-left"  element-loading-text="拼命加载中"> -->
    <div class="cat-left">
      <div class="cat-title">
        法律法规目录
        <el-button
          style="float: right; padding-right:10px;margin:5px"
          type="primary"
          size="mini"
          @click="catFromShow = !catFromShow"
          >{{ catFromShow ? "取消新增" : "新增" }}</el-button
        >
      </div>
      <div class="laws-cat">
        <div v-if="catFromShow" class="add-cat">
          <el-input
            v-model="catName"
            placeholder="名称"
            class="cus-cat-input"
          ></el-input>
          <el-button
            type="primary"
            class="cat-btn"
            size="mini"
            @click="subCatData($event)"
            >确定</el-button
          >
        </div>
        <ul style="height:100%;overflow:auto;">
          <li
            class="cat-list"
            v-for="(item, idx) in catList"
            :key="idx"
            @click="checkData(item.MLID)"
          >
            <a class="cat-text">{{ item.NAME }}</a>
            <div class="cat-item-right">
              <el-button
                type="text"
                style="color:red"
                @click="delLawsCatData($event, item.MLID)"
                >删除</el-button
              >
            </div>
          </li>
        </ul>
      </div>
    </div>
    <!-- </div> -->
    <div class="cat-right">
      <!-- <div slot="right" class="org-right"> -->
      <!-- <div class="breadcrumb-box cat-breadcrumb">
        <el-breadcrumb separator="/">
          <el-breadcrumb-item :to="{ path: '/index' }">首页</el-breadcrumb-item>
          <el-breadcrumb-item>政策法规</el-breadcrumb-item>
        </el-breadcrumb>
      </div>-->
      <div class="laws-search-form" ref="LawForm">
        <el-form
          :inline="true"
          :model="formInline"
          label-position="right"
          label-width="40px"
          class="demo-form-inline"
        >
          <el-form-item label="类型">
            <el-select
              v-model="formInline.mlid"
              placeholder="类型目录"
              clearable
              style="width:190px;padding-left:10px"
            >
              <el-option
                v-for="item in catList"
                :key="item.MLID"
                :label="item.NAME"
                :value="item.MLID"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="标题">
            <el-input
              v-model="formInline.wj_name"
              placeholder="关键字"
              style="width:180px"
            ></el-input>
          </el-form-item>
          <!-- <el-form-item label="创建时间" clearable>
            <el-date-picker
              type="date"
              :editable="false"
              placeholder="选择日期"
              v-model="formInline.lg_time"
              format="yyyy-MM-dd"
              value-format="yyyy-MM-dd"
            ></el-date-picker>
          </el-form-item>
          <el-form-item label="离开时间" clearable>
            <el-date-picker
              type="date"
              :editable="false"
              placeholder="选择日期"
              format="yyyy-MM-dd"
              value-format="yyyy-MM-dd"
              v-model="formInline.lg_move"
            ></el-date-picker>
          </el-form-item>-->
          <el-form-item>
            <el-button type="primary" @click="getLawsData">查询</el-button>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="addLawsData">新增</el-button>
          </el-form-item>
        </el-form>
      </div>
      <el-table
        :data="data"
        border
        style="width: 100%"
        :height="height"
        class="version-table"
        v-loading="loading"
      >
        <el-table-column type="index" width="50" label="序号"></el-table-column>
        <el-table-column prop="NAME" label="类型" width="200"></el-table-column>
        <el-table-column prop="WJ_NAME" show-overflow-tooltip label="标题">
          <template slot-scope="scope">
            <!-- <el-button @click="handleClick(scope.row)" type="text">{{scope.row.WJ_NAME}}</el-button> -->
            <span
              @click="handleClick(scope.row)"
              style="cursor:pointer"
              class="el-button--text"
              >{{ scope.row.WJ_NAME }}</span
            >
          </template>
        </el-table-column>
        <el-table-column
          prop="SCSJ"
          label="发布时间"
          width="110"
        ></el-table-column>
        <el-table-column fixed="right" label="操作" width="180">
          <template slot-scope="scope">
            <el-button @click="editLawsData(scope.row)" type="text">
              <i class="el-icon-edit"></i>
              编辑
            </el-button>
            <el-button @click="delLawsData(scope.row.WIID)" type="text">
              <i class="el-icon-delete common-text common-red"></i>
              <font class="common-red">删除</font>
            </el-button>
          </template>
        </el-table-column>
      </el-table>
      <el-pagination
        background
        @current-change="onChangePage"
        layout="total,prev,pager,next,jumper"
        :total="total"
        :page-size="formInline.pagesize"
        class="cus-pagination"
      ></el-pagination>
    </div>
    <!-- </v-flex-container> -->
    <el-dialog
      title="新增"
      :visible.sync="dialogVisible"
      width="900px"
      v-dialogDrag
      append-to-body
      :close-on-click-modal="false"
      class="custom-dialog"
    >
      <add
        @onSubmit="onSubmit"
        :catList="catList"
        :curData="curData"
        v-if="dialogVisible"
      ></add>
      <!-- <div slot="footer" class="dialog-footer">
				<el-button @click="dialogVisible = false">取 消</el-button>
				<el-button type="primary" @click="onSubmit">确 定</el-button>
      </div>-->
    </el-dialog>
  </div>
</template>
<script>
import * as dataService from "@/public/apiService/home";
import flexContainer from "@/components/FlexContainer";
import add from "./children/LawsAdd";
export default {
  name: "Home",
  data: function() {
    return {
      data: [],
      catList: [],
      catName: "",
      curData: null,
      height: "calc(100% - 62px)!important",
      loading: false,
      dialogVisible: false,
      catFromShow: false,
      // relaObj: {},
      formInline: {
        page: 1,
        pagesize: 10,
        mlid: "",
        wj_name: "",
        lg_time: "",
        lg_move: "",
        fl: 0
      },
      total: 0
    };
  },
  computed: {},
  created() {
    this.getCatList();
    this.getLawsData();
    this.initLayout();
    window.onresize = () => {
      this.initLayout();
    };
  },
  watch: {
    catFromShow: function(val) {
      if (!val) {
        this.catName = "";
      }
    }
  },
  mounted() {},
  methods: {
    initLayout: function() {
      let that = this;
      this.$nextTick(() => {
        var height = that.$refs.LawForm.clientHeight;
        that.height =
          that.$refs["laws-home"].clientHeight - height - 130 + "px";
      });
    },
    subCatData: function(e) {
      e.stopPropagation();
      if (this.catName == "") {
        this.$message({
          type: "warning",
          message: "内容不能为空!"
        });
        return;
      }
      dataService.addCat(this.catName, 0).then(res => {
        if (res.success) {
          this.getCatList();
          this.catFromShow = false;
          this.$message({
            type: "success",
            message: "添加成功!"
          });
        }
      });
    },
    delLawsCatData(e, mlid) {
      e.stopPropagation();
      this.$confirm("此操作将永久删除该数据, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      }).then(() => {
        dataService.delCat(mlid).then(res => {
          if (res.success) {
            this.getCatList();
            this.$message({
              type: "success",
              message: "删除成功!"
            });
          }
        });
      });
    },
    getCatList: function() {
      dataService.getLawsCat(0).then(res => {
        this.catList = res.data;
        // res.data &&
        //   res.data.forEach(item => {
        //     this.relaObj[item.MLID] = item.NAME;
        //   });
      });
    },
    getLawsData() {
      this.loading = true;
      dataService.getLawsData(this.formInline).then(res => {
        this.data = res.DATA;
        this.total = res.SIZE;
        this.loading = false;
      });
    },
    addLawsData: function() {
      this.dialogVisible = true;
      this.curData = null;
      this.formInline = {
        page: 1,
        pagesize: this.formInline.pagesize,
        mlid: "",
        wj_name: "",
        lg_time: "",
        lg_move: "",
        fl: 0
      };
    },
    editLawsData: function(row) {
      this.curData = row;
      dataService.checkLaws(row.WIID).then(res => {
        //  this.detail=res;
        let WJ_NR = "";
        res.data.forEach(item => {
          WJ_NR += item.WJ_NR;
        });
        this.curData.WJ_NR = WJ_NR;
        this.dialogVisible = true;
      });
    },
    delLawsData(wiid) {
      this.$confirm("此操作将永久删除该文件, 是否继续?", "提示", {
        closeOnClickModal: false,
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      }).then(() => {
        dataService.delLaws(wiid).then(res => {
          if (res.success) {
            this.getLawsData();
            this.$message({
              type: "success",
              message: "删除成功!"
            });
          }
        });
      });
    },

    onSubmit: function() {
      // this.getCatList();
      this.getLawsData();
      this.dialogVisible = false;
    },
    checkData: function(param) {
      this.formInline.mlid = param;
      this.getLawsData();
    },
    onChangePage: function(page) {
      this.formInline.page = page;
      this.getLawsData();
    },
    handleClick: function(obj) {
      let routeData = this.$router.resolve({
        path: "/LawsDetail",
        query: { wiid: obj.WIID, type: obj.NAME, flag: "manage" }
      });
      window.open(routeData.href, "_blank");
    }
  },
  components: {
    "v-flex-container": flexContainer,
    add
  }
};
</script>

<style lang="scss">
.home-laws {
  // padding:10px;
  height: 100%;
  // height: calc(100% - 90px);
  .cat-breadcrumb {
    border-bottom: 1px solid #dcdfe6;
    padding: 6px 0px 15px 0px;
    margin-bottom: 10px;
  }
  .cat-left {
    // border: 1px solid #ddd;
    border-top: 0px;
    // margin:10px;
    float: left;
    height: 100%;
    padding: 10px;
    font-size: 14px;
    width: 300px;
    .cat-title {
      height: 40px;
      line-height: 40px;
      font-weight: bold;
      background: #f5f7f9;
      position: relative;
      border: 1px solid #ebeef5;
      text-indent: 20px;
    }
    .laws-cat {
      border: 1px solid #ebeef5;
      border-top: none;
      box-shadow: 0 1px 5px rgba(0, 0, 0, 0.1);
      height: calc(100% - 30px);
      .add-cat {
        position: relative;
        border-bottom: 1px solid #ebeef5;
        transition: all 1s ease-in;
        .cus-cat-input {
          width: calc(100% - 80px);
          margin: 5px;
          > .el-input__inner {
            height: 32px;
            line-height: 32px;
          }
        }
        .cat-btn {
          position: absolute;
          right: 10px;
          top: 7px;
        }
      }

      .cat-list {
        // height: 35px;
        // line-height: 35px;
        position: relative;
        box-sizing: border-box;
        border-bottom: 1px dashed #ebeef5;
        &:hover {
          background: #f9f9f9;
          // color: #2196F3;
          cursor: pointer;
          font-weight: bolder;
          .cat-item-right {
            display: inline-block;
          }
        }

        .cat-text {
          display: inline-block;
          padding: 10px 0px 10px 20px;
          width: calc(100% - 50px);
          .cus-cat-input {
            float: left;
            .el-input__inner {
              height: 32px;
              line-height: 32px;
            }
          }
        }
        .cat-item-right {
          width: 40px;
          position: absolute;
          right: 0px;
          top: 0px;
          display: none;
          transition: all 1s ease-in;
        }
      }
    }
  }
  .cat-right {
    float: left;
    width: calc(100% - 310px);
    padding: 10px;
    height: calc(100% - 20px);
    border: 1px solid #ebeef5;

    margin: 10px 10px 10px 0px;
  }
  .org-left,
  .org-right {
    height: 100%;
  }
  .tab-list {
    float: right;
    .tab-item {
      float: left;
    }
  }
  .ma-list {
    // padding-right: 30px;
    // padding-left: 20px;
    .list-item {
      height: 32px;
      line-height: 32px;
      border-bottom: 1px dashed #ddd;
      > a {
        text-decoration: none;
        font-size: 14px;
        color: #333;
        width: calc(100% - 90px);
        overflow: hidden;
        white-space: nowrap;
        text-overflow: ellipsis;
        float: left;
      }
      > span {
        display: inline-block;
        width: 90px;
        color: #909399;
        font-size: 14px;
      }
    }
  }
  .cus-pagination {
    text-align: center;
    padding-top: 10px;
  }
}
</style>
