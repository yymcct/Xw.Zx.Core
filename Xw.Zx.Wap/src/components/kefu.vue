<template>
  <div>
       <div class="linkman">
      <div class="linkman-title">债减减专属客服</div>
      <div class="linkman-content">
        <div class="item">
          <div class="ercode">
            <img
              :src="require('@/assets/images/kf11.jpg')"
              alt=""
            />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="#">债减减客服1线</a>
          </div>
        </div>
        <div class="item">
          <div class="ercode">
            <img :src="require('@/assets/images/kf12.jpg')" alt="" />
            <p>扫码添加微信</p>
          </div>
          <div class="link">
            <a class="link-tel" href="#">债减减客服2线</a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

  export default {
    name:'',
    props:[''],
    data () {
      return {

      };
    },

    components: {},

    computed: {},

    beforeMount() {},

    mounted() {},

    methods: {},

    watch: {}

  }

</script>
<style lang='scss' scoped>
  .linkman {
    background-color: #fff;
    &-title {
      padding-top: 20px;
      margin-left: 10px;
      font-weight: bold;
    }
    &-content {
      padding: 30px 20px;
      .item {
        width: 100%;
        padding-bottom: 50px;
        .ercode {
          padding: 0px 40px;
          img {
            width: 100%;
          }
          p {
            text-align: center;
            color: #777;
            font-size: 16px;
            padding-top: 10px;
          }
        }

        .link {
          display: flex;
          align-items: center;
          justify-content: center;
          .link-tel {
            color: #000;
            text-align: center;
            font-size: 18px;
            font-weight: bold;
            padding-top: 10px;
          }
        }
      }
    }
  }

</style>