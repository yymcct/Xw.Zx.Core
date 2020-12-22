
<template>
  <section v-if="orderMDtos">
    <el-row>
      <el-col :span="24" class="toolbar" style="padding-bottom: 0px">
        <el-form :inline="true" :model="filters">
          <el-form-item>
            <el-input
              v-model.trim="filters.keyword"
              placeholder="单号,姓名,电话"
            ></el-input>
          </el-form-item>
          <el-form-item>
            <el-date-picker
              v-model="filters.addTimeStart"
              type="date"
              placeholder="开始时间"
              align="right"
              :picker-options="glpickerOptions"
              value-format="yyyy-MM-dd"
            ></el-date-picker>
            <el-date-picker
              v-model="filters.addTimeEnd"
              type="date"
              placeholder="结束时间"
              align="right"
              :picker-options="glpickerOptions"
              value-format="yyyy-MM-dd"
            ></el-date-picker>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="getOrderMDtos">查询</el-button>
          </el-form-item>
        </el-form>
      </el-col>
    </el-row>
    <el-row class="toolbar" style="padding-top: 20px; padding-bottom: 20px">
      <el-col :span="24">
        <el-tag type="danger"
          >当查询条件下合计:{{ orderMDtos.queryTotal }}</el-tag
        >
        <el-tag>全部毛收入合计:{{ orderMDtos.allOrderTotal }}</el-tag>
        <el-tag>全部提现合计:{{ orderMDtos.withdrawDepositsTotal }}</el-tag>
        <el-tag>(全部毛收入-全部提现)合计:{{ orderMDtos.balance }}</el-tag>
      </el-col>
    </el-row>
    <!--列表-->
    <el-table
      :data="orderMDtos.orderMDtos"
      highlight-current-row
      v-loading="listLoading"
      style="width: 100%"
      :header-cell-style="{
        'background-color': '#eef1f6',
        color: '#1f2d3d',
      }"
    >
      <el-table-column prop="id" label="Id" width="60px"></el-table-column>
      <el-table-column
        prop="timestamp"
        label="单号"
        width="180px"
      ></el-table-column>
      <el-table-column prop="producName" label="商品名"></el-table-column>
      <el-table-column prop="amount" label="支付金额" width="160px">
        <template slot-scope="scope">
          <span style="color: #ff5000; font-weight: bold">{{
            scope.row.amount
          }}</span>
          <p style="color: #999999">
            商品价格:  {{ scope.row.productAmount }}
            <br/>
            支付通道: {{ scope.row.orderPaymentTypeName }}
          </p>
        </template>
      </el-table-column>
      <el-table-column prop="realName" label="姓名" width="130px">
        <template slot-scope="scope">
          <p style="font-weight: bold">{{ scope.row.realName }}</p>
          <p style="color: #999999; font-weight: bold">
            {{ scope.row.memberPhone }}
          </p>

          <p>
            <el-link type="primary" @click="showParentTree(scope.row)"
              >查看团队树</el-link
            >
          </p>
        </template>
      </el-table-column>
      <el-table-column prop="realName" label="客户姓名" width="180px">
        <template slot-scope="scope">
          <el-popover trigger="hover" placement="top">
            <p style="font-weight: bold">{{ scope.row.customerName }}</p>
            <p style="color: #999999; font-weight: bold">
              {{ scope.row.customerPhone }}
            </p>
            <p style="color: #999999" v-if="scope.row.remark">
              备注: {{ scope.row.remark }}
            </p>
            <div slot="reference" class="name-wrapper">
              <p style="font-weight: bold">{{ scope.row.customerName }}</p>
              <p style="color: #999999; font-weight: bold">
                {{ scope.row.customerPhone }}
              </p>
              <p
                style="
                  color: #999999;
                  overflow: hidden;
                  text-overflow: ellipsis;
                  white-space: nowrap;
                "
                v-if="scope.row.remark"
              >
                备注: {{ scope.row.remark }}
              </p>
            </div>
          </el-popover>
        </template>
      </el-table-column>
      <el-table-column prop="addTime" label="时间" width="120px">
        <template slot-scope="scope">
          <p>{{ scope.row.addTime.split(" ")[0] }}</p>
          <p style="color: #999999; font-size: 14px">
            {{ scope.row.addTime.split(" ")[1] }}
          </p>
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

    <parent v-model="parent.showParent" :memberId="parent.memberId" />
  </section>
</template>

<script>
import parent from "@/components/parentTree";
import { api_getOrderMDtos } from "../../api/api";
export default {
  components: { parent },
  data() {
    return {
      requestParams: {
        page: 1,
        pageSize: 10,
        filters: "",
        sorts: "-id",
      },
      filters: {
        keyword: null,
        addTimeStart: null,
        addTimeEnd: null,
      },
      orderMDtos: null,
      total: 0,
      listLoading: false,
      parent: {
        showParent: false,
        memberId: 0,
      },
    };
  },
  methods: {
    handleSizeChange(val) {
      this.requestParams.pageSize = val;
      this.getOrderMDtos();
    },
    handleCurrentChange(val) {
      this.requestParams.page = val;
      this.getOrderMDtos();
    },
    getOrderMDtos() {
      this.listLoading = true;
      this.page = 1;
      this.requestParams.filters = "";

      if (this.filters.keyword)
        this.requestParams.filters += `(Timestamp|RealName|MemberPhone|customerName|customerPhone)@=${this.filters.keyword},`;

      if (this.filters.addTimeStart)
        this.requestParams.filters += `AddTime>=${this.filters.addTimeStart},`;
      if (this.filters.addTimeEnd)
        this.requestParams.filters += `AddTime<=${this.filters.addTimeEnd},`;

      api_getOrderMDtos(this.requestParams).then((respone) => {
        this.listLoading = false;
        this.orderMDtos = respone.result;
        this.total = respone.total;
      });
    },
    showParentTree(val) {
      this.parent.showParent = true;
      this.parent.memberId = val.memberId;
    },
  },

  mounted() {
    this.getOrderMDtos();
  },
};
</script>

<style scoped>
p {
  padding: 0px;
  margin: 0px;
}
.el-tag {
  margin-left: 10px;
}
</style>