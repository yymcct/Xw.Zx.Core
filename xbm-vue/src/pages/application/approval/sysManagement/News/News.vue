<template>
  <div class="grid-inner-content home-laws" ref="laws-home">
    <div class="breadcrumb-box index-breadcrumb">
      <el-breadcrumb separator="/">
        <el-breadcrumb-item :to="{ path: '/index' }">首页</el-breadcrumb-item>
        <el-breadcrumb-item>新闻中心</el-breadcrumb-item>
      </el-breadcrumb>
    </div>
    <div class="cat-box1">
      <div class="laws-search-form" ref="LawForm">
        <el-form
          :inline="true"
          :model="formInline"
          label-position="right"
          label-width="70px"
          class="demo-form-inline"
        >
          <el-form-item label="类型" class="law-form-item">
            <el-select v-model="formInline.mlid" placeholder="类型目录" clearable style="width:100%">
              <el-option
                v-for="item in catList"
                :key="item.MLID"
                :label="item.NAME"
                :value="item.MLID"
              ></el-option>
            </el-select>
          </el-form-item>
          <el-form-item label="标题" class="law-form-item">
            <el-input v-model="formInline.wj_name" placeholder="关键字"></el-input>
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
        <el-table-column prop="NAME" label="类型" width="200">
          <!-- <template slot-scope="scope">
            <span>{{scope.row.NAME}}</span>
          </template>-->
        </el-table-column>
        <el-table-column prop="WJ_NAME" show-overflow-tooltip label="标题">
          <template slot-scope="scope">
            <span
              @click="handleClick(scope.row)"
              style="cursor:pointer"
              class="el-button--text"
            >{{scope.row.WJ_NAME}}</span>
            <!-- <el-button @click="handleClick(scope.row)" type="text">{{scope.row.WJ_NAME}}</el-button> -->
          </template>
        </el-table-column>
        <el-table-column prop="SCSJ" label="发布时间" width="110"></el-table-column>
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
      height: "calc(100% - 62px)!important",
      loading: false,
      dialogVisible: false,
      catFromShow: false,
      // relaObj: {},
      formInline: {
        page: 1,
        pagesize: 10,
        // mlid: "",
        wj_name: "",
        lg_time: "",
        lg_move: "",
        fl: 1
      },
      total: 0
    };
  },
  computed: {},
  created() {
    this.getLawsData();
    this.getCatList();

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
    getCatList: function() {
      dataService.getLawsCat(1).then(res => {
        this.catList = res.data;
      });
    },
    getLawsData() {
      this.loading = true;
      console.log(this.formInline, "pp");
      dataService.getLawsData(this.formInline).then(res => {
        this.data = res.DATA;
        this.total = res.SIZE;
        this.loading = false;
      });
    },
    addLawsData: function() {
      this.dialogVisible = true;
      this.formInline = {
        page: 1,
        pagesize: this.formInline.pagesize,
        mlid: "",
        wj_name: "",
        lg_time: "",
        lg_move: "",
        fl: 1
      };
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
      let temp = {
        router: true,
        name: "政策法规",
        path: "/Laws",
        url: "/Laws"
      };
      this.$store.commit("navTabs/getTopMenuData", temp);
      this.$router.push({
        path: "/LawsDetail",
        query: { wiid: obj.WIID, type: obj.NAME }
      });
    }
  },
  components: {
    "v-flex-container": flexContainer,
    add
  }
};
</script>

<style lang="scss" >
.home-laws {
  height: 100%;
  .index-breadcrumb {
    border: 1px solid #dcdfe6;
    padding: 10px;
    background-color: #f5f7fa;
  }
  .cat-box1 {
    float: left;
    width: 100%;
    padding: 10px;
    height: calc(100% - 36px);
    border: 1px solid #ebeef5;
    // margin: 10px 10px 10px 0px;
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
  .law-form-item {
    .el-form-item__label {
      font-size: 16px;
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
