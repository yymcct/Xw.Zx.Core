<template>
  <el-radio-group v-model="vipType" @change="handleCheckedChange">
    <el-radio-button
      v-for="iteam in vipTypeDrop"
      :label="iteam.value"     
      :key="iteam.value"
      >{{ iteam.name }}</el-radio-button
    >
  </el-radio-group>
</template>

<script>
export default {
  name: "",
  props: {
    value: Number,
  },
  data() {
    return {
      vipType: 0,
      vipTypeDrop: [
        { name: "客户", value: 0 },
        { name: "业务经理", value: 10 },
        { name: "运营中心", value: 20 },
        { name: "大区经理", value: 30 },
        { name: "分公司", value: 40 },
      ],
    };
  },
  watch: {
    value: {
      handler(val) {
        if (this.vipType != val) {
          this.vipType = val;
        }
      },
    },
  },
  components: {},

  computed: {},

  beforeMount() {},

  mounted() {},

  methods: {
    handleCheckedChange() {
      this.$emit("input", this.vipType);
    },
  },
};
</script>
<style lang='' scoped>
</style>