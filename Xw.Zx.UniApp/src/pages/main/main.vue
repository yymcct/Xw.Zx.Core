<template>
  <view>
    <view v-if="showLoading" class="example-body">
      <uni-load-more :content-text="contentText" status="loading" />
    </view>
    <view class="heard">
      <view class="title">账户利息合计:</view>
      <view class="total">{{heardInfo.overdueFine}}元</view>
      <view class="zhuixi">
        <button
          type="primary"
          class="parmary-color"
          hover-class="none"
          size="mini"
          @click="zhuxi"
        >申请追息</button>
      </view>
    </view>
    <view class="lixi">
      <view class="lixi-title">
        账户利息
        <view class="lixi-title-hr"></view>
      </view>
      <view
        v-for="(iteam,index) in cardList"
        v-bind:key="iteam.id"
        class
        @click="bindClick(iteam.bank)"
      >
        <uni-swipe-action :options="options1">
          <view class="card">
            <view class="card-left">
              ¥
              <view class="card-left-money">{{iteam.overdueFine}}</view>
            </view>
            <view class="card-line"></view>
            <view class="card-right">
              <view class="uni-triplex-left">
                <text class="uni-title uni-ellipsis">{{iteam.bankName}}</text>
                <text class="uni-text">卡号: {{iteam.cardNum}}</text>
                <text
                  class="uni-text-small uni-ellipsis"
                  v-if="!iteam.lastSyncIsOk"
                >状态: 同步失败 {{iteam.lastSyncTime}}</text>
                <text
                  class="uni-text-small uni-ellipsis"
                  v-if="iteam.lastSyncIsOk"
                >状态: 已同步 {{iteam.lastSyncTime}}</text>
              </view>
              <view class="uni-triplex-right">
                <text class="uni-h5"></text>
              </view>
            </view>
          </view>
          <view :class="index!=cardList.length-1?'card-hr':''"></view>
        </uni-swipe-action>
      </view>
    </view>
    <view class="btn">
      <button type="primary" class="primary" hover-class="none" @click="syncBankCard">查找罚息</button>
    </view>
  </view>
</template>

<script>
import uniSwipeAction from "@/components/uni-swipe-action/uni-swipe-action.vue";
import uniIcon from "@/components/uni-icon/uni-icon.vue";
import uniLoadMore from "@/components/uni-load-more/uni-load-more.vue";
export default {
  components: {
    uniSwipeAction,
    uniIcon,
    uniLoadMore
  },
  data() {
    return {
      user: null,
      btnEnable: true,
      options1: [
        {
          text: "删除",
          style: {
            backgroundColor: "#dd524d"
          }
        }
      ],
      cardList: [],
      heardInfo: null,
      showLoading: false,
      contentText: {
        contentdown: "后台更新中...",
        contentrefresh: "后台更新中...",
        contentnomore: "后台更新中..."
      },
      mySelf: null
    };
  },
  methods: {
    bindClick(value) {
      uni.navigateTo({
        url: `../cards/carddetail?bank=${value}`
      });
    },
    getBankName(bankid) {
      switch (bankid) {
        case 0:
          return "招商银行";
        case 1:
          return "浦发银行";
        case 2:
          return "中信银行";
        case 3:
          return "平安银行";
        case 4:
          return "光大银行";
        case 5:
          return "华夏银行";
        case 6:
          return "民生银行";
      }
    },
    zhuxi: function name() {
      uni.request({
        url: `${this.baseUrl}/api/ApplyForZx/PostApply`,
        data: {
          Remark: ""
        },
        method: "POST",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            uni.showModal({
              title: "提示",
              content: "您的申请已收到, 客服稍后回访,请保持电话畅通!",
              success: function(res) {
                if (res.confirm) {
                } else if (res.cancel) {
                }
              }
            });
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
    getBankCard: function() {
      uni.request({
        url: `${this.baseUrl}/api/BankCard/Gets?&sorts=id&Page=1&PageSize=100`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            this.cardList = res.data.result;
            // console.log(this.cardList);
            // if (this.cardList.length == 0) {
            //   uni.showModal({
            //     title: "提示",
            //     content: "立即导入银行卡?",
            //     success: function(res) {
            //       if (res.confirm) {
            //         uni.navigateTo({ url: "../user/web" }); //TODO 导航下载APP
            //       }
            //     }
            //   });
            // }
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
    getBankTotal: function() {
      uni.request({
        url: `${this.baseUrl}/api/BankCard/GetCardTotal`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            this.heardInfo = res.data.result;
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
    syncAsync: function(IsBefore) {
		// 直接邮箱登录
		uni.navigateTo({
		  url: "../user/web"
		}); 
		
		
      this.isloading = true;
      var url = `${this.baseUrl}/api/Sync/SyncAsync?IsBefore=t`;
      if (IsBefore == true) {
        url += "?IsBefore=t";
      }
      uni.showLoading({
        title: "同步中"
      });
      uni.request({
        url: url,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            var msg = `同步完成,截至日期:${res.data.result.lastSyncTime}`;
            if (res.data.result.bankBillAmount == "0") {
              msg = `恭喜您!截止日期:${res.data.result.lastSyncTime},您的账户未发现滞纳金或利息!`;
            }
            uni.showToast({
              icon: "none",
              title: msg
            });
            // uni.showModal({
            //   title: "提示",
            //   content: `截止到${res.data.result.lastSyncTime}合计利息:${res.data.result.bankBillAmount},是否继续检测`,
            //   success: function(res) {
            //     if (res.confirm) {
            //      // this.syncAsync("t");
            //     } else if (res.cancel) {
            //       console.log("用户点击取消");
            //     }
            //   }
            // });
          } else {
            // uni.showToast({
            //   icon: "none",
            //   title: res.data.msg
            // });
            uni.navigateTo({
              url: "../user/web"
            }); //TODO 导航下载APP
          }
          uni.hideLoading();
        },
        fail: () => {
          uni.showToast({
            icon: "none",
            title: "网络异常"
          });
          uni.hideLoading();
        }
      });
    },
    checkCanAsync: function() {
      var _this = this;
      uni.request({
        url: `${_this.baseUrl}/api/Member/GetSelf`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + _this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            _this.mySelf = res.data.result;
            console.log(_this.mySelf.queryTimes);
            if (_this.mySelf.memberVipType == 0 && (_this.mySelf.queryTimes >= 1)) {
                uni.showModal({
                  title: "提示",
                  content: "升级VIP即可检查罚息, 立即升级?",
                  success: function(res) {
                    if (res.confirm) {
                      uni.navigateTo({ url: "../user/pay" }); //TODO 导航下载APP
                    } else if (res.cancel) {
                    }
                  }
                });
            } else {
              _this.syncAsync();
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
    syncBankCard: function(IsBefore) {
      this.checkCanAsync();
    },
    androidCheckUpdate: function() {
      var _this = this;
      uni.request({
        url: `${this.baseUrl}/api/SqbApp/GetAndroidVersion`,
        method: "GET",
        data: {},
        success: res => {
          if (res.data.statusCode == 200) {
            _this.remoteVer = res.data.result;
            console.log(_this.remoteVer);
            if (_this.androidfilter()) {
              _this.androidUpdate();
            }
          }
        },
        fail: () => {},
        complete: () => {}
      });
    },
    androidfilter: function() {
      console.log(this.SqbVer);
      if (this.remoteVer.version > this.SqbVer) {
        if (this.remoteVer.isAllUpdate == true) {
          return true;
        } else {
          var usertmp = this.getUser("");
          console.log(usertmp);
          if (usertmp) {
            if (this.remoteVer.withPhones.indexOf(usertmp.account) != -1) {
              return true;
            }
          }
        }
      }
      return false;
    },
    androidUpdate: function() {
      var _this = this;
      uni.showModal({
        title: "提示",
        content: "发现新版本,在浏览器中下载更新?",
        success: function(res) {
          if (res.confirm) {
			  var dowlodurl="http://139.155.8.217:8081/sqb.apk";
			  plus.runtime.openURL(dowlodurl);
            /* _this.showLoading = true;
            var dtask = plus.downloader.createDownload(
              // _this.downLoadUrl,
              "http://139.155.8.217:8081/sqb.apk",
              {},
              function(d, status) {
                // 下载完成
                console.log("下载完成");
                _this.showLoading == false;
                if (status == 200) {
                  plus.runtime.install(
                    plus.io.convertLocalFileSystemURL(d.filename),
                    {},
                    {},
                    function(error) {
                      uni.showToast({
                        title: "安装失败",
                        mask: false,
                        duration: 1500
                      });
                    }
                  );
                } else {
                  uni.showToast({
                    title: "更新失败",
                    mask: false,
                    duration: 1500
                  });
                }
              }
            );
            dtask.start(); */
          } else if (res.cancel) {
            console.log("用户点击取消");
          }
        }
      });
    }
  },
  onShow: function() {
    let user = this.getUser("../user/user");
    if (!user) {
      return false;
    }

    if (this.showLoading == false) {
      uni.getSystemInfo({
        success: res => {
          if (res.platform == "android") {
            this.androidCheckUpdate();
          }
        }
      });
    }
  },
  onLoad: function() {
    this.user = this.getUser("../main/main");
    console.log(this.user);
    if (!this.user) {
      return false;
    }
    this.getBankCard();
    this.getBankTotal();
  },

  onTabItemTap: function() {
    console.log("我显示了");
    this.getBankCard();
    this.getBankTotal();
  }
};
</script>

<style>
.lixi {
  background-color: #fff;
}

.lixi-title {
  padding: 5px 14px 0px 14px;
}

.lixi-title-hr {
  padding: 1px 14px;
  border-bottom: 1px #ccc solid;
}

.parmary-color {
  background-color: #ff9046;
  margin-top: 10px;
  display: block;
  margin: 0px;
  width: 45%;
  font-size: 14px;
  font-weight: bold;
}

.heard {
  height: 200px;
  background: url("data:image/jpeg;base64,/9j/4QAYRXhpZgAASUkqAAgAAAAAAAAAAAAAAP/sABFEdWNreQABAAQAAABkAAD/4QONaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLwA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzEzOCA3OS4xNTk4MjQsIDIwMTYvMDkvMTQtMDE6MDk6MDEgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdFJlZj0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlUmVmIyIgeG1sbnM6eG1wPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvIiB4bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ9InhtcC5kaWQ6MDM5MjBjZGItMzg2YS0xNjRlLWFlYWEtYTIyZDRhNjNjZTY4IiB4bXBNTTpEb2N1bWVudElEPSJ4bXAuZGlkOjZGNEQyQzg1RTU5NzExRTlCQkUyQjlENkYyNzdBRDRDIiB4bXBNTTpJbnN0YW5jZUlEPSJ4bXAuaWlkOjZGNEQyQzg0RTU5NzExRTlCQkUyQjlENkYyNzdBRDRDIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE3IChXaW5kb3dzKSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOmU0ODlkZjZiLTU4ZTYtMDU0OC05OGNiLWM4NDEyM2IyNzJiNCIgc3RSZWY6ZG9jdW1lbnRJRD0iYWRvYmU6ZG9jaWQ6cGhvdG9zaG9wOjY3NzViMmI4LWU0ZDEtMTFlOS1iZTZlLWE0NTM2MjA1YWZiYSIvPiA8L3Jk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");
  background-repeat: no-repeat;
  background-size: 100% 100%;
  -moz-background-size: 100% 100%;
  display: -webkit-flex;
  display: flex;
  flex-direction: column;
  justify-content: center;
  padding: 10px;
}

.card {
  display: -webkit-flex;
  display: flex;
  flex-direction: row;
  align-items: center;
  padding: 5px 14px;
  width: 100%;
  height: 55px;
}
.card-left {
  display: -webkit-flex;
  display: flex;
  flex-direction: row;
  align-items: center;
  width: 30%;
  color: #f16613;
}
.card-left-money {
  font-size: 18px;
}
.card-line {
  height: 80%;
  width: 7%;
  border-left: 1px #ccc solid;
}
.card-right {
  width: 63%;
  font-size: 12px;
}
.card-hr {
  border-bottom: 1px solid #ccc;
  margin: 5px 14px;
}

.btn {
  height: 150px;
  padding-left: 20px;
  padding-right: 20px;
  margin-top: 20px;
}

.tbn button {
  width: 80%;
}

.title {
  font-weight: bold;
  font-size: 16px;
  color: #fff;
}

.total {
  font-weight: bold;
  font-size: 40px;
  color: #fff;
}

.zhuixi {
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
}

.zhuixi button {
  margin-top: 10px;
  display: block;
  margin: 0px;
}
</style>
