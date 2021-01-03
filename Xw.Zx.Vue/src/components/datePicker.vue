<template>
  <el-date-picker
    v-model="selectDate"
    type="datetimerange"
    align="right"
    start-placeholder="开始日期"
    end-placeholder="结束日期"
    value-format="yyyy-MM-dd HH:mm:ss"
    :default-time="['00:00:00', '23:59:59']"
    @change="change"
  >
  </el-date-picker>
</template>

<script>
export default {
  name: "date-picker",
  props: [""],
  data() {
    return {
      selectDate: null,
    };
  },

  components: {},

  computed: {},

  beforeMount() {},

  mounted() {},

  methods: {
    change(val) {
      this.$emit("change", val);
    },
  },

  watch: {},
};
</script>
<style lang='' scoped>
</style>