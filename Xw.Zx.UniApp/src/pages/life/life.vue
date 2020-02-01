<template>
  <div class="warp">
    <div class="example-body">
      <div>
        <img style="width:100%;height: 200px;" src="../../static/img/adv.jpg" />
      </div>
      <ul class="menus">
        <li class="menu" @click="goWebView">
          <img class="image2" src="../../static/img/voicenow.png" mode="aspectFill" />
          <span>音频课程</span>
        </li>
        <li class="menu">
          <img class="image" src="../../static/img/9_tie.jpg" mode="aspectFill" />
        </li>
        <li class="menu">
          <img class="image" src="../../static/img/9_xinyong.jpg" mode="aspectFill" />
        </li>
        <li class="menu">
          <img class="image" src="../../static/img/9_zhuixi.jpg" mode="aspectFill" />
        </li>
        <li class="menu">
          <img class="image" src="../../static/img/9_fuzhai.jpg" mode="aspectFill" />
        </li>
        <li class="menu" @click="gocReditsExchange">
          <uni-view data-v-a82b5552 data-v-b8516bd6 class="uni-icon uni-icon-spinner credits_icon1"></uni-view>
          <uni-text data-v-b8516bd6 style="font-size:.6rem;">
            <span>积分兑换</span>
          </uni-text>
        </li>
        <li class="menu">
          <img class="image" src="../../static/img/9_fangchan.jpg" mode="aspectFill" />
        </li>
        <li class="menu">
          <img class="image" src="../../static/img/9_ceshika.jpg" mode="aspectFill" />
        </li>
        <li class="menu">
          <img class="image" src="../../static/img/9_daikuan.jpg" mode="aspectFill" />
        </li>
      </ul>
    </div>
  </div>
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
      list: [],
      user: null,
      jfurl: null
    };
  },
  onShow: function() {
    let user = this.getUser("../user/user");
    console.log(user);
    if (!user) {
      return false;
    }
  },
  methods: {
    goWebView() {
      let mySelf;
      uni.request({
        url: `${this.baseUrl}/api/Member/GetSelf`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            mySelf = res.data.result;

            if (mySelf.memberVipType == 0) {
              uni.showModal({
                title: "提示",
                content: "VIP专享课程! 立即升级?",
                success: function(res) {
                  if (res.confirm) {
                    uni.navigateTo({ url: "../user/pay" }); //TODO 导航下载APP
                  } else if (res.cancel) {
                  }
                }
              });
            } else {
              //#ifdef H5
              window.location.href =
                "http://139.155.8.217/live/#/news/audioNews";
              //#endif
              //#ifdef APP-PLUS
              uni.navigateTo({
                url:
                  "../life/news/audioNews?url=" +
                  encodeURI("http://139.155.8.217/live/#/news/audioNews")
              });
              //#endif
            }
          } else {
            uni.showToast({
              icon: "none",
              title: res.data.msg
            });
          }
        },
        fail: () => {
          uni.showToast({
            icon: "none",
            title: "网络异常"
          });
        }
      });
    },
    gocReditsExchange() {
      uni.navigateTo({
        url: "../user/creditsExchange"
      });
    },
    change(e) {
      let { index } = e.detail;
      console.log("index:" + index);

      switch (index) {
        case 1:
          uni.navigateTo({
            url:
              "../life/news/audioNews?url=" +
              encodeURI("http://139.155.8.217/live/#/news/audioNews")
          });
          break;
        case 5: // 积分兑换
          uni.navigateTo({
            url: "../user/creditsExchange"
          });
          break;
      }
    }
  },
  onLoad: function() {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
    this.jfurl = `${
      this.baseUrl
    }/api/Sync/Notthing?id=${this.user.id.toString()}`;
    console.log(this.jfurl);
  },
  onReady() {}
};
</script>

<style>
.example-body {
  border-top: 1px #f5f5f5 solid;

  background: #fff;
}
ul,
li {
  padding: 0;
  margin: 0;
  list-style: none;
}

.menus {
  display: flex;
  flex-direction: row;
  justify-content: center;
  flex-wrap: wrap;
  align-items: center;
}
.menu {
  width: 33.3vw;
  height: 33.3vw;
  display: flex;
  flex-direction: column;
  justify-content: center;
  flex-wrap: wrap;
  align-items: center;
}
.image {
  width: 90px;
  height: 90px;
}
.image2 {
  width: 70upx;
  height: 70upx;
}
.text {
  font-size: 26upx;
  margin-top: 10upx;
}

.credits_icon1 {
  margin: 0.4rem 0;
  line-height: 1.2rem;
  color: rgb(241, 102, 19);
  font-size: 24px;
}
</style>
