<template>
  <div class="list-box">
    <ul>
      <li v-for="(item, index) in data" :key="index" @click="handleClick(item,type)">
        <p class="left">{{ item.WJ_NAME ? item.WJ_NAME : (item.NT_NAME?item.NT_NAME:item.BT)}}</p>
        <span class="right">{{ item.SCSJ ? item.SCSJ : (item.NT_TIME?item.NT_TIME:item.CJRQ) }}</span>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  name: "part4",
  props: ["type", "list"],
  data() {
    return {
      data: []
    };
  },
  created() {
  },
  methods: {
    handleClick(item,type) {
      this.$emit("handleClick",item,type) 
    }
  },
  watch: {
    list: function(v) {
      this.data = v;
    }
  }
};
</script>

<style lang="scss" scoped>
@import "~@/assets/scss/variables";
.list-box {
  height: calc(100% - 45px);
  width: 100%;
  overflow-y: auto;
  ul {
    padding: 0 20px;
  }
  li {
    height: 42px;
    line-height: 42px;
    cursor: pointer;
    &:hover{
      color: $base-color;
    }
    p {
      width: 70%;
      white-space: nowrap;
      overflow: hidden;
      text-overflow: ellipsis;
    }
    span {
      font-size: 12px;
      color: #666;
    }
  }
}
</style>
