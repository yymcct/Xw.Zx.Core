<template>
  <div>
    <div>
      <slot></slot>
    </div>
    <van-tabbar v-model="activeNum" active-color="#07c160" route>
      <van-tabbar-item to="/expo/1">
        <span>展商</span>
        <img
          class="tabbar-img"
          slot="icon"
          slot-scope="props"
          :src="!props.active ? require('@/assets/images/expo/home.png') : require('@/assets/images/expo/home2.png')"
        />
      </van-tabbar-item>
      <van-tabbar-item to="/expo/pro/1">
        <span>展品</span>
        <img
          class="tabbar-img"
          slot="icon"
          slot-scope="props"
          :src="!props.active ? require('@/assets/images/expo/zhanpin.png') : require('@/assets/images/expo/zhanpin2.png')"
        />
      </van-tabbar-item>
      <van-tabbar-item to="/expo/shortmsg">
        <span>动态</span>
        <img
          class="tabbar-img"
          slot="icon"
          slot-scope="props"
          :src="!props.active ? require('@/assets/images/expo/dongtai.png') : require('@/assets/images/expo/dongtai1.png')"
        />
      </van-tabbar-item>
      <!--  <van-tabbar-item to="/livelist">
        <span>直播</span>
        <img
          class="tabbar-img"
          slot="icon"
          slot-scope="props"
          :src="!props.active ? require('@/assets/images/expo/zhibo.png') : require('@/assets/images/expo/zhibo2.png')"
        />
      </van-tabbar-item>
      <van-tabbar-item icon="user-o" to="/livelist">
        <span>我的</span>
        <img
          class="tabbar-img"
          slot="icon"
          slot-scope="props"
          :src="!props.active ? require('@/assets/images/expo/my.png') : require('@/assets/images/expo/my2.png')"
        />
      </van-tabbar-item>-->
    </van-tabbar>
  </div>
</template>

<script>
import { Tabbar, TabbarItem } from "vant";
export default {
  name: "cnLayout",
  components: {
    [Tabbar.name]: Tabbar,
    [TabbarItem.name]: TabbarItem
  },
  props: {
    active: null
  },
  data() {
    return {
      activeNum: "0"
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
    }
  }
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
