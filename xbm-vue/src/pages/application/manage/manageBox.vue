<template>
  <!--电子政务-审批中心-->
  <div class="box" style="height:100%;width:100%">
    <div style="height:100%;width:100%;display:flex;">
      <!-- v-if="showMenu" -->
      <div class="left-menu left">
        <ManageMenu @selectItem="selectItem"></ManageMenu>
      </div>
      <!-- <div class="resize">
          <div class="inside-bar"></div>
      </div>-->
      <div class="right-box right">
        <!-- v-if="iframe_src=='/jz/XBM_Service.bsp?EXEC&Source=FORM[268].[54]&token='||iframe_src=='/jz/XBM_Service.bsp?EXEC&Source=FORM[268].[55]&token='||iframe_src=='/jz/XBM_Service.bsp?EXEC&Source=FORM[268].[56]&token='" -->
        <el-breadcrumb separator="/" style="border-bottom: 1px solid #E4E7ED;margin-bottom: 5px;">
          <el-breadcrumb-item v-if="manageActive.Ba_Name!=='主页'">
            <span
              style="cursor:pointer;color: #07438b;font-weight: bold;font-size: 14px;"
              @click="toHome"
            >主页</span>
          </el-breadcrumb-item>
          <el-breadcrumb-item>{{manageActive.Ba_Name}}</el-breadcrumb-item>
        </el-breadcrumb>
        <div style="height:calc(100% - 50px)">
          <iframe
            v-if="iframe_src.indexOf('FORM') != -1"
            :src="iframe_src + token"
            frameborder="0"
            width="100%"
            height="100%"
          ></iframe>
          <router-view v-else></router-view>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import xbmUrl from "@/public/xbmUrl.js";
import bus from "@/public/event.js";
import { getToken } from "@/public/auth";
import Breadcrumb from "@/components/breadcrumb";
import ManageMenu from "@/pages/application/manage/manageMenu";
// import Right from "@/pages/application/manage/right";
import Error from "@/pages/404";
export default {
  name: "manage",
  components: {
    ManageMenu,
    Breadcrumb,
    // Right,
    Error,
  },
  props: ["defaultMenu", "navList"],
  data() {
    return {
      token: getToken(),
      menuList: [],
    };
  },
  mounted() {
    // this.dragControllerDiv()
  },
  computed: {
    manageActive() {
      return this.$store.state.approvalMenu.manageActive;
    },
    iframe_src() {
      return this.$store.state.approvalMenu.manageActive.BA_PATH;
    },
  },

  methods: {
    selectItem(val) {
      if (val.BA_PATH && val.BA_PATH.indexOf("FORM") == -1) {
        this.$router.push(val.BA_PATH);
      } else {
        this.$router.push({ path: "/manage" });
      }
      this.$store.commit("manageMenuDefault", val);
    },
    toHome: function () {
      this.$router.push({ path: "/manage" });
      this.$store.commit("manageMenuDefault", {
        BA_PATH: "/jz/XBM_Service.bsp?EXEC&Source=FORM[268].[50]&token=",
        Ba_Name: "待办工作",
      });
    },
    dragControllerDiv: function () {
      var that = this;
      var resize = document.getElementsByClassName("resize");
      var left = document.getElementsByClassName("left");
      var right = document.getElementsByClassName("right");
      var box = document.getElementsByClassName("box");
      for (let i = 0; i < resize.length; i++) {
        resize[i].onmousedown = function (e) {
          var startX = e.clientX;
          resize[i].left = resize[i].offsetLeft;
          that.ismouseDown = true;
          document.onmousemove = function (e) {
            var endX = e.clientX;
            var moveLen = resize[i].left + (endX - startX);
            var maxT = box[i].clientWidth - resize[i].offsetWidth;
            if (moveLen < 38) moveLen = 38;
            if (moveLen > 400) moveLen = 400;
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
              right[j].style.width = box[i].clientWidth - moveLen - 10 + "px";
            }
          };
          document.onmouseup = function (evt) {
            document.onmousemove = null;
            document.onmouseup = null;
            resize[i].releaseCapture && resize[i].releaseCapture();
            that.ismouseDown = false;
          };
          resize[i].setCapture && resize[i].setCapture();
          return false;
        };
      }
    },
  },
};
</script>
<style lang="scss" scoped>
.breadcrumb-box {
  padding: 0;
}
.el-breadcrumb {
  height: 60px;
  line-height: 60px;
}
.left-menu {
  float: left;
  width: 280px;
  height: 100%;
  box-shadow: -2px 2px 3px 0px rgba(0, 0, 0, 0.15);
  background: #07438b;
}
.resize {
  width: 8px;
  float: left;
  height: 100%;
  // box-shadow: inset 0 0 3px 1px rgba(0,0,0,0.4);
  // cursor: w-resize;
  .inside-bar {
    width: 7px;
    height: 30px;
    position: absolute;
    top: 0;
    bottom: 0;
    margin: auto;
    background-color: #20a0ff;
  }
}
.right-box {
  width: calc(100% - 282px);
  float: left;
  height: 100%;
  background: #fff;
  padding-left: 10px;
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
</style>
