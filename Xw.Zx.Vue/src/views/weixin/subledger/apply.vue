

<template>
  <section>
    <el-dialog
      title="提交分账"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <p>可分账金额: {{ amount }} * 30%={{ amount * 0.3 }}</p>
      <p>实际分账金额:{{ realyAmount }}</p>
      <el-form
        :model="editForm"
        label-width="80px"
        :rules="editFormRules"
        ref="editForm"
      >
        <el-row>
          <el-col :span="12">
            <el-radio
              v-for="(item,index)in subLedgerReceivers"
              v-model="radio"
              label="item.id"
              :key="index"
              >备选项</el-radio
            >
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="分账金额" prop="transactionID">
              <el-input v-model="editForm.transactionID"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
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
    out_order_no: Number,
    amount: Number,
  },
  watch: {
    value: {
      handler(val) {
        this.dialogVisible = val;
        if (this.out_order_no) {
          this.initApply();
        }
      },
    },
  },
  computed: {
    realyAmount() {
      let amount = 0;

      if (subLedgerReceivers.length == 0) {
        return amount;
      }

      this.subLedgerReceivers.map((i) => {
        amount += i.amount;
      });

      return amount;
    },
  },
  data() {
    return {
      dialogVisible: false,
      editLoading: false,
      editFormRules: {
        transactionID: [
          { required: true, message: "不可为空", trigger: "blur" },
        ],
      },
      subLedgerReceivers: [],
    };
  },
  methods: {
    initApply() {
      api.weixinSubLedger.getWechatSubLedgerReceivers().then((res) => {
        this.subLedgerReceivers = [];
        res.result.map((i) => {
          i.amount = 0;
          this.subLedgerReceivers.push(i);
        });
      });
    },

    //提交
    editSubmit: function () {
      const handlePostSucess = () => {
        this.editLoading = false;
        this.$message({
          message: "提交成功",
          type: "success",
        });

        this.$refs["editForm"].resetFields();
        this.dialogVisible = false;
        this.$emit("input", false);
        this.$emit("change");
      };
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          this.$confirm("确认提交吗？", "提示", {}).then(() => {
            this.editLoading = true;
            if (this.id) {
              api.wechatOrders
                .put(this.id, this.editForm)
                .then((res) => {
                  handlePostSucess();
                })
                .catch(() => {
                  this.editLoading = false;
                });
            } else {
              api.wechatOrders
                .post(this.editForm)
                .then((res) => {
                  handlePostSucess();
                })
                .catch(() => {
                  this.editLoading = false;
                });
            }
          });
        }
      });
    },
    cancelSubmit: function () {
      this.dialogVisible = false;
      this.$emit("input", false);
    },
  },
  mounted() {},
};
</script>

<style scoped>
</style>