
<template>
  <view>
    <view class="heard">
      <view class="title">账户利息合计:</view>
      <view class="total">{{heardInfo.overdueFine}}元</view>
      <view class="zhuixi">
        <button type="primary" size="mini" @click="zhuxi">申请追息</button>
      </view>
    </view>
    <view
      v-for="iteam in cardList"
      v-bind:key="iteam.id"
      class="card"
      @click="bindClick(iteam.cardNum)"
    >
      <uni-swipe-action :options="options1">
        <view class="uni-triplex-row crd">
          <view class="uni-triplex-left">
            <text class="uni-title uni-ellipsis">{{getBankName(iteam.bank)}}</text>
            <text class="uni-text">卡号: {{iteam.cardNum}}</text>
            <text
              class="uni-text-small uni-ellipsis"
              v-if="!iteam.lastSyncIsOk"
            >状态: 同步失败 {{iteam.lastSyncTime}}</text>
            <text
              class="uni-text-small uni-ellipsis"
              v-if="iteam.lastSyncIsOk"
            >状态: 已同步 {{iteam.lastSyncTime}} 利息:{{iteam.overdueFine}}</text>
          </view>
          <view class="uni-triplex-right">
            <text class="uni-h5"></text>
          </view>
        </view>
      </uni-swipe-action>
    </view>
    <view class="btn">
      <button type="primary" @click="getBankCard">更新账单</button>
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
      user: null,
      btnEnable: true,
      options1: [
        {
          text: "删除",
          style: {
            backgroundColor: "#dd524d"
          }
        }
      ],
      cardList: [],
      heardInfo: null
    };
  },
  methods: {
    bindClick(value) {
      uni.navigateTo({
        url: `../cards/carddetail?cardnum=${value}`
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
          return "广大银行";
        case 5:
          return "华夏银行";
        case 6:
          return "民生银行";
      }
    },
    zhuxi: function name() {
      uni.showModal({
        title: "提示",
        content: "您的申请已收到, 客服稍后回访,请保持电话畅通!",
        success: function(res) {
          if (res.confirm) {
          } else if (res.cancel) {
          }
        }
      });
    },
    getBankCard: function() {
      uni.request({
        url: `${this.baseUrl}/api/BankCard/Gets?&sorts=id&Page=1&PageSize=100`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            this.cardList = res.data.result;
            // console.log(this.cardList);
            if (this.cardList.length == 0) {
              uni.showModal({
                title: "提示",
                content: "请添加银行卡!",
                success: function(res) {
                  if (res.confirm) {
                    uni.navigateTo({ url: "../cards/addcard" }); //TODO 导航下载APP
                  }
                }
              });
            }
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
  },
  onLoad: function() {
    this.user = this.getUser("../main/main");
    console.log(this.user);
    if (!this.user) {
      return false;
    }
    this.getBankCard();
    uni.request({
      url: `${this.baseUrl}/api/BankCard/GetCardTotal`,
      method: "GET",
      header: {
        "Content-Type": "application/json",
        Authorization: `Bearer ` + this.user.token
      },
      success: res => {
        if (res.data.statusCode == 200) {
          this.heardInfo = res.data.result;
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
  height: 150px;
  padding-left: 20px;
  padding-right: 20px;
}
.tbn button {
  width: 80%;
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
.zhuixi {
  display: flex;
  flex-direction: row-reverse;
  justify-content: flex-start;
}
.zhuixi button {
  margin-top: 10px;
  display: block;
  margin: 0px;
}
</style>