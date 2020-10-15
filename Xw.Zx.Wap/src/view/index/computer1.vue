<template>
  <!-- 利息计算方式修订 -->
  <div class="wrapper">
    <h1 class="head">贷款利息减免计算器 <span>V1.1测试版</span></h1>
    <div class="content">
      <van-field v-model="name" label="姓名" placeholder="请输入姓名" />
      <van-field
        v-model="phone"
        required
        label="手机"
        type="tel"
        placeholder="请输入联系人手机"
        :error-message="option.errPhone"
      />
      <van-field
        v-model="borrowCompany"
        label="贷款机构"
        placeholder="请输入贷款机构名"
      />
      <van-field
        v-model="borrowAmount"
        type="digit"
        required
        label="到账总额"
        placeholder="请输入借款到账总额"
      />
      <van-field
        v-model="cycle"
        required
        label="期数"
        placeholder="请输入分期期数"
        type="digit"
      />
      <van-field
        v-model="cycleAmount"
        type="digit"
        required
        :error-message="option.errorMessage"
        label="每期金额"
        placeholder="请输入每期金额"
      />
      <van-field
        v-model="repaymentCycle"
        label="已还期数"
        required
        placeholder="请输入已还期数"
        type="digit"
      />
      <van-field
        v-model="overdueCycle"
        label="逾期期数"
        placeholder="请输入逾期期数"
        type="digit"
      />
      <div class="content-btn" v-show="option.btnShow">
        <van-button
          type="primary"
          class="content-btn-btn"
          @click="btnClikc"
          :disabled="
            borrowAmount == '' ||
            cycle == '' ||
            cycleAmount == '' ||
            phone.length < 11 ||
            repaymentCycle.length == 0
          "
          >开始计算</van-button
        >
      </div>
    </div>
    <!--  v-show="!option.btnShow"-->
    <div class="result">
      <h2>计算结果</h2>
      <p>
        <span class="result-lable">应付利息:&nbsp;</span>
        <span class="result-value">{{ yflx }}元</span>
      </p>
      <p>
        <span class="result-lable">减免利息最小:&nbsp;</span>
        <span class="result-value2">{{ jmlx_min }}</span>
        <span class="result-value">元</span>
      </p>
      <p>
        <span class="result-lable">减免利息最大:&nbsp;</span>
        <span class="result-value2">{{ jmlx_max }}</span>
        <span class="result-value">元</span>
      </p>
    </div>
    <div class="footer">
      <img :src="require('@/assets/images/log.png')" alt="" />
      <span>Copy Right 2020 成都再减减企业管理服务有限公司</span>
    </div>
  </div>
</template>

<script>
//import api from "@/api/sqbApi";
// import vueQr from "vue-qr";
export default {
  name: "",
  props: [""],
  data() {
    return {
      name: "",
      phone: "18777777777",
      borrowCompany: "",
      borrowAmount: "260000",
      cycle: 36,
      cycleAmount: "11050",
      repaymentCycle: "7",
      overdueCycle: "0",

      yflx: 0,
      jmlx_min: 0,
      jmlx_max: 0,

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

  components: {
    // vueQr
  },
  computed: {
    // yflx: function () {
    //   return parseInt(this.borrowAmount * 0.008 * this.cycle);
    // },
    // jmlx_min: function () {
    //   let l = parseInt(
    //     this.cycle * this.cycleAmount -
    //       this.borrowAmount -
    //       this.borrowAmount * 0.008 * this.cycle
    //   );
    //   if (l < 0) l = 0;
    //   return l;
    // },
    // jmlx_max: function () {
    //   let l = parseInt(
    //     this.cycle * this.cycleAmount -
    //       this.borrowAmount -
    //       (this.borrowAmount * 0.008 * this.cycle) / 3
    //   );
    //   if (l < 0) l = 0;
    //   return l;
    // },
  },

  beforeMount() {},

  mounted() {
    if (this.$route.query.p) {
      this.member.phone = this.$route.query.p;
    }
  },

  methods: {
    jsq_yflx(jixibenjin, qishu, meiyuehuankuan) {
      let yinhuanlixiTotal = 0;
      for (var i = 0; i < qishu; i++) {
        let yinhuanlixi = (jixibenjin * 17) / 100 / 12;
        let yindibenji = meiyuehuankuan - yinhuanlixi;

        jixibenjin = jixibenjin - yindibenji;

        //console.log(i + 1, jixibenjin, yinhuanlixi);
        if (yinhuanlixi > 0) {
          yinhuanlixiTotal += yinhuanlixi;
        }
      }
      return Math.round(yinhuanlixiTotal);
    },
    jsq_minjmlx(jixibenjin, qishu, meiyuehuankuan, yihuanqishu) {
      let hetongjine = qishu * meiyuehuankuan; //合同金额
      let yflx = this.jsq_yflx(jixibenjin, qishu, meiyuehuankuan);
      let minjmlx = hetongjine - jixibenjin - yflx;
      // let getshengyujixibenjin = (jixibenjin, meiyuehuankuan, yihuanqishu) => {
      //   for (var i = 0; i < yihuanqishu; i++) {
      //     let yinhuanlixi = (jixibenjin * 15.4) / 100 / 12;
      //     let yindibenji = meiyuehuankuan - yinhuanlixi;
      //     jixibenjin = jixibenjin - yindibenji;
      //   }
      //   return jixibenjin;
      // };
      let getshengyuhetongjine = (meiyuehuankuan, qishu, yihuanqishu) => {
        let shengyuhetongjine = (qishu - yihuanqishu) * meiyuehuankuan;
        return shengyuhetongjine;
      };

      let shengyujixibenjin = getshengyuhetongjine(
        meiyuehuankuan,
        qishu,
        yihuanqishu
      );
      console.log(shengyujixibenjin);
      if (minjmlx > shengyujixibenjin) {
        minjmlx = shengyujixibenjin - 5000;
      }
      if (minjmlx < 0) minjmlx = 0;

      return Math.round(minjmlx);
    },
    jsq_maxjmlx(jixibenjin, qishu, meiyuehuankuan, yihuanqishu) {
      let hetongyinhuan = (qishu - yihuanqishu) * meiyuehuankuan; //合同金额
      let getshengyujixibenjin = (jixibenjin, meiyuehuankuan, yihuanqishu) => {
        for (var i = 0; i < yihuanqishu; i++) {
          let yinhuanlixi = (jixibenjin * 17) / 100 / 12;
          let yindibenji = meiyuehuankuan - yinhuanlixi;
          jixibenjin = jixibenjin - yindibenji;
        }
        return jixibenjin;
      };
      let shengyujixibenjin = getshengyujixibenjin(
        jixibenjin,
        meiyuehuankuan,
        yihuanqishu
      );

      let getyuqilixi = (shengyujixibenjin, yuqiqishu) => {
        if (yuqiqishu == 0) {
          return 0;
        } else {
          return (shengyujixibenjin * 17) / 100 / (12 * yuqiqishu);
        }
      };
      let yuqilixi = getyuqilixi(shengyujixibenjin, this.overdueCycle);
      console.log(hetongyinhuan, shengyujixibenjin, yuqilixi);
      let maxjmlx = hetongyinhuan - shengyujixibenjin - yuqilixi;
      return Math.round(maxjmlx);
    },
    btnClikc() {
      if (this.phone.length != 11) {
        this.option.errPhone = "手机号格式错误";
        return;
      }
      if (this.cycle * this.cycleAmount < this.borrowAmount) {
        this.option.errorMessage = "期数乘每期金额应大于到账总额";
        return;
      }

      this.yflx = this.jsq_yflx(
        this.borrowAmount,
        this.cycle,
        this.cycleAmount
      );
      this.jmlx_min = this.jsq_minjmlx(
        this.borrowAmount,
        this.cycle,
        this.cycleAmount,
        this.repaymentCycle
      );
      this.jmlx_max = this.jsq_maxjmlx(
        this.borrowAmount,
        this.cycle,
        this.cycleAmount,
        this.repaymentCycle
      );

      // api.computer.postComputerUser({
      //   name: this.name,
      //   phone: this.phone,
      //   borrowCompany: this.borrowCompany,
      //   borrowAmount: this.borrowAmount,
      //   cycle: this.cycle,
      //   cycleAmount: this.cycleAmount,
      //   repaymentCycle: this.repaymentCycle,
      //   overdueCycle: this.overdueCycle,
      //   sourcePhone: this.member.phone,
      //   MinReduce: this.jmlx_min,
      //   MaxReduce: this.jmlx_max,
      // });
      //   this.option.btnShow = false;
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  background-color: #fff;
  padding-bottom: 30px;
  .head {
    background-color: #ff976a;
    color: #fff;
    text-align: center;
    padding: 20px;
    font-size: 24px;
    span {
      font-size: 14px;
    }
  }
  .content {
    overflow: hidden;
    margin: 10px;
    border: 1px solid #ff976a;
    border-radius: 10px;
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
  .result {
    margin: 10px;
    border: 1px solid #ff976a;
    padding: 20px 10px;
    border-radius: 10px;
    background-color: #fff;

    h2 {
      margin-bottom: 10px;
      text-align: center;
      font-weight: bold;

      width: 100%;
    }
    p {
      margin-top: 5px;
    }
    &-lable {
      font-size: 14px;
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
    background-color: #fff;
    margin: 10px;
    padding-bottom: 10px;
    border: 1px solid #ff976a;
    border-radius: 10px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    &-linkman {
      display: flex;
      flex-direction: column;
      justify-content: center;
      font-size: 14px;
      font-weight: bold;
      margin-top: 20px;
      margin-bottom: 20px;
    }
    &-tel {
      margin-bottom: 10px;
      &-num {
        font-size: 16px;
        font-weight: bold;
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
  .share {
    background-color: #fff;
    margin: 10px;
    padding-bottom: 10px;
    border: 1px solid #ff976a;
    border-radius: 10px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    &-title {
      display: flex;
      flex-direction: column;
      justify-content: center;
      font-size: 14px;
      font-weight: bold;
      margin-top: 20px;
      margin-bottom: 20px;
    }
    &-phone {
      margin-bottom: 10px;
      display: flex;
      flex-direction: row;
      justify-content: center;
      align-items: center;
      width: 100%;
      &-btn {
        margin-right: 10px;
        width: 100px;
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
  .footer {
    display: flex;
    flex-direction: column;
    background-color: #fff;
    justify-content: center;
    align-items: center;
    margin-top: 20px;
    padding: 10px 0;
    img {
      width: 150px;
    }
    span {
      font-size: 15px;
      color: #666;
      margin-top: 10px;
    }
  }
}
</style>