<template v-cloak>
  <view class="container">
    <view class="list" id="list">
      <view
        v-for="iteam in myTeamUsersDto"
        v-bind:key="iteam.id"
        class="card"
        @click="bindClick(iteam.id)"
      >
        <uni-swipe-action :options="options1" :show-arrow="true">
          <view class="uni-triplex-row crd">
            <view class="uni-triplex-left">
              <text class="uni-title uni-ellipsis">{{iteam.phone}}</text>
              <text class="uni-text">级别: {{GetVipType(iteam.memberVipType)}}</text>
              <text class="uni-text-small uni-ellipsis">注册时间: {{iteam.createDate}}</text>
            </view>
            <view class="uni-triplex-right">
              <!-- <text class="uni-h5">直接:{{iteam.firstChildCnt}}</text>
              <br />
              <text class="uni-h5">间接:{{iteam.secondChildCnt}}</text> -->
            </view>
          </view>
        </uni-swipe-action>
      </view>
    </view>
  </view>
</template>
<script>
import uniIcon from "@/components/uni-icon/uni-icon.vue";
import segmentedControl from "../../components/segmented-control/segmented-control";
import uniSwipeAction from "@/components/uni-swipe-action/uni-swipe-action.vue";
export default {
  data() {
    return {
      user: null,
      mySelf: null,
      myTeamUsersDto: null,
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
    GetVipType: function(vipid) {
      if (vipid == 0) return "普通会员";
      if (vipid == 1) return "VIP会员";
      if (vipid == 2) return "合伙人";
      if (vipid == 3) return "服务站";
      if (vipid == 4) return "运营商";
      return vipid;
    },
    bindClick: function(id) {
      uni.navigateTo({
        url: `../user/myteamsecond?memberid=${id}`
      });
    }
  },
  components: {
    uniIcon,
    segmentedControl,
    uniSwipeAction
  },
  onLoad: function(opthion) {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
    uni.request({
      url: `${this.baseUrl}/api/Member/GetMyFirstTeamUser?filter=0&memberId=${opthion.memberid}`,
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
  color: deeppink;
  font-weight: bolder;
  font-size: 15px;
  margin-right: 20px;
}

.phone {
  font-weight: bolder;
  font-size: 25px;
  margin-right: 20px;
}
.invite {
  color: #999;
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
.uni-list-item__extra {
  display: flex;
  flex-direction: row;
  justify-content: flex-end;
  align-items: center;
}

[v-cloak] {
  display: none;
}
</style>
