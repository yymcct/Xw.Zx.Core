<template>
<div class="powerOperation">
	 <Vtable  tabName="powerOperation"></Vtable>
</div>
</template>
<script>
import table from "./children/comTable";
export default {
  components: {
    'Vtable':table
  },
  data() {
    return {
    
    };
  },
  created() {
  	
  },
  computed: {
  },
  methods: {
    
    },
};
</script>

<style lang="scss" scoped>
.powerOperation{
  height:100%;
}
</style>
