<template>
  <div class="box">
    <ul>
      <li
        v-for="(item, index) in navLists"
        :class="{ active: cur == index }"
        :key="index"
        @click="navHandle(index, item)"
      >
        <span>{{ item.Ba_Name }}</span>
      </li>
    </ul>
  </div>
</template>

<script>
import bus from "@/public/event.js";
export default {
  name: "headerIn",
  props:['navList','navCur'],
  data() {
    return {
      cur: this.navCur,
      navLists: []
    };
  },
  watch: {
    navCur(a, b) {
      this.cur = a;
    },
    navList(a, b) {
      a.forEach(item => {
        if (item.TU==null) {
          this.navLists.push(item);
        }
      });
    }
  },
  created() {},
  methods: {
    navHandle(index, item) {
      this.$emit("navHandle", index, item);
      bus.$emit("clickit", item);
    }
  }
};
</script>

<style scoped lang="scss">
@import "~@/assets/scss/variables";
.box {
  height: 100%;
  color: #fdfefe;
  font-size: 16px;
  font-weight: 600;
  ul {
    width: 1200px;
    margin: 0 auto;
    height: 100%;
    display: flex;
    justify-content: space-between;
  }
  li {
    text-align: center;
    cursor: pointer;
    flex: 1;
    font-size: 18px;
    span {
      display: inline-block;
      height: 24px;
      line-height: 24px;
      width: 100%;
      border-right: 1px solid #f2f2f2;
    }
  }
  li.active {
    background: #f2f2f2;
    // color: #07438b;
    color: $base-color;
    span {
      border-right: 0;
    }
  }
  li:last-of-type span {
    border-right: 0;
  }
}
</style>
