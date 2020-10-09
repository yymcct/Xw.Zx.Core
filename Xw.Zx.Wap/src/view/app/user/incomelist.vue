<template>
  <div class="wrapper">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>
    <div class="container">
      <div class="card" v-for="iteam in incomeDetail" v-bind:key="iteam.id">
        <div class="card-left">
          <div class="card-left-title">
            {{ iteam.addTime }}
          </div>
          <p class="card-left-content">备注:{{ iteam.remark }}</p>
        </div>
        <div class="card-right">
          <span class="amount">{{ iteam.amount }}</span>
        </div>
      </div>
      <div class="none" v-if="incomeDetail.length == 0">--您还没有收益--</div>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "",
  props: [""],
  data() {
    return {
      incomeDetail: [],
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    api.income.getDetails().then((res) => {
      this.incomeDetail = res.result;
    });
  },

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.card {
  margin-bottom: 10px;
  background-color: white;
  padding: 10px;
  font-size: 16px;
  display: flex;
  flex-direction: row;
  justify-content: flex-start;
  align-items: flex-start;
  height: 80px;
  &-left {
    &-title {
      font-weight: bold;
    }
    &-content {
      margin-top: 10px;
      color: #666;
      line-height: 22px;
    }
  }
  &-right {
    width: 100px;
    height: 100%;
    display: flex;
    flex-direction: row;
    justify-content: flex-end;
    align-items: center;
    span {
      font-size: 30px;
      font-weight: bolder;
      margin-left: 5px;
      margin-right: 5px;
      color: #ff5000;
    }
  }
}

.none {
  font-size: 14px;
  text-align: center;
  color: #999999;
  margin-top: 20px;
}
</style>