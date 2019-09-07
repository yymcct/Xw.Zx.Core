
<template>
  <view>
    <navigator url="../cards/addcard" hover-class="navigator-hover">
      <button type="primary">添加新卡片</button>
    </navigator>
  </view>
</template>

<script>
export default {
  data() {
    return {};
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