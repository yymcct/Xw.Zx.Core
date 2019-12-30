<template>
  <div class="wrapper">
    <div ref="player" class="aplayer">
      <aplayer :audio="curAudio" ref="aplayer"></aplayer>
    </div>
    <div>
      <van-notice-bar
        class="noticebar"
        color="#1989fa"
        background="#ecf9ff"
      >您想了解银行有没有多收您的钱!!! 怎么多收的!!! 什么时候收的!!! 能不能找回!!! 欢迎关注本课程, 不定期更新</van-notice-bar>
    </div>
    <div :style="{height:scrollHeight}">
      <scroll class="scroll" :data="audioNews" pullup @scrollToEnd="scrollToEnd">
        <div
          class="list"
          v-for="(item,index) in audioNews"
          :class="{active:curAudio.url===item.source}"
          :key="index"
          @click="setcurNews(item)"
        >
          <div class="list-title">第{{index+1}}讲:&nbsp;{{item.title}}</div>
          <img class="list-pic" :src="require('@/assets/images/music.gif')" />
        </div>
        <div class="nomore" v-show="isEnd">---&nbsp;没有更多&nbsp;---</div>
      </scroll>
    </div>
  </div>
</template>

<script>
import { api_GetVoiceNews } from "@/api/api";
import scroll from "@/components/scroll/scroll";
import { NoticeBar } from "vant";
export default {
  name: "",
  props: [""],
  data() {
    return {
      page: 1,
      isEnd: false,
      audioNews: [],
      now: 0,
      scrollHeight: "0px",
      curAudio: {
        name: "点击列表播放",
        artist: "夏老师",
        url: "",
        cover: require("@/assets/images/money_bag.png")
      },

      user: null
    };
  },

  components: {
    [NoticeBar.name]: NoticeBar,
    scroll
  },

  computed: {},

  beforeMount() {},

  mounted() {
    this.setscrollHeight();
    this.getVoicNews();
  },

  methods: {
    setcurNews(cur) {
      this.curAudio.name = cur.title;
      this.curAudio.url = cur.source;
      this.$refs.aplayer.play();
    },
    setscrollHeight() {
      let expotopHight = this.$refs.player.offsetHeight;
      let tmphight = window.innerHeight - expotopHight - 50 + "px";
      if (this.scrollHeight != tmphight) {
        this.scrollHeight = tmphight;
      }
    },
    scrollToEnd() {
      if (!this.isEnd) {
        this.page++;
        this.getVoicNews();
      }
    },
    getVoicNews() {
      api_GetVoiceNews({
        Filters: "",
        Sorts: "-id",
        Page: this.page,
        PageSize: 10
      }).then(res => {
        if (res.result.length == 0) {
          this.isEnd = true;
        }
        this.audioNews = this.audioNews.concat(res.result);
      });
    }
  },

  watch: {}
};
</script>
<style lang='scss' scoped>
.wrapper {
  padding: 10px;

  .list {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 10px;
    background: #fff;
    border-radius: 10px;
    margin-top: 10px;
    color: #333;
    .list-title {
      font-size: 15px;
      line-height: 44px;
    }
    .list-pic {
      display: none;
      width: 28px;
      height: 28px;
    }
  }

  .active {
    background: #169af3;
    color: #fff;
    .list-pic {
      display: block;
    }
  }
}
</style>