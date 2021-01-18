<template>
  <el-select
    v-model="selectValue"
    filterable
    remote
    reserve-keyword
    placeholder="请输入接收人姓名"
    :remote-method="remoteMethod"
    :loading="loading"
    @change="selectChange"
  >
    <el-option
      v-for="item in options"
      :key="item.value"
      :label="item.label"
      :value="item.value"
    >
    </el-option>
  </el-select>
</template>

<script>
import api from "@/api/app";
export default {
  name:"selectSubMember",
  data() {
    return {
      options: [],
      selectValue: [],
      loading: false,
    };
  },
  props: {
    value: Number,
  },
  mounted() {},
  methods: {
    remoteMethod(query) {
      if (query !== "") {
        this.loading = true;
        api.member.queryMember(query).then((res) => {
          this.options = res.result.map((item) => {
            return {value: item.id,  label: `${item.realName} ${item.phone}` };
          });
          this.loading = false;
        });
      } else {
        this.options = [];
      }
    },
    selectChange() {
      this.$emit("input", this.selectValue);
    },
  },
};
</script>