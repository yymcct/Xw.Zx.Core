<template>
  <div class="wrapper">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>

    <div class="my">
      <p class="my-name">
        {{ my.realName ? my.realName : "未填写姓名" }}
        <span>级别:{{ my.memberVipTypeName }}</span>
      </p>
      <p class="my-invite">我的推荐人: {{ my.invitePhone }}</p>
    </div>
    <div class="info">
      <div class="info-item">
        <p class="info-item-num">{{ myTeam.userTotal }}</p>
        <p class="info-item-desc">总人数</p>
      </div>
      <div class="info-item">
        <p class="info-item-num">{{ myTeam.dayTotal }}</p>
        <p class="info-item-desc">本日新增</p>
      </div>
      <div class="info-item">
        <p class="info-item-num">{{ myTeam.monthTotal }}</p>
        <p class="info-item-desc">本月新增</p>
      </div>
    </div>
    <div class="team">
      <!-- <div class="team-title">直接团队成员</div> -->
      <div class="team-item" v-for="item in firstTeamUser" :key="item.id">
        <user-info :user="item" />
      </div>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
import userInfo from "./components/userinfo";
export default {
  name: "myteam",
  props: [""],
  data() {
    return {
      my: null,
      myTeam: null,
      firstTeamUser: null,
    };
  },

  components: { userInfo },

  computed: {},

  beforeMount() {
    api.member.getSelf().then((res) => {
      this.my = res.result;
    });
    api.member.getMyTeam().then((res) => {
      this.myTeam = res.result;
    });
    api.member.getMyFirstTeamUser().then((res) => {
      this.firstTeamUser = res.result;
    });
  },

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.my {
  background-color: #fff;
  padding: 20px;
  &-name {
    font-weight: bold;
    font-size: 24px;
    span {
      margin-left: 20px;
      font-size: 16px;
      color: #999;
    }
  }
  &-invite {
    margin-top: 20px;
    font-size: 16px;
    color: #999;
  }
}

.info {
  background-color: #fff;
  //margin-top: 10px;
  padding: 20px;
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
  &-item {
    width: 33.33333%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    &-num {
      font-weight: bold;
      font-size: 24px;
      color: #ff5000;
    }
    &-desc {
      margin-top: 10px;
      font-size: 16px;
      color: #999;
    }
  }
}
</style>