
<template>
  <view>
    <view v-for="iteam in cardList" v-bind:key="iteam.id">
      <uni-swipe-action :options="options1" @click="bindClick">
        <view class="uni-triplex-row crd">
          <view class="uni-triplex-left">
            <text class="uni-title uni-ellipsis">列表主标题</text>
            <text class="uni-text">列表副标题</text>
            <text class="uni-text-small uni-ellipsis">列表内容文字,列表内容文字,列表内容文字,列表内容文字,列表内容文字,列表内容文字</text>
          </view>
          <view class="uni-triplex-right">
            <text class="uni-h5">12:15</text>
          </view>
        </view>
      </uni-swipe-action>
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
      options1: [
        {
          text: "删除",
          style: {
            backgroundColor: "#dd524d"
          }
        }
      ],
      cardList: []
    };
  },
  methods: {
    bindClick(value) {
      uni.showToast({
        title: `点击了${value.text}按钮`,
        icon: "none"
      });
    }
  },
  onLoad: function() {
    console.log('AAAAAAAAAAAAAAA');
    let user =  this.getUser("../user/user");
      console.log(user);
    if (!user) {
      return false;
    }
  
    uni.request({
      url:
        "http://localhost:63836/api/BankCard/Gets?&sorts=id&Page=1&PageSize=100",
      method: "GET",
      header: {
        "Content-Type": "application/json",
        "Authorization": `Bearer `+ user.token
      },
      success: res => {
        if (res.data.statusCode == 200) {
          this.cardList = res.data.result;
          console.log(this.cardList);
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
</style>