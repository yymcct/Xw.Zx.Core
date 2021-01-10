



<template>
  <section>
    <search-bar @search="handleSearch" />
    <!--列表-->
    <el-table
      :data="couponItems"
      highlight-current-row
      v-loading="loading"
      style="width: 100%"
    >
      <el-table-column
        prop="couponReceiveId"
        label="ID"
        width="100px"
        sortable
      ></el-table-column>
      <el-table-column prop="realName" label="用户" width="140px" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.realName }}</p>
          <p style="color: #999999">{{ scope.row.phone }}</p>
          <p style="color: #999999; font-weight: bold">
            {{ scope.row.memberVipTypeName }}
          </p>
          <el-link type="success" @click="showMemberInfo(scope.row)"
            >查看详情</el-link
          >
        </template>
      </el-table-column>

      <el-table-column prop="name" label="优惠券" sortable>
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.name }}</p>
          <p style="color: #ff5000">金额: {{ scope.row.money }}</p>
          <p style="color: #999999">
            有效期至: {{ scope.row.endTime.split(" ")[0] }} <br />领取时间:
            {{ scope.row.createTime }}
          </p>
        </template>
      </el-table-column>
      <el-table-column
        prop="couponUseStateName"
        label="使用状态"
        width="200px"
        sortable
      >
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.couponUseStateName }}</p>
        </template>
      </el-table-column>
      <el-table-column
        prop="couponUseStateName"
        label="使用详情"
        width="300px"
        sortable
      >
        <template slot-scope="scope" v-if="scope.row.couponUseState">
          <p style="color: #999999">使用商品: {{ scope.row.productName }}</p>
          <p style="color: #999999">该订单ID: {{ scope.row.orderid }}</p>
          <p style="color: #999999">使用时间: {{ scope.row.useTime }}</p>
        </template>
      </el-table-column>

      <el-table-column label="操作" width="100px">
        <template scope="scope">
          <el-button
            v-if="scope.row.couponUseState == 0"
            type="text"
            size="mini"
            @click="handleCouponToMemberIntegral(scope.row)"
            >兑换积分</el-button
          >
          <!-- <i
            class="el-icon-edit"
            style="margin: 0 5px; font-weight: bold; cursor: pointer"
            @click="handleEdit(scope.$index, scope.row)"
          ></i>
          <i
            class="el-icon-delete"
            style="margin: 0 5px; font-weight: bold; cursor: pointer"
            @click="handleDel(scope.$index, scope.row)"
          ></i> -->
        </template>
      </el-table-column>
    </el-table>

    <!--工具条align='center'-->
    <el-col :span="24" class="toolbar" align="right">
      <el-pagination
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page="requestParams.page"
        :page-sizes="[10, 50, 100, 500]"
        :page-size="requestParams.pageSize"
        layout="total, sizes, prev, pager, next, jumper"
        :total="total"
        background
      ></el-pagination>
    </el-col>
    <member-info v-model="memberInfo.show" :memberId="memberInfo.memberId" />
  </section>
</template>

<script>
import api from "@/api/app";
import searchBar from "./searchBar";
import memberInfo from "@/components/memberInfo";
export default {
  name: "coupon",
  components: {
    searchBar,
    memberInfo,
  },
  data() {
    return {
      requestParams: {
        page: 1,
        pageSize: 10,
        filters: "",
        sorts: "-id",
      },
      couponItems: [],
      total: 0,
      loading: false,
      edit: {
        id: 0,
        showEdit: false,
      },
      memberInfo: {
        show: false,
        memberId: 0,
      },
    };
  },
  mounted() {
    this.getCouponItems();
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getCouponItems();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getCouponItems();
    },
    handleSearch(filterStr) {
      this.requestParams.page = 1;
      this.requestParams.filters = filterStr;
      this.getCouponItems();
    },
    getCouponItems() {
      this.loading = true;
      api.coupon.getCoupon(this.requestParams).then((respone) => {
        this.loading = false;
        this.couponItems = respone.result;
        this.total = respone.total;
      });
    },
    //显示编辑界面
    handleCouponToMemberIntegral(row) {
      this.$confirm("确认将优惠券兑换为积分？", "提示", {}).then(() => {
        api.coupon.couponToMemberIntegral(row.couponReceiveId).then((res) => {
          this.$message({
            message: `兑换成功, 当前用户积分为${res.result.availableIntegrals}`,
            type: "success",
          });
          this.getCouponItems();
        });
      });
    },

    // //删除
    // handleDel(index, row) {
    //   this.$confirm("确认删除?", "提示", { type: "warning" }).then(() => {
    //     this.loading = true;
    //     api.coupon
    //       .del(row.id)
    //       .then((res) => {
    //         this.loading = false;
    //         this.$message({
    //           message: "删除成功",
    //           type: "success",
    //         });
    //         this.getCouponItems();
    //       })
    //       .catch(() => {
    //         this.loading = false;
    //       });
    //   });
    // },
    editChange(cancel) {
      if (cancel != "cancel") {
        this.getCouponItems();
      }
    },

    showMemberInfo(row) {
      if (row.memberId) {
        this.memberInfo.memberId = row.memberId;
        this.memberInfo.show = true;
      }
    },
  },
};
</script>

<style scoped>
p {
  padding: 0px;
  margin: 0px;
}
</style>