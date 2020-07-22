<template>
  <div class="wrapper">
    <div class="wrapper-title">法律债务处理大礼包课程</div>
    <div class="wrapper-title-t">债减减</div>
    <div class="audio">
      <vue-audio ref="audio" class="audio-audio" :audio-source="curPlayUrl"></vue-audio>
      <div class="title">怎样应对催收(音频课程)</div>
      <div class="list">
        <div
          class="item"
          v-for="(item,index) in music"
          :key="index"
          @click="play(item.url)"
          :class="{ 'active': curPlayUrl==item.url }"
        >{{item.name}}</div>
      </div>
    </div>

    <div class="pdf">
      <div class="pdf-title">债务问题100问</div>
      <div class="pdf-download">
        <van-button type="primary" class="pdf-download-btn" @click="btnDownload">点击下载(请用pdf阅读器查看)</van-button>
      </div>
    </div>

    <div class="linkman">
      <div class="linkman-title">债减减专属客服</div>
      <div class="linkman-content">
        <div class="item">
          <div class="ercode">
            <img src="/static/kf11.png" alt="/static/kf11.png" />
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
      <!-- <div class="linkman-content">
        <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/linkman1.jpg" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">梁经理: 15878744581</a>
          </div>
        </div>
        <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/kf2.png" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">赖经理: 17878688933</a>
          </div>
        </div>

        <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/kf3.png" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">赖经理: 17878688933</a>
          </div>
        </div>

        <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/kf4.png" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">覃经理: 13877116647</a>
          </div>
        </div>

        <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/kf5.png" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">韦经理: 13877116647</a>
          </div>
        </div>
        <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/kf6.png" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">龙经理: 15289689676</a>
          </div>
        </div>
        <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/kf7.png" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">潘经理: 13627716084</a>
          </div>
        </div>
         <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/kf8.png" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">胡经理: 767157763</a>
          </div>
        </div>
         <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/linkman1.jpg" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">冯经理: 13788695064</a>
          </div>
        </div>
         <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/kf9.png" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">谢经理: 19175521131</a>
          </div>
        </div>
         <div class="item">
          <div class="ercode">
            <img src="/static/linkman1.jpg" alt="/static/kf10.png" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="tel:15878744581">罗经理: 13517719317</a>
          </div>
        </div>
      </div>-->
    </div>
  </div>
</template>

<script>
export default {
  name: "",
  props: [""],
  data() {
    return {
      curPlayUrl: "/static/1.mp3",
      music: [
        {
          name: "第一讲: 催收的四大套路",
          url: "/static/1.mp3"
          //"http://139.155.8.217:8081/mp3/1%E5%82%AC%E6%94%B6%E7%9A%84%E5%9B%9B%E5%A4%A7%E5%A5%97%E8%B7%AF.mp3"
        },
        {
          name: "第二讲: 如何应对电话骚扰",
          url: "/static/2.mp3"
          //"http://139.155.8.217:8081/mp3/2%E5%A6%82%E4%BD%95%E5%BA%94%E5%AF%B9%E7%94%B5%E8%AF%9D%E9%AA%9A%E6%89%B0.mp3"
        },
        {
          name: "第三讲: 如何应对上门催收",
          url: "/static/3.mp3"
          // "http://139.155.8.217:8081/mp3/3%E5%A6%82%E4%BD%95%E5%BA%94%E5%AF%B9%E4%B8%8A%E9%97%A8%E5%82%AC%E6%94%B6.mp3"
        },
        {
          name: "第四讲: 如何应对高额违约金",
          url: "/static/4.mp3"
          // "http://139.155.8.217:8081/mp3/4%E5%A6%82%E4%BD%95%E5%BA%94%E5%AF%B9%E9%AB%98%E9%A2%9D%E8%BF%9D%E7%BA%A6%E9%87%91.mp3"
        },
        {
          name: "第五讲: 如何应对诉讼",
          url: "/static/5.mp3"
          // "http://139.155.8.217:8081/mp3/5%E5%A6%82%E4%BD%95%E5%BA%94%E5%AF%B9%E8%AF%89%E8%AE%BC.mp3"
        },
        {
          name: "第六讲: 征信和失信",
          url: "/static/6.mp3"
          //"http://139.155.8.217:8081/mp3/6%E5%BE%81%E4%BF%A1%E5%92%8C%E5%A4%B1%E4%BF%A1.mp3"
        }
      ],
      kefus: [
        {
          categoryIteam: 5,
          name: "李经理",
          sequence: 0,
          phone1: "17329336031",
          phone2: "",
          photo:
            "https://app.huobaowang.com/UpLoad/jpg/2020-03-16/20200316172040216386.jpg", // require('@/assets/images/kf1.png'),
          qq: "",
          wxQrCode:
            "https://app.huobaowang.com/UpLoad/jpg/2020-03-16/20200316172040217669.jpg"
        }
      ]
    };
  },

  components: {},

  computed: {},

  beforeMount() {},

  mounted() {
    let key = localStorage.getItem("IS_VIP_KEY");
    if (!key) {
      window.location.href =
        "http://zjj.xtsuo.cn/app/index.php?i=1&c=entry&m=ewei_shopv2&do=mobile&r=goods.detail&id=213&mid=2214";
    }
  },

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
    }
  },

  watch: {}
};
</script>
<style lang='scss' scoped>
.wrapper-title {
  text-align: center;
  background-color: #fff;
  padding-top: 30px;
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
  //background-color: #fff;
  .audio {
    width: 100%;
    background-color: #fff;
    &-audio {
      width: 100% !important;
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