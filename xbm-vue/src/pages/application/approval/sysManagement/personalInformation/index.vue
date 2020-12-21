<template>
  <div class="personalInformation" v-loading="loading">
    <h3 class="title">个人信息</h3>
    <el-form
      :model="informationForm"
      ref="information"
      label-width="100px"
      class="information"
    >
      <el-row :gutter="10">
        <el-col :span="12">
          <el-form-item label="用户编号：" prop="UR_IDENT">
            <el-input
              v-model="informationForm.UR_IDENT"
              placeholder="请输入编号"
              disabled
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="用户名称："
            prop="UR_NAME"
            :rules="[
              { required: true, message: '用户名称不能为空', trigger: 'blur' }
            ]"
          >
            <el-input
              v-model="informationForm.UR_NAME"
              placeholder="请输入用户名称"
              disabled
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="办公室电话：" prop="UR_TAKE">
            <el-input
              v-model="informationForm.UR_TAKE"
              placeholder="请输入办公室电话"
              @blur="checkPhoneInput"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="手机号码：" prop="UR_KAPE">
            <el-input
              v-model="informationForm.UR_KAPE"
              placeholder="请输入手机号码"
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="部门名称："
            prop="UR_ZONE"
            :rules="[
              { required: true, message: '部门名称不能为空', trigger: 'blur' }
            ]"
          >
            <el-cascader
              style="width: 100%;"
              @change="handleChange"
              :options="options"
              v-model="haha"
              :props="props"
            ></el-cascader>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item
            label="登录名称："
            prop="UR_LOGIN"
            :rules="[
              { required: true, message: '登录名称不能为空', trigger: 'blur' }
            ]"
          >
            <el-input
              v-model="informationForm.UR_LOGIN"
              placeholder="请输入登录名称"
              disabled
            ></el-input>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="注册时间：" prop="UR_TIME">
            <el-date-picker
              style="width: 100%;"
              v-model="informationForm.UR_TIME"
              disabled
              type="datetime"
              placeholder="选择日期时间"
            ></el-date-picker>
          </el-form-item>
        </el-col>
        <el-col :span="12">
          <el-form-item label="注销时间：" prop="UR_MOVE">
            <el-date-picker
              style="width: 100%;"
              v-model="informationForm.UR_MOVE"
              disabled
              format="yyyy-MM-dd HH:mm:ss"
              type="datetime"
              value-format="yyyy-MM-dd HH:mm:ss"
              placeholder="选择日期时间"
            ></el-date-picker>
          </el-form-item>
        </el-col>
        <el-col :span="24">
          <el-form-item label="备注信息：" prop="UR_REMARK">
            <el-input
              v-model="informationForm.UR_REMARK"
              placeholder="请输入备注信息"
            ></el-input>
          </el-form-item>
        </el-col>
      </el-row>
      <el-row style="text-align: center;">
        <el-button type="primary" @click="submitEdit" plain>提交修改</el-button>
        <el-button type="success" plain @click="password">修改密码</el-button>
      </el-row>
    </el-form>
    <el-dialog
      title="修改密码"
      :visible.sync="dialogFormVisible"
      label-width="100px"
      append-to-body
      :close-on-click-modal="false"
    >
      <el-form
        :model="ruleForm2"
        status-icon
        :rules="rules2"
        ref="ruleForm2"
        label-width="100px"
        class="demo-ruleForm"
      >
        <el-form-item label="原始密码" prop="oldPass">
          <el-input
            type="password"
            v-model="ruleForm2.oldPass"
            clearable
            autocomplete="off"
            :disabled="dis"
          ></el-input>
        </el-form-item>
        <el-form-item label="密码" prop="pass">
          <el-input
            type="password"
            v-model="ruleForm2.pass"
            clearable
            autocomplete="off"
            :disabled="dis"
          ></el-input>
        </el-form-item>
        <el-form-item label="确认密码" prop="checkPass">
          <el-input
            type="password"
            v-model="ruleForm2.checkPass"
            clearable
            autocomplete="off"
            :disabled="dis"
          ></el-input>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogFormVisible = false">取 消</el-button>
        <el-button type="primary" @click="submitPass">确 定</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import flexContainer from "@/components/FlexContainer";
