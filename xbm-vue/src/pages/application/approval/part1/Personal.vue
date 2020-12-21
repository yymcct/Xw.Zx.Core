<template>
  <div
    class="box"
    ref="personal"
    style="background: #fff;height: 100%;position: relative;"
  >
    <div style="overflow: hidden;height: 100%;display:flex;">
      <div class="left-menu left">
        <LeftMenu @menu-item="getPath"></LeftMenu>
      </div>
      <div class="resize"></div>
      <div class="right-box right">
        <div>
          <p class="el-breadcrumb">
            <span @click="changeIframe" style="cursor:pointer">首页</span>
            <i class="el-icon-arrow-right"></i>
            <span>{{ menutitle }}</span>
          </p>
          <RightIframe
            :iframe_src="iframe_src"
            v-if="isIframe"
            :ismouseDown="ismouseDown"
            @getstate="getstate"
          ></RightIframe>
          <template v-else>
            <RightTag
              v-if="sendtags.length"
              @tagHandle="getPath"
              :tags="sendtags"
            ></RightTag>
            <router-view v-else></router-view>
          </template>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import Breadcrumb from "@/components/breadcrumb";
import LeftMenu from "@/pages/application/approval/part1/leftMenu";
import RightTag from "@/pages/application/approval/part1/right_tag";
import RightIframe from "@/pages/application/approval/part1/right_iframe";
import { getToken } from "@/public/auth";
export default {
  components: {
    Breadcrumb,
    LeftMenu,
    RightTag,
    RightIframe
  },
  props: ["isShowIframe"],
  data() {
    return {
      breadcrumbItem: "行政审批",
      isIframe: true,
      sendtags: [],
      ismouseDown: false, //推拽的时候控制iframe上透明盒子隐藏
      token: getToken()
    };
  },
  watch: {
    isShowIframe: (n, o) => {
      this.isIframe = n;
    }
  },
  mounted() {
    this.dragControllerDiv();
    this.initPage();
  },
  computed: {
    iframe_src() {
      return this.$store.state.approvalMenu.active.BA_PATH;
    },
    menutitle() {
      return this.$store.state.approvalMenu.active.Ba_Name;
    }
  },
  methods: {
    initPage: function() {
      let temp = this.$store.state.approvalMenu.active;
      if (temp) {
        this.getPath(temp, false);
      }
    },
    //获取当前页面的缩放值
    detectZoom() {
      var ratio = 0,
        screen = window.screen,
        ua = navigator.userAgent.toLowerCase();

      if (window.devicePixelRatio !== undefined) {
        ratio = window.devicePixelRatio;
      } else if (~ua.indexOf("msie")) {
        if (screen.deviceXDPI && screen.logicalXDPI) {
          ratio = screen.deviceXDPI / screen.logicalXDPI;
        }
      } else if (
        window.outerWidth !== undefined &&
        window.innerWidth !== undefined
      ) {
        ratio = window.outerWidth / window.innerWidth;
      }

      if (ratio) {
        ratio = Math.round(ratio * 100);
      }
      return ratio;
    },
    getPath(data, isiframe) {
      this.isIframe = isiframe;
      // console.log(data, "data==");
      this.sendtags = data.children ? data.children : [];
      if (this.sendtags.length) {
        this.$store.commit("changeMenuDefault", data);
        return;
      }
      if (data.BA_PATH) {
        console.log(data.BA_PATH);
        if (data.BA_PATH.indexOf("FORM") == -1) {
          this.$router.push(data.BA_PATH);
        } else {
          this.$router.push("/approval");
          this.isIframe = true;
        }
      } else {
        return;
      }
      this.$store.commit("changeMenuDefault", data);
    },
    getstate(state) {
      this.isIframe = state;
    },
    dragControllerDiv: function() {
      var that = this;
      var resize = document.getElementsByClassName("resize");
      var left = document.getElementsByClassName("left");
      var right = document.getElementsByClassName("right");
      var box = document.getElementsByClassName("box");

      for (let i = 0; i < resize.length; i++) {
        resize[i].onmousedown = function(e) {
          var startX = e.clientX;
          resize[i].left = resize[i].offsetLeft;
          that.ismouseDown = true;
          document.onmousemove = function(e) {
            // console.log(box[i]);
            var endX = e.clientX;
            var moveLen = resize[i].left + (endX - startX);
            var maxT = box[i].offsetWidth - resize[i].offsetWidth;
            if (moveLen < 38) moveLen = 38;
            if (moveLen > 401) moveLen = 402;
            if (moveLen < 140) {
              $(".el-icon-arrow-down").hide();
              $(".is-opened ul").hide();
            } else {
              $(".el-icon-arrow-down").show();
              $(".is-opened ul").show();
            }
            resize[i].style.left = moveLen;

            for (let j = 0; j < left.length; j++) {
              left[j].style.width = moveLen + "px";
              // clientWidth
              right[j].style.width = box[i].offsetWidth - moveLen - 12 + "px";
            }
          };
          document.onmouseup = function(evt) {
            document.onmousemove = null;
            document.onmouseup = null;
            resize[i].releaseCapture && resize[i].releaseCapture();
            that.ismouseDown = false;
          };
          resize[i].setCapture && resize[i].setCapture();
          return false;
        };
      }
      window.onresize = function() {
        var eleWid = document.body.clientWidth - left[0].offsetWidth - 10;
        right[0].style.width = eleWid + "px";
      };
    },
    changeIframe() {
      // /jz/XBM_Service.bsp?EXEC&Source=FORM[268].[50]&token=
      this.$store.commit("changeMenuDefault", {
        BA_PATH: "/jz/XBM_Service.bsp?EXEC&Source=FORM[268].[51]&token=",
        Ba_Name: "待办任务"
      });
      this.$router.push("/approval");
    }
  }
};
</script>

<style lang="scss" scoped>
.left-menu {
  float: left;
  width: 280px;
  height: 100%;
  box-shadow: -2px 2px 3px 0px rgba(0, 0, 0, 0.15);
  background: #07438b;
}
.resize {
  width: 10px;
  float: left;
  height: 100%;
  cursor: w-resize;
}
.right-box {
  // width: calc(100% - 402px);
  width: calc(100% - 292px);
  float: left;
  height: 100%;
  & > div {
    height: 100%;
    width: 100%;
    padding-right: 5px;
  }
  .home-box {
    position: relative;
    .transparent-box {
      position: absolute;
      top: 0;
      width: 100%;
      height: 100%;
    }
    .transparent-box.none {
      display: none;
    }
  }
}
.el-breadcrumb {
  padding-left: 0 !important;
  font-size: 16px;
}
</style>
