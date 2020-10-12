<template>
  <div>
    <div>
      <slot></slot>
    </div>
    <van-tabbar v-model="activeNum" active-color="#ff5000" route>
      <van-tabbar-item :to="`/sqb/home`">
        <span>首页</span>
        <img
          class="tabbar-img"
          slot="icon"
          slot-scope="props"
          :src="
            !props.active
              ? require('@/assets/images/home.png')
              : require('@/assets/images/home2.png')
          "
        />
      </van-tabbar-item>
      <van-tabbar-item :to="`/sqb/app/computer`">
        <span>计算器</span>
        <img
          class="tabbar-img"
          slot="icon"
          slot-scope="props"
          :src="
            !props.active
              ? require('@/assets/images/computer.png')
              : require('@/assets/images/computer2.png')
          "
        />
      </van-tabbar-item>
      <van-tabbar-item :to="`/sqb/user`">
        <span>我的</span>
        <img
          class="tabbar-img"
          slot="icon"
          slot-scope="props"
          :src="
            !props.active
              ? require('@/assets/images/user.png')
              : require('@/assets/images/user2.png')
          "
        />
      </van-tabbar-item>
    </van-tabbar>
  </div>
</template>

<script>
import { Tabbar, TabbarItem } from "vant";
import { mapGetters } from "vuex";
export default {
  name: "cnLayout",
  components: {
    [Tabbar.name]: Tabbar,
    [TabbarItem.name]: TabbarItem,
  },
  computed: {
    ...mapGetters("meeting", {
      meetingId: "meetingId",
    }),
  },
  props: {
    active: null,
  },
  data() {
    return {
      activeNum: "0",
    };
  },
  activated() {
    //因为被缓存了 所有激活时重新设置被选中的选项
    this.activeNum = this.active;
  },
  created() {
    this.activeNum = this.active;
  },
  methods: {
    go(url) {
      if (!window.location.href.endsWith(url)) {
        this.$router.push(url);
      }
    },
  },
};
</script>
<style lang="scss" scoped>
.tabbar-img {
  width: 26px;
  height: 26px;
}

// .active_tab .router-link-active {
//     color: #e10f02;
// }
</style>
