
<template>
  <view>
    <view class="heard">
      <view class="bank">
        <view class="bank_title">{{getBankName(bankInfo.bank)}}</view>
        <view>卡号:{{bankInfo.cardNum}}</view>
      </view>
      <view class="title">利息:</view>
      <view class="total">{{bankInfo.overdueFine}}元</view>
      <view class="state">
        <view v-if="bankInfo.lastSyncIsOk">{{bankInfo.lastSyncTime}}</view>
        <view style="font-weight: bold;">{{bankInfo.lastSyncIsOk?' 已同步':' 未同步'}}</view>
      </view>
    </view>
    <view
      v-for="iteam in cardBills"
      v-bind:key="iteam.id"
      class="card"
      @click="bindClick(iteam.id)"
    >
      <uni-swipe-action :options="options1">
        <view class="uni-triplex-row crd">
          <view class="uni-triplex-left">
            <text class="uni-title uni-ellipsis">账单: {{iteam.cycleStop}}</text>
            <text class="uni-text">利息: {{iteam.overdueFine}}元</text>
            <text class="uni-text">账单金额: {{iteam.newBalance}}元 | 还款日:{{iteam.paymentDueData}}</text>
          </view>
          <view class="uni-triplex-right">
            <text class="uni-h5"></text>
          </view>
        </view>
      </uni-swipe-action>
    </view>
    <view class="btn">
      <view>
        <button type="warn" @click="delBankCard">删除</button>
      </view>
      <view>
        <button :disabled="!btnEnable" type="primary" @click="oneKeySync">{{btnText}}</button>
      </view>
    </view>
  </view>
</template>

<script>
import uniSwipeAction from "@/components/uni-swipe-action/uni-swipe-action.vue";

export default {
  components: {
    uniSwipeAction
  },
  data() {
    return {
      cardId: 0,
      btnText: "同步",
      btnEnable: true,
      options1: [
        {
          text: "删除",
          style: {
            backgroundColor: "#dd524d"
          }
        }
      ],
      cardBills: [],
      bankInfo: {
        cardNum: "",
        bank: 0
      },
      user: null
    };
  },
  methods: {
    bindClick(value) {
      console.log(value);
      // uni.showToast({
      //   title: `TODO 页面${value}`,
      //   icon: "none"
      // });
    },
    oneKeySync() {
      uni.request({
        url: `${this.baseUrl}/api/BankCard/Sync`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            this.btnEnable = false;
            this.btnText = "同步中,约5分钟..";
          } else {
            uni.showToast({
              icon: "none",
              title: res.data.msg
            });
          }
        },
        fail: () => {
          uni.showToast({
            icon: "none",
            title: "网络异常"
          });
        }
      });
    },
    delBankCard() {

      uni.request({
        url: `${this.baseUrl}/api/BankCard/Delete?id=${this.cardId}`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            uni.reLaunch({
              url: "../main/main"
            });
          } else {
            uni.showToast({
              icon: "none",
              title: res.data.msg
            });
          }
        },
        fail: () => {
          uni.showToast({
            icon: "none",
            title: "网络异常"
          });
        }
      });
    },
    getBankName(bankid) {
      switch (bankid) {
        case 0:
          return "招商银行";
        case 1:
          return "浦发银行";
        case 2:
          return "中信银行";
        case 3:
          return "平安银行";
        case 4:
          return "光大银行";
        case 5:
          return "华夏银行";
        case 6:
          return "民生银行";
      }
    }
  },
  onLoad: function(opthion) {
    this.user = this.getUser("../main/main");
    if (!this.user) {
      return false;
    }
    this.cardId = opthion.id;

    uni.request({
      url: `${this.baseUrl}/api/BankCard/GetCardContent?id=${opthion.id}`,
      method: "GET",
      header: {
        "Content-Type": "application/json",
        Authorization: `Bearer ` + this.user.token
      },
      success: res => {
        if (res.data.statusCode == 200) {
          this.bankInfo = res.data.result.bankInfo;
          this.cardBills = res.data.result.cardBills;
        } else {
          uni.showToast({
            icon: "none",
            title: res.data.msg
          });
        }
      },
      fail: () => {
        uni.showToast({
          icon: "none",
          title: "网络异常"
        });
      }
    });
  }
};
</script>

<style>
.heard {
  height: 200px;
  background-color: white;
  border-radius: 10px;
  margin: 10px;
  display: -webkit-flex;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 10px;
}
.card {
  margin-bottom: 20px;
}
.btn {
  margin-bottom: 100px;
  display: flex;
  flex-direction: row;
  justify-content: center;
}
.btn view {
  width: 50%;
  margin: 10px;
}

.title {
  font-weight: bold;
  font-size: 20px;
}
.total {
  font-weight: bold;
  font-size: 50px;
  margin-left: 50px;
  color: rgb(250, 81, 2);
}
.bank {
  display: flex;
  flex-direction: row;
  color: #999999;
  align-items: baseline;
}
.bank .bank_title {
  font-size: 20px;
}

.bank view {
  margin-right: 10px;
}
.state {
  display: flex;
  display: -webkit-flex;
  color: #999999;
  flex-direction: row;
  justify-content: flex-end;
}
.state view {
  margin-left: 10px;
}
</style>