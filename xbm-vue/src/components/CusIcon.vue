<template>
   <i class="cus-icon" :class="icon" v-if="icon.indexOf('fa')==-1"></i>
              <i class="fa-icon fa" :class="icon" v-else></i>
</template>
<script>
export default {
  name: 'nx-icon',
  props: {
    icon: {
      type: String,
      default: ''
    }
  }
}
</script>
<style lang="scss" scoped>
@import "~@/assets/scss/iconImg";
.cusIcon{
  display:inline-block;
  text-align: center;
   width: 33.3%;
  >span{
    display:inline-block;
    font-size:12px;
  }
}
</style>

