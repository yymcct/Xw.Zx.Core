<template>
  <section>
    <!--TODO:删减编辑界面数据-->
    <el-dialog
      :title="member.phone +'升级会员'"
      :visible.sync="editFormVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <el-row :span="24">
        <el-tag type="danger" class="tag">重要提示: 提交后立即产生收益单,客户可立即提现, 请核实电话,确认收款后操作</el-tag>
      </el-row>
      <el-form :model="postUpdateVipMDto" label-width="80px" :rules="editFormRules" ref="editForm">
        <el-row>
          <el-col :span="24">
            <el-tooltip class="item" effect="dark" content="选择升级的类型" placement="top-start">
              <el-form-item label="级别" prop="memberVipType">
                <el-radio-group
                  v-model="postUpdateVipMDto.memberVipType"
                  @change="handleCheckedChange"
                >
                  <!-- <el-checkbox-button label="0" :disabled="true">普通</el-checkbox-button>
                  <el-checkbox-button label="1" :disabled="member.memberVipType > 0">VIP会员</el-checkbox-button>
                  <el-checkbox-button label="2" :disabled="member.memberVipType > 1">创客</el-checkbox-button>
                  <el-checkbox-button label="3" :disabled="member.memberVipType > 2">服务站</el-checkbox-button>
                  <el-checkbox-button label="4">运营商</el-checkbox-button>-->
                  <el-radio-button
                    v-for="iteam in vipTypeDrop"
                    :label="iteam.value"
                    :disabled="iteam.value <= member.memberVipType"
                    :key="iteam.value"
                  >{{iteam.name}}</el-radio-button>
                </el-radio-group>
              </el-form-item>
            </el-tooltip>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="24">
            <el-tooltip class="item" effect="dark" content="金额" placement="top-start">
              <el-form-item label="金额" prop="amount">
                <el-input-number
                  v-model="postUpdateVipMDto.amount"   
                  :min="0"
                  :max="200000"
                  label="描述文字"
                ></el-input-number>
              </el-form-item>
            </el-tooltip>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click.native="editFormVisible = false">取消</el-button>
        <el-button type="primary" @click.native="editSubmit">提交</el-button>
      </div>
    </el-dialog>
  </section>
</template>

<script>
import { api_PostUpdateVip } from "../../api/api";
export default {
  name: "UpdateVip",
  props: {
    action: String, //'none' 'add' 'edit'
    member: Object
  },
  data() {
    return {
      vipTypeDrop: [
        { name: "普通", value: 0 },
        { name: "VIP会员", value: 1 },
        { name: "创客", value: 2 },
        { name: "服务站", value: 3 },
        { name: "运营商", value: 4 }
      ],
      editFormVisible: false,
      postUpdateVipMDto: {
        memberid: 0,
        memberVipType: 0,
        amount: 0
      },
      editFormRules: {
        memberVipType: [
          { required: true, message: "不可为空", trigger: "blur" }
        ],
        amount: [{ required: true, message: "不可为空", trigger: "blur" }]
      }
    };
  },

  components: {},

  computed: {},

  beforeMount() {},

  mounted() {},

  methods: {
    handleCheckedChange(value) {
      switch (value) {
        case 1:
          this.postUpdateVipMDto.amount = 198;
          break;
        case 2:
          this.postUpdateVipMDto.amount = 2000;
          break;
        case 3:
          this.postUpdateVipMDto.amount = 20000;
          break;
        case 4:
          this.postUpdateVipMDto.amount = 200000;
          break;
      }
      console.log(value);
    },
    //编辑
    editSubmit: function() {
      this.$refs.editForm.validate(valid => {
        if (valid) {
          this.$confirm("确认提交吗？", "提示", {}).then(() => {
            this.editLoading = true;
            api_PostUpdateVip(this.postUpdateVipMDto).then(res => {
              this.editLoading = false;
              //NProgress.done();
              this.$message({
                message: "提交成功",
                type: "success"
              });
              this.$refs["editForm"].resetFields();
              this.editFormVisible = false;
              this.$emit("change", "sumbit");
            });
          });
        }
      });
    },
    cancelSubmit: function() {
      this.editFormVisible = false;
      this.$emit("change", "cancel");
    }
  },

  watch: {
    action: {
      handler(val) {
        if (val == "none") {
          this.editFormVisible = false;
        } else {
          this.editFormVisible = true;
          this.postUpdateVipMDto.memberid = this.member.id;
          this.postUpdateVipMDto.memberVipType = this.member.memberVipType + 1;
          this.handleCheckedChange(this.postUpdateVipMDto.memberVipType);
        }
      }
    }
  }
};
</script>
<style scoped>
.tag{
  width: 100%;
  margin-bottom: 30px;
  font-weight: bold;
  font-size: 18px;
}
</style>