

<template>
  <section>
    <el-dialog
      title="发放优惠券"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <el-form :model="editForm" label-width="100px" ref="editForm">
        <el-row>
          <el-col :span="24">
            <el-form-item label="优惠券">
              <el-select v-model="editForm.couponid" placeholder="请选择">
                <el-option
                  v-for="item in selectOptions"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id"
                >
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="发放数量">
              <el-input v-model="editForm.count" :min="1" :max="999"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancelSubmit">取消</el-button>
        <el-button
          type="primary"
          @click="editSubmit"
          :disabled="editForm.inviteId == 0"
          >提交</el-button
        >
      </div>
    </el-dialog>
  </section>
</template>

<script>
import api from "@/api/app";
export default {
  name: "giveCoupon",
  components: {  },
  props: {
    value: Boolean,
    memberId: Number,
  },
  watch: {
    value: {
      handler(val) {
        this.dialogVisible = val;
        if (this.memberId > 0 && val) {
          this.init();
        }
      },
    },
  },
  data() {
    return {
      dialogVisible: false,
      editLoading: false,
      editForm: {
        couponid: 1,
        count: 8,
      },
      selectOptions: [],
    };
  },
  methods: {
    init() {
      this.editForm.couponid = 1;
      api.coupon.getCouponList().then((res) => {
        this.selectOptions = res.result;
      });
    },

    //提交
    editSubmit: function () {
      if (!this.editForm.couponid) {
        this.$message({
          message: "请选择优惠券!",
          type: "error",
        });
        return;
      }

      this.$confirm("确认提交吗？", "提示", {}).then(() => {
        this.editLoading = true;
        api.coupon
          .giveCoupon({
            Memberid: this.memberId,
            Couponid: this.editForm.couponid,
            Count: this.editForm.count,
          })
          .then(() => {
            this.$message({
              message: "发放成功!",
              type: "success",
            });
            this.editLoading = false;

            this.$refs["editForm"].resetFields();
            this.dialogVisible = false;
            this.$emit("input", false);
            this.$emit("change");
          })
          .catch(() => {
            this.editLoading = false;
          });
      });
    },
    cancelSubmit: function () {
      this.member = null;
      this.dialogVisible = false;
      this.$emit("input", false);
    },
  },
  mounted() {},
};
</script>

<style lang="scss" scoped>
.info {
  p {
    margin: 5px 0;
  }
}
</style>
