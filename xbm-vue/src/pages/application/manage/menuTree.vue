<template>
  <div class="menutree">
    <!-- :key="menu.BA_PATH" -->
    <label v-for="(menu,idx) in data" :key="idx">
      <el-submenu :index="menu.BA_PATH" v-if="menu.children">
        <template slot="title">
          <i class="fa fa-folder"></i>
          <span>{{menu.Ba_Name}}</span>
        </template>
        <label>
          <menutree :data="menu.children"></menutree>
        </label>
      </el-submenu>
      <el-menu-item v-else :index="menu.BA_PATH">
        <i class="fa fa-file-text"></i>
        <span slot="title">{{menu.Ba_Name}}</span>
      </el-menu-item>
    </label>
  </div>
</template>

<script>
import menutree from "@/pages/application/manage/menuTree";
export default {
  name: "menutree",
  data() {
    return {
      menu_data: {},
    };
  },
  components: {
    menutree: menutree,
  },
  props: ["data"],
  created() {
    this.menu_data = this.data;
    // console.log(this.menu_data);
  },
};
</script>
<style lang="scss" scoped>
/deep/ .fa {
  color: #a1b7ca !important;
  margin-right: 5px;
}
/deep/ .el-submenu__title i {
  color: #b3d0e8;
}
</style>