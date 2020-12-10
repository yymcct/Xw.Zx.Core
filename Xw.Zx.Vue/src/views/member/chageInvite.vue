

<template>
  <section>
    <!--TODO:删减编辑界面数据-->
    <el-dialog
      title="更换推荐人"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <el-row>
        <el-col :span="24">姓名: {{member.realName}} </el-col>
        <el-col :span="24">电话: {{member.phone}} </el-col>
        <el-col :span="24">当前邀请人: {{member.inviteId}} </el-col>
        <el-col :span="24">当前邀请电话: {{member.realName}} </el-col>
      </el-row>
      <el-form :model="editForm" label-width="100px" ref="editForm">
        <el-row>
          <el-col :span="12"> </el-col>
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
  name: "changIndex",
  components: {},
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
        inviteId: 0,
      },
    };
  },
  methods: {
    init() {
      this.editForm.inviteId = 0;
      api.member.getMember(this.memberId).then((res) => {
        this.member = res.result;
      });
    },

    //提交
    editSubmit: function () {
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          this.$confirm("确认提交吗？", "提示", {}).then(() => {
            this.editLoading = true;
            api.member
              .changeInvite({
                memberId: this.member.id,
                inviteId: this.editForm.inviteId,
              })
              .then((res) => {
                this.$message({
                  message: "修改成功!",
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