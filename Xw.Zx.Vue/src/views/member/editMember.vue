

<template>
  <section>
    <el-dialog
      title="编辑"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <el-form
        :model="editForm"
        label-width="80px"
        ref="editForm"
        :rules="editFormRules"
      >
        <el-row>
          <el-col :span="12">
            <el-form-item label="手机" prop="phone">
              <el-input v-model="editForm.phone" :disabled="true"></el-input>
            </el-form-item>
          </el-col>

          <el-col :span="12">
            <el-tooltip
              class="item"
              effect="dark"
              content="请填写身份证姓名"
              placement="top-start"
            >
              <el-form-item label="姓名" prop="realName">
                <el-input v-model="editForm.realName"></el-input>
              </el-form-item>
            </el-tooltip>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="22">
            <el-form-item label="级别" prop="memberVipType">
              <radio-viptype v-model="editForm.memberVipType" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-tooltip
              class="item"
              effect="dark"
              content="客户编码, 由运营中心编码"
              placement="top-start"
            >
              <el-form-item label="客户编码" prop="businessCode">
                <el-input
                  v-model="editForm.businessCode"
                  placeholder="请填写客户编码"
                ></el-input>
              </el-form-item>
            </el-tooltip>
          </el-col>

          <el-col :span="12">
            <el-form-item label="身份证" prop="identityCardNum">
              <el-input
                v-model="editForm.identityCardNum"
                placeholder="请填写身份证编码"
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="客户地址" prop="address">
              <el-input
                v-model="editForm.address"
                placeholder="请填写客户地址"
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="备注">
              <el-input
                v-model="editForm.remark"
                type="textarea"
                placeholder="请填写备注信息"
                :rows="3"
              ></el-input>
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
import radioViptype from "@/components/radioVipType";
export default {
  name: "changIndex",
  components: { radioViptype },
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
        phone: "",
        realName: "",
        businessCode: "",
        identityCardNum: "",
        memberVipType: 0,
        remark: "",
        address:"",
      },
      editFormRules: {
        realName: [{ required: true, message: "不可为空", trigger: "blur" }],
      },
    };
  },
  methods: {
    init() {
      this.editForm.phone = "";
      this.editForm.realName = "";
      this.editForm.businessCode = "";
      this.editForm.identityCardNum = "";
      this.editForm.remark = "";
      this.editForm.address="";
      this.editForm.memberVipType = 0;
      api.member.getMember(this.memberId).then((res) => {
        this.member = res.result;
        this.editForm.phone = res.result.phone;
        this.editForm.realName = res.result.realName;
        this.editForm.businessCode = res.result.businessCode;
        this.editForm.identityCardNum = res.result.identityCardNum;
        this.editForm.remark = res.result.remark;
        this.editForm.memberVipType = res.result.memberVipType;
        this.editForm.address= res.result.address;
      });
    },

    //提交
    editSubmit: function () {
      this.$refs.editForm.validate((valid) => {
        if (valid) {
          this.$confirm("确认提交吗？", "提示", {}).then(() => {
            this.editLoading = true;
            api.member
              .put(this.member.id, this.editForm)
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
