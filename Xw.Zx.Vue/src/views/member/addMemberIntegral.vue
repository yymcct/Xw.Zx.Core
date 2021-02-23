

<template>
  <section>
    <el-dialog
      title="充值积分"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >

      <el-form :model="editForm" label-width="100px" ref="editForm">
        <el-row v-if="member">
          <el-col :span="12">
            <el-form-item label="客户电话">
              {{ member.phone }}
            </el-form-item>
          </el-col>
        </el-row>
        <el-row v-if="member">
          <el-col :span="12">
            <el-form-item label="客户姓名">
              {{ member.realName }}
            </el-form-item>
          </el-col>
        </el-row>
        <el-row v-if="member">
          <el-col :span="12">
            <el-form-item label="当前积分">
              {{ member.memberIntegral }}
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="充值积分">
              <el-input
                v-model="editForm.integral"
                :min="-9999999"
                :max="9999999"
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="备注">
              <el-input
                type="textarea"
                :rows="2"
                placeholder="请输入充值原因"
                v-model="editForm.remark"
              >
              </el-input>
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
import memberInfo from "@/components/memberInfo";
export default {
  name: "addMemberIntegral",
  components: {
    memberInfo,
  },
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
      member: null,
      editForm: {
        integral: 0,
        remark: "",
      },
      selectOptions: [],
    };
  },
  methods: {
    init() {
      this.editForm.integral = 0;
      this.editForm.remark = "";
      this.member = null;
      api.member.getMember(this.memberId).then((res) => {
        this.member = res.result;
      });
    },

    //提交
    editSubmit: function () {
      if (!this.editForm.remark) {
        this.$message({
          message: "请填写备注!",
          type: "error",
        });
        return;
      }
      //TODO 判断积分不能为负值

      this.$confirm("确认提交吗？", "提示", {}).then(() => {
        this.editLoading = true;
        api.memberIntegral
          .add(this.memberId, {
            Integral: this.editForm.integral,
            Remark: this.editForm.remark,
          })
          .then((res) => {
            this.$message({
              message: res.result,
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
