
<template>
  <view>
    <view>    </view>
    <view>
      <uni-list>
        <navigator url="../../pages/user/myteam">
          <uni-list-item :show-extra-icon="true" :extra-icon="icon.person" title="我的团队" />
        </navigator>
        <navigator url="../../pages/user/share">
          <uni-list-item :show-extra-icon="true" :extra-icon="icon.paperplane" title="我要分享" />
        </navigator>
      </uni-list>
    </view>
    <navigator url="../cards/addcard" hover-class="navigator-hover">
      <button type="primary">添加信用卡</button>
    </navigator>
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
  methods: {},
  onLoad: function() {
    console.log("AAAAAAAAAAAAAAA");
    let user = this.getUser("../user/user");
    console.log(user);
    if (!user) {
      return false;
    }
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
button {
  margin: 20px;
}
</style>