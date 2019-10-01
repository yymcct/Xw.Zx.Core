
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
      @click="bindClick(iteam.bank)"
    >
      <uni-swipe-action :options="options1">
        <view class="uni-triplex-row crd">
          <view class="uni-triplex-left">
            <text class="uni-title uni-ellipsis">{{iteam.bankName}}</text>
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
            <text class="uni-h5">查看详情</text>
          </view>
        </view>
      </uni-swipe-action>
    </view>
    <view class="btn">
      <button type="primary" @click="syncBankCard">检查信用卡滞纳金</button>
    </view>
  </view>
</template>

<script>
import uniSwipeAction from "@/components/uni-swipe-action/uni-swipe-action.vue";
import uniIcon from "@/components/uni-icon/uni-icon.vue";
export default {
  components: {
    uniIcon,
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
        url: `../cards/carddetail?bank=${value}`
      });
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
            // if (this.cardList.length == 0) {
            //   uni.showModal({
            //     title: "提示",
            //     content: "立即导入银行卡?",
            //     success: function(res) {
            //       if (res.confirm) {
            //         uni.navigateTo({ url: "../user/web" }); //TODO 导航下载APP
            //       }
            //     }
            //   });
            // }
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
    getBankTotal: function() {
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
    },
    syncAsync: function(IsBefore) {
      this.isloading = true;
      var url = `${this.baseUrl}/api/Sync/SyncAsync?IsBefore=t`;
      if (IsBefore == true) {
        url += "?IsBefore=t";
      }
      uni.showLoading({
        title: "同步中"
      });
      uni.request({
        url: url,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            var msg = `同步完成,截至日期:${res.data.result.lastSyncTime}`;
            if (res.data.result.bankBillAmount == "0") {
              msg = `恭喜您!截止日期:${res.data.result.lastSyncTime},您的账户未发现滞纳金或利息!`;
            }
            uni.showToast({
              icon: "none",
              title: msg
            });
            // uni.showModal({
            //   title: "提示",
            //   content: `截止到${res.data.result.lastSyncTime}合计利息:${res.data.result.bankBillAmount},是否继续检测`,
            //   success: function(res) {
            //     if (res.confirm) {
            //      // this.syncAsync("t");
            //     } else if (res.cancel) {
            //       console.log("用户点击取消");
            //     }
            //   }
            // });
          } else {
            // uni.showToast({
            //   icon: "none",
            //   title: res.data.msg
            // });
            uni.navigateTo({ url: "../user/web" }); //TODO 导航下载APP
          }
          uni.hideLoading();
        },
        fail: () => {
          uni.showToast({
            icon: "none",
            title: "网络异常"
          });
          uni.hideLoading();
        }
      });
    },
    syncBankCard: function(IsBefore) {
      this.syncAsync();
    }
  },
  onLoad: function() {
    this.user = this.getUser("../main/main");
    console.log(this.user);
    if (!this.user) {
      return false;
    }
    this.getBankCard();
    this.getBankTotal();
  },

  onTabItemTap: function() {
    console.log("我显示了");
    this.getBankCard();
    this.getBankTotal();
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