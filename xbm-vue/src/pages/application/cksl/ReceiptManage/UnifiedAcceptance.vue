<template>
<div class="WorkTable">
	 <Vtable  tabName="WorkTable"></Vtable>
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
.WorkTable{
  height:100%;
}
</style>
