<template>
  <div>
    
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

    mounted() {
         localStorage.setItem('IS_VIP_KEY',"111");
         this.$router.push(`/sqb/chapter`)
    },

    methods: {},

    watch: {}

  }

</script>
<style lang='' scoped>

</style>