<template>
  <div>
    <h5>网上申请</h5>
    <div class="form-box">
      <el-form ref="form" :model="form" label-width="120px">
        <el-form-item label="项目名称：">
          <el-input v-model="form.name" style="width:500px"></el-input>
        </el-form-item>
        <el-form-item label="办事类型：">
          <el-select v-model="form.region" placeholder="请选择活动区域" style="width:500px">
            <el-option v-for="item in thingType" :label="item.text" :value="item.value" :key="item.value"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="详细信息：">
          <el-input type="textarea" v-model="form.desc" style="width:500px"></el-input>
        </el-form-item>
        <el-form-item label="姓名：">
          <el-input v-model="form.name" style="width:500px"></el-input>
        </el-form-item>
        <el-form-item label="电子邮箱：">
          <el-input type="email" v-model="form.name" style="width:500px"></el-input>
        </el-form-item>
        <el-form-item label="打印：">
          <el-radio-group v-model="form.resource">
            <el-radio label="1" border>是</el-radio>
            <el-radio label="2" border>否</el-radio>
          </el-radio-group>
          <span style="padding-left:30px;">地址：河南省郑州市金水区XX街道XX大楼X号</span>
        </el-form-item>

        <el-form-item>
          <button type="primary" @click="onSubmit">确认提交</button>
        </el-form-item>
      </el-form>
    </div>
  </div>
</template>

<script>
export default {
  name: "right",
  data() {
    return {
      thingType: [
        {
          value: "1",
          text: "权籍调查"
        },
        {
          value: "2",
          text: "不动产测绘"
        },
        {
          value: "3",
          text: "争议调处"
        },
        {
          value: "4",
          text: "成果应用的制度"
        },
        {
          value: "5",
          text: "控制性详细规划"
        }
      ],
      form: {
        name: "",
        region: "",
        date1: "",
        date2: "",
        delivery: false,
        type: [],
        resource: "",
        desc: ""
      }
    };
  },
  methods: {
    onSubmit() {
      console.log("submit!");
    }
  }
};
</script>

<style lang="scss" scoped>
div {
  h5 {
    height: 64px;
    line-height: 64px;
    color: #333333;
    font-size: 18px;
    padding-left: 20px;
    font-weight: normal;
  }
  .form-box {
    padding-left: 155px;
    button {
      width: 380px;
      height: 52px;
      line-height: 52px;
      background: rgba(7, 67, 139, 1);
      border-radius: 5px;
      color: rgba(255, 255, 255, 1);
      font-size: 24px;
      margin-top: 38px;
    }
  }
  >>> .el-input__suffix {
    right: 0;
    background: url("../../../../assets/select_ico.png");
  }
  >>> .el-input__icon:after,
  >>> .el-input__icon:before {
    display: none;
  }
  >>> .el-input.is-active .el-input__inner,
  >>> .el-input__inner:focus,
  >>> .el-textarea__inner:focus,
  >>> .el-select .el-input.is-focus .el-input__inner {
    border-color: rgba(7, 67, 139, 1);
  }
  >>> .el-radio__input {
    display: none;
  }
  .el-radio.is-bordered {
    height: 36px;
    border: 1px solid #ccc;
    &.is-checked {
      background: url("../../../../assets/radio_bg.png") no-repeat;
      background-size: contain;
      border: 1px solid transparent;
    }
  }
  >>> .el-radio__input.is-checked + .el-radio__label {
    color: #3368bf;
  }
}
</style>