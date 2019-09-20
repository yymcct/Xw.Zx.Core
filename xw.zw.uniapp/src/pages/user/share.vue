<template>
  <view class="container">
    <view class="dsq">
      您想知道您被银行多收过钱吗?<br/>扫码注册送钱宝,立刻知道被银行多收了多少钱!
    </view>
    <view class="qrimg">
      <view class="qrimg-i">
        <tki-qrcode
          cid="qrcode1"
          ref="qrcode"
          :val="val"
          :size="size"
          :onval="onval"
          :loadMake="loadMake"
          :usingComponents="true"
        />
      </view>
    </view>
    <view class="title">
      <view class="uni-title">请截图分享</view>
    </view>
  </view>
</template>
<script>
import tkiQrcode from "@/components/tki-qrcode/tki-qrcode.vue";
export default {
  data() {
    return {
      user: null,
      val: "",
      size: 400, // 二维码大小
      onval: true, // val值变化时自动重新生成二维码
      loadMake: true, // 组件加载完成后自动生成二维码
      src: "" // 二维码生成后的图片地址或base64
    };
  },
  methods: {},
  components: {
    tkiQrcode
  },
  onLoad: function() {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
    this.val = `${
      this.baseUrl
    }/#/pages/reg/invitereg?inviteid=${this.user.id.toString()}`;

    console.log(this.val);
  }
};
</script>

<style>
/* @import "../../../common/icon.css"; */
.container {
  display: flex;
  flex-direction: column;
  padding: 20px;
}
.dsq{
  font-size: 18px;
  font-weight: bold;
}

.qrimg {
  display: flex;
  justify-content: center;
  margin-top: 30px;
}
.title {
  display: flex;
  justify-content: center;
  width: 100%;
}
.uni-title {
  display: block;
  color: #999;
}
</style>
