
<template>
  <section>
    <!--TODO:删减编辑界面数据-->
    <el-dialog
      :title="editForm.id==0 ? '添加':'编辑'"
      :visible.sync="editFormVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <el-form :model="editForm" label-width="100px" :rules="editFormRules" ref="editForm">
        <el-row>
          <el-col :span="12">
            <el-tooltip class="item" effect="dark" content="待生成VIP码的数量" placement="top-start">
              <el-form-item label="VIP码数量" prop="cnt">
                <el-input-number v-model="editForm.cnt" :min="1" :max="999"></el-input-number>
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
import { api_postUpdateVipAuthCodeMDto } from "../../api/api";
import { type } from "os";

export default {
  name: "PostUpdateVipAuthCodeMDtoEdit",
  components: {},
  props: {
    action: String, //'none' 'add' 'edit'
    PostUpdateVipAuthCodeMDto: Object
  },
  watch: {
    action: {
      handler(val) {
        if (val == "none") {
          this.editFormVisible = false;
        } else if (val == "add") {
          this.initAdd();
          this.editFormVisible = true;
        } else if (val == "edit") {
          this.initEdit();
          this.editFormVisible = true;
        }
      }
    }
  },
  data() {
    return {
      editFormVisible: false,
      editLoading: false,
      editFormRules: {
        cnt: [{ required: true, message: "不可为空", trigger: "blur" }]
      },
      //TODO:删减编辑界面数据
      editForm: {
        cnt: 0
      }
    };
  },
  methods: {
    //显示编辑界面
    initEdit: function() {
      this.editForm.cnt = this.PostUpdateVipAuthCodeMDto.cnt;
      this.editFormVisible = true;
    },
    //显示新增界面
    initAdd: function() {
      this.editForm.cnt = 0;
    },
    //编辑
    editSubmit: function() {
      this.$refs.editForm.validate(valid => {
        if (valid) {
          this.$confirm("确认提交吗？", "提示", {}).then(() => {
            this.editLoading = true;
            api_postUpdateVipAuthCodeMDto(this.editForm).then(res => {
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
  mounted() {}
};
</script>

<style scoped>
</style>