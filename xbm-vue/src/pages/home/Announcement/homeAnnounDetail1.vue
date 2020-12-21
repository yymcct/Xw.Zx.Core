<template>
  <div id="detail" class="detail-box">
    <div class="header-box header-box9">
      <h3>成都再减减企业管理服务有限公司</h3>
    </div>
    <div class="detail" v-loading="loading">
    <template v-if="detail">
    <certificate1 :data="detail" v-if="$route.query.ZZMLID=='11100000000013338W064'"></certificate1>
    <certificate2  :data="detail" v-if="$route.query.ZZMLID=='11100000MB03271699035'"></certificate2>
    <certificate3  :data="detail" v-if="$route.query.ZZMLID=='11410800MB18523953004'"></certificate3>
    <certificate4  :data="detail" v-if="$route.query.ZZMLID=='11100000000013338W010'"></certificate4>
    </template>
    </div>
  </div>
</template>


<script>
import certificate1 from './certificates/11100000000013338W064';
import certificate2 from './certificates/11100000MB03271699035';
import certificate3 from './certificates/11410800MB18523953004';
import certificate4 from './certificates/11100000000013338W010';
import { apiUrl } from "@/public/apiUrl";
export default {
  name: "right1",
  components: {
    certificate1,
    certificate2,
    certificate3,
    certificate4
  },
  data() {
    return {
      detail: null,
      loading: false
    };
  },
  mounted() {
    this.getDetail();
  },
  methods: {
    getDetail: function() {
      this.loading = true;
         this.$http
        .get(apiUrl.GET_HOME_ANNOUNCEMENT_DETAIL, { params: {ZZID:this.$route.query.ZZID} })
        .then(res => {
           this.loading = false;
          this.detail = res.data.data;
        });
    }
  }
};
</script>

<style  lang="scss" scoped>
@import "~@/assets/scss/variables";
.detail-box {
  height: 100%;
  width: 100%;
  overflow-y: auto;
  background: #fff;
   .header-box {
    height: 70px;
    margin-bottom: 6px;
    padding-left: 30px;
    border-bottom: 3px solid $base-color;
    h3 {
      margin: 0 auto;
      height: 70px;
      line-height: 70px;
      min-width: 480px;
      color: $base-color;
      font-size: 32px;
      padding-left: 60px;
      background: url("~@/assets/logo.png") no-repeat left center;
      background-size: 60px 60px;
      cursor: pointer;
    }
  }

  .detail {
    width: 80%;
    margin: 0 auto;}
}
</style>