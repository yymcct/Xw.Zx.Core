<template>
  <div class="LawsAdd">
    <div class="mg-10" style="margin: 10px;">
      <table class="em-table-top" width="100%">
        <tbody>
          <tr>
            <td class="em-table-title">政策法规</td>
          </tr>
        </tbody>
      </table>
      <!-- <form enctype="multipart/form-data" action method="post" id="form1" name="form1"> -->
      <table class="em-form-table" width="100%" align="center">
        <tbody>
          <tr>
            <td nowrap class="em-consignee" width="100">类型：</td>
            <td class="em-person-select">
              <el-select
                v-model="form.mlid"
                filterable
                default-first-option
                placeholder="请选择类型目录"
                clearable
              >
                <el-option
                  v-for="(item, idx) in catList"
                  :key="idx"
                  :label="item.NAME"
                  :value="item.MLID"
                ></el-option>
              </el-select>
            </td>
            <td nowrap class="em-consignee">发布时间:</td>
            <td class="em-person-select">
              <el-date-picker
                v-model="form.scsj"
                type="date"
                placeholder="选择日期"
                format="yyyy-MM-dd"
                value-format="yyyy-MM-dd"
                :picker-options="pickerOptions0"
              >
              </el-date-picker>
            </td>
          </tr>
        </tbody>
        <tbody>
          <tr>
            <td nowrap class="em-consignee">标题:</td>
            <td class="em-person-select" colspan="3">
              <el-input
                v-model="form.wj_name"
                placeholder="请输入内容"
              ></el-input>
            </td>
          </tr>
          <tr>
            <td valign="top" nowrap class="em-consignee">
              内容:
              <br />
              <br />
            </td>
            <td class="em-person-select" colspan="3">
              <editor @ready="editorReady" :defaultMsg="NR"></editor>
              <!-- <editor :defauleMsg="NR"  @onEditorChange="onEditorChange"></editor> -->
            </td>
          </tr>
          <tr align="center" class="TableControl">
            <td colspan="4" nowrap>
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
  props: ["catList", "curData"],
  data() {
    return {
      NR: ``,
      subLoading: false,
      value: "",
      form: {
        mlid: "",
        wj_name: "",
        DATA: [],
        scsj: "",
        wiid: ""
      },
      pickerOptions0: {
        disabledDate(time) {
          return time.getTime() > Date.now(); //如果没有后面的-8.64e7就是不可以选择今天的
        }
      }
    };
  },
  created() {
    let obj = this.curData;
    if (this.curData) {
      this.form = {
        mlid: obj.MLID,
        wiid: obj.WIID,
        wj_name: obj.WJ_NAME,
        DATA: obj.WJ_NR,
        scsj: obj.SCSJ
      };
      this.NR = obj.WJ_NR;
    } else {
      this.form = {
        mlid: "",
        wj_name: "",
        DATA: [],
        scsj: ""
      };
    }
  },
  mounted() {},
  methods: {
    onEditorChange: function(html) {
      this.NR = html;
    },
    editorReady(instance) {
      this.$nextTick(() => {
        if (this.curData) {
          instance.setContent(this.curData.WJ_NR);
        }
      });
      instance.addListener("contentChange", () => {
        this.NR = instance.getContent();
      });
    },
    SubmitForm: function() {
      if (!this.NR) {
        this.$message.warning("内容不能为空!");
        return;
      }
      this.subLoading = true;
      let func = (source, count) => {
        let arr = [];
        for (let i = 0, len = source.length / count; i < len; i++) {
          let subStr = source.substr(0, count);
          arr.push({ wj_nr: subStr });
          source = source.replace(subStr, "");
        }
        return arr;
      };
      function addSlashes(str) {
        return str.replace(/[\\"']/g, "\\$&");
      }

      let temp = this.NR.replace(/\"/g, "'");
      this.form.DATA = func(temp, 2000);
      dataService
        .addLaws(this.form)
        .then(res => {
          this.subLoading = false;
          this.$emit("onSubmit");
        })
        .catch(res => {
          this.$message({
            type: "warning",
            message: ""
          });
          this.subLoading = false;
        });
    }
  },
  components: { editor }
};
</script>
<style lang="scss" scoped>
.LawsAdd {
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