import { getToken, removeToken } from "@/public/auth";
import * as dataService from "@/public/apiService/PersonalAffairs/personalInformation";
export default {
  name: "filingCabinet",
  data() {
    var validatePass = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("请输入密码"));
      } else {
        if (this.ruleForm2.checkPass !== "") {
          this.$refs.ruleForm2.validateField("checkPass");
        }
        callback();
      }
    };
    var validatePass2 = (rule, value, callback) => {
      if (value === "") {
        callback(new Error("请再次输入密码"));
      } else if (value !== this.ruleForm2.pass) {
        callback(new Error("两次输入密码不一致!"));
      } else {
        callback();
      }
    };
    return {
      informationForm: {
        UR_IDENT: ""
      },
      userInfo: JSON.parse(localStorage.getItem("data")),
      dialogFormVisible: false,
      ur_login: "",
      ruleForm2: {
        pass: "",
        checkPass: "",
        oldPass: ""
      },
      rules2: {
        oldPass: [
          {
            required: true,
            message: "请输入原始密码",
            trigger: "blur"
          }
        ],
        pass: [
          {
            validator: validatePass,
            trigger: "blur"
          }
        ],
        checkPass: [
          {
            validator: validatePass2,
            trigger: "blur"
          }
        ]
      },
      options: [],
      props: {
        label: "or_name",
        value: "or_code",
        expandTrigger: "hover"
      },
      haha: [],
      nodeName: "",
      loading: false,
      dis: true,
      userForm: {
        ur_zone: "",
        ur_node: ""
      },
      gly: {}
    };
  },
  created() {
    this.xgpassword();
  },
  mounted() {
    this.getData(); //获取信息
    //获取部门信息
    this.$nextTick(function() {
      this.ruleForm2 = {
        pass: "",
        checkPass: "",
        oldPass: ""
      };
      this.dis = false;
    });
  },
  methods: {
    checkPhoneInput: function(e) {
      // var phone = /^\d{7,8}$/;
      var phone = /^((\d{4})?(\-)?\d{7,8}|\d{3}\-\d{6}|(\d{3}\-\d{7}-\d{3}))$/;
      if (!phone.test(e.target.value)) {
        this.$message({
          message: "电话号码格式有误，请重填",
          type: "warning"
        });
        return false;
      }
    },
    handleChange(data) {
      for (var i = 0; i < this.options.length; i++) {
        //console.log(i)
        if (data.length == 1 && data == this.options[i].or_code) {
          console.log(data[0]);
          this.userForm.ur_node = data[0];
          this.userForm.ur_zone = this.options[i].or_name;
        } else if (data.length == 2) {
          this.userForm.ur_node = data[1];
          this.options.map(item => {
            if (item.children) {
              item.children.map(item => {
                if (data[1] == item.or_code) {
                  this.userForm.ur_zone = item.or_name;
                }
              });
            }
          });
        }
      }
      console.log(this.userForm.ur_node, this.userForm.ur_zone);
    },

    getData() {
      var params = {
        ur_ident: this.userInfo.ur_ident
      };
      this.loading = true;
      dataService
        .getData(params)
        .then(res => {
          console.log(res);
          this.loading = false;
          this.informationForm = res.data[0];
          this.ur_login = res.data[0].UR_LOGIN;
          console.log(res.data[0].UR_NODE); //"410301001"
          //当为管理员时
          var glyData = {};
          var data = [];
          if (res.data[0].UR_NODE == "1") {
            this.gly = {
              or_code: res.data[0].UR_NODE,
              or_name: res.data[0].UR_NAME
            };
            data.push(this.gly);
          }
          var shu = [];
          shu.push(res.data[0].UR_NODE);
          this.haha = shu;
          var _this = this;
          dataService
            .getDepartment()
            .then(re => {
              re.map(item => {
                if (item.children.length == 0) {
                  data.push({ or_code: item.or_code, or_name: item.or_name });
                } else {
                  data.push(item);
                }
              });

              this.options = data;

              // for (var i = 0; i < r e.length; i++) {
              // 	//console.log(i)
              // 	if (id == re[i].or_code) {
              // 		this.haha.push(id)
              // 	} else {
              // 		for (var a = 0; a < re[i].children.length; a++) {
              // 			//console.log(a)
              // 			if (id == re[i].children[a].or_code) {
              // 				console.log(i, a)
              // 				this.haha.push(re[i].or_code)
              // 				this.haha.push(id)
              // 			}
              // 		}
              // 	}
              // }
            })
            .catch(err => {
              console.log(err);
            });
        })
        .catch(err => {
          console.log(err);
        });
    },
    password() {
      this.dialogFormVisible = true;
    },
    xgpassword() {
      if (this.$store.state.approvalMenu.password) {
        this.password();
      }
    },
    submitPass() {
      var that = this;
      this.$refs["ruleForm2"].validate(valid => {
        var params = {
          pass: this.ruleForm2.oldPass,
          ur_login: this.ur_login,
          npass: this.ruleForm2.pass
        };

        if (valid) {
          dataService
            .getChangeWord(params)
            .then(res => {
              console.log(res);

              if (res.success) {
                this.$message({
                  message: "密码修改成功，需要重新登录！",
                  type: "success"
                });
                that.loginOut();
              } else {
                this.$message({
                  message: res.msg,
                  type: "warning"
                });
              }
            })
            .catch(err => {
              console.log(err);
            });
        } else {
          console.log("error submit!!");
          return false;
        }
      });
    },
    submitEdit() {
      var params = {
        ur_ident: this.informationForm.UR_IDENT,
        ur_login: this.informationForm.UR_LOGIN,
        ur_name: this.informationForm.UR_NAME,
        ur_node: this.userForm.ur_node,
        ur_zone: this.userForm.ur_zone,
        ur_take: this.informationForm.UR_TAKE,
        ur_kape: this.informationForm.UR_KAPE,
        ur_time: this.informationForm.UR_TIME,
        ur_remark: this.informationForm.UR_REMARK,
        ur_move: this.informationForm.UR_MOVE
      };
      var reg = /^(0|86|17951)?(13[0-9]|15[012356789]|17[01678]|18[0-9]|14[57])[0-9]{8}$/;
      var phone = /^((\d{4})?(\-)?\d{7,8}|\d{3}\-\d{6}|(\d{3}\-\d{7}-\d{3}))$/;
      if (params.ur_take !== null) {
        if (!phone.test(params.ur_take)) {
          this.$message({
            message: "电话号码格式有误，请重填",
            type: "warning"
          });
          return false;
        }
      }

      if (params.ur_kape !== null) {
        if (!reg.test(params.ur_kape)) {
          this.$message({
            message: "手机号码格式有误，请重填",
            type: "warning"
          });

          return false;
        }
      }

      console.log(params);
      dataService
        .getDataEdit(params)
        .then(res => {
          this.$message({
            message: "个人信息成功",
            type: "success"
          });
          this.getData();
        })
        .catch(err => {
          console.log(err);
        });
    },
    hehe(id) {
      for (var i = 0; i < this.options.length; i++) {
        //console.log(i)
        if (id == this.options[i].or_code) {
          this.nodeName = this.options[i].or_name;
        } else {
          for (var a = 0; a < this.options[i].children.length; a++) {
            //console.log(a)
            if (id == this.options[i].children[a].or_code) {
              console.log(i, a);
              this.nodeName = this.options[i].children[a].or_name;
            }
          }
        }
      }

      return this.nodeName;
    },
    loginOut() {
      localStorage.clear();
      sessionStorage.clear();
      removeToken();
      this.$store.commit("SET_TOKEN", "");
      this.$router.push("/login");
    }
  },
  components: {
    "v-flex-container": flexContainer
  }
};
</script>

<style lang="scss">
.personalInformation {
  width: 100%;
  margin: 0 auto;
  padding: 0 10px;

  h3 {
    padding: 10px 20px;
  }
  .el-cascader-panel {
    .el-cascader-menu {
      overflow: hidden;
      .el-scrollbar__wrap {
        overflow: hidden;
      }
    }
  }
}
</style>
