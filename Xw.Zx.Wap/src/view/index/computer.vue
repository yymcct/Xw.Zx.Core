<template>
  <div class="wrapper">
    <h1 class="head">计算器</h1>
    <div class="content">
      <van-field v-model="name" required label="姓名" placeholder="请输入姓名" />
      <van-field v-model="borrowCompany" required label="贷款机构" placeholder="请输入贷款机构名" />
      <van-field v-model="borrowAmount" type="digit" required label="到账总额" placeholder="请输入借款到账总额" />
      <van-field
        v-model="cycle"
        required
        label="期数"
        placeholder="请选择分期期数"
        @click="option.showCyclePicker = true"
      />
      <van-field v-model="cycleAmount" type="digit" required label="每期金额" placeholder="请输入每期金额" />
      <van-field
        v-model="repaymentCycle"
        required
        label="已还期数"
        placeholder="请选择已还期数"
        @click="repaymentCycleInputClick"
      />
      <van-field
        v-model="overdueCycle"
        required
        label="逾期期数"
        placeholder="请选择逾期期数"
        @click="option.showOverdueCyclePicker = true"
      />

      <van-popup v-model="option.showCyclePicker" position="bottom">
        <van-picker
          show-toolbar
          :columns="option.cyclePickerColumns"
          @cancel="option.showCyclePicker = false"
          @confirm="cyclePickerOnConfirm"
        />
      </van-popup>

      <van-popup v-model="option.showRepaymentCyclePicker" position="bottom">
        <van-picker
          show-toolbar
          :columns="option.repaymentCyclePickerColumns"
          @cancel="option.showRepaymentCyclePicker = false"
          @confirm="repaymentCyclePickerOnConfirm"
        />
      </van-popup>

      <van-popup v-model="option.showOverdueCyclePicker" position="bottom">
        <van-picker
          show-toolbar
          :columns="option.overdueCyclePickerColumns"
          @cancel="option.showOverdueCyclePicker = false"
          @confirm="overdueCyclePickerOnConfirm"
        />
      </van-popup>
      <div class="content-btn" v-show="option.btnShow">
        <van-button type="primary" class="content-btn-btn" @click="option.btnShow=false">开始计算</van-button>
      </div>
    </div>

    <div class="result" v-show="!option.btnShow">
      <h2>计算结果</h2>
      <p>
        <span class="result-lable">应付利息:&nbsp;</span>
        <span class="result-value">{{yflx}}</span>
      </p>
      <p>
        <span class="result-lable">减免利息最小:&nbsp;</span>
        <span class="result-value2">{{jmlx_min}}</span>
      </p>
      <p>
        <span class="result-lable">减免利息最大:&nbsp;</span>
        <span class="result-value2">{{jmlx_max}}</span>
      </p>
    </div>

    <div class="contact">
      <div class="contact-linkman" v-show="member.linkMan">立刻咨询: {{member.linkMan}}</div>
      <div class="contact-tel" v-show="member.phone" @click="call">
        <span class="contact-tel-num">{{member.phone}}</span>
        <a class="contact-tel-btn" :href="'tel:'+member.phone">一键拨打</a>
      </div>
      <div class="contact-qrcode" v-show="member.wxQrCode">
        <img class="contact-qrcode-code" v-lazy="member.wxQrCode" alt />
        <span>识别二维码联系我们</span>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "",
  props: [""],
  data() {
    return {
      name: "",
      borrowCompany: "",
      borrowAmount: "",
      cycle: "",
      cycleAmount: "",
      repaymentCycle: "",
      overdueCycle: "",
      member: {
        linkMan: "xians",
        phone: "1876666666",
        wxQrCode: ""
      },
      option: {
        cyclePickerColumns: [],
        showCyclePicker: false,
        repaymentCyclePickerColumns: [],
        showRepaymentCyclePicker: false,
        overdueCyclePickerColumns: [],
        showOverdueCyclePicker: false,
        btnShow: true
      }
    };
  },

  components: {},
  computed: {
    yflx: function() {
      return parseInt(this.borrowAmount * 0.01 * Number(this.cycle));
    },
    jmlx_min: function() {
      return parseInt(
        Number(this.cycle) * this.cycleAmount -
          this.borrowAmount -
          this.borrowAmount * 0.01 * Number(this.cycle)
      );
    },
    jmlx_max: function() {
      return parseInt(
        Number(this.cycle) * this.cycleAmount -
          this.borrowAmount -
          (this.borrowAmount * 0.01 * Number(this.cycle)) / 3
      );
    }
  },

  beforeMount() {
    for (let i = 1; i < 100; i++) {
      this.option.cyclePickerColumns.push(String(i));
      this.option.overdueCyclePickerColumns.push(String(i));
    }
  },

  mounted() {},

  methods: {
    cyclePickerOnConfirm(value) {
      this.cycle = value;
      this.option.showCyclePicker = false;
      for (let i = 0; i < Number(value) + 1; i++) {
        this.option.repaymentCyclePickerColumns.push(String(i));
      }
    },
    repaymentCyclePickerOnConfirm(value) {
      this.repaymentCycle = value;
      this.option.showRepaymentCyclePicker = false;
    },
    overdueCyclePickerOnConfirm(value) {
      this.overdueCycle = value;
      this.option.showOverdueCyclePicker = false;
    },
    repaymentCycleInputClick() {
      if (this.cycle) {
        this.option.showRepaymentCyclePicker = true;
      } else {
        this.$toast("请先选择分期数!");
      }
    }
  },

  watch: {}
};
</script>
<style lang='scss' scoped>
.wrapper {
  background-color: #fff;
  padding-bottom: 30px;
  .head {
    text-align: center;
    padding: 20px;
  }
  .content {
    padding: 10px;
    &-btn {
      margin: 20px 0;
      width: 100%;
      display: flex;
      justify-content: center;
      &-btn {
        width: 90%;
      }
    }
  }
  .result {
    padding: 20px;
    h2 {
      margin-top: 30px;
      margin-bottom: 10px;
    }
    &-lable {
      font-size: 16px;
      line-height: 24px;
    }
    &-value {
      font-size: 18px;
      font-weight: bold;
    }
    &-value2 {
      font-size: 18px;
      font-weight: bold;
      color: red;
    }
  }
  .contact {
    margin-bottom: 20px;

    margin: 0px auto;
    width: 300px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    &-linkman {
      display: flex;
      flex-direction: column;
      justify-content: center;
      font-size: 20px;
      font-weight: bold;
      margin-top: 20px;
      margin-bottom: 20px;
    }
    &-tel {
      margin-bottom: 10px;
      &-num {
        font-size: 16px;
        color: #333;
        margin-right: 10px;
      }
      &-btn {
        font-size: 16px;
        color: #fff;
        background: #e8372d;
        padding: 5px 15px 5px 15px;
        margin-right: 10px;
        border-radius: 10px;
        margin-right: 10px;
      }
    }
    &-qrcode {
      margin-bottom: 10px;
      display: flex;
      flex-direction: column;
      justify-content: center;
      img {
        width: 220px;
        height: 220px;
      }
      span {
        margin-top: 10px;
        margin-bottom: 10px;
        font-size: 16px;
        color: #999;
        text-align: center;
      }
    }
  }
}
</style>