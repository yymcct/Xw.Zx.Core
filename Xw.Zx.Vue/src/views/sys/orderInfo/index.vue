<template>
  <section>
    <search-bar @search="handleSearch" />
    <table v-if="orderInfo">
      <tr>
        <th>类别</th>
        <th>状态</th>
        <th>合计订单数(单)</th>
        <th>合计金额(元)</th>
      </tr>
      <tr>
        <td rowspan="2">订单</td>
        <td>已支付</td>
        <td>{{ orderInfo.orderSucess.count }}</td>
        <td>{{ orderInfo.orderSucess.amount }}</td>
      </tr>
      <tr>
        <td>待支付</td>
        <td>{{ orderInfo.orderWaitPay.count }}</td>
        <td>{{ orderInfo.orderWaitPay.amount }}</td>
      </tr>
      <tr>
        <td rowspan="3">分润</td>
        <td>已通过</td>
        <td>{{ orderInfo.incomeSucess.count }}</td>
        <td>{{ orderInfo.incomeSucess.amount }}</td>
      </tr>
      <tr>
        <td>待审核</td>
        <td>{{ orderInfo.incomeWaitAudit.count }}</td>
        <td>{{ orderInfo.incomeWaitAudit.amount }}</td>
      </tr>
      <tr>
        <td>已拒绝</td>
        <td>{{ orderInfo.incomeFail.count }}</td>
        <td>{{ orderInfo.incomeFail.amount }}</td>
      </tr>
      <tr>
        <td rowspan="5">提现</td>
        <td>申请提现</td>
        <td>{{ orderInfo.withdrawApplyFor.count }}</td>
        <td>{{ orderInfo.withdrawApplyFor.amount }}</td>
      </tr>
      <tr>
        <td>统计部审核</td>
        <td>{{ orderInfo.withdrawTongjibuAudit.count }}</td>
        <td>{{ orderInfo.withdrawTongjibuAudit.amount }}</td>
      </tr>
      <tr>
        <td>财务部审核</td>
        <td>{{ orderInfo.withdrawCaiwubuAudit.count }}</td>
        <td>{{ orderInfo.withdrawCaiwubuAudit.amount }}</td>
      </tr>
      <tr>
        <td>提现成功</td>
        <td>{{ orderInfo.withdrawSucess.count }}</td>
        <td>{{ orderInfo.withdrawSucess.amount }}</td>
      </tr>
      <tr>
        <td>提现失败</td>
        <td>{{ orderInfo.withdrawFail.count }}</td>
        <td>{{ orderInfo.withdrawFail.amount }}</td>
      </tr>
    </table>
  </section>
</template>

<script>
import api from "@/api/app";
import searchBar from "./searchBar";

export default {
  name: "orderInfo",
  components: {
    searchBar,
  },
  data() {
    return {
      orderInfo: null,
      loading: false,
    };
  },
  mounted() {
    //this.handleSearch();
  },
  methods: {
    handleSearch(filters) {
      this.loading = true;
      console.log(filters);
      api.order.getInfo(filters).then((respone) => {
        this.loading = false;
        this.orderInfo = respone.result;
      });
    },
  },
};
</script>

<style lang="scss"  scoped>
p {
  padding: 0px;
  margin: 0px;
}
table {
  color: #606266;
  font-size: 14px;
  border-color: grey;
  border: 1px solid #ebeef5;
  border-collapse: collapse;
  border-spacing: 0;
  th {
    border: 1px solid #ebeef5;
    padding: 20px;
    width: 100px;
  }
  td {
    border: 1px solid #ebeef5;
    padding: 10px;
    text-align: center;
  }
}
</style>