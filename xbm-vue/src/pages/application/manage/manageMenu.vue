<template>
  <div
    class="left-box"
    v-loading="loading"
    element-loading-text="拼命加载中"
    element-loading-spinner="el-icon-loading"
    element-loading-background="transparent"
  >
    <el-scrollbar style="height:100%" class="custom-scrollbar">
      <el-menu
        :default-active="iframe_src"
        class="el-menu-vertical-demo"
        @select="selectItems"
        background-color="#07438b"
        text-color="#f8fcff"
        active-text-color="#ffd04b"
        :unique-opened="true"
      >
        <menutree :data="menuLists"></menutree>
      </el-menu>
    </el-scrollbar>
  </div>
</template>

<script>
import { getManageMenuList } from "@/public/apiService/sysManagement/menu";
import { forMateData } from "@/public/utils";
import menutree from "@/pages/application/manage/menuTree";
export default {
  name: "leftmenu",
  props: {
    // defaultItem: {
    //   type: String
    // },
  },
  components: {
    menutree: menutree,
  },
  data() {
    return {
      opends: ["主页"],
      menuLists: [],
    };
  },
  computed: {
    iframe_src() {
      return this.$store.state.approvalMenu.manageActive.BA_PATH;
    },
  },
  created() {
    this.getMenuList();
  },
  methods: {
    getMenuList() {
      this.loading = true;
      getManageMenuList()
        .then((response) => {
          const data = response.data;
          this.menuLists = forMateData(data, "TU", "Ba_Name");
          this.loading = false;
        })
        .catch((error) => {
          console.log(error, "error");
        });
    },
    selectItems(index, indexPath) {
      // console.log(index, indexPath);
      // console.log(this.menuLists);
      if (indexPath.length == 3) {
        this.menuLists.forEach((item) => {
          item.children &&
            item.children.forEach((ele) => {
              ele.children &&
                ele.children.forEach((list) => {
                  if (list.BA_PATH == index) {
                    this.$emit("selectItem", list);
                  }
                  return;
                });
            });
        });
      } else if (indexPath.length == 2) {
        this.menuLists.forEach((item) => {
          item.children &&
            item.children.forEach((ele) => {
              if (ele.BA_PATH == index) {
                this.$emit("selectItem", ele);
              }
              return;
            });
        });
      } else if (indexPath.length == 1) {
        this.menuLists.forEach((item) => {
          if (item.BA_PATH == index) {
            this.$emit("selectItem", item);
          }
        });
      }
    },
    handleOpen(index, indexPath) {
      if ($(".left-menu").width() <= 140) {
        var dw = $(document).width();
        $(".left-menu").animate({ width: "280px" }, 100);
        $(".right-box").animate({ width: dw - 280 - 10 + "px" }, 100);
        $(".el-icon-arrow-down").show();
      }
      this.$emit("selectItem", index, false);
    },
  },
};
</script>

<style lang="scss" scoped>
.left-box {
  width: 100%;
  height: 100%;
  background: #fff;
  /deep/ .custom-scrollbar {
    background-color: #07438b;
  }

  .el-scrollbar__view > .el-menu > .el-menu-item {
    font-weight: bold;
  }
  .el-menu {
    //  width:90%;
    margin: 0 auto;
    border-right: none;
    >>> .el-submenu__title {
      font-weight: bold;
      // text-align: center;
      font-size: 16px;
    }

    /deep/ .el-menu-item {
      // text-align: center;
      font-size: 16px;
      color: #4c4948;
      // font-weight: bolder;
      // border-bottom: 1px solid #edecec;
    }
    >>> .el-menu-item.is-active {
      color: rgb(255, 208, 75);
      font-weight: bolder !important;
      font-size: 16px;
    }
  }
  .el-menu:nth-of-type(1) > .el-menu-item {
    border-bottom: 0 none;
  }
}
</style>
