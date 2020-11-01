<template>
  <div class="wrapper">
    <el-row>
      <el-radio v-model="radio" label="1">支付宝</el-radio>
      <el-radio v-model="radio" label="2">碧麒麟</el-radio>
    </el-row>
    <el-row>
      <el-button class="btn" type="primary" @click="post">提交</el-button>
    </el-row>
  </div>
</template>

<script>
import api from "@/api/app";
export default {
  name: "",
  props: [""],
  data() {
    return {
      radio: "0",
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    api.sysParam.getValue("FirstUseAlipay").then((res) => {
      this.radio = res.result;
    });
  },

  mounted() {},

  methods: {
    post() {
      api.sysParam.setValue("FirstUseAlipay", this.radio).then(() => {
        this.$message({
          message: "提交成功",
          type: "success",
        });
      });
    },
  },

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper {
  padding: 10px;
  .btn {
    width: 150px;
    margin-top: 20px;
  }
}
</style>