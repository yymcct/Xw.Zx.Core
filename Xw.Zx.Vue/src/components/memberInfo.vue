

<template>
  <section>
    <el-dialog
      title="客户详情"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
      v-if="member"
    >
      <el-form label-width="80px">
        <el-row>
          <el-col :span="12">
            <el-form-item label="手机">
              <el-input v-model="member.phone" :disabled="true"></el-input>
            </el-form-item>
          </el-col>

          <el-col :span="12">
            <el-form-item label="姓名">
              <el-input v-model="member.realName" :disabled="true"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="上级手机">
              <el-input
                v-model="member.invitePhone"
                :disabled="true"
              ></el-input>
            </el-form-item>
          </el-col>

          <el-col :span="12">
            <el-form-item label="上级姓名">
              <el-input v-model="member.inviteName" :disabled="true"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="积分">
              <el-input
                v-model="member.memberIntegral"
                :disabled="true"
              ></el-input>
            </el-form-item>
          </el-col>          
        </el-row>
        <el-row>
          <el-col :span="22">
            <el-form-item label="级别">
              <radio-viptype v-model="member.memberVipType" :disabled="true" />
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
              <el-form-item label="客户编码">
                <el-input
                  v-model="member.businessCode"
                  placeholder="请填写客户编码"
                  :disabled="true"
                ></el-input>
              </el-form-item>
            </el-tooltip>
          </el-col>

          <el-col :span="12">
            <el-form-item label="身份证">
              <el-input
                v-model="member.identityCardNum"
                :disabled="true"
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="客户地址">
              <el-input v-model="member.address" :disabled="true"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="备注">
              <el-input
                v-model="member.remark"
                type="textarea"
                :disabled="true"
                :rows="3"
              ></el-input>
            </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancelSubmit">取消</el-button>
      </div>
    </el-dialog>
  </section>
</template>

<script>
import api from "@/api/app";
import radioViptype from "@/components/radioVipType";
export default {
  name: "memberInfo",
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
      member: null,
    };
  },
  methods: {
    init() {
      api.member.getMember(this.memberId).then((res) => {
        this.member = res.result;
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
