<template>
  <div class="LawsAdd">
    <div class="mg-10" style="margin: 10px;">
       <!-- <editor  :defaultMsg="NRStr" :id="curNodeID" @ready="editorReady" style="width:100%;" ref="UE"></editor>
           <el-button
                    type="primary"
                    class="btn css_1007 submit-btn"
                    title="提交"
                    :loading="subLoading"
                    @click="SubmitForm"
                  >
                    <label v-if="!subLoading">提交</label>
                    <label v-else>提交中</label>
                  </el-button> -->
      <table class="em-table-top" width="100%">
        <tbody>
          <tr>
            <td class="em-table-title">使用帮助</td>
          </tr>
        </tbody>
      </table>
        <table class="em-form-table" width="100%" align="center">
          <tbody>
            <tr>
              <td valign="top" nowrap class="em-consignee">
                内容:
                <br>
                <br>
              </td>
              <td class="em-person-select">
                <editor  :defaultMsg="NRStr" ref="ue"  @ready="editorReady" :ueditorConfig="config" style="width:100%;"></editor>
              </td>
            </tr>
            <tr align="center" class="TableControl">
              <td colspan="2" nowrap>
                <div class="handle-btn">
                  <el-button
                    type="primary"
                    class="btn css_1007 submit-btn"
                    title="提交"
                    :loading="subLoading"
                    @click="SubmitForm"
                  >
                    <label v-if="!subLoading">提交</label>
                    <label v-else>提交中</label>
                  </el-button>
                  <!-- <el-button
                    type="primary"
                    class="btn css_1007 submit-btn"
                    title="提交"
                    @click="doBack"
                  >
                    <label>返回</label>
                  </el-button> -->
                </div>
              </td>
            </tr>
          </tbody>
        </table>
    </div>
  </div>
</template>
<script>
	import * as dataService from "@/public/apiService/home";
import editor from "@/components/Ueditor.vue";
export default {
  props: ['NRStr','curNodeData'],
  data() {
    return {
      NR: "",
      subLoading: false,
      value:'',
      config:{
        initialFrameHeight: 500
      }
    };
  },
  mounted() {
  },
  methods: {
       editorReady (instance) {
       
        
        instance.setContent(this.NRStr);
        instance.addListener('contentChange', () => {
          this.NR = instance.getContent();
        });
      },
    SubmitForm: function() {
      this.subLoading=true;
      
            let func = (source, count) => {
      let arr = [];
   
      for (let i = 0, len = source.length / count; i < len; i++) {
        let subStr = source.substr(0, count);
        arr.push({nr:subStr});
        source = source.replace(subStr, "");
      }
      return arr;
    }
     function addSlashes (str) {
       return str.replace(/[\\"']/g, '\\$&');
     }
      // let temp=this.Base64.encode(this.NR);
      let temp=this.NR.replace(/\"/g,"'");
      let DATA=func(temp,1000);
      dataService.addHelpCont(this.curNodeData.NODEID,DATA)
        .then(res => {
          this.subLoading=false;
          this.$emit("onSubmit",res.data[0].NODEID);
        }).catch(res=>{
          this.$message({
            type:'warning',
            message:''
          })
          this.subLoading=false;
        });
    },
    doBack:function(){
      this.$emit('close')
    },
  },
  components: { editor }
};
</script>
<style lang="scss" scoped>
.LawsAdd {
  // height:100%;
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
    .em-form-table {
      border-top: 0px !important;
      border: 1px #dddddd solid;
      line-height: 20px;
      font-size: 9pt;
      border-collapse: collapse;
      .em-consignee {
        text-align: center;
        font-size: 14px;
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
</style>
