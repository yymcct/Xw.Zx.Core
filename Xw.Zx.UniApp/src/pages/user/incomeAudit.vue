<template v-cloak>
  <view class="container">
    <view class="uni-triplex-row crd card" v-for="(iteam,index) in incomeDetail" v-bind:key="index">
      <view class="uni-triplex-left">
        <view class="uni-title uni-ellipsis">申请人: {{iteam.memberDto.realName}}</view>
        <view class="uni-title uni-ellipsis">时间: {{iteam.detailsDto.addTime}}</view>
        <view class="uni-title uni-ellipsis">电话: {{iteam.memberDto.phone}}</view>
        <view class="uni-title uni-ellipsis">地址: {{iteam.memberDto.address}}</view>
        <view class="uni-title uni-ellipsis">状态: {{iteam.detailsDto.withdrawDepositStateName}}</view>
        <view v-if="iteam.detailsDto.withdrawDepositState==3 || iteam.detailsDto.withdrawDepositState==2" class="uni-title uni-ellipsis">
          备注:
          <text>{{iteam.detailsDto.remark}}</text>
        </view>
        <view class="auditbtn" v-if="iteam.detailsDto.withdrawDepositState ==0 ">
          <button
            type="primary"
            hover-class="none"
            v-on:click="postAuditWithdrawDepositdetail(iteam.detailsDto.timestamp, false)"
          >拒绝</button>
          <button
            type="primary"
            class="primary"
            v-on:click="postAuditWithdrawDepositdetail(iteam.detailsDto.timestamp, true)"
          >通过</button>
		  <button
		    type="primary"
		    hover-class="none"
			v-on:click="bindClick(iteam.memberDto.id)"
		  >收益</button>
        </view>
      </view>
      <view class="uni-triplex-right">
        <text class="amount">{{iteam.detailsDto.amount}}</text>
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
      incomeDetail: null
    };
  },
  methods: {
    getAuditWithdrawDepositdetails: function() {
      uni.request({
        url: `${this.baseUrl}/api/WithdrawDeposit/GetAuditWithdrawDepositdetails`,
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
    },
    postAuditWithdrawDepositdetail: function(timestamp, ispass) {
      var that = this;
      uni.request({
        url: `${this.baseUrl}/api/WithdrawDeposit/AuditWithdrawDepositdetail`,
        data: {
          timestamp: timestamp,
          ispass: ispass
        },
        method: "POST",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          that.getAuditWithdrawDepositdetails();
          if (res.data.statusCode == 200) {
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
	bindClick: function(id) {
	  uni.navigateTo({
	    url:`../user/myteamsecond1?memberid=${id}`
	  });
	},
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
    this.getAuditWithdrawDepositdetails();
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

.auditbtn {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
}
.auditbtn button {
  width: 100%;
  margin: 10px;
}
</style>