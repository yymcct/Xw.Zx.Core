<template>
  <div class="approval-right">
    <div class="tag-box" v-if="tags.length>0">
      <p v-for="item in tags" @click="tagClick(item)" :key="item.BA_PATH">
        <span>{{item.Ba_Name}}</span>
      </p>
    </div>
  </div>
</template>

<script>
export default {
  name: "right",
  props: ["tags"],
  data() {
    return {};
  },
  methods: {
    tagClick(data) {
      this.$emit("tagHandle", data, true);
      // this.$emit("getTags", list);
    }
  }
};
</script>

<style lang="scss" scoped>
.approval-right {
  // padding: 20px;
  height: calc(100% - 45px);
  // width: 100%;
  overflow: auto;
  width: calc(100% + 10px);
  background: #f7fbfd;
  margin: 0px -10px;
}
.tag-box {
  padding: 20px;
    height: auto;
    width: 90%;
    overflow: hidden;
    margin:0 auto;
  p {
    cursor: pointer;
    width: 200px;
    height: 100px;
    float: left;
    text-align: center;
    // border: 1px solid #87b9f5;
    margin-bottom: 20px;
    margin-right: 25px;
    display: flex;
    display: -webkit-flex;
    align-items: center;
    justify-content: center;
    padding: 10px 20px;
    background:#fff;
    box-shadow: 0 0 8px 0 rgba(232,237,250,.6),0 2px 4px 0 rgba(232,237,250,.5);
    transition: all 0.3s ease-out;
    border:1px solid #ebeef5;
    // border-left:2px solid #f9a69f;
    border-left: 2px solid #2196F3;
    border-radius: 5px;
    &:hover{
      box-shadow: 0 2px 12px 0 rgba(0,0,0,.1);
      transform: scale(1.1)
    }
  }
}
</style>