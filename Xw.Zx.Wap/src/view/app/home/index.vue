<template>
  <div class="wrapper">
    <hb-layout :active="0">
      <div class="banner">
        <img
          :src="require('@/assets/images/home/banner.png')"
          style="display: block; width: 100%; height: auto"
        />
      </div>
      <div class="product">
        <div class="product-title">
          <img :src="require('@/assets/images/home/fire.png')" alt="" />
          <h2>推荐服务</h2>
        </div>
        <div class="product-content">          
          <product
            class="product-content-product"
            v-for="(item, index) in products"
            :key="index"
            :product="item"
          />
        </div>
      </div>
    </hb-layout>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
import product from "./components/product";
import HbLayout from "@/components/layout/hbLayout";
export default {
  name: "",
  props: [""],
  data() {
    return {
      products: [],
    };
  },

  components: {
    HbLayout,
    product,
  },

  computed: {},

  beforeMount() {
    api.product
      .gets({
        Filters: "",
        Sorts: "-id",
        Page: 1,
        PageSize: 10,
      })
      .then((res) => {
        this.products = res.result;
      });
  },

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.wrapper{
  padding-bottom: 60px;
.product {
  &-title {
    display: flex;
    flex-direction: row;
    justify-content: flex-start;
    align-items: center;
    font-size: 16px;
    background-color: #fff;
    color: #ff5000;
    padding: 10px;
    img {
      height: 22px;
      margin-right: 5px;
    }
  }
  &-content {
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: space-between;
    
    align-items: flex-start;
    padding: 10px;
    &-product{
      width: 172.5px;
      margin-bottom: 10px;
    }
  }
}

}

</style>