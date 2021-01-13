<template>
  <div class="box">
    <template v-if="token">
      <iframe
        :src="'/jz/form_1_2.bsp?token=' + token"
        style="display:none"
      ></iframe
    ></template>
    <ul>
      <li
        v-for="(item, index) in navList"
        :key="index"
        :class="{ cur: cur == index }"
        @click="navHandle(index, item)"
      >
        <i class="iconfont icon-zhuye" v-if="index == 0"></i>
        <span>{{ item.tag }}</span>
      </li>
    </ul>
  </div>
</template>

<script>
import { getToken, getUserInfo } from "@/public/auth";
export default {
  name: "headerIn",
  props: ["noticievw", "notic"],
  data() {
    return {
      // token:getToken(),
      // imgsrc: require("../../assets/home.png"),
      navList: [
        {
          tag: "首页",
          path: "home"
        },
        {
          tag: "应用中心",
          path: "application"
        },

        {
          tag: "通知公告",
          path: "notice"
        },
        {
          tag: "新闻中心",
          path: "news"
        },
        {
          tag: "政策法规",
          path: "laws"
        },
        {
          tag: "学习教育",
          path: "study"
        },
        {
          tag: "公示公告",
          path: "Announcement"
        }
      ],
      cur: 0
    };
  },
  created() {
    console.log(this.noticievw);
    var navinfo = JSON.parse(sessionStorage.getItem("nav"));
    if (navinfo) {
      this.cur = navinfo.index;
    } else if (this.token && getUserInfo() && !this.notic) {
      // this.navHandle(this.navList.length-1, {
      //   tag: "效能监管",
      //   path: "supervise"
      // });
      this.navHandle(this.navList.length - 1, {
        tag: "个人中心",
        path: "PersonCenter"
      });
    } else {
      this.cur = this.notic ? this.notic : 0;
    }
    // getUserInfo()&&this.navList.push({tag: "个人中心",path: "PersonCenter"})
  },
  computed: {
    token: function() {
      let temp = this.$store.state.user.token;
      if (getUserInfo() && temp) {
        this.onReloadNav();
      }
      return temp;
    }
  },
  methods: {
    navHandle(index, item) {
      this.cur = index;
      sessionStorage.removeItem("nav");
      if (this.notic) {
        this.$emit("clickit", item, index);
        console.log(this.cur, "sss");
      }
      this.$emit("clickit", item, index);
    },
    onReloadNav: function() {
      if (getUserInfo().qx == 1) {
        this.navList[this.navList.length] = {
          tag: "效能监管",
          path: "supervise"
        };
      }
      let obj = { tag: "个人中心", path: "PersonCenter" };
      this.navList[this.navList.length] = obj;
    }
  }
};
</script>
<style scoped lang="scss">
@import "~@/assets/scss/variables";
.box {
  height: 100%;
  color: #fdfefe;
  font-size: 20px;
  ul {
    width: 1200px;
    margin: 0 auto;
    height: 100%;
    display: flex;
    justify-content: space-around;
  }
  li {
    text-align: center;
    padding: 0 18px;
    cursor: pointer;
    i {
      font-size: 24px;
    }
  }
  li.cur {
    background: #f2f2f2;
    color: $base-color;
    // color: #07438b;
  }
}
</style>
