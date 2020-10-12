<template>
  <div>
    <hb-layout :active="1">
      <computer />
    </hb-layout>
  </div>
</template>

<script>
import computer from "@/components/computer";
import HbLayout from "@/components/layout/hbLayout";
export default {
  name: "",
  props: [""],
  data() {
    return {};
  },

  components: { computer, HbLayout },

  computed: {},

  beforeMount() {},

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='' scoped>
</style>