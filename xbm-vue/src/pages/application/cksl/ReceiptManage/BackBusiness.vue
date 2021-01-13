<template>
<div class="BackBusiness">
	 <Vtable  tabName="BackBusiness"></Vtable>
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
.BackBusiness{
  height:100%;
}
</style>
