<template>
  <div class="wrapper">
    <div v-if="started">
      <video-player
        class="video-player vjs-custom-skin"
        ref="videoPlayer"
        :options="playerOptions"
        :playsinline="true"
        @ready="playerReadied"
      ></video-player>
    </div>
  </div>
</template>

<script>
import "video.js/dist/video-js.css";
import { videoPlayer } from "vue-video-player";
import "videojs-contrib-hls";
export default {
  name: "VideoPlayer",
  props: {
    started: Boolean,
    url: String
  },
  data() {
    return {
      playerOptions: {
        // videojs options
        muted: false, // 默认情况下将会消除任何音频。
        language: "zh-CN",
        sources: [
          {
            type: "video/mp4",
            src: this.url //这是hls流
          }
        ],
        controlBar: {
          timeDivider: false,
          durationDisplay: false
        },
        flash: { hls: { withCredentials: false } },
        html5: { hls: { withCredentials: false } },
        width: document.documentElement.clientWidth,
        autoplay: true, //如果true,浏览器准备好时开始回放
        preload: "false", // 建议浏览器在<video>加载元素后是否应该开始下载视频数据。auto浏览器选择最佳行为,立即开始加载视频（如果浏览器支持）
        fluid: true,
        notSupportedMessage: "此视频暂无法播放，请稍后再试"
      }
    };
  },

  components: { videoPlayer },

  computed: {},

  beforeMount() {},

  mounted() {},

  methods: {
    playerReadied(player) {
      player.tech({ IWillNotUseThisInPlugins: true }).hls;
      // player.tech_.hls.xhr.beforeRequest = function(options) {
      //   // console.log(options)
      //   return options;
      // };
    }
  },

  watch: {
    url: function(val) {
      //  this.$refs.videoPlayer.reset();
      if (this.url) {
        this.playerOptions.sources[0].src = val;
        // this.$refs.videoPlayer.player.src(this.url);
        // this.$refs.videoPlayer.player.load(this.url);
      }

      //this.$refs.videoPlayer.player.play();
    },
    // started: function(val) {
    // //  if (!val) this.$refs.videoPlayer.player.paused();
    // }
  }
};
</script>
<style lang='scss' scoped>
.wrapper {
  position: relative;
  img {
    width: 100%;
  }
  .watch {
    position: absolute;
    top: 10px;
    right: 5px;
    color: white;
    padding-left: 5px;
    padding-right: 5px;
    background-color: rgba(0, 0, 0, 0.3);
    border-radius: 10px;
    font-size: 12px;
    text-align: center;
    height: 18px;
  }
}
</style>