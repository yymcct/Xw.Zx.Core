<template>
  <view class="container">
    <view>
      <uni-list>
        <navigator url="../../pages/user/myteam">
          <uni-list-item :show-extra-icon="true" :extra-icon="icon.person" title="我的团队" />
        </navigator>
        <navigator url="../../pages/user/income">
          <uni-list-item :show-extra-icon="true" :extra-icon="icon.paperplane" title="我的收益" />
        </navigator>

        <view v-if="isWhite">
          <navigator url="../../pages/user/incomeAudit">
            <uni-list-item :show-extra-icon="true" :extra-icon="icon.paperplane" title="收益审核" />
          </navigator>
          <navigator url="../../pages/user/code">
            <uni-list-item :show-extra-icon="true" :extra-icon="icon.paperplane" title="我的兑换码" />
          </navigator>
        </view>
        <view>
          <navigator url="../../pages/user/share">
            <uni-list-item :show-extra-icon="true" :extra-icon="icon.paperplane" title="我要分享" />
          </navigator>
        </view>
        <navigator url="../../pages/user/edit">
          <uni-list-item :show-extra-icon="true" :extra-icon="icon.paperplane" title="个人信息" />
        </navigator>
        <navigator url="#" v-on:click="logout">
          <uni-list-item :show-extra-icon="true" :extra-icon="icon.paperplane" title="退出登录" />
        </navigator>
      </uni-list>     
    </view>
     <view class="ver">Ver:{{SqbVersion}}</view>
    <!--    <navigator url="../cards/addcard" hover-class="navigator-hover">
      <button type="primary">添加信用卡</button>
    </navigator>
	<navigator url="../user/web" hover-class="navigator-hover">
	  <button type="primary">QQ邮箱导入</button>
    </navigator>-->
  </view>
</template>

<script>
import uniList from "@/components/uni-list/uni-list.vue";
import uniListItem from "@/components/uni-list-item/uni-list-item.vue";
export default {
  components: {
    uniList,
    uniListItem
  },
  data() {
    return {
      user: null,
      sharetype: 0,
      isWhite: false,
      SqbVersion: this.SqbVer,
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
    logout: function() {
      uni.showModal({
        title: "提示",
        content: "是否要退出？",
        success: function(res) {
          if (res.confirm) {
            uni.setStorageSync("USERS_KEY", "");
            uni.navigateTo({
              url: "../login/login"
            });
          }
        }
      });
    },
    checkWithphone: function() {
      uni.request({
        url: `${this.baseUrl}/api/Member/IsWhite`,
        method: "GET",
        header: {
          "Content-Type": "application/json",
          Authorization: `Bearer ` + this.user.token
        },
        success: res => {
          if (res.data.statusCode == 200) {
            this.isWhite = true;
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
  onShow: function() {
    console.log("monitor user state with onShow");
    let user = this.getUser("../user/user");
    console.log(user);
    if (!user) {
      return false;
    }
  },
  onLoad: function() {
    this.user = this.getUser("../user/user");
    if (!this.user) {
      return false;
    }
    this.checkWithphone();
    uni.request({
      url: `${this.baseUrl}/api/Member/GetSelf`, 
      method: "get",
      header: {
        "Content-Type": "application/x-www-form-urlencoded",
        Authorization: `Bearer ` + this.user.token
      },
      success: res => {
        if (res.statusCode == "200") {
          let isvip = res.data.result.memberVipType == 1;
          if (isvip) {
            this.sharetype = 0;
          } else {
            this.sharetype = 1;
          }
        } else {
          uni.showToast({
            icon: "none",
            title: res.data.msg
          });
        }
        this.text = "request success";
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
.heard {
  height: 200px;
  background-color: white;
  border-radius: 10px;
  margin: 10px;
}

.card {
  margin-bottom: 20px;
}

.btn {
  height: 150px;
  width: 100%;
}
.ver {
  display: flex;
  flex-direction: column-reverse;
  align-items: center;
  margin-top: 20px;
  color:gray;
}
button {
  margin: 20px;
}
</style>
