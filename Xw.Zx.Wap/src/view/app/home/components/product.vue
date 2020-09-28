<template>
  <div>
    <router-link class="product" :to="`/sqb/product/${product.id}`">
      <img class="product-image" :src="product.images" alt="" />
      <h2 class="product-name">{{ product.name }}</h2>
      <p class="product-sales">销量{{ product.salesVolume }}</p>
      <div class="product-foot">
        <div class="product-foot-price">
          <p>￥{{ product.price }}</p>
        </div>
        <van-icon name="add-o" size="20" />       
      </div>
    </router-link>
  </div>
</template>

<script>
export default {
  name: "",
  props: {
    product: Object,
  },
  data() {
    return {};
  },

  components: {},

  computed: {},

  beforeMount() {},

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.product {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: flex-start;
  font-size: 16px;
  background-color: #fff;
  padding-bottom: 10px;
  &-image {
    width: 100%;
    min-height: 172.5px;
  }
  &-name {
    margin: 10px 10px 5px 10px;
    font-size: 14px;
    line-height: 20px;
    color: #333333;
    height: 3em;
    overflow: hidden;
  }
  &-sales {
    margin: 0 10px;
    font-size: 13px;
    line-height: 20px;
    color: #999999;
  }
  &-foot {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
    align-items: center;
    font-size: 18px;
    color: #ff5000;
    width: 100%;
    padding: 2px 10px;
    box-sizing: border-box;
    img {
      height: 20px;
    }
    &-price {
      display: flex;
      flex-direction: row;
      justify-content: flex-start;
      align-items: center;
    }
  }
}
</style>