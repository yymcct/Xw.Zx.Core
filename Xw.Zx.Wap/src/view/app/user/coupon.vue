<template>
  <div class="wrapper" v-if="coupons">
    <div class="bar">
      <van-nav-bar
        :title="$route.meta.title"
        left-arrow
        @click-left="$router.go(-1)"
      />
    </div>
    <div class="container">
      <template v-for="(item, index) in coupons">
        <router-link
          :to="{ path: `/sqb/user/coupon/${item.couponReceiveId}` }"
          :key="index"
          class="coupon-item orange"
        >
          <div class="coupon-dots">
            <i></i><i></i><i></i><i></i><i></i><i></i><i></i><i></i><i></i
            ><i></i><i></i><i></i><i></i>
          </div>
          <div class="coupon-type">代金券</div>
          <div class="coupon-left">
            <div class="title">
              <span class="subtitle">￥</span>{{ item.money }}
            </div>
            <div class="subtitle">无金额门槛</div>
          </div>
          <div class="coupon-right">
            <div class="title">{{ item.name }}</div>
            <div class="subtitle">消费任意金额立减{{ item.money }}</div>
            <div class="subtitle"></div>
            <div class="usetime">
              <div class="text">有效期 {{ item.endTime.split(" ")[0] }}</div>
              <div class="usebtn">立即使用</div>
            </div>
          </div>
        </router-link>
      </template>
    </div>
  </div>
</template>

<script>
import api from "@/api/sqbApi";
export default {
  name: "coupons",
  props: [""],
  data() {
    return {
      coupons: null,
    };
  },

  components: {},

  computed: {},

  beforeMount() {
    api.coupon
      .getCoupons({
        pagesize: 100,
      })
      .then((res) => {
        this.coupons = res.result;
      });
  },

  mounted() {},

  methods: {},

  watch: {},
};
</script>
<style lang='scss' scoped>
.coupon-item {
  height: 100px;
  margin-top: 10px;
  background: #ffffff;
  display: flex;
  position: relative;
  overflow: hidden;
  .coupon-dots {
    height: inherit;
    width: 8px;
    position: absolute;
    top: 0;
    left: 5px;
    z-index: 10;
  }
  .coupon-dots:before,
  .coupon-dots:after {
    content: "";
    height: 10px;
    width: 10px;
    background: #ededed;
    border-radius: 10px;
    position: absolute;
    left: 5.25rem;
  }
  .coupon-type {
    width: 80px;
    text-align: center;
    padding: 2px 0;
    background: #ff5000;
    font-size: 10px;
    color: #ffffff;
    position: absolute;
    top: 0;
    right: 0;
    transform: rotate(45deg);
    transform-origin: 40px 40px;
    z-index: 10;
  }

  .coupon-left {
    height: inherit;
    width: 105px;
    background: #ff5000;
    color: #fff;
    text-align: center;
    display: flex;
    flex-direction: column;
    justify-content: center;
    .title {
      font-size: 24px;
      line-height: 32px;
      font-weight: bold;
    }
    .subtitle {
      font-size: 12px;
    }
    img {
      height: 100%;
      width: 100%;
    }
  }
  .coupon-right {
    padding: 10px;
    flex: 1;
    .title {
      font-size: 16px;
      height: 24px;
      color: #1a1a1a;
      overflow: hidden;
    }
    .subtitle {
      height: 18px;
      line-height: 18px;
      font-size: 14px;
      color: #666;
      text-overflow: -o-ellipsis-lastline;
      overflow: hidden;
      text-overflow: ellipsis;
      display: -webkit-box;
    }
    .usetime {
      line-height: 14px;
      margin-top: 6px;
      font-size: 12px;
      color: #999;
      display: flex;
      .text {
        flex: 1;
      }
      .usebtn {
        height: 22px;
        width: 64px;
        line-height: 22px;
        text-align: center;
        border: 1px solid #ff5000;
        border-radius: 22px;
        color: #ff5000;
      }
    }
  }
}
</style>