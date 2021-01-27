<template>
  <!-- 利息计算方式修订 -->
  <div class="wrapper">
    <h1 class="head">贷款利息减免计算器 <span>V1.3测试版</span></h1>
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
        type="number"
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
      <!-- <van-field
        v-model="overdueCycle"
        label="逾期期数"
        required
        placeholder="请输入逾期期数"
        type="digit"
      /> -->
      <van-field
        readonly
        clickable
        required
        label="最后还款"
        :value="_lastRefundDate"
        placeholder="选择城市"
        @click="showPicker = true"
      />
      <van-popup v-model="showPicker" round position="bottom">
        <van-datetime-picker
          v-model="lastRefundDate"
          type="date"
          title="选择最后还款时间"
          :min-date="minDate"
          :max-date="maxDate"
          :formatter="formatter"
          @confirm="pickerConfirmHandle"
        />
      </van-popup>
      <div class="content-btn" v-show="option.btnShow">
        <van-button
          type="primary"
          class="content-btn-btn"
          color="linear-gradient(to right, #ff7a00, #ff5000)"
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
    <!--  -->
    <div class="result" v-show="!option.btnShow">
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
      <p style="font-size: 13px" v-html="maxlxLog"></p>
    </div>

    <div class="content-btn" v-show="!option.btnShow">
      <van-button
        type="primary"
        class="content-btn-btn"
        color="linear-gradient(to right, #ff7a00, #ff5000)"
        @click="btnRest"
        >复位</van-button
      >
    </div>
    <div class="footer">
      <img :src="require('@/assets/images/log.png')" alt="" />
      <span>Copy Right 2020 成都再减减企业管理服务有限公司</span>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
// import vueQr from "vue-qr";
export default {
  name: "jsq123",
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
      overdueCycle: "0",
      lastRefundDate: new Date(),
      showPicker: false,

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

      minDate: new Date(2010, 0, 1),
      maxDate: new Date(),
      maxlxLog: "",
    };
  },

  components: {},
  computed: {
    _lastRefundDate() {
      return this.lastRefundDate.Format("yyyy-MM-dd");
    },
  },

  beforeMount() {
    this.befor202008Month();
  },

  mounted() {
    if (this.$route.query.p) {
      this.member.phone = this.$route.query.p;
    }
  },

  methods: {
    formatter(type, val) {
      if (type === "year") {
        return `${val}年`;
      } else if (type === "month") {
        return `${val}月`;
      } else if (type === "day") {
        return `${val}日`;
      }
      return val;
    },
    befor202008Month() {
      let monDiff = (endTime, startTime) => {
        startTime = startTime.split("-");
        // 得到月数
        startTime = parseInt(startTime[0]) * 12 + parseInt(startTime[1]);
        // 拆分年月日
        endTime = endTime.split("-");
        // 得到月数
        endTime = parseInt(endTime[0]) * 12 + parseInt(endTime[1]);
        var m = endTime - startTime;

        return m;
      };
      let r = {
        befor: 0, //2020-08之前已还期数
        after: 0, //2020-08之后已还的期数
        overdueTotal: 0, //逾期期数
      };

      let lastHuankuan = this._lastRefundDate; //最后一次还款

      let cur_202008 = monDiff(lastHuankuan, "2020-08-20"); //当前时间到2020-08的期数

      //如果是2020-08-20号后 且还款日是20号后,则17的多算一天
      if (Number(this._lastRefundDate.split("-")[2]) >= 20 && cur_202008 > 0) {
        cur_202008 = cur_202008 + 1; //如果是20号后的少算一个月
      }
      if (cur_202008 < 0) cur_202008 = 0;

      let repaymentCycle = Number(this.repaymentCycle);
      if (repaymentCycle > cur_202008) {
        r.befor = repaymentCycle - cur_202008;
        r.after = repaymentCycle - r.befor;
      } else {
        r.after = repaymentCycle;
      }

      //计算逾期, 注意这里没有区分8月20号之前的
      let overdueTotal = monDiff(new Date().Format("yyyy-MM-dd"), lastHuankuan);
      if (overdueTotal > 0) {
        //计算当月算不算逾期, 如果本月还没到还款日, 则本月不算
        if (
          Number(
            new Date().Format("yyyy-MM-dd").split("-")[2] <
              Number(this._lastRefundDate.split("-")[2])
          )
        ) {
          overdueTotal -= 1;
        }
      }
      if (overdueTotal < 0) overdueTotal = 0;
      r.overdueTotal = overdueTotal;
      console.log("overdueTotal", overdueTotal);

      console.log(r);
      return r;
    },

    jsq_yflx(jixibenjin, qishu, meiyuehuankuan, lili) {
      let yinhuanlixiTotal = 0;
      for (var i = 0; i < qishu; i++) {
        let yinhuanlixi = (jixibenjin * lili) / 100 / 12;
        let yindibenji = meiyuehuankuan - yinhuanlixi;

        jixibenjin = jixibenjin - yindibenji;

        if (yinhuanlixi > 0) {
          yinhuanlixiTotal += yinhuanlixi;
        }
      }
      return {
        lx: Math.round(yinhuanlixiTotal),
        shengyubenjin: jixibenjin,
      };
    },
    jsq_yuqulx() {
      let r = this.befor202008Month();
      let daozhangjine = Number(this.borrowAmount);
      let befor_lixi = this.jsq_yflx(
        daozhangjine,
        r.befor,
        Number(this.cycleAmount),
        24
      );

      let after_lixi = this.jsq_yflx(
        befor_lixi.shengyubenjin,
        r.after,
        Number(this.cycleAmount),
        17
      );
      console.log("after_lixi",after_lixi)
      let overdue_lixi = this.jsq_yflx(
        after_lixi.shengyubenjin,
        r.overdueTotal,
        Number(this.cycleAmount),
        17
      );
      console.log("overdue_lixi",overdue_lixi)
      return  overdue_lixi.lx;
      // let monDiff = (endTime, startTime) => {
      //   startTime = startTime.split("-");
      //   // 得到月数
      //   startTime = parseInt(startTime[0]) * 12 + parseInt(startTime[1]);
      //   // 拆分年月日
      //   endTime = endTime.split("-");
      //   // 得到月数
      //   endTime = parseInt(endTime[0]) * 12 + parseInt(endTime[1]);
      //   var m = endTime - startTime;
      //   return m;
      // };
      // let cur_202008 = monDiff(new Date().Format("yyyy-MM-dd"), "2020-08-19");
      // let beforyuqi = Number(this.overdueCycle) - cur_202008;
      // if (beforyuqi < 0) beforyuqi = 0;
      // let afteryuqi = Number(this.overdueCycle) - beforyuqi;

      // let lixi = this.jsq_yflx(
      //   Number(this.borrowAmount),
      //   Number(this.repaymentCycle),
      //   Number(this.cycleAmount),
      //   24
      // );

      // let beforyuqiAmount = ((lixi.shengyubenjin * 24) / 100 / 12) * beforyuqi;
      // let afteryuqiAmount = ((lixi.shengyubenjin * 17) / 100 / 12) * beforyuqi;
      // console.log(beforyuqiAmount, afteryuqiAmount);

      // console.log(
      //   "逾期",
      //   beforyuqi,
      //   afteryuqi,
      //   lixi,
      //   beforyuqiAmount,
      //   afteryuqiAmount
      // );
      // this.maxlxLog += `8月20号前逾期利息${beforyuqiAmount}<br\>`;
      // this.maxlxLog += `8月20号后逾期利息${afteryuqiAmount}<br\>`;
      // this.maxlxLog += `合计逾期利息${beforyuqiAmount + afteryuqiAmount}<br\>`;
      // return beforyuqiAmount + afteryuqiAmount;
    },
    jsq_minjmlx() {
      let hetongjine = Number(this.cycleAmount) * Number(this.cycle);
      let daozhangjine = Number(this.borrowAmount);

      let yingfulixi = this.yflx;

      // var yuqi = this.jsq_yuqulx();
      // console.log(yuqi);
      let jm = hetongjine - daozhangjine - yingfulixi;

      return Math.round(jm);
    },
    jsq_maxjmlx() {
      let htje = Number(this.cycleAmount) * Number(this.cycle);
      let bj = Number(this.borrowAmount);

      //计算已付利息
      let r = this.befor202008Month();
      let befor_lixi = this.jsq_yflx(
        Number(this.borrowAmount),
        r.befor,
        Number(this.cycleAmount),
        24
      );
      let after_lixi = this.jsq_yflx(
        befor_lixi.shengyubenjin,
        r.after,
        Number(this.cycleAmount),
        17
      );
      let yhlx = befor_lixi.lx + after_lixi.lx;
      console.log("1111合同金额", htje, bj);
      console.log("1111已付利息", befor_lixi.lx, after_lixi.lx, yhlx);

      var yuqi = this.jsq_yuqulx();
      console.log("1111逾期", yuqi);
      let jm = htje - bj - yhlx - yuqi;

      return Math.round(jm);
    },
    jsq_lx() {
      let r = this.befor202008Month();
      let daozhangjine = Number(this.borrowAmount);
      let befor_lixi = this.jsq_yflx(
        daozhangjine,
        r.befor,
        Number(this.cycleAmount),
        24
      );

      let after_lixi = this.jsq_yflx(
        befor_lixi.shengyubenjin,
        Number(this.cycle) - r.befor,//除了8月20号前的, 剩余都是17%
        Number(this.cycleAmount),
        17
      );

      let yingfulixi = befor_lixi.lx + after_lixi.lx ;
     
      return Math.round(yingfulixi);
    },
    btnClikc() {
      this.maxlxLog = "";
      if (this.phone.length != 11) {
        this.option.errPhone = "手机号格式错误";
        return;
      }
      if (this.cycle * this.cycleAmount < this.borrowAmount) {
        this.option.errorMessage = "期数乘每期金额应大于到账总额";
        return;
      }

      this.yflx = this.jsq_lx();
      this.jmlx_min = this.jsq_minjmlx();
      this.jmlx_max = this.jsq_maxjmlx();

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
        lastRefundDate: this._lastRefundDate,
      });

      this.option.btnShow = false;
    },
    btnRest() {
      this.option.btnShow = true;
      this.name = "";
      this.phone = "";
      this.borrowCompany = "";
      this.borrowAmount = "";
      this.cycle = 36;
      this.cycleAmount = "";
      this.repaymentCycle = "";
      this.overdueCycle = "0";
      this.lastRefundDate = new Date();
    },
    pickerConfirmHandle() {
      let monDiff = (endTime, startTime) => {
        startTime = startTime.split("-");
        // 得到月数
        startTime = parseInt(startTime[0]) * 12 + parseInt(startTime[1]);
        // 拆分年月日
        endTime = endTime.split("-");
        // 得到月数
        endTime = parseInt(endTime[0]) * 12 + parseInt(endTime[1]);
        var m = endTime - startTime;
        return m;
      };
      this.showPicker = false;

      this.overdueCycle =
        monDiff(new Date().Format("yyyy-MM"), this._lastRefundDate) - 1;
      if (this.overdueCycle < 0) this.overdueCycle = 0;
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