<template>
  <div class="wrapper" v-if="isPay">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>
    <div class="banner">
      <img
        :src="require('@/assets/images/home/banner.jpg')"
        style="display: block; width: 100%; height: auto"
      />
    </div>
    <div class="wrapper-title">法律债务处理大礼包课程</div>
    <div class="audio">
      <vue-audio
        ref="audio"
        class="audio-audio"
        :audio-source="curPlayUrl"
      ></vue-audio>
      <div class="title">怎样应对催收(音频课程)</div>
      <div class="list">
        <div
          class="item"
          v-for="(item, index) in music"
          :key="index"
          @click="play(item.url)"
          :class="{ active: curPlayUrl == item.url }"
        >
          {{ item.name }}
        </div>
      </div>
    </div>

    <div class="pdf">
      <div class="pdf-title">债务问题100问</div>
      <div class="pdf-download">
        <van-button type="primary" class="pdf-download-btn" @click="btnDownload"
          >点击下载(请用pdf阅读器查看)</van-button
        >
      </div>
    </div>

    <div class="linkman">
      <div class="linkman-title">债减减专属客服</div>
      <div class="linkman-content">
        <div class="item">
          <div class="ercode">
            <img
              :src="require('../../../../assets/images/kf11.jpg')"
              alt="/static/kf11.png"
            />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="#">债减减客服1线</a>
          </div>
        </div>
        <div class="item">
          <div class="ercode">
            <img src="/static/kf12.jpg" alt="/static/kf12.jpg" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="#">债减减客服2线</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "chapter",
  props: [""],
  data() {
    return {
      isPay: false,
      curPlayUrl: "/static/1.mp3",
      music: [
        {
          name: "第一课 民间借贷法律法规",
          url: "/static/1.mp3",
        },
        {
          name: "第二课 催收的四大套路",
          url: "/static/2.mp3",
        },
        {
          name: "第三课 如何因对电话骚扰",
          url: "/static/3.mp3",
        },
        {
          name: "第四课 如何应对上门催收",
          url: "/static/4.mp3",
        },
        {
          name: "第五课 如何应对高额违约金",
          url: "/static/5.mp3",
        },
        {
          name: "第六课 征信和失信的区别",
          url: "/static/6.mp3",
        },
        {
          name: "第七课 是否构成刑事责任",
          url: "/static/7.mp3",
        },
      ],
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    const _this = this;
    api.order
      .gets({
        Filters: "ProductId==10,OrderState==1",
      })
      .then((res) => {
        if (res.result.length > 0) {
          _this.isPay = true;
        }
      });
  },

  mounted() {},

  methods: {
    play(url) {
      console.log(url);
      this.$refs.audio.stop();
      this.curPlayUrl = url;
      let _this = this;
      setTimeout(() => {
        _this.$refs.audio.play();
      }, 1000);
    },
    btnDownload() {
      window.open("/static/100ask.pdf");
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper-title {
  text-align: center;
  background-color: #fff;
  margin: 20px;
  font-weight: bold;
}
.wrapper-title-t {
  background-color: #fff;
  text-align: center;
  font-size: 20px;
  padding-top: 10px;
  padding-bottom: 20px;
  color: #777;
}
.wrapper {
  background-color: #fff;
  .audio {
    width: 100%;
    background-color: #fff;
    &-audio {
      width: 90% !important;
      //background-image: linear-gradient(90deg,#ff7a00,#ff5000);
    }
    .title {
      margin-top: 20px;
      margin-left: 10px;
      font-weight: bold;
    }
    .list {
      padding: 10px 20px;
      //background-color: rgb(247, 246, 244);
      .item {
        font-size: 18px;
        background-color: #fff;
        height: 50px;
        line-height: 50px;
      }

      .item:not(:last-child) {
        border-bottom: 1px solid #ebedf0;
      }
    }
  }

  .pdf {
    margin-top: 20px;
    background-color: #fff;
    &-title {
      padding-top: 20px;
      margin-left: 10px;
      font-weight: bold;
    }
    &-download {
      padding: 30px 20px;
      &-btn {
        width: 100%;
      }
    }
  }

  .linkman {
    margin-top: 20px;
    background-color: #fff;
    &-title {
      padding-top: 20px;
      margin-left: 10px;
      font-weight: bold;
    }
    &-content {
      padding: 30px 20px;
      .item {
        width: 100%;
        padding-bottom: 50px;
        .ercode {
          padding: 0px 40px;
          img {
            width: 100%;
          }
          p {
            text-align: center;
            color: #777;
            font-size: 16px;
            padding-top: 10px;
          }
        }

        .link {
          display: flex;
          align-items: center;
          justify-content: center;
          .link-tel {
            color: #000;
            text-align: center;
            font-size: 18px;
            font-weight: bold;
            padding-top: 10px;
          }
        }
      }
    }
  }
}
.active {
  font-weight: bold;
  color: #ff5000;
}
</style>