

<template>
  <section>
    <el-dialog
      title="提交分账"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <p>可分账金额: {{ amount }} * 30%={{ amount * 0.3 }}</p>
      <p>
        实际分账金额:
        <span
          :class="{
            green: realyAmount <= amount * 0.3,
            red: realyAmount > amount * 0.3,
          }"
          >{{ realyAmount }}</span
        >
      </p>
      <el-form label-width="80px" ref="editForm">
        <el-checkbox-group v-model="checkList">
          <el-row v-for="(item, index) in subLedgerReceivers" :key="index">
            <el-col :span="12">
              <el-checkbox :label="item.id">
                <el-form-item :label="item.name" prop="transactionID">
                  <el-input-number
                    v-model="item.amount"
                    :precision="2"
                    :step="0.01"
                    :min="0"
                  ></el-input-number>
                </el-form-item>
              </el-checkbox>
            </el-col>
            <!-- <el-col :span="12">
              <el-form-item label="分账金额" prop="transactionID">
                <el-input v-model="item.amount"></el-input>
              </el-form-item>
            </el-col> -->
          </el-row>
        </el-checkbox-group>
        <el-row> </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancelSubmit">取消</el-button>
        <el-button type="primary" @click="editSubmit">提交</el-button>
      </div>
    </el-dialog>
  </section>
</template>

<script>
import api from "@/api/app";

export default {
  name: "WechatOrdersEdit",
  components: {},
  props: {
    value: Boolean,
    out_order_no: {
      type: String,
      default: "SH20200915215509564442",
    },
    amount: {
      type: Number,
      default: 9.9,
    },
  },
  watch: {
    value: {
      handler(val) {
        this.dialogVisible = val;
        if (this.out_order_no && val) {
          console.log("initApply");
          this.initApply();
        }
      },
    },
  },
  computed: {
    realyAmount() {
      let amount = 0.0;

      if (this.subLedgerReceivers.length == 0) {
        return amount;
      }

      this.subLedgerReceivers.map((i) => {
        amount += Number(i.amount);
      });

      return amount.toFixed(2);
    },
  },
  data() {
    return {
      dialogVisible: false,
      editLoading: false,

      subLedgerReceivers: [],
      checkList: [],
    };
  },
  methods: {
    initApply() {
      api.weixinSubLedger.getWechatSubLedgerReceivers().then((res) => {
        this.subLedgerReceivers = [];
        res.result.map((i) => {
          i.amount = 0;
          i.radio = 0;
          this.subLedgerReceivers.push(i);
        });
      });
    },

    //提交
    editSubmit: function () {
      if (this.realyAmount > this.amount * 0.3) {
        this.$message({
          message: "可分账最大金额为" + this.amount * 0.3,
          type: "error",
        });
        return;
      }
      if (this.realyAmount == 0) {
        this.$message({
          message: "分账金额不能为0",
          type: "error",
        });
        return;
      }

      this.$confirm("确认提交吗？", "提示", {}).then(() => {
        this.editLoading = true;

        let dto = {
          out_order_no: this.out_order_no,
          SubLedgerListInfo: [],
        };
        this.subLedgerReceivers.map((i) => {
          if (i.amount > 0) {
            dto.SubLedgerListInfo.push({
              account: i.account,
              amount: Math.trunc(i.amount * 100),
            });
          }
        });

        api.weixinSubLedger.dealWithSubLedger(dto).then((res) => {
          this.$message({
            message: res.msg,
            type: "error",
          });
        });
        this.dialogVisible = false;
        this.$emit("input", false);
        this.$emit("change");
      });
    },
    cancelSubmit: function () {
      this.dialogVisible = false;
      this.$emit("input", false);
    },
  },
  mounted() {
    this.initApply();
  },
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