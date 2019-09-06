<template>
  <view class="content">
    <swiper
      class="swiper"
      indicator-dots
      :autoplay="autoplay"
      :interval="interval"
      :duration="duration"
    >
      <swiper-item>
        <view class="swiper-item uni-flex uni-column">
          <view class>
            <text>火爆农资招商网</text>
          </view>
          <view class="zlj">
            <text>2019-08-09</text>
          </view>
        </view>
      </swiper-item>
    </swiper>
    <view class="uni-flex uni-column">
      <view class="bank uni-row uni-flex">
        <text>123</text>
        <text>123</text>
        <text>123</text>
        <text>123</text>
      </view>
    </view>

    <view class="uni-padding-wrap uni-common-mt">
      <view class="uni-btn-v">
        <navigator url="../cards/addcard" hover-class="navigator-hover">
          <button type="default">添加新卡片</button>
        </navigator>
      </view>
    </view>
  </view>
</template>

<script>
export default {
  data() {
    return {
      title: "测试"
    };
  },
  methods: {},
  onLoad: function(opthions) {
    var loginRes = this.getUser("../main/main");
    if (!loginRes) {
      return false;
    }
  }
};
</script>

<style>
.swiper {
  height: 200px;
  background-color: white;
  border-radius: 10px;
}
.swiper-item {
  height: 100%;
  display: flex;
  justify-content: center; /* 水平居中 */
  align-items: center; /* 垂直居中 */
}
.zlj {
  color: tomato;
  font-weight: bolder;
  font-size: 20px;
}
.bank {
  height: 80px;
  background: white;
  margin-top: 20px;

  align-items: center; /* 垂直居中 */
}
.bank text {
  width: 25%;
}
</style>
