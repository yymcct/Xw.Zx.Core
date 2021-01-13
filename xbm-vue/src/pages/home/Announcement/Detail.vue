<template>
  <div id="detail" class="detail-box" v-loading="loading">
    <template v-if="detail">
    <certificate1 :data="detail" v-if="$route.params.ZZMLID=='11100000000013338W064'"></certificate1>
    <certificate2  :data="detail" v-if="$route.params.ZZMLID=='11100000MB03271699035'"></certificate2>
    <certificate3  :data="detail" v-if="$route.params.ZZMLID=='11410800MB18523953004'"></certificate3>
    <certificate4  :data="detail" v-if="$route.params.ZZMLID=='11100000000013338W010'"></certificate4>
    </template>
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
        .get(apiUrl.GET_HOME_ANNOUNCEMENT_DETAIL, { params: {ZZID:this.$route.params.ZZID} })
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
 
}
</style>