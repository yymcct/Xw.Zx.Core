<template>
  <view class="warp">
    <view class="example-body">
      <uni-grid :column="3" :show-border="false" :square="false" @change="change">
        <uni-grid-item :hor="35" :ver="-45" marker="badge" type="error" >
          <image
            class="image"
            src="../../static/img/membercard.png"
            mode="aspectFill"
          />
          <text class="text">办卡</text>
        </uni-grid-item>
        <uni-grid-item
          :hor="40"
          :ver="-55"
          :img-width="30"
          marker="image"
          src=""
        >
          <image
            class="image"
            src="../../static/img/money.png"
            mode="aspectFill"
          />
          <text class="text">提额</text>
        </uni-grid-item>
        <uni-grid-item>
          <image
            class="image"
            src="../../static/img/test.png"
            mode="aspectFill"
          />
          <text class="text">信用评估</text>
        </uni-grid-item>
        <uni-grid-item>
          <image
            class="image"
            src="../../static/img/going.png"
            mode="aspectFill"
          />
          <text class="text">追息</text>
        </uni-grid-item>
        <uni-grid-item>
          <image
            class="image"
            src="../../static/img/xy5.png"
            mode="aspectFill"
          />
          <text class="text">负债优化</text>
        </uni-grid-item>
        <uni-grid-item>
          <image
            class="image"
            src="../../static/img/xy6.png"
            mode="aspectFill"
          />
          <text class="text">企业信用</text>
        </uni-grid-item>
        <uni-grid-item>
          <image
            class="image"
            src="../../static/img/xy7.png"
            mode="aspectFill"
          />
          <text class="text">房产</text>
        </uni-grid-item>
        <uni-grid-item>
          <image
            class="image"
            src="../../static/img/xy8.png"
            mode="aspectFill"
          />
          <text class="text">测卡</text>
        </uni-grid-item>
        <uni-grid-item>
          <image
            class="image"
            src="../../static/img/xy9.png"
            mode="aspectFill"
          />
          <text class="text">贷款</text>
        </uni-grid-item>
      </uni-grid>
    </view>
  </view>
</template>

<script>
import uniGrid from "@/components/uni-grid/uni-grid.vue";
import uniGridItem from "@/components/uni-grid-item/uni-grid-item.vue";

export default {
  components: {
    uniGrid,
    uniGridItem
  },
  data() {
    return {
      list: [
        {
          url: "https://img-cdn-qiniu.dcloud.net.cn/uni-ui/grid-1.png",
          text: "新浪"
        },
        {
          url: "https://img-cdn-qiniu.dcloud.net.cn/uni-ui/grid-2.png",
          text: "微信"
        },
        {
          url: "https://img-cdn-qiniu.dcloud.net.cn/uni-ui/gird-3.png",
          text: "QQ"
        },
        {
          url: "https://img-cdn-qiniu.dcloud.net.cn/uni-ui/grid-6.png",
          text: "抖音"
        },
        {
          url: "https://img-cdn-qiniu.dcloud.net.cn/uni-ui/grid-7.png",
          text: "百度"
        },
        {
          url: "https://img-cdn-qiniu.dcloud.net.cn/uni-ui/grid-8.png",
          text: "支付宝"
        },
        {
          url: "https://img-cdn-qiniu.dcloud.net.cn/uni-ui/grid-9.png",
          text: "字节跳动"
        },
        {
          url: "https://img-cdn-qiniu.dcloud.net.cn/uni-ui/grid-4.png",
          text: "手机"
        },
        {
          url: "https://img-cdn-qiniu.dcloud.net.cn/uni-ui/grid-5.png",
          text: "工具"
        }
      ]
    };
  },
  methods: {
    change(e) {
      let { index } = e.detail;
      console.log(index);
    }
  }
};
</script>

<style>
page {
  display: flex;
  flex-direction: column;
  box-sizing: border-box;
  background-color: #efeff4;
}

view {
  font-size: 28upx;
  line-height: inherit;
}

.example {
  padding: 0 30upx 30upx;
}

.example-title {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-size: 32upx;
  color: #464e52;
  padding: 30upx;
  margin-top: 20upx;
  position: relative;
  background-color: #fdfdfd;
}

.example-title__after {
  position: relative;
  color: #031e3c;
}

.example-title:after {
  content: "";
  position: absolute;
  left: 0;
  margin: auto;
  top: 0;
  bottom: 0;
  width: 10upx;
  height: 40upx;
  border-top-right-radius: 10upx;
  border-bottom-right-radius: 10upx;
  background-color: #031e3c;
}

.example .example-title {
  margin: 40upx 0;
}

.example-body {
  border-top: 1px #f5f5f5 solid;
  padding: 30upx;
  background: #fff;
}

.example-info {
  padding: 30upx;
  color: #3b4144;
  background: #fff;
}

.image {
  width: 50upx;
  height: 50upx;
}

.text {
  font-size: 26upx;
  margin-top: 10upx;
}
</style>