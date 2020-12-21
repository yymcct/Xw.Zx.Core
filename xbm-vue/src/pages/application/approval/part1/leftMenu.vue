<template>
  <div
    class="left-box"
    v-loading="isloading"
    element-loading-text="拼命加载中"
    element-loading-spinner="el-icon-loading"
    element-loading-background="transparent"
  >
    <el-scrollbar style="height:100%;" class="custom-scrollbar">
      <el-menu
        :default-active="active"
        :default-openeds="openeds"
        background-color="#07438b"
        text-color="#fff"
        active-text-color="#ffd04b"
        :unique-opened="true"
        @select="selectItems"
        @open="handleOpen"
      >
        <!--一级菜单-->
        <template v-for="item in menulist">
          <el-submenu
            v-if="item.children && item.children.length"
            :index="item.BA_PATH"
            :key="item.BA_PATH"
          >
            <template slot="title">
              <i :class="item.Ba_Icon" class="icon"></i>
              <span>{{ item.Ba_Name }}</span>
            </template>

            <!--二级菜单-->
            <template v-for="itemChild in item.children">
              <el-submenu
                v-if="itemChild.children && itemChild.children.length"
                :index="itemChild.BA_PATH"
                :key="itemChild.BA_PATH"
              >
                <template slot="title">
                  <i :class="itemChild.Ba_Icon" class="icon fa fa-folder"></i>
                  <span>{{ itemChild.Ba_Name }}</span>
                  <!-- :title="itemChild.Ba_Name"-->
                </template>
                <!--三级菜单-->
                <el-menu-item
                  v-for="itemChild_child in itemChild.children"
                  :index="itemChild_child.BA_PATH"
                  :key="itemChild_child.BA_PATH"
                >
                  <!--  <el-tooltip
                    class="item"
                    effect="dark"
                    :content="itemChild_child.Ba_Name"
                    placement="top-start"
                  >-->
                  <div class="child-item-text">
                    <i class="fa fa-file-text" style="padding-right:5px"></i>
                    <span class="menu-tip">{{ itemChild_child.Ba_Name }}</span>
                  </div>
                  <!-- </el-tooltip>-->
                </el-menu-item>
              </el-submenu>
              <el-menu-item v-else :index="itemChild.BA_PATH" :key="itemChild.BA_PATH">
                <i class="fa fa-file-text" style="padding-right:5px"></i>
                <span>{{ itemChild.Ba_Name }}</span>
              </el-menu-item>
            </template>
          </el-submenu>
          <el-menu-item v-else :index="item.BA_PATH" :key="item.BA_PATH">
            <i :class="item.Ba_Icon" class="fa fa-file-text"></i>
            <span slot="title">{{ item.Ba_Name }}</span>
          </el-menu-item>
        </template>
      </el-menu>
    </el-scrollbar>
  </div>
</template>

<script>
import $ from "jquery";
import xbmUrl from "@/public/xbmUrl.js";
import { mapState, mapMutations } from "vuex";
import bus from "@/public/event.js";
import { getToken } from "@/public/auth";
import { getApprovalMenuList } from "@/public/apiService/sysManagement/menu";
import { forMateData } from "@/public/utils";
export default {
  name: "breadcrumb",
  data() {
    return {
      menulist: [],
      openeds: ["个人中心"],
      isFrame: false,
      isCollapse: true,
      isloading: true,
    };
  },
  mounted() {
    let temp = this.$store.state.approvalMenu.active;
    this.openeds = temp.children ? [temp.TU, temp.BA_PATH] : [temp.BA_PATH];
    this.getMenuList();
  },
  computed: {
    active() {
      // console.log(this.$store.state.approvalMenu.active,'active');
      return this.$store.state.approvalMenu.active.BA_PATH;
    },
  },
  methods: {
    getMenuList() {
      return new Promise((resolve, reject) => {
        getApprovalMenuList()
          .then((response) => {
            const data = response.data;
            this.menulist = forMateData(data, "TU", "Ba_Name");
            // console.log(this.menulist);
            this.isloading = false;
            resolve(response);
          })
          .catch((error) => {
            reject(error);
          });
      });
    },
    selectItems(index, indexPath) {
      this.openeds = indexPath;
      // console.log(index, indexPath);
      if (indexPath[0] == "行政审批") {
        this.menulist.forEach((item) => {
          item.children.forEach((ele) => {
            if (ele.children) {
              ele.children.forEach((list) => {
                if (list.BA_PATH == index) {
                  this.$emit("menu-item", list, true);
                }
                return;
              });
            } else {
              if (ele.BA_PATH == index) {
                this.$emit("menu-item", ele, false);
              }
              return;
            }
          });
        });
      } else {
        this.menulist.forEach((item) => {
          item.children.forEach((ele) => {
            if (ele.BA_PATH == index) {
              this.$emit("menu-item", ele, false);
            }
            return;
          });
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
      // this.$emit("menu-item", index, false);
      if (indexPath.length > 1 && indexPath[0] == "行政审批") {
        this.menulist.forEach((item) => {
          item.children.forEach((ele) => {
            if (ele.children) {
              if (ele.BA_PATH == index) {
                // this.$emit("getTags", ele);
                this.$emit("menu-item", ele);
              }
            }
          });
        });
      }
    },
  },
};
</script>

<style lang="scss" scoped>
@import "~@/assets/scss/common";
.left-box {
  height: 100%;
  width: 100%;
  background: #07438b;
  font-size: 16px;
  /deep/ .custom-scrollbar {
    background: #07438b;
    .el-scrollbar__wrap {
      margin-right: 0px;
    }
  }
  i {
    color: #fff;
  }
  .el-menu {
    border-right: 0 none;
    font-size: 16px;
  }
  .icon {
    width: 24px;
    display: inline-block;
  }
  >>> .el-menu > li > .el-submenu__title {
    font-size: 17px;
    font-weight: 600;
  }
  .el-menu-item.is-active {
    background: rgb(6, 54, 111) !important;
    font-weight: bold;
  }
  >>> .el-menu .el-submenu ul .el-submenu__title {
    // padding-left: 10px !important;
    font-size: 16px;
    font-weight: 600;
    span {
      display: inline-block;
      width: calc(100% - 40px);
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }
  }
  .el-submenu .el-menu-item {
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
    font-size: 16px;
    transition: all 1s ease;
    >>> .child-item-text {
      overflow: hidden;
      text-overflow: ellipsis;
      white-space: nowrap;
    }
  }
  >>> .el-submenu__title i {
    color: #fff;
    // font-size: 22px;
  }
  >>> .el-submenu__title .el-submenu__icon-arrow {
    color: #ccc;
    font-size: 16px;
  }
  .el-submenu.is-opened >>> .el-submenu__title >>> .el-submenu__icon-arrow {
    color: #fff;
  }
}
</style>
