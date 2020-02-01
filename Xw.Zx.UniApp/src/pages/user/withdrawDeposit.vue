<template>
  <view class="content">
    <view class="input-group">
      <view class="input-row border">
        <text class="title">金额：</text>
        <m-input type="text" focus clearable v-model="getamount" placeholder="请输入提现金额,最小2.1元"></m-input>
      </view>
    </view>
    <view class="border">
      <text class="sxf">提现手续费: 2元/笔</text>
    </view>
    <view class="btn-row">
      <button type="primary" hover-class="none" class="primary" @tap="edit">提交</button>
    </view>
  </view>
</template>

<script>
import mInput from "../../components/m-input.vue";
import QSpicker from "@/components/QuShe-picker/QuShe-picker.vue";
export default {
  components: {
    mInput,
    QSpicker
  },
  data() {
    return {
      user: null,
      canget: 0,
      getamount: 0
    };
  },
  methods: {
    edit() {

      if (parseFloat(this.canget)  < parseFloat(this.getamount) || parseFloat(this.getamount)  < parseFloat(2.1)) {       
        uni.showToast({
          icon: "none",
          title: `最大提现金额为${this.canget}元, 最小为2.1元`
        });
        return;
      }
      uni.request({
        url: `${this.baseUrl}/api/WithdrawDeposit/PostWithdrawDeposit`,
        data: {
          Amount: this.getamount
        },
        method: "POST",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            uni.showModal({
              title: "提示",
              content: "我们已收到您的申请,请在提现记录中查看提现进度",
              success: function(res) {
                if (res.confirm) {
                  uni.navigateBack();
                } else if (res.cancel) {
                  console.log("用户点击取消");
                }
              }
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
    }
  },
  onLoad: function(option) {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
    this.canget = option.canget;
    this.getamount = option.canget;
  }
};
</script>

<style>
.sxf{
  margin-top: 20px;
  margin-left: 10px;
  color:darkgrey;
}
</style>