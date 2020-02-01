<template v-cloak>
  <view class="container">
    <view class="uni-triplex-row crd card" v-for="iteam in incomeDetail" v-bind:key="iteam.id">
      <view class="uni-triplex-left">
        <view class="uni-title uni-ellipsis">
          {{iteam.addTime}} {{iteam.incomeAccountTypeName}}          
        </view>
        <text class="uni-text">备注:{{iteam.remark}}</text>
      </view>
      <view class="uni-triplex-right">
        <text class="amount">{{iteam.amount}}</text>
      </view>
    </view>
  </view>
</template>
<script>
import uniIcon from "@/components/uni-icon/uni-icon.vue";
import segmentedControl from "../../components/segmented-control/segmented-control";
import uniSwipeAction from "@/components/uni-swipe-action/uni-swipe-action.vue";
export default {
  data() {
    return {
      user: null,
      mySelf: null,
      incomeDetail: null,
      current: 0
    };
  },
  methods: {

  },
  components: {
    uniIcon,
    segmentedControl,
    uniSwipeAction
  },
  onLoad: function(opthion) {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
    uni.request({
      url: `${this.baseUrl}/api/Income/GetPeronDetails?memberId=${opthion.memberid}`,
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