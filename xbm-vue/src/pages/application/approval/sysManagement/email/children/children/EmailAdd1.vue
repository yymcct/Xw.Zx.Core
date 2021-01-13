<template>
  <div class="email-add">
    <div class="mg-10" style="margin: 10px; min-width: 838px;">
      <table class="em-table-top" width="100%">
        <tbody>
          <tr>
            <td class="em-table-title" v-if="replyData==null||isDraft==true">写邮件</td>
            <td class="em-table-title" v-if="isReply==true&&isForWard==false">回复邮件</td>
            <td class="em-table-title" v-if="isReply==true&&isForWard==true">转发邮件</td>
          </tr>
        </tbody>
      </table>
      <form
        enctype="multipart/form-data"
        action
        method="post"
        id="form1"
        name="form1"
        onsubmit="return CheckForm();"
      >
        <table class="em-form-table" width="100%" align="center">
          <tbody>
            <tr>
              <td nowrap class="em-consignee" width="100">收件人：</td>
              <td class="em-person-select">
                <div class="td-contact-choose-wrapper tags">
                  <el-tag
                    :key="tag.id"
                    v-for="tag in selPersonTags"
                    :closable="isReply&&!isForWard?false:true"
                    :disable-transitions="false"
                    @close="handleClose(tag)"
                    class="em-tags"
                  >{{tag.name}}</el-tag>
                  <span v-if="!isReply||isForWard">
                    <el-button type="primary" @click="selectPerson" size="small">添加</el-button>
                    <el-button
                      type="text"
                      size="small"
                      class="em-clear-text"
                      @click="resetPerson"
                    >清空</el-button>
                  </span>
                </div>
              </td>
            </tr>
          </tbody>
          <tbody>
            <tr>
              <td nowrap class="em-consignee">邮件主题</td>
              <td class="em-person-select">
                <el-input v-model="FS_BT" placeholder="请输入内容"></el-input>
              </td>
            </tr>
            <tr>
              <td nowrap class="em-consignee" style="position:relative"><span class="fjtext">附件</span></td>
              <td class="em-person-select">
              	<el-upload style="width:70%"
              class="upload-demo"
              action="/dghy/XBM_Service.bsp?FILE"
               :on-remove="handleRemove"
              :file-list="fileList"
              :on-preview="onPreview"
              :http-request="customRequst"> 
               <el-button size="mini" type="primary">上传</el-button>
            </el-upload>
              </td>
            </tr>
            <tr>
              <td valign="top" nowrap class="em-consignee">
                正文
              </td>
              <td class="em-person-select">
                 <editor class="editor-box" @ready="editorReady" :ueditorConfig="ueditorConfig" :defaultMsg="NR"></editor>
              </td>
            </tr>
            <tr align="center" class="TableControl" v-if="isReply&&!isForWard">
              <td colspan="2" nowrap style="padding:10px;">
                <el-button
                  type="primary"
                  value="回复"
                  class="btn css_1007 submit-btn"
                  title="回复邮件"
                  :loading="subLoading"
                  @click="SubmitReply"
                >
                  <label v-if="!subLoading">发送</label>
                  <label v-else>发送中</label>
                </el-button>
              </td>
            </tr>
            <tr align="center" class="TableControl" v-else>
              <td colspan="2" nowrap>
                <div class="handle-btn">
                  <el-button
                    type="primary"
                    value="立即发送"
                    class="btn css_1007 submit-btn"
                    title="立即发送此邮件"
                    :loading="subLoading"
                    @click="SubmitForm(1)"
                  >
                    <label v-if="!subLoading">立即发送</label>
                    <label v-else>发送中</label>
                  </el-button>
                  <el-button
                    type="primary"
                    value="保存到草稿箱"
                    class="btn css_1007 submit-btn"
                    title="保存到草稿箱"
                    :loading="draftLoading"
                    v-if="!isForWard"
                    @click="SubmitForm(0)"
                  >保存到草稿箱</el-button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </form>
      <el-dialog
        title="选择人员"
        :close-on-click-modal="false"
        :visible.sync="addDialogShow"
        width="500px"
        v-dialogDrag
        append-to-body
      >
        <selPerson @closeDialog="closeDialog" ref="people" :selPerson="selPersonTags"></selPerson>
      </el-dialog>
    </div>
  </div>
