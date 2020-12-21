<template>
  <div class="off-form" v-loading="loading">
    <div class="basic-box" v-if="isShowJSXZ">
      <h2 class="pro-panel">申报项目信息</h2>
      <!-- <h2 class="pro-panel">基本信息</h2> -->
      <table border="0" class="pro-table" :class="type!='detail'?'':'detail-table'">
        <tr>
          <td class="td-title td-rule">申报者</td>
          <td>
            <el-select
              v-model="proForm.apply_type"
              placeholder="请选择"
              class="cus-select cus-rule apply_type"
              @blur="onBlur"
              :disabled="type=='detail'"
            >
              <el-option value="0" label="个人"></el-option>
              <el-option value="1" label="企业"></el-option>
              <el-option value="2" label="非企业"></el-option>
            </el-select>
          </td>
          <td class="td-title td-rule">申报者名称</td>
          <td>
            <!-- APPLYNAME -->
            <el-input
              v-if="type!='detail'"
              v-model="proForm.jsdwmc"
              class="cus-rule jsdwmc"
              placeholder="请输入"
              clearable
              @blur="onBlur"
            ></el-input>
            <span v-else>{{detailForm.APPLYNAME}}</span>
          </td>
        </tr>
        <tr>
          <td class="td-title td-rule">申报者证件类型</td>
          <td>
            <template v-if="type!='detail'">
              <el-select
                v-model="proForm.jsdwzjlx"
                placeholder="请选择建设单位证件类型"
                class="cus-select cus-rule jsdwzjlx"
                @visible-change="onChangeUnitCode"
                clearable
                @blur="onBlur"
              >
                <el-option
                  v-for="item in JSDWZJLX"
                  :key="item.ITEMVAL"
                  :label="item.ITEMNAME"
                  :value="item.ITEMVAL"
                ></el-option>
              </el-select>
            </template>
            <span v-else>{{detailForm.APPLY_CARDTYPE}}</span>
          </td>
          <td class="td-title td-rule">申报者证件号码</td>
          <td>
            <el-input
              v-if="type!='detail'"
              v-model="proForm.jsdwzjhm"
              placeholder="请输入"
              class="cus-rule jsdwzjhm"
              clearable
              @blur="onBlur"
            ></el-input>
            <span v-else>{{detailForm.APPLY_CARDNUMBER}}</span>
          </td>
        </tr>

        <tr>
          <td class="td-title td-rule">权力事项名称</td>
          <td>
            <el-select
              v-model="proForm.servicename"
              placeholder="请选择"
              class="cus-select cus-rule servicename"
              v-if="type!='detail'"
              clearable
              @blur="onBlur"
            >
              <el-option
                v-for="item in SXMC"
                :key="item.ITEMID"
                :label="item.ITEMNAME"
                :value="item.ITEMNAME"
              ></el-option>
            </el-select>
            <span v-else>{{detailForm.SERVICENAME}}</span>
          </td>
             <td class="td-title">申报来源</td>
          <td>
            <span>{{SBLYRela[detail.applyfrom]}}</span>
            <!-- <el-select v-model="proForm.applyfrom" placeholder="请选择" class="cus-select"  :disabled="type=='detail'">
              <el-option  value="0" label="工改窗口"></el-option>
              <el-option  value="1" label="综合窗口"></el-option>
              <el-option  value="2" label="内网录入"></el-option>
            </el-select>-->
          </td>
        
          <!-- <td class="td-title">权力事项编码</td>
            <td>
            <el-input v-if="type!='detail'" v-model="proForm.SERVICECODE" placeholder="无需填写自动生成" clearable disabled></el-input>
            <span v-else>{{detailForm.SERVICECODE}}</span>
          </td>-->
        </tr>
        <tr>
          <td class="td-title td-rule">项目名称</td>
          <td colspan="3">
            <!-- PROJECTNAME -->
            <el-input
              v-if="type!='detail'"
              v-model="proForm.xmmc"
              class="cus-rule xmmc"
              placeholder="请输入"
              clearable
              @blur="onBlur"
            ></el-input>
            <span v-else>{{detailForm.PROJECTNAME}}</span>
          </td>
        </tr>
        <tr v-if="detailForm.STATE=='1'">
         <td class="td-title">
           承诺期限(天)
          </td>
          <td>
            <!-- PROMISEVALUE -->
              <el-input
                v-if="type!='detail'"
                v-model="proForm.promisevalue"
                placeholder="请输入"
                clearable
              ></el-input>
              <span v-else>{{detailForm.promisevalue}}</span>
          </td>
           <td class="td-title">承诺办结时间</td>
           <!-- PROMISE_ETIME -->
            <td>
              <span v-if="type=='detail'">{{ detailForm.promisee_time }}</span>
           </td>
          <!-- <td class="td-title td-rule">审批类型</td>
          <td>
            <el-select
              v-model="proForm.approve_type"
              placeholder="请选择"
              class="cus-select cus-rule approve_type"
              clearable
              @blur="onBlur"
              :disabled="type=='detail'"
            >
              <el-option
                v-for="item in SPLX"
                :key="item.ITEMVAL"
                :label="item.ITEMNAME"
                :value="item.ITEMVAL"
              ></el-option>
            </el-select>
          </td> -->
        </tr>
        <!-- <tr> -->
          <!-- <td class="td-title">项目性质</td>
          <td>
            <el-radio-group v-model="proForm.apply_propertiy" v-if="type!='detail'"
              class="cus-select cus-rule APPLY_PROPERTIY"
              @change="onChangeRadio(proForm.apply_propertiy,'.apply_propertiy')">
              <template v-for="(item,idx) in XMLX">
                <el-radio :label="item.ITEMVAL" :key="idx">{{item.ITEMNAME}}</el-radio>
              </template>
            </el-radio-group>
            <el-select
              v-model="proForm.APPLY_PROPERTIY"
              placeholder
              v-else
              class="cus-select"
              :disabled="type=='detail'"
            >
              <el-option
                v-for="(item,idx) in XMLX"
                :key="idx"
                :label="item.ITEMNAME"
                :value="item.ITEMVAL"
              ></el-option>
            </el-select>
          </td> -->
         
        <!-- </tr> -->
        <tr>
          <td class="td-title td-rule">联系人/代理人姓名</td>
          <td>
            <el-input
              v-if="type!='detail'"
              class="cus-rule wtdlr"
              v-model="proForm.wtdlr"
              placeholder="如：张三"
              clearable
              @blur="onBlur"
            ></el-input>
            <span v-else>{{detailForm.wtdlr}}</span>
          </td>
          <td class="td-title td-rule">联系人手机号码</td>
          <td>
            <el-input
              v-if="type!='detail'"
              v-model="proForm.wtdlrdh"
              class="cus-rule wtdlrdh"
              type="number"
              placeholder
              @blur="onBlur"
            ></el-input>
            <span v-else>{{detailForm.wtdlrdh}}</span>
          </td>
        </tr>
        <tr>
          <td class="td-title">联系人/代理人证件类型</td>
          <td>
            <span>{{proForm.contactman_cardtype}}</span>
            <!-- <el-select
              v-model="proForm.contactman_cardtype"
              placeholder="请选择"
              class="cus-select"
              :disabled="type == 'detail'"
              clearable>
              <el-option  v-for="item in YXZJ"  :key="item.ITEMVAL"
                :label="item.ITEMNAME"  :value="item.ITEMVAL" ></el-option>
            </el-select> -->
          </td>
          <td class="td-title">联系人/代理人证件号码</td>
          <td>
            <el-input
              v-if="type!='detail'"
              v-model="proForm.wtrdlrzjh"
              class="cus-rule"
              placeholder="如：411123112222366789"
            ></el-input>
            <span v-else>{{detailForm.contactman_cardnumber}}</span>
          </td>
        </tr>
        <tr>
          <td class="td-title">通讯地址</td>
          <td>
            <el-input
              v-if="type!='detail'"
              class="cus-rule"
              type="text"
              autosize
              v-model="proForm.address"
              placeholder="请输入"
              clearable
            ></el-input>
            <span v-else>{{detailForm.address}}</span>
          </td>
          <td class="td-title">邮编</td>
          <td>
            <el-input v-if="type!='detail'" v-model="proForm.postcode" placeholder="请输入邮编"></el-input>
            <span v-else>{{detailForm.postcode}}</span>
          </td>
        </tr>
        <tr>
          <td class="td-title">备注</td>
          <td colspan="3" style="height:80px">
            <el-input
              v-if="type!='detail'"
              class="cus-rule"
              :rows="3"
              type="textarea"
              autosize
              v-model="proForm.MEMO"
              placeholder="请输入"
              clearable
            ></el-input>
            <span v-else>{{detailForm.MEMO}}</span>
          </td>
        </tr>
      </table>
    </div>
  </div>
</template>
<script>
import * as dataService from "@/public/apiService/ckcl/proDict.js";
import { addDate } from "@/public/utils.js";
import { setTimeout } from "timers";
import { getUserInfo } from "@/public/auth";
// import {typeList,BJLX} from './data.js'
export default {
  components: {},
  props: ["tabName", "type", "detail"],
  data() {
    return {
      // type: "edit",
      loading: false,
      fileList: [],
      JSDWZJLX: [], //申报单位证件类型
      // JSXZ: [], //建设性质
      // JSXZRela: {},
      LXLX: [], //立项类型
      XMLX: [], //项目类型
      BJLX: [], //办件类型
      SXMC: [], //事项名称
      YXZJ: [], //有效证件
      SBLY: [], //申报来源
      SPLX: [], //审批类型
       SBLYRela: { "0": "工改窗口", "1": "综合窗口", "2": "内网录入" },
      //  typeList:typeList,
      proForm: {
        wiid: "",
        cs: "", //是否发起 f发起
        xmdm: "", //项目代码
        xmmc: "", //项目名称
        jsdd: "", //建设地点
        xmlx: "政府投资", //项目类型
        lxlx: "审批", //立项类型
        bjlx: "", //办件类型
        sxmc: "", //事项名称
        sldw: "", //受理单位
        jsxz: "", //建设性质
        jjr: getUserInfo().ur_name, //接件人
        jjrid: getUserInfo().ur_ident, //接件人id
        jjrq: addDate(Date.now(), 0), //接件日期
        jsgm: "", //建设规模
        zyjsnrhjszb: "", //主要建设内容和技术指标
        jsdwmc: "", //申报单位名称
        shzxdm: "", //社会征信代码/组织机构
        jsdwzjlx: "", //申报单位证件类型
        jsdwzjhm: "", //申报单位证件号码
        frdb: "", //法人代表
        frlxdh: "", //法人联系电话
        wtdlr: "", //委托代理人
        wtrdlrzjh: "", //委托代理人证件号
        wtdlrdh: "", //委托代理人电话
        wtdlrdzyx: "", //委托代理人电子邮箱
        uid: getUserInfo().ur_ident,
        unm: getUserInfo().ur_name,
        zone: getUserInfo().ur_zone,
        data1: [],
        projid: "",
        projpwd: "", //必填 查询密码,由业务系统随机自动生成的数字，如：234765。
        is_manubrium: "", //不必填 0=非垂管事项1=使用中央垂管系统的事项2=使用省级垂管系统的事项3=使用市级垂管系统的事项4=使用县级垂管系统的事项。
        servicecode: "", //权力事项的事项编码,从权力事项库中获取。如：781217682XK10212001
        service_deptid: "", //该事项终审部门所对应的部门编码。该编码由河南政务服务网统一用户平台提供
        bus_mode: "", //该业务办理方式00=普通模式01=快递送达99=其他
        bus_mode_desc: "", //如果办理方式指为其他，可对该办理方式进行描述
        serviceversion: "", //权力事项的版本号
        rel_bus_id: "", //当业务为并联业务时候，该项必填，值为牵头事项办件的ID；当业务为多级联动是，值为下级办件唯一标识
        apply_type: "", //申请人类型，0-个人，1-企业，2-非企业
        contactman_cardtype: "", //提供的有效证件名称，包括身份证、组织机构代码证等详见7.3证件类型
        postcode: "", //申报者联系地址对应的邮政编码
        address: "", //申报者的联系地址
        deptid: "", //参与该业务办理第一环节的部门所属编码。该编码由河南政服务网统一用户平台提供
        ss_orgcode: "", //实施机构组织机构代码
        receive_useid: "", //创建用户唯一标识（窗口申报则为窗口工作人员系统id，网上申报则为网上申报注册用户id）
        receive_name: "", //创建用户名称（窗口申报则为窗口工作人员系统账户，网上申报则为网上申报注册用户账户）
        applyfrom: "", //标识办件的申报源头
        approve_type: "",
        belongto: "", //有注册项目的需要填写项目关联号（省发改委投资项目代码）
        areacode: "", //收件部门所属行政区划，如省级相关部门所属地区为： 410000，行政区划编码由省政务服务网。统一用户平台提供
        datastate: "", //标识办件是否为有效件，默认是有效。0=作废1=有效
        belongsystem: "", //用于区分不同业务系统报送的数据，标识由省级平台分配
        extend: "",
        create_time: "", //由各业务系统产生，时间格式：yyyy-mm-ddhh24:mi:ss
        sync_status: "", //插入：I，更新：U，删除：D，已同步：S
        dataversion: "", //默认值=1，如果有信息变更，则版本号递增
        memo: "" //备注
      },
      detailForm: null,
      isShowJSXZ: false
    };
  },
  created() {
    this.initData();
    this.getDictData();
  },
  mounted() {},
  computed: {},
  methods: {
    getDictData: function() {
      // this.BJLX = BJLX;
      // this.SXMC=typeList;
      this.loading = true;
      dataService.getBJLXDict().then(res => {
        this.BJLX = res.data; //办件类型
      });
      dataService.getSXMCDict().then(res => {
        this.SXMC = res.data; //事项名称
      });
      dataService.getProjectNature().then(res => {
        this.XMLX = res.data; //项目性质
      });
      dataService.getProRegistType().then(res => {
        this.LXLX = res.data; //项目立项类型
      });
       dataService.getLegalPerson().then(res => {
        this.JSDWZJLX = res.data; //建设单位证件类型/有效证件
      });
      dataService.getOwerPerson().then(res => {
        this.YXZJ = res.data; //个人有效证件
      });
      dataService.getCardDict().then(res => {
        this.JSDWZJLX = res.data; //申报单位证件类型/有效证件
        this.loading = false;
        this.isShowJSXZ = true;
      });
      dataService.getDeclareSource().then(res => {
        this.SBLY = res.data; //申报来源
      });
      dataService.getApprovalType().then(res => {
        this.SPLX = res.data; //审批类型
      });
    },
    onBlur: function(e, ele) {
      setTimeout(() => {
        var val = e.target ? e.target.value : e.value;
        var eleObj = e.target
          ? $(e.target)
          : $("." + ele).find(".el-input__inner");
        if (val == "") {
          eleObj.addClass("error");
          return;
        }
        eleObj.removeClass("error");
      }, 250);
    },
    onChangeUnitCode: function(val) {
      if (val) {
        return;
      }
      if (!this.proForm.lxrzjhm) {
        return;
      }
      this.$confirm("此操作将清除证件号码, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.$message({
            type: "success",
            message: "删除成功!"
          });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除"
          });
        });
    },
    onChangeCode: function() {
      if (!this.proForm.jsdwzjlx) {
        this.$message({
          message: "请先选择证件号码!",
          type: "warning"
        });
      }
    },
    onChangeRadio: function(val, id) {
      if (val) {
        $(id)
          .find(".el-radio__inner")
          .removeClass("error");
      }
    },
    initData: function() {
      this.detailForm = this.detail;
      let obj = this.proForm;
      var key;
      if (this.type !== "add") {
        for (key in obj) {
          obj[key] = this.detail[key] || obj[key];
        }
        //  this.proForm=obj;
      }
      // console.log(this.proForm,obj,'ssss22');
    },
    verificationForm: function() {
      var temp = Object.keys(this.proForm);
      var flag = [];
      temp.forEach(item => {
        var val = this.proForm[item];
        if (!val || (typeof val == "object" && val.length == 0)) {
          var targetEle;
          if (item == "jsgmjnr") {
            targetEle = $("." + item).find(".el-textarea__inner");
          } else if (item == "xmlx" || item == "lxlx") {
            targetEle = $("." + item).find(".el-radio__inner");
          } else {
            targetEle = $("." + item).find(".el-input__inner");
          }
          if (targetEle.length) {
            targetEle.addClass("error");
            flag.push(false);
          }
        }
      });
      if (flag.toString().indexOf(false) != -1) {
        this.$message.warning("信息填写不完整!");
        return false;
      } else {
        return true;
      }
    },
    submitForm: function() {
      if (this.detail.projid) {
        let flag = this.verificationForm();
        if (!flag) {
          return;
        }
      }
      // this.proForm.DATA = [];
      // this.fileList.length &&
      // this.proForm.DATA.push({ jzwjmc: this.fileList[0].jzwjmc });
      this.$emit("submitForm", this.proForm);
    }
  }
};
</script>

<style lang="scss" scoped>
.off-form {
  .basic-box {
    margin: 10px;
    border: 1px solid #e5e5e5;
    .pro-panel {
      background: #51a6dd;
      color: #fff;
      font-size: 16px;
      height: 30px;
      line-height: 30px;
      font-size: 14px;
      padding-left: 10px;
    }
    .cus-rule {
      /deep/ .el-textarea__inner {
        height: 100% !important;
      }
    }

    .pro-table {
      width: 100%;
      > tr {
        border-bottom: 1px solid #e5e5e5; // &:nth-child(odd){
        //   background:#f7f7f7
        // }
        .cus-select {
          width: 100%;
          height: 100%;
        }
        &:nth-last-child(1) {
          border: none;
        }
        .td-title {
          background: #f7f7f7;
          width: 165px;
          font-size: 12px;
          font-weight: bolder;
          vertical-align: middle;
          text-align: right;
        }
        .td-rule {
          &::before {
            content: "*";
            color: red;
          }
        }
        td {
          border-right: 1px solid #e5e5e5;
          padding: 3px 5px;
          height: auto;
          vertical-align: middle;
          /deep/ .error {
            border: 1px solid red !important;
          }
          /deep/ .el-input {
            height: 100%;
            > .el-input__inner {
              height: 34px;
              line-height: 34px;
              border: none;
              border-bottom: 1px solid #dcdfe6;
              background: transparent;
              font-size: 14px;
            }
            .el-input__icon {
              line-height: 30px;
            }
          }
          /deep/ .el-input.is-disabled .el-input__inner {
            background-color: #f5f7fa !important;
            border-color: #e4e7ed !important;
            color: #787878;
          }
          &:nth-last-child(1) {
            border: none;
          }
        }
      }
    }
    .detail-table {
      > tr {
        // border-bottom:1px solid #e5e5e5;
        &:nth-child(odd) {
          background: #f7f7f7;
        }
        .td-title {
          font-size: 12px;
          background: transparent !important;
          vertical-align: middle;
          padding: 8px;
        }
        > td {
          height: 30px; // line-height: 30px;
          &:nth-of-type(even) {
            width: 15%;
          }
          &:nth-of-type(even) {
            width: 35%;
          }
          /deep/ .el-input.is-disabled {
            .el-input__inner {
              border: none;
              background-color: transparent !important;
              border-color: transparent !important;
              cursor: auto;
              padding-left: 0px;
              color: #333;
              margin-top: 3px;
            }
            .el-input__suffix {
              display: none;
            }
          }
          > span {
            display: inline-block;
            width: 100%;
          }
        }
      }
    }
  }
  /deep/ .el-radio {
    line-height: 34px;
    height: 34px;
  }
  .jsgmjnr {
    .el-textarea__inner {
      height: 80px !important;
    }
  }
  .arrow_box {
    animation: glow 800ms ease-out infinite alternate;
  }
  @keyframes glow {
    0% {
      border-color: #f44336;
      box-shadow: 0 0 5px rgba(255, 106, 0, 0.2),
        inset 0 0 5px rgba(245, 19, 19, 0.1), 0 1px 0 #e00202;
    }
    100% {
      border-color: #f44336;
      box-shadow: 0 0 20px rgba(255, 106, 0, 0.6),
        inset 0 0 10px rgba(245, 19, 19, 0.4), 0 1px 0 #e00202;
    }
  }
}
</style>
