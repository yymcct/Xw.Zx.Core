

<template>
  <section>
    <el-dialog
      title="更换推荐人"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <el-row v-if="member" class="info">
        <el-col :span="24"
          ><p>姓名: {{ member.realName }}</p></el-col
        >
        <el-col :span="24"
          ><p>电话: {{ member.phone }}</p></el-col
        >
        <el-col :span="24"
          ><p>当前上级: {{ member.inviteName }}</p>
        </el-col>
        <el-col :span="24"
          ><p>当前上级电话: {{ member.invitePhone }}</p>
        </el-col>
      </el-row>
      <el-form :model="editForm" label-width="100px" ref="editForm">
        <el-row style="margin-top:10px;">
          <el-col :span="12">
            <select-member v-model="editForm.inviteId" v-if="dialogVisible" />
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
import selectMember from "./selectMember";
export default {
  name: "changIndex",
  components: { selectMember },
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
              .then(() => {
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
      this.member= null;
      this.dialogVisible = false;
      this.$emit("input", false);
    },
  },
  mounted() {},
};
</script>

<style lang="scss" scoped>
.info{
  p{
    margin: 5px 0;
  }
}
</style>