</template>
<script>
import {
  subAddEmail,
  subDraftEmail,
  subReplyEmail
} from "@/public/apiService/PersonalAffairs/email";
import editor from "@/components/Ueditor.vue";
import selPerson from "./EmailPersonSel.vue";
export default {
  name: "email-add",
  props: {
    isDraft: {
      type: Boolean,
      default: false
    },
    isReply: {
      type: Boolean,
      default: false
    },
    isForWard: {
      type: Boolean,
      default: false
    },
    replyData: {
      type: Object,
      default: null
    }
  },
  data() {
    return {
      addDialogShow: false,
      selPersonTags: [],
      FS_BT: "", //发送标题
      NR: "",
      subLoading: false,
      draftLoading: false,
      fileList:[],
      userInfo: JSON.parse(localStorage.getItem("data")),
      ueditorConfig:{ toolbars: [['source','undo','redo','link', //超链接
          'unlink', '|',//取消链接
          'forecolor', //字体颜色
          'backcolor', //背景色
          'fontfamily', //字体
         'fontsize', '|',//字号        
         'bold', //加粗
         'italic', //斜体
         'underline', //下划线
         'strikethrough','|', //删除线
         'formatmatch', //格式刷
         'pasteplain',
         'removeformat','|',  //清除格式
        'insertorderedlist', //有序列表
        'insertunorderedlist', '|', //无序列表
        'inserttable', //插入表格
         'paragraph', //段落格式
         'simpleupload', //单图上传
         'imagecenter', //居中
         'attachment',  '|',//附件
         'justifyleft', //居左对齐
        'justifycenter', //居中对齐
         'horizontal','|', //分隔线
         'blockquote', //引用
         'preview', //预览
         'fullscreen']], //全屏
         }
    };
  },
  created() {
    if (this.isReply || this.isDraft) {
      this.getReplyPerson();
    }
    //是转发的时候清空已选择人员
    if (this.isForWard) {
      this.selPersonTags = [];
    }
  },
  computed:{
    FJLIST:function(){
      let arr=[];
      this.fileList.forEach(item=>{
        arr.push({fjcode:item.Code})
      })
      return arr
    }
  },
  mounted() {
  },
  watch: {
    replyData: {
      handler(newVal, oldVal) {
        console.log(newVal);
        console.log(oldVal);
      },
      deep: true
    }
  },
  methods: {
    //获取编辑器内容
    // getUEContent() {
    //   let content = this.$refs.ue.getUEContent();
    //   this.$notify({
    //     title: "获取成功，可在控制台查看！",
    //     message: content,
    //     type: "success"
    //   });
    // },
    // onEditorChange: function(html) {
    //   console.log(html, "html===");
    //   this.NR = html;
    // },
     editorReady (instance) {
      this.$nextTick(()=>{
        // this.isReply||
       if (this.NR) {
          instance.setContent(this.NR);
        }
      })
      instance.addListener('contentChange', () => {
        this.NR = instance.getContent();
      });
      },
    //选择人员弹框
    selectPerson: function() {
      this.addDialogShow = true;
    },
    //清空人员
    resetPerson: function() {
      this.$confirm("此操作将清空选择的人员, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.selPersonTags = [];
          this.$message({
            type: "success",
            message: "删除成功!"
          });
        })
        .catch(() => {});
    },
    //关闭弹框
    closeDialog: function(val) {
      this.selPersonTags = val;
      this.addDialogShow = false;
    },
    //删除人员
    handleClose: function(tag) {
      this.selPersonTags.splice(this.selPersonTags.indexOf(tag), 1);
    },
    SubmitForm: function(type) {
      let selArr = [];
      this.selPersonTags.forEach(function(item) {
        selArr.push({ jsr: item.name, jsridl: item.id });
      });
      var NR = this.NR;
      if (selArr.length == 0) {
        this.$message({
          message: "收件人不能为空,请添加收件人!",
          type: "warning"
        });
        return;
      }
      if (this.FS_BT == "") {
        this.$message({
          message: "邮件主题不能为空!",
          type: "warning"
        });
        return;
      }
      // if (NR == "") {
      //   this.$message({
      //     message: "邮件内容不能为空!",
      //     type: "warning"
      //   });
      //   return;
      // }
      type == 1 ? (this.subLoading = true) : (this.draftLoading = true);
      var obj = {
        fs_id: this.userInfo.ur_ident,
        fs_stzte: type,
        fs_bt: this.FS_BT,
        fsr: this.userInfo.ur_name,
        nr: NR,
        DATA: selArr,
        FJLIST:this.FJLIST
      };
      let that = this;
      var tips = "发送";
      if (!type) {
        tips = "保存到草稿箱";
      }
      function resoveRes(data) {
        //转换data的数据获取rel的值
        if (data.rel == "1") {
          that.$message({
            message: tips + "成功!",
            type: "success"
          });
        } else {
          that.$message.error(tips + "失败!");
        }
        type == 1 ? (that.subLoading = false) : (that.draftLoading = false);
        if (type == 1) {
          that.$store.commit("curSideName", "outBox");
          return;
        }
        that.$store.commit("curSideName", "draft");
      }
      //从草稿箱提交
      if (this.isDraft) {
        obj.wiid = this.replyData.MXID;
        tips = "保存";
        subDraftEmail(obj).then(res => {
          resoveRes(res);
        });
        return;
      }
      //  提交
      subAddEmail(obj).then(res => {
        resoveRes(res);
      });
    },
    //如果是回复邮件，发件人自动赋值
    getReplyPerson: function() {
      this.selPersonTags = [];
      let obj = this.replyData;
      this.FS_BT = obj.MX_THEME;
      this.NR = obj.MX_CONTENT;
      obj.FILE&&obj.FILE.forEach(item=>{
        item.Code=item.AC_NAME;
        item.Addr='/jz/XBM_Service.bsp?GetDoc&Source='+item.AC_NAME;
        item.name=item.SR_NAME;
      })
      this.fileList=obj.FILE;
     
      if (this.isReply) {
        this.selPersonTags.push({
          id: obj.MX_USER,
          name: obj.MX_SENDER
        });
        return;
      }
      obj.DATE.forEach(item => {
        this.selPersonTags.push({
          id: item.ID,
          name: item.NAME
        });
      });
    },
    //提交邮件回复
    SubmitReply: function() {
      var NR = this.NR;
      let obj = this.replyData;
      if (this.FS_BT == "") {
        this.$message({
          message: "邮件主题不能为空!",
          type: "warning"
        });
        return;
      }
      // if (NR == "") {
      //   this.$message({
      //     message: "邮件内容不能为空!",
      //     type: "warning"
      //   });
      //   return;
      // }
      subReplyEmail(
        this.userInfo.ur_ident,
        this.userInfo.ur_name,
        1,
        this.FS_BT,
        obj.MX_SENDER,
        obj.MX_USER,
        NR,
        this.FJLIST
      ).then(res => {
        if (res.rel == "1") {
          var text = "回复成功!";
          if (this.isForWard == true) {
            text = "转发成功 ！";
          }
          this.$store.commit("curSideName", "outBox");
          this.$message({
            type: "success",
            message: text
          });
        }
      });
    },
    customRequst: function(file) {
      var formData = new FormData();
      var xmlhttp;
      // this.fileList=[];
      if (window.XMLHttpRequest) {
        xmlhttp = new XMLHttpRequest();
      } else {
        xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
      }
      var _this = this;
      xmlhttp.open("POST", "/jz/XBM_Service.bsp?FILE", true);
      xmlhttp.setRequestHeader("X-Requested-With", "XMLHttpRequest");
      formData.append("filename", file.file.name);
      formData.append("FX_0F00000000", file.file);
      formData.append("_Code_", "");
      formData.append("Submit", "提交");
      xmlhttp.send(formData);
      xmlhttp.onreadystatechange = function() {
        if (xmlhttp.readyState == 4) {
          if (xmlhttp.status == 200) {
							var data=JSON.parse(xmlhttp.responseText);
              data.name=file.file.name;
              _this.fileList.push(data);
          } else {
            console.log("上传失败" + xmlhttp.responseText);
          }
        }
			};
    },
    onPreview:function(file){
      window.open('/jz/'+file.Addr)
    },
	  handleRemove(file, fileList) {
             this.fileList=fileList;
    },
  },
  components: { selPerson, editor }
};
</script>
<style lang="scss" scoped>
/deep/ .editor-box .edui-default .edui-editor-iframeholder {
  height: auto !important;
}
.email-add {
  .em-table-top {
    border: 1px solid #ddd;
    font-size: 12px;
    line-height: 40px;
    .em-table-title {
      text-align: center;
      font-weight: bolder;
      background: #f5f5f5;
      font-size: 14px;
      font-weight: bolder;
    }
    > td {
      height: 30px;
      font-weight: bold;
      color: #383838;
      background-color: #fff;
      &.left {
        border-top-left-radius: 2px;
      }
      &.right {
        border-top-right-radius: 2px;
      }
    }
  }
  #form1 {
    .em-form-table {
      border-top: 0px !important;
      border: 1px #dddddd solid;
      line-height: 20px;
      font-size: 9pt;
      border-collapse: collapse;
      .em-consignee {
        text-align: center;
        font-size: 14px;
        .fjtext{
        position:absolute;
        top:50%;
        left:0px;
        width:100%;
        transform: translateY(-50%);
        }
      }
       .em-consignee,
      .em-person-select {
        background: #ffffff;
        border-bottom: 1px #dddddd solid;
        border-top: 1px #dddddd solid;
        border-right: 1px #dddddd solid;
        padding: 3px;
        height: 30px;
        .em-clear-text {
          color: red;
        }
        .em-tags {
          margin-right: 10px;
        }
       
      }
    }
    .handle-btn {
      padding: 10px;
    }
  }
}
</style>
