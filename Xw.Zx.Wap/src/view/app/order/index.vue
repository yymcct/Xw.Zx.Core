<template>
  <div v-if="order && product">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>
    <div class="product">
        <div class="img"></div>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "Order",
  props: [""],
  data() {
    return {
      product: null,
      order: null,
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    this.await();
  },

  mounted() {},

  methods: {
    async await() {
      const _this = this;
      _this.order = (await api.order.get(this.$route.params.id)).result;
      _this.product = (
        await api.product.get({
          id: _this.order.productId,
        })
      ).result;
    },
  },

  watch: {},
};
</script>
<style lang='' scoped>
</style>