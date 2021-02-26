<template>
  <div class="wrapper">
    <div
      class="wrapper-bg"
      :style="{backgroundImage:'url('+require('../../assets/images/computer/bg2.png')+') '}"
    >
      <div class="block">
        <div class="block-title">
          <img :src="require('@/assets/images/computer/title2.png')" alt />
        </div>
        <div class="block-content">
          <van-field v-model="name" label="姓名" placeholder="请输入姓名" />
          <van-field
            v-model="phone"
            required
            label="手机"
            type="tel"
            placeholder="请输入联系人手机"
            :error-message="option.errPhone"
          />
          <van-field v-model="borrowCompany" label="贷款机构" placeholder="请输入贷款机构名" />
          <van-field
            v-model="borrowAmount"
            type="digit"
            required
            label="到账总额"
            placeholder="请输入借款到账总额"
          />
          <van-field v-model="cycle" required label="期数" placeholder="请输入分期期数" type="digit" />
          <van-field
            v-model="cycleAmount"
            type="digit"
            required
            :error-message="option.errorMessage"
            label="每期金额"
            placeholder="请输入每期金额"
          />
          <van-field v-model="repaymentCycle" label="已还期数" placeholder="请输入已还期数" type="digit" />
          <van-field v-model="overdueCycle" label="逾期期数" placeholder="请输入逾期期数" type="digit" />
          <div class="block-content-btn" v-show="option.btnShow">
            <van-button
              type="info"
              class="block-content-btn-btn"
              @click="btnClikc"
              :disabled="borrowAmount==''|| cycle=='' || cycleAmount=='' ||  phone.length<11 "
            >开始计算</van-button>
          </div>
        </div>

        <div class="block-result" v-show="!option.btnShow">
          <h2>计算结果</h2>
          <p>
            <span class="block-result-lable">应付利息:&nbsp;</span>
            <span class="block-result-value">{{yflx}}元</span>
          </p>
          <p>
            <span class="block-result-lable">减免利息最小:&nbsp;</span>
            <span class="block-result-value2">{{jmlx_min}}</span>
            <span class="block-result-value">元</span>
          </p>
          <p>
            <span class="block-result-lable">减免利息最大:&nbsp;</span>
            <span class="block-result-value2">{{jmlx_max}}</span>
            <span class="block-result-value">元</span>
          </p>
        </div>
        <div class="block-footer">
          <img :src="require('@/assets/images/log.png')" alt />
          <span>Copy Right 2021 成都再减减企业管理服务有限公司</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "",
  props: [""],
  data() {
    return {
      name: "",
      phone: "",
      borrowCompany: "",
      borrowAmount: "",
      cycle: 36,
      cycleAmount: "",
      repaymentCycle: "",
      overdueCycle: "",
      member: {
        linkMan: "xians",
        phone: "1876666666",
        wxQrCode: "",
      },
      option: {
        btnShow: true,
        errorMessage: "",
        errPhone: "",
        sharePhone: "",
        showQrcode: false,
      },
    };
  },

  components: {},

  computed: {
    yflx: function () {
      return parseInt(this.borrowAmount * 0.007 * this.cycle);
    },
    jmlx_min: function () {
      let l = parseInt(
        this.cycle * this.cycleAmount -
          this.borrowAmount -
          this.borrowAmount * 0.007 * this.cycle
      );
      if (l < 0) l = 0;
      return l;
    },
    jmlx_max: function () {
      let l = parseInt(
        this.cycle * this.cycleAmount -
          this.borrowAmount -
          (this.borrowAmount * 0.007 * this.cycle) / 3
      );
      if (l < 0) l = 0;
      return l;
    },
  },

  beforeMount() {},

  mounted() {
    if (this.$route.query.p) {
      this.member.phone = this.$route.query.p;
    }
  },

  methods: {
    btnClikc() {
      if (this.phone.length != 11) {
        this.option.errPhone = "手机号格式错误";
        return;
      }
      if (this.cycle * this.cycleAmount < this.borrowAmount) {
        this.option.errorMessage = "期数乘每期金额应大于到账总额";
        return;
      }

      api.computer.postComputerUser({
        name: this.name,
        phone: this.phone,
        borrowCompany: this.borrowCompany,
        borrowAmount: this.borrowAmount,
        cycle: this.cycle,
        cycleAmount: this.cycleAmount,
        repaymentCycle: this.repaymentCycle,
        overdueCycle: this.overdueCycle,
        sourcePhone: this.member.phone,
        MinReduce: this.jmlx_min,
        MaxReduce: this.jmlx_max,
      });
      this.option.btnShow = false;
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  width: 100%;
  //min-height: 100vh;
  background-size: 100%;
  background-repeat: no-repeat;
  background: linear-gradient(0deg, rgb(69, 69, 241) 0%, rgb(20, 28, 200) 100%);
  &-bg {
    width: 100%;
   // min-height: 100vh;
    background-size: 100%;
    background-repeat: no-repeat;
    padding-top: 40px;
    padding-bottom: 20px;
    .block {
      &-title {
        margin: 0 70px;
        img {
          width: 100%;
        }
      }
      &-content {
        overflow: hidden;
        margin: 20px 10px 0px 10px;
        padding: 10px 5px;

        border-top-left-radius: 5px;
        border-top-right-radius: 5px;
        background-color: #fff;
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
      &-result {
        margin: 0 10px;
        //border: 1px solid #ff976a;
        padding: 20px 20px;
        //border-radius: 10px;
        background-color: #fff;

        h2 {
          margin: 15px 10px;
          text-align: center;
          font-weight: bold;
        }
        p {
          margin-top: 5px;
        }
        &-lable {
          font-size: 14px;
          line-height: 24px;
          width: 95px;
          display: inline-block;
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
      &-footer {
        margin: 0px 10px 0px 10px;
        display: flex;
        flex-direction: column;
        background-color: #fff;
        justify-content: center;
        align-items: center;
        padding-top: 20px;
        padding: 10px 0;
        border-bottom-left-radius: 5px;
        border-bottom-right-radius: 5px;
        img {
          width: 150px;
        }
        span {
          font-size: 13px;
          color: #66666663;
          margin-top: 10px;
        }
      }
    }
  }
}
</style>