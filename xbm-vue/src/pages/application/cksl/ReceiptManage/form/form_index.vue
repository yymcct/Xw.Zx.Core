<template>
  <div class="CreatPro">
    <div class="CreatPro-inner">
      <h2 class="CreatPro-top">
        <!-- 发起协调 -->
        <template v-if="this.detail.projid&&detail.STATE=='0'">
          <el-button  type="primary"
            icon="el-icon-s-claim" round
            size="mini" :disabled="userName=='马玉霞'?false:true"
            @click="HandleApproval"
            :loading="BCloading">受理通过</el-button>
          <el-button type="primary" :disabled="userName=='马玉霞'?false:true" icon="el-icon-upload2" round size="mini" @click="onSubmitGiveBack">补齐补正</el-button>
        </template>
        <el-button type="primary" icon="el-icon-back" round size="mini" @click="close">返回</el-button>
      </h2>
      <el-tabs v-model="activeName" type="card">
        <el-tab-pane label="接件基本信息" name="info">
          <vGForm
            ref="form"
            :type="type"
            :detail="curData"
            :tabName="tabName"
            @submitForm="submitForm"></vGForm>
          <!-- <vForm ref="form" :type="type" :detail="curData" :tabName="tabName" @submitForm="submitForm" v-else></vForm>  -->
        </el-tab-pane>
        <el-tab-pane label="材料清单" name="file">
            <vGFile
              ref="file"
              :type="type" :attachList="attachList"
              :tabName="tabName"
              :detail="curData"
              v-if="activeName=='file'"></vGFile>
            <!-- <vFile ref="file" :attachList="attachList" :type="type" :tabName="tabName" :detail="curData" v-else></vFile> -->
        </el-tab-pane>
        <!--  <template v-if="tabName=='受理'">
              <el-tab-pane label="流转意见" name="3" v-if="parseInt(detail.xmzt)>2">
                <Issue :id="detail.wiid" v-if="activeName=='3'"></Issue>
              </el-tab-pane>
              <el-tab-pane label="办理过程" name="4" v-if="parseInt(detail.xmzt)>2">
                  <iframe :src="flowerUrl" border="0" width="100%" height="100%"></iframe>
              </el-tab-pane>
        </template>-->
        <!-- <el-tab-pane label="意见附件" name="5">
          <AttachSheets v-if="activeName=='5'"></AttachSheets>
        </el-tab-pane> -->
      </el-tabs>
    </div>
    <el-dialog
      :title="dialogTitle"
      :visible.sync="stDialog"
      width="600px"
      :close-on-click-modal="false"
      append-to-body
      v-dialogDrag
    >
      <iframe id="iframe" :src="url" border="0" width="100%" height="500px"></iframe>
    </el-dialog>
    <el-dialog
      title="填写补件原因"
      :visible.sync="GiveBackDialog"
      width="600px"
      height="400px"
      custom-class="proCenterDialog"
      :close-on-click-modal="false"
      append-to-body
      v-dialogDrag
    >
      <template v-if="GiveBackDialog">
        <el-form
          :model="BackForm"
          label-width="100px"
          ref="BackForm"
          class="demo-form-inline"
          style="padding:10px 5px">
          <el-form-item
            label="补件原因："
            prop="backReason"
            :rules="[{ required: true, message: '请输入补件原因', trigger: 'change' }]">
            <el-input type="textarea" v-model="BackForm.backReason" :rows="4"></el-input>
          </el-form-item>
        </el-form>
        <span
          slot="footer"
          class="dialog-footer"
          style="text-align:center;display:inline-block;width:100%">
          <el-button type="primary" @click="onSubmitGiveBack" :loading="subLoading">确定</el-button>
          <el-button @click="GiveBackDialog=false" size="medium">取消</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>
<script>
import * as dataService from "@/public/apiService/ckcl/jointCheck.js";
import vGForm from "./GForm";
import GFileList from "./GFileList";
// import AttachSheets from "./AttachSheets";
import { getUserInfo,getToken } from "@/public/auth";
import { apiUrl } from "@/public/apiUrl";
import { getNowTime } from "@/public/utils";
import Qs from "qs";
export default {
  components: {
    // "vFile": FileList,
    vGFile: GFileList,
    // "vForm": form,
    vGForm,
    // Issue,
    // AttachSheets
    // idiom
  },
  props: ["detail", "type", "tabName"],
  data() {
    return {
      activeCollapse: ["1", "2"],
      activeName: "info",
      dialogVisible: false,
      approveForm: {
        scyj: "",
        ycontent: "",
        fileList: []
      },
      current: "",
      curData: {},
      stDialog: false,
      TYloading: false,
      BCloading: false,
      cs: "",
      attachList: [],
      fileList: [],
      url: "",
      dialogTitle: "项目发起",
      GiveBackDialog: false,
      BackForm: {
        backReason: ""
      },
      subLoading: false,
      userName:getUserInfo().ur_name
    };
  },
  created() {
    this.curData = this.detail;
  },
  mounted() {
    // if (this.detail.projid) {
      this.getGfileList();
    // }
  },
  computed: {},
  methods: {
    getGfileList: function() {
      this.attachList = [];
      this.$http
        .get(apiUrl.Get_fileList + "?PROJID=" + this.detail.projid)
        .then(res => {
          let arr = [];
          arr = res.data.data;
          arr &&
            arr.forEach(item => {
              item.FX_CLASS = item.ATTRID;
              item.FX_NAME = item.ATTRNAME;
              item.file = [{ SR_NAME: item.FILENAME, AC_IDENT: item.FILEURL }];
            });
          this.attachList = this.uniqForBasic(arr);
        });
    },
    uniqForBasic: function(array) {
      var songs = array;
      let result = {};
      let finalResult = [];
      for (let i = 0; i < songs.length; i++) {
        result[songs[i].FX_NAME] = songs[i];
      }
      for (var item in result) {
        finalResult.push(result[item]);
      }
      return finalResult;
    },
    // getAttachSheet: function() {
    //   this.attachList = [];
    //   let temp = [];
    //   var that = this;
    //   dataService.getJiontFileList(this.detail.isContainPlan).then(res => {
    //     //  console.log(res,'全部清单');
    //     if (this.type !== "add") {
    //       dataService.CheckJiontFileList(this.detail.wiid).then(res1 => {
    //         // console.log(res1,'res===');
    //         res.data.forEach(ele => {
    //           if (res1.length !== 0) {
    //             res1.forEach(item => {
    //               if (ele.FX_CLASS == parseInt(item.AC_REMARK)) {
    //                 ele.file = item.children;
    //               } else {
    //                 ele.file = ele.file || [];
    //               }
    //             });
    //           } else {
    //             ele.file = [];
    //           }
    //         });
    //         this.attachList = res.data;
    //         // console.log(this.attachList,'===');
    //       });
    //     } else {
    //       res.data.forEach(item => {
    //         item.file = [];
    //       });
    //       this.attachList = res.data;
    //     }
    //   });
    // },
    closeForm: function() {
      this.$emit("close");
    },
    HandleApproval: function() {
      this.BCloading = true;
      let data=this.detail;
      let params = {
        PROJID: data.PROJID,
        ACCEPT_MAN: getUserInfo().ur_name, //受理人员
        HANDER_DEPTNAME: getUserInfo().ur_zone, //受理部门
        HANDER_DEPTID: getUserInfo().ur_node, //受理人员所属部门编码
        AREACODE: "", //受理人员所属部门的所在行政区划编码
        ACCEPT_TIME: data.ACCEPT_TIME || "", //受理时间
        PROMISEVALUE: data.PROMISEVALUE || "", //承诺期限
        PROMISETYPE: data.PROMISETYPE || "", //工作日、工作小时
        PROMISE_ETIME: "", //日期格式yyyy-mm-dd hh:mm:ss
        BELONGSYSTEM: data.BELONGSYSTEM || "",
        DATAVERSION: data.DATAVERSION || "",
        SYNC_STATUS: data.SYNC_STATUS || "",
        CREATE_TIME: data.CREATE_TIME,
        UNM: getUserInfo().ur_name, //受理人员
        UID: getUserInfo().ur_ident, //受理人员id
        token:getToken()
        // flowUnid:data.FLOW_UNID//就是标识这是一起推送过来的
      };
      this.$http({
        headers: {
          "Content-Type": "application/x-www-form-urlencoded"
        },
        url: apiUrl.Approval_Acceptance,
        method: "post",
        data: Qs.stringify(params)
      }).then(response => {
        console.log(response,'response==');
        this.BCloading = false;
        // this.$emit("close");
        // this.$emit("onrefres");
        if (response.data.resultmsg == "受理成功") {
          this.BCloading = false;
          // this.$emit("close");
          // this.$emit("onrefres");
          this.$message.success("操作成功!");
          this.$router.push("/cksl/DoneBusiness");
        } else {
          this.$message.error("操作失败!");
          this.BCloading = false;
        }
      });
    },
    // giveBack: function() {
    //    this.activeName='file';
    //   this.$message.warning('请选择材料!');
    //   // this.GiveBackDialog = true;
    //   // this.$emit('giveBack','2',temp.wiid);
    // },
    onSubmitGiveBack: function() {
      if(this.activeName!='file'){
         this.activeName='file';
        this.$message.warning('请选择材料!');
        return
      }
      //  this.activeName='file';
      let arr=[];
      var fileTemp=this.$refs.file.multipleSelection;
      fileTemp.forEach(item=>{
        arr.push(item.UNID)
      })
       if(!fileTemp.length){
        this.$message.warning('请选择材料!');
        return
       }
      var data = this.detail;
      // this.$refs["BackForm"].validate(valid => {
      //   if (valid) {
          let params = {
            UNID: data.UNID || "",
            PROJID: data.PROJID,
            // PATCH_DATE: getNowTime(), //补件日期
            PATCH_REASON: this.BackForm.backReason, //补件原因
            PATCH_TIME_LIMIT: 1, //补件时限
            PATCH_TIME_LIMIT_UNIT: "", //补件时限
            HANDLE_USERUNID: getUserInfo().ur_ident, //办理人标识
            HANDLE_USERNAME: getUserInfo().ur_name, //办理人姓名
            HANDER_DEPTNAME: getUserInfo().ur_zone, //办理人姓名
            HANDER_DEPTID: getUserInfo().ur_node, //受理人员所属部门编码
            AREACODE: data.AREACODE || "", //受理人员所属部门的所在行政区划编码
            MEMO: data.MEMO || "",
            // CREATE_TIME: getNowTime(),
            SYNC_STATUS: data.SYNC_STATUS || "",
            BELONGSYSTEM: data.BELONGSYSTEM || "",
            EXTEND: "",
            DATAVERSION: data.DATAVERSION || "",
            PATCHATTR:arr.toString()
          };
        // console.log(params,'params');
          this.subLoading = true;
          this.$http({
            headers: {
              "Content-Type": "application/x-www-form-urlencoded"
            },
            url: apiUrl.TurnBack_Acceptance,
            method: "post",
            data: Qs.stringify(params)
          }).then(response => {
            this.subLoading = false;
            this.$message.success("操作成功!");
            this.$router.push('/cksl/BackBusiness');
          });
        // } else {
        //   console.log("error submit!!");
        //   return false;
        // }
      // });
    },
    submitStart: function(n) {
      this.$refs["approveForm"].validate(valid => {
        if (valid) {
          //       if(!this.detail){
          //    let flag=this.$refs.vTab1.verificationForm();
          //    if(flag==false){
          //        return
          //    }
          // }
          this.cs = "";
          if (n != undefined) {
            this.approveForm.zt = n;
          }
          this.$refs.form.submitForm();
        } else {
          console.log("error submit!!");
          return false;
        }
      });
    },
    submitForm: function(params) {
      params.DATA1 = [];
      if (this.$refs.file) {
        let temp = this.$refs.file.cusFileList || [];
        temp.forEach(item => {
          if (item.file.length !== 0) {
            item.file.forEach(ele => {
              let ac_remark = item.FX_CLASS;
              params.DATA1.push({
                ac_name: ele.AC_IDENT,
                ac_remark: item.FX_CLASS
              });
            });
          }
        });
      } else {
        params.DATA1 = [];
      }
      params.cs = this.cs;
      params.ycontent = this.approveForm.ycontent;
      if (this.approveForm.zt != undefined) {
        this.approveForm.zt == 1
          ? (this.BTYloading = true)
          : (this.TYloading = true);
      } else {
        this.BCloading = true;
      }
      var serverMethodName =
        this.detail.isContainPlan == "20601"
          ? "SaveNoPlanJiontData"
          : "SavePlanJiontData";
      dataService[serverMethodName](params)
        .then(res => {
          if (res.success) {
            if (this.cs == "f") {
              this.TYloading = false;
              this.dialogTitle = "发起协调";
              this.url =
                "/dghy/XBM_Service.bsp?EXEC&Source=WF_Transfer('" +
                res.WIID +
                "',1)";
              this.stDialog = true;
              this.ListenMsgEvent(res.WIID, this.detail.xmmc);
              return;
            }
            if (this.approveForm.zt != undefined) {
              this.approveForm.zt == 1
                ? (this.BTYloading = false)
                : (this.TYloading = false);
              this.dialogTitle = "流程";
              this.stDialog = true;
              this.url =
                "/dghy/XBM_Service.bsp?EXEC&Source=WF_Transfer('" +
                this.detail.wiid +
                "'," +
                this.detail.wa_ident +
                ")";
              this.ListenMsgEvent1(res.WIID, this.detail.xmmc);
              this.dialogVisible = false;
              return;
            }
            // if (this.isGongGai) {
            //   this.HandleApproval(this.detail);
            //   return;
            // }
            this.BCloading = false;
            this.$emit("close");
            this.$emit("onrefres");
          }
        })
        .catch(err => {
          this.cs == "f" ? (this.TYloading = false) : (this.BCloading = false);
          console.log(err, "err==");
        });
    },
    approve: function() {
      //    if (e) {
      //     e.stopPropagation();
      //   }
      // this.proForm=param;
      //   if(!this.detail.YJ.length){
      //          this.approveForm.ycontent='';
      //       }else{
      //    this.approveForm.ycontent=this.detail.YJ[0].yjnr||'';
      //       }
      // this.isGongGai=false;
      this.dialogVisible = true;
    },
  
    
    //关闭发起弹窗页面
    close: function() {
      this.stDialog = false;
      this.closeForm();
    },
    ListenMsgEvent: function(WIID, XMBC) {
      console.log(WIID, XMBC);
      let _this = this;
      window.addEventListener("message", function(e) {
        if (e.data == "'" + WIID + "'") {
          if (e.currentTarget.location.hash.indexOf("pend") != -1) {
            return;
          }
          let curHash = e.currentTarget.location.hash;
          if (
            curHash.indexOf("reserve") != -1 ||
            curHash.indexOf("CreatPro") != -1
          ) {
            addPushMsg(WIID, XMBC, 4).then(res => {
              _this.$emit("close");
              _this.$emit("onrefres");
            });
          }
          return;
        }
      });
    },
    ListenMsgEvent1: function(WIID, XMBC) {
      let _this = this;
      window.addEventListener("message", function(e) {
        if (e.data == "'" + WIID + "'") {
          let curHash = e.currentTarget.location.hash;
          if (
            curHash.indexOf("reserve") != -1 ||
            curHash.indexOf("CreatPro") != -1
          ) {
            return;
          }
          if (
            curHash.indexOf("DutyPend") != -1 ||
            curHash.indexOf("pend") != -1
          ) {
            addPushMsg(WIID, XMBC, 4).then(res => {
              _this.$emit("close");
              _this.$emit("onrefres");
            });
          }
          return;
        }
      });
    }
  }
};
</script>

<style lang="scss" scoped>
/deep/ .proCenterDialog{
    .el-dialog__footer .el-button{
    float:initial!important
  }
}

.CreatPro {
  position: relative;
  height: calc(100% + 5px);
  background: #fff;

  .left-close {
    position: absolute;
    background: #f97f94;
    color: #fff;
    /* padding: 10px 2px; */
    left: -17px;
    border-top-left-radius: 10px;
    border-bottom-left-radius: 10px;
    cursor: pointer;
    display: inline-block;
    width: 17px;
    height: 40px;
    text-align: center;
    line-height: 40px;
    cursor: pointer;
    > i {
      font-weight: bolder;
      font-size: 14px;
    }
  }

  .CreatPro-top {
    position: absolute;
    z-index: 3;
    right: 20px;
    // left: 150px;
    top: 0px;
    // transition: all 1s ease-in;
    .cre-close {
      display: inline-block;
      width: 20px;
      height: 20px;
      line-height: 20px;
      border-radius: 10px;
      border: 1px solid #30aee7;
      float: right;
      font-size: 18px;
      text-align: center;
      margin-top: 5px;
      color: #30aee7;
      cursor: pointer;
      transition: all 1s ease;
      &:hover {
        -webkit-transform: scale(1.1);
        -moz-transform: scale(1.1);
        -ms-transform: scale(1.1);
        transform: scale(1.1);
      }
    }
  }
  .flex-left {
    overflow: auto;
  }
  //  .pro-content {
  // overflow: auto;
  >>> .el-tabs--card {
    height: 100%;
    .el-tabs__header {
      margin-bottom: 0px;
    }
    .el-tabs__content {
      height: calc(100% - 46px);
      overflow: auto;
      /deep/ .el-tab-pane {
        height: calc(100% - 10px);
        .demo-form-inline {
          width: calc(100% - 60px);
          padding: 30px;
          height: calc(100% - 58px);
          background: rgb(249, 249, 249);
          /deep/ .el-form-item {
            width: 600px;
            /deep/ .el-form-item__error {
              left: 100px;
              bottom: 20px;
              top: initial;
            }
          }
        }
      }
    }
  }
  // margin: -10px 0px;
  // }
  .CreatPro-inner {
    height: 100%;
    // padding:10px;
    position: relative;
    /deep/ .el-tabs--border-card {
      height: 100%;
      /deep/ .el-tabs__header {
        margin: 0 0 10px;
      }
      /deep/ .el-tabs__content {
        height: calc(100% - 30px);
        /deep/ .el-tab-pane {
          height: 100%;
        }
      }
    }

    .pro-right {
      height: 100%;
      .create-right-top {
        // min-width:330px;
        height: calc(100% - 5px);
        font-size: 14px;
        .cre-bg-title {
          background: #30aee6;
          color: #fff;
          padding: 10px;
        }
        .cre-title {
          height: 40px;
          line-height: 40px;
          // background:#d7ecff;
          font-size: 16px;
          padding-left: 10px;
          background-color: #f5f7fa;
          border-bottom: 1px solid #e4e7ed;
          .cre-btn {
            font-weight: bolder;
            color: #007584;
          }
        }
      }
      .cre-top-box {
        height: 100%;
        overflow: auto;
        padding: 0px 10px;
        /deep/ .el-tabs {
          height: 100%;
          /deep/ .el-tabs__header {
            margin: 0 0 10px;
          }
          /deep/ .el-tabs__content {
            height: calc(100% - 60px);
            /deep/ .el-tab-pane {
              height: 100%;
            }
            .cus-collapse {
              padding: 0px 10px;
              border: 1px solid #ebeef5;
              /deep/ .el-collapse-item {
                .redLineIcon {
                  display: inline-block;
                  width: 18px;
                  height: 18px;
                  margin: 0px 5px;
                  background: url("~@/assets/images/file-tree.png");
                }
                > div:first-child {
                  /deep/ .el-collapse-item__header {
                    height: 32px;
                    line-height: 32px;
                    margin: 0px -10px 10px -10px;
                    cursor: pointer;
                    font-size: 14px;
                    box-shadow: inset 0 1px 13px rgba(0, 0, 0, 0.1);
                    color: #263288;
                  }
                }
                /deep/ .el-collapse-item__arrow {
                  float: right;
                }
                .disabledHXBtn {
                  .el-upload {
                    display: none;
                  }
                  .el-upload-list__item-status-label,
                  .el-icon-close,
                  .el-icon-close-tip {
                    display: none;
                  }
                }
              }
            }
          }
        }
        .cus-tree-text {
          display: inline-block;
          font-size: 12px;
          .cus-fileType {
            position: relative;
            display: inline-block;
            height: 20px;
          }
          .tree-upload {
            position: absolute;
            font-size: 12px;
            padding: 0px;
            height: 23px;
            width: 70px;
            top: 4px;
          }
          .tree-file {
            position: absolute;
            opacity: 0;
            top: 4px;
            width: 70px;
          }
          .cus-tree-icon {
            display: inline-block;
            width: 18px;
            height: 18px;
            margin-right: 5px;
            // margin-top: 5px;
            vertical-align: middle;
          }
          .icon-fileType {
            background: url("~@/assets/images/ic_bar.png");
            background-position: 24px 860px;
          }
          .icon-file {
            background: url("~@/assets/images/ic_bar.png");
            background-position: 260px 1180px;
          }
        }
      }
    }
  }
}
</style>
