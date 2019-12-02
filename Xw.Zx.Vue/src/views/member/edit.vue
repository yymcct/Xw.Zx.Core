
<template>
  <section>
    <!--TODO:删减编辑界面数据-->
    <el-dialog
      :title="editForm.id==0 ? '添加':'编辑'"
      :visible.sync="editFormVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
    >
      <el-form :model="editForm" label-width="80px" :rules="editFormRules" ref="editForm">
        <el-row>
          <el-col :span="12">
            <el-tooltip class="item" effect="dark" content="手机号, 也是管理员的登陆账号" placement="top-start">
              <el-form-item label="手机" prop="phone">
                <el-input v-model="editForm.phone"></el-input>
              </el-form-item>
            </el-tooltip>
          </el-col>
          <el-col :span="12">
            <el-tooltip class="item" effect="dark" content="昵称" placement="top-start">
              <el-form-item label="昵称" prop="nick">
                <el-input v-model="editForm.nick"></el-input>
              </el-form-item>
            </el-tooltip>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-tooltip
              class="item"
              effect="dark"
              content="普通用户填:AppUser, 管理员填:Admin"
              placement="top-start"
            >
              <el-form-item label="角色" prop="roleName">
                <el-input v-model="editForm.roleName"></el-input>
              </el-form-item>
            </el-tooltip>
          </el-col>
          <el-col :span="12">
            <el-tooltip class="item" effect="dark" content="登陆密码" placement="top-start">
              <el-form-item label="密码" prop="password">
                <el-input v-model="editForm.password"></el-input>
              </el-form-item>
            </el-tooltip>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="12">
            <el-tooltip class="item" effect="dark" content="图像" placement="top-start">
              <el-form-item label="图像" prop="photo">
                <el-upload
                  class="avatar-uploader"
                  :action="glfileUploadUrl"
                  :show-file-list="false"
                  :on-success="handleImgUploaderSuccess"
                  :before-upload="glhandleBeforeImgUpload"
                >
                  <img v-if="editForm.photo" :src="editForm.photo" class="avatar" />
                  <i v-else class="el-icon-plus avatar-uploader-icon"></i>
                </el-upload>
              </el-form-item>
            </el-tooltip>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-tooltip class="item" effect="dark" content="生日" placement="top-start">
              <el-form-item label="生日" prop="birthDay">
                <el-date-picker
                  v-model="editForm.birthDay"
                  type="datetime"
                  placeholder="时间"
                  align="right"
                  :picker-options="glpickerOptions"
                ></el-date-picker>
              </el-form-item>
            </el-tooltip>
          </el-col>
        </el-row>

        <el-row>
          <el-col :span="24">
            <el-tooltip class="item" effect="dark" content="TODO" placement="top-start">
              <el-form-item label="备注">
                <el-input v-model="editForm.remark"></el-input>
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
import { api_postMemberMDto } from "../../api/api";
import { type } from "os";

export default {
  name: "PostMemberMDtoEdit",
  components: {},
  props: {
    action: String, //'none' 'add' 'edit'
    PostMemberMDto: Object
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
        id: [{ required: true, message: "不可为空", trigger: "blur" }],
        roleName: [{ required: true, message: "不可为空", trigger: "blur" }],
        password: [{ required: true, message: "不可为空", trigger: "blur" }],
        nick: [{ required: true, message: "不可为空", trigger: "blur" }],
        photo: [{ required: true, message: "不可为空", trigger: "blur" }],
        birthDay: [
          {
            required: true,
            message: "请选择日期",
            trigger: "change"
          }
        ],
        phone: [{ required: true, message: "不可为空", trigger: "blur" }],
        remark: [{ required: true, message: "不可为空", trigger: "blur" }]
      },
      //TODO:删减编辑界面数据
      editForm: {
        id: 0,
        roleName: "",
        password: "",
        nick: "",
        photo: "",
        birthDay: "",
        phone: "",
        remark: ""
      }
    };
  },
  methods: {
    handleImgUploaderSuccess(res, file) {
      if (res.statusCode === 200) {
        this.editForm.photo =
          process.env.VUE_APP_BASE_API + res.result.files[0].curPathName;
      }
    },
    //显示编辑界面
    initEdit: function() {
      this.editForm.id = this.PostMemberMDto.id;
      this.editForm.roleName = this.PostMemberMDto.roleName;
      this.editForm.password = this.PostMemberMDto.password;
      this.editForm.nick = this.PostMemberMDto.nick;
      if (this.PostMemberMDto.photo)
        this.editForm.photo =
          process.env.VUE_APP_BASE_API + this.PostMemberMDto.photo;
      this.editForm.birthDay = this.PostMemberMDto.birthDay;
      this.editForm.phone = this.PostMemberMDto.phone;
      this.editForm.remark = this.PostMemberMDto.remark;
      this.editFormVisible = true;
    },
    //显示新增界面
    initAdd: function() {
      this.editForm.id = 0;
      this.editForm.roleName = "";
      this.editForm.password = "";
      this.editForm.nick = "";
      this.editForm.photo = "";
      this.editForm.birthDay = "";
      this.editForm.phone = "";
      this.editForm.remark = "";
    },
    //编辑
    editSubmit: function() {
      this.editForm.photo = this.editForm.photo.replace(
        process.env.VUE_APP_BASE_API,
        ""
      );
      this.$refs.editForm.validate(valid => {
        if (valid) {
          this.$confirm("确认提交吗？", "提示", {}).then(() => {
            this.editLoading = true;
            api_postMemberMDto(this.editForm).then(res => {
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
.avatar-uploader .el-upload {
  border: 1px dashed #d9d9d9;
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
}
.avatar-uploader .el-upload:hover {
  border-color: #409eff;
}
.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 50px;
  height: 50px;
  line-height: 50px;
  text-align: center;
}
.avatar {
  width: 50px;
  height: 50px;
  display: block;
}
</style>