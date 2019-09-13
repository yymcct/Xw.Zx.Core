
<template>
  <view>
    <view class="heard">
      <view class="title">账户利息合计:</view>
      <view class="total">{{heardInfo.overdueFine}}元</view>
    </view>
    <view v-for="iteam in cardList" v-bind:key="iteam.id" class="card" @click="bindClick(iteam.id)">
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
      <button :disabled="!btnEnable" type="primary" @click="oneKeySync">{{btnText}}</button>
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
      btnText: "一键同步",
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
        url: `../cards/content?id=${value}`
      });
    },
    oneKeySync() {
      let user = this.getUser("../main/main");
      uni.request({
        url: `${this.baseUrl}/api/BankCard/Sync`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            this.btnEnable = false;
            this.btnText = "努力同步中,约10分钟..";
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
          return "广大银行";
        case 5:
          return "华夏银行";
        case 6:
          return "民生银行";
      }
    }
  },
  onLoad: function() {
    let user = this.getUser("../main/main");
    console.log(user);
    if (!user) {
      return false;
    }

    uni.request({
      url: `${this.baseUrl}/api/BankCard/Gets?&sorts=id&Page=1&PageSize=100`,
      method: "GET",
      header: {
        "Content-Type": "application/json",
        Authorization: `Bearer ` + user.token
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

    uni.request({
      url: `${this.baseUrl}/api/BankCard/GetCardTotal`,
      method: "GET",
      header: {
        "Content-Type": "application/json",
        Authorization: `Bearer ` + user.token
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
</style>