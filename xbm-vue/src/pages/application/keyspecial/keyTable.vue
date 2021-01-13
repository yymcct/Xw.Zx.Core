<template>
  <div class="approval-right">
    <el-table
      ref="multipleTable"
      :data="tableData"
      tooltip-effect="dark"
      :row-style="{height:'38px'}"
      :cell-style="{padding:'0px'}"
      header-row-class-name="tableHead"
      style="width: 100%"
      :row-class-name="tableColor"
    >
      <el-table-column type="selection" width="55"></el-table-column>
      <el-table-column prop="zip" label="编号" width="120"></el-table-column>
      <el-table-column label="内容" width="200">
        <template slot-scope="scope">{{ scope.row.date }}</template>
      </el-table-column>
      <el-table-column prop="name" label="创建人" width="120"></el-table-column>
      <el-table-column prop="address" label="创建时间" show-overflow-tooltip></el-table-column>
      <el-table-column prop="city" label="状态" width="120">
        <template slot-scope="scope">
          <span>{{ scope.row.city }}</span>
        </template>
      </el-table-column>
    </el-table>
    <Pagination
      :total="tableData.length"
      :pageSize="5"
      @handleSizeChangeSub="handleSizeChangeFun"
      @handleCurrentChangeSub="handleCurrentChangeFun"
    ></Pagination>
  </div>
</template>

<script>
import Pagination from "@/components/pagination";
export default {
  name: "right",
  components: { Pagination },
  data() {
    return {
      tableData: [
        {
          date: "地名地址资源",
          name: "邓力夫",
          province: "上海",
          city: "编辑中...",
          address: "2019-12-24",
          zip: 113530,
          status: 0
        },
        {
          date: "国土空间规划",
          name: "范长江",
          province: "上海",
          city: "执行中",
          address: "2019-12-24",
          zip: 113534,
          status: 1
        },
        {
          date: "国土空间基础信息平台",
          name: "郝爱民",
          province: "上海",
          city: "已终结",
          address: "2019-12-24",
          zip: 113535,
          status: 2
        },
        {
          date: "基础用地",
          name: "李强",
          province: "上海",
          city: "已作废",
          address: "2019-12-24",
          zip: 10633,
          status: 3
        },
        {
          date: "建设用地",
          name: "王小虎",
          province: "上海",
          city: "已作废",
          address: "2019-12-24",
          zip: 11022,
          status: 3
        },
        {
          date: "建设用地2",
          name: "杨勇",
          province: "上海",
          city: "已作废",
          address: "2019-12-24",
          zip: 10010,
          status: 3
        },
        {
          date: "工业用地",
          name: "孙顺达",
          province: "上海",
          city: "执行中",
          address: "2019-12-24",
          zip: 20133,
          status: 1
        },
        {
          date: "禁止建设用地",
          name: "张石山",
          province: "上海",
          city: "执行中",
          address: "2019-12-24",
          zip: 20199,
          status: 1
        },
        {
          date: "洛阳基本农田",
          name: "吴国梁",
          province: "上海",
          city: "编辑中...",
          address: "2019-12-24",
          zip: 20313,
          status: 0
        },
        {
          date: "焦作基本农田",
          name: "王仁兴",
          province: "上海",
          city: "执行中",
          address: "2019-12-24",
          zip: 2033,
          status: 1
        }
      ],
      status: false
    };
  },
  methods: {
    handleClick(row) {
      console.log(row);
    },
    handleSizeChangeFun(v) {
      this.pageSize = v;
      //   this._enterpriseList(); //更新列表
    },

    handleCurrentChangeFun(v) {
      //页面点击
      this.pageNum = v; //当前页
      //   this._enterpriseList(); //更新列表
    },
    tableColor({ row, rowIndex }) {
      if (rowIndex == rowIndex) {
        if (row.status == 0) {
          return "tableColor1";
        } else if (row.status == 1) {
          return "tableColor2";
        } else if (row.status == 2) {
          return "tableColor3";
        } else if (row.status == 3) {
          return "tableColor4";
        }
      }
    },
    handleClick(row) {
      console.log(row);
    }
  }
};
</script>

<style lang="scss" scoped>
.approval-right {
  width: 100%;
  border-top: 3px solid #07438b;
  /* box-shadow: 0px 2px 3px 0px rgba(0, 0, 0, 0.15); */
  box-shadow: 0px 12px 8px -12px rgba(0, 0, 0, 0.15);
  padding: 0px 54px 20px;
  background: #fff;
}
.table-button {
  width: 50px;
  height: 24px;
  background: rgba(237, 236, 236, 1);
  border-radius: 3px;
  color: #4c4948;
}
.el-table {
  margin-bottom: 20px;
}
.el-checkbox__input.is-checked .el-checkbox__inner,
.el-checkbox__input.is-indeterminate .el-checkbox__inner {
  background-color: #07438b;
  border-color: #07438b;
}
.tableHead {
  font-size: 18px;
  color: rgba(7, 67, 139, 1);
}
.tableColor1 td:nth-last-of-type(1) span {
  color: #ff5b5b;
}
.tableColor2 td:nth-last-of-type(1) span {
  color: #07438b;
}
.tableColor3 td:nth-last-of-type(1) span {
  color: #00c989;
}
.tableColor4 td:nth-last-of-type(1) span {
  color: #999999;
}
</style>