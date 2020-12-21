<template>
<div class="DoneBusiness">
	 <Vtable  tabName="DoneBusiness"></Vtable>
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
.DoneBusiness{
  height:100%;
}
</style>
