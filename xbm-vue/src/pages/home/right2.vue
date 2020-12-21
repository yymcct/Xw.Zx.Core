<template>
  <div class="li-box">
    <ul>
      <li @click="toPath('/apply')">
        <div>
          <img src="@/assets/images/index/wssq.png" alt />
        </div>
        <p>网上申请</p>
      </li>
      <li @click="toParent()">
        <div>
          <img src="@/assets/images/index/ziyuan.png" alt />
        </div>
        <p>效能监管</p>
      </li>
      <li @click="toPath('/handle')">
        <div>
          <img src="@/assets/images/index/app.png" alt />
        </div>
        <p>移动APP</p>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "right2",
  methods: {
    toPath(path) {
       this.$message({
          message: '正在开发中…',
          type: 'warning'
        });
     // this.$router.push(path);
    },
    toParent() {
        this.$message({
          message: '正在开发中…',
          type: 'warning'
        });
      // sessionStorage.setItem(
      //   "nav",
      //   JSON.stringify({ path: "supervise", index: "5" })
      // );
      // // 刷新页面
      // let NewPage = "_empty" + "?time=" + new Date().getTime() / 500;
      // this.$router.push(NewPage);
      // this.$router.go(-1);
    }
  }
};
</script>

<style lang="scss" scoped>
.li-box {
  width: 100%;
  height: 100%;
  ul {
    display: flex;
  }
  li {
    flex: 1;
    text-align: center;
    padding: 30px 0;
    border: 1px solid transparent;
    cursor: pointer;
    div {
      width: 76px;
      height: 76px;
      line-height: 76px;
      margin: 0 auto;
    }
    &:hover {
      border: 1px solid #07438b;
    }
  }
  p {
    font-weight: bold;
    font-size: 20px;
    line-height: 32px;
  }
  span {
    color: #aaa;
    font-size: 14px;
  }
}
</style>