

<template>
  <section>
    <el-dialog
      title="分账详情"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <div v-if="subLedger">
        <p>商户订单: {{ subLedger.out_Order_No }}</p>
        <p>微信订单: {{ subLedger.transactionID }}</p>
        <p>订单金额: {{ subLedger.amount }}</p>
        <p>可分账金额: {{ subLedger.subCharge }}</p>
        <p>订单状态: {{ subLedger.payDescription }}</p>
        <p>分账状态: {{ subLedger.subState }}</p>
        <el-table
          :data="subLedger.receivers"
          highlight-current-row
          style="width: 100%"
        >
          <el-table-column
            prop="return_OrderID"
            label="分账单号"
          ></el-table-column>
          <el-table-column
            prop="subName"
            label="分账人"
            width="100px"
          ></el-table-column>

          <el-table-column
            prop="subAmount"
            label="分账金额"
            width="100px"
          ></el-table-column>

          <el-table-column
            prop="subTime"
            label="分账时间"
            width="100px"
          ></el-table-column>

          <el-table-column prop="subState" label="分账状态" width="100px">
          </el-table-column>
        </el-table>
      </div>

      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">取消</el-button>
        <!-- <el-button type="primary" @click="editSubmit">提交</el-button> -->
      </div>
    </el-dialog>
  </section>
</template>

<script>
import api from "@/api/app";

export default {
  name: "QueryPay",
  components: {},
  props: {
    value: Boolean,
    out_order_no: {
      type: String,
      default: "",
    },
  },
  watch: {
    value: {
      handler(val) {
        this.dialogVisible = val;
        if (this.out_order_no && val) {
          this.initQueryPay();
        }
      },
    },
  },
  computed: {},
  data() {
    return {
      dialogVisible: false,
      editLoading: false,
      subLedger: null,
    };
  },
  methods: {
    initQueryPay() {
      this.subLedger = null;
      api.weixinSubLedger
        .querySubLedgerResult({
          out_order_no: this.out_order_no,
        })
        .then((res) => {
          this.subLedger = res.result;
        });
    },

    cancelSubmit: function () {
      this.$emit("input", false);
      this.$emit("change");
      console.log(22222222);
    },
  },
  mounted() {},
};
</script>

<style scoped lang="scss">
.red {
  color: red;
  font-size: 18px;
}
.green {
  color: greenyellow;
}
</style>