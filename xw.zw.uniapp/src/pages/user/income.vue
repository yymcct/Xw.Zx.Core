<template v-cloak>
  <view class="container">
    <view class="info">
      <view class="invite">累计收入(元)</view>
      <view class="income">
        <view class="amoount">0</view>
      </view>
      <view class="invite">当前可提现金额: 0.00</view>
      <view></view>
    </view>
    <view class="list">
      <uni-list>
        <navigator url="../../pages/user/incomelist">
          <uni-list-item :show-extra-icon="true" :extra-icon="icon.person" title="收益记录" />
        </navigator>
        <navigator url="../../pages/user/incomelist">
          <uni-list-item :show-extra-icon="true" :extra-icon="icon.paperplane" title="提现记录" />
        </navigator>
      </uni-list>
    </view>
    <view class="btn">
      <button type="primary" @click="tixian">我要提现</button>
    </view>
  </view>
</template>
<script>
import uniList from "@/components/uni-list/uni-list.vue";
import uniListItem from "@/components/uni-list-item/uni-list-item.vue";
export default {
  data() {
    return {
      user: null,
      mySelf: null,
      myTeamDto: null,
      myTeamUsersDto: null,
      items: ["已注册", "待绑定"],
      current: 0,
      icon: {
        person: {
          color: "#007aff",
          size: "22",
          type: "person"
        },
        paperplane: {
          color: "#007aff",
          size: "22",
          type: "paperplane"
        }
      }
    };
  },
  methods: {
    tixian: function name() {
      uni.showModal({
        title: "提示",
        content: "您还没有收益",
        success: function(res) {}
      });
    }
  },
  components: {
    uniList,
    uniListItem
  },
  onLoad: function() {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
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
.info {
  background-color: white;
  padding: 10px;
}
.invite {
  color: #999;
}
.income {
  display: flex;
  flex-direction: row;
  align-items: center;
}
.amoount {
  font-weight: bolder;
  font-size: 40px;
  margin-right: 30px;
  color: cornflowerblue;
}

.viptype {
  color: deeppink;
  font-weight: bolder;
  font-size: 15px;
  margin-right: 20px;
}
.list {
  margin-top: 20px;
}
.btn {
  height: 150px;
  margin-top: 50px;
  padding-left: 20px;
  padding-right: 20px;
}
[v-cloak] {
  display: none;
}
</style>
