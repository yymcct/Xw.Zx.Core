<template v-cloak>
  <view class="container">
    <view class="userinfo">
      <view class="selfinfo">
        <view class="phone">{{mySelf.phone}}</view>
        <view class="viptype">{{GetVipType(mySelf.memberVipType)}}</view>
        <view><button type="warn" size="mini">升级VIP</button></view>
      </view>
      <view>我的推荐人:{{mySelf.invitePhone}}</view>
    </view>
    <view class="head">
      <view class="headblock">
        <view class="headblock1">{{myTeamDto.userTotal}}</view>
        <view class="headblock2">总人数</view>
      </view>
      <view class="headblock">
        <view class="headblock1">{{myTeamDto.dayTotal}}</view>
        <view class="headblock2">本日新增</view>
      </view>
      <view class="headblock">
        <view class="headblock1">{{myTeamDto.monthTotal}}</view>
        <view class="headblock2">本月新增</view>
      </view>
    </view>
    <view class="tabbar">
      <segmented-control
        id="tabbar"
        :values="items"
        :stickyTop="108"
        :current="current"
        @clickItem="onClickItem"
      ></segmented-control>
      <view class="list" id="list">
        <view
          v-for="iteam in myTeamUsersDto"
          v-bind:key="iteam.id"
          class="card"
          @click="bindClick(iteam.id)"
        >
          <uni-swipe-action :options="options1">
            <view class="uni-triplex-row crd">
              <view class="uni-triplex-left">
                <text class="uni-title uni-ellipsis">{{iteam.phone}}</text>
                <text class="uni-text">级别: VIP{{iteam.memberVipType}}</text>
                <text class="uni-text-small uni-ellipsis">注册时间 {{iteam.createDate}}</text>
              </view>
              <view class="uni-triplex-right">
                <text class="uni-h5"></text>
              </view>
            </view>
          </uni-swipe-action>
        </view>
      </view>
    </view>
  </view>
</template>
<script>
import segmentedControl from "../../components/segmented-control/segmented-control";
import uniSwipeAction from "@/components/uni-swipe-action/uni-swipe-action.vue";
export default {
  data() {
    return {
      user: null,
      mySelf: null,
      myTeamDto: null,
      myTeamUsersDto: null,
      items: ["已注册", "待绑定"],
      current: 0,
      options1: [
        {
          text: "删除",
          style: {
            backgroundColor: "#dd524d"
          }
        }
      ]
    };
  },
  methods: {
    onClickItem(index) {
      if (this.current !== index) {
        this.current = index;
        uni.request({
          url: `${
            this.baseUrl
          }/api/Member/GetMyTeamNoCardUser?filter=${this.current.toString()}`,
          method: "GET",
          header: {
            "Content-Type": "application/json",
            Authorization: `Bearer ` + this.user.token
          },
          success: res => {
            if (res.data.statusCode == 200) {
              this.myTeamUsersDto = res.data.result;
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
      }
    },
    GetVipType: function(vipid) {
      if (vipid == 0) return "普通会员";
      if (vipid == 1) return "VIP会员";
      if (vipid == 2) return "合伙人";
      if (vipid == 3) return "服务站";
      if (vipid == 4) return "运营商";
      return vipid;
    }
  },
  components: {
    segmentedControl,
    uniSwipeAction
  },
  onLoad: function() {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
    //获取个人信息
    uni.request({
      url: `${this.baseUrl}/api/Member/GetSelf`,
      method: "GET",
      header: {
        "Content-Type": "application/json",
        Authorization: `Bearer ` + this.user.token
      },
      success: res => {
        if (res.data.statusCode == 200) {
          this.mySelf = res.data.result;
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

    uni.request({
      url: `${this.baseUrl}/api/Member/GetMyTeam`,
      method: "GET",
      header: {
        "Content-Type": "application/json",
        Authorization: `Bearer ` + this.user.token
      },
      success: res => {
        if (res.data.statusCode == 200) {
          this.myTeamDto = res.data.result;
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

    uni.request({
      url: `${this.baseUrl}/api/Member/GetMyTeamNoCardUser?filter=0`,
      method: "GET",
      header: {
        "Content-Type": "application/json",
        Authorization: `Bearer ` + this.user.token
      },
      success: res => {
        if (res.data.statusCode == 200) {
          this.myTeamUsersDto = res.data.result;
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
  }
};
</script>

<style>
/* @import "../../../common/icon.css"; */
.container {
  display: flex;
  flex-direction: column;
  width: 100%;
}
.userinfo {
  background-color: white;
  padding: 10px;
}
.selfinfo {
  display: flex;
  flex-direction: row;
  align-items: center;
}
.viptype {
  color:deeppink;
  font-weight: bolder;
  font-size: 15px;
  margin-right: 20px;
}

.phone {
  font-weight: bolder;
  font-size: 25px;
  margin-right: 20px;
}
.head {
  display: flex;
  flex-direction: row;
  justify-content: center;
  margin-top: 10px;
  background-color: white;
}
.headblock {
  width: 33%;
  padding: 10px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}
.headblock view {
  display: block;
}
.headblock1 {
  font-weight: bolder;
  font-size: 20px;
}
.headblock2 {
  color: #999;
}
.tabbar {
  margin-top: 20px;
}
.card {
  margin-bottom: 10px;
}
.uni-title {
  font-weight: bolder;
  font-size: 18px;
}
[v-cloak] {
  display: none;
}
</style>
