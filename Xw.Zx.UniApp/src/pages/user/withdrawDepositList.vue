<template v-cloak>
  <view class="container">
    <view class="uni-triplex-row crd card" v-for="iteam in incomeDetail" v-bind:key="iteam.id">
      <view class="uni-triplex-left">
        <view class="uni-title uni-ellipsis">
          {{iteam.addTime}} {{iteam.incomeAccountTypeName}}          
        </view>
        <text class="uni-text">状态:{{iteam.withdrawDepositStateName}}</text>
         <view v-if="iteam.withdrawDepositState==3" class="uni-title uni-ellipsis"><text>备注:  {{iteam.remark}}</text></view>
      </view>
      <view class="uni-triplex-right">
        <text class="amount">{{iteam.amount}}</text>
      </view>
    </view>
  </view>
</template>
<script>
import uniList from "@/components/uni-list/uni-list.vue";
import uniListItem from "@/components/uni-list-item/uni-list-item.vue";
export default {
  data() {
    return {
      user: null,
      mySelf: null,
      myTeamDto: null,
      incomeDetail: null,
      items: ["已注册", "待绑定"],
      current: 0,
      icon: {
        person: {
          color: "#007aff",
          size: "22",
          type: "person"
        },
        paperplane: {
          color: "#007aff",
          size: "22",
          type: "paperplane"
        }
      }
    };
  },
  methods: {
    getWithdrawDepositdetails: function() {
      uni.request({
        url: `${this.baseUrl}/api/WithdrawDeposit/GetWithdrawDepositdetails`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            this.incomeDetail = res.data.result;
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
  components: {
    uniList,
    uniListItem
  },
  onLoad: function() {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
    this.getWithdrawDepositdetails();
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
  background-color: white;
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
.amount {
  font-size: 30px;
  font-weight: bolder;
  margin-left: 5px;
  margin-right: 5px;
  color: coral;
}
</style>