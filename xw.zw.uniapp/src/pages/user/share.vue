<template>
  <view class="container">
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
      <view class="uni-title">请截图后分享</view>
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
  width: 100%;
}

.qrimg {
  display: flex;
  justify-content: center;
  margin-top: 50px;
}
.title {
  display: flex;
  justify-content: center;
  width: 100%;
}
.uni-title {
  display: block;
}
</style>
