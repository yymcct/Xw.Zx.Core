<template>
  <!--电子政务-效能中心-->
  <div class="breadcrumb-box">
    <div class="top">
      <Breadcrumb :title="breadcrumbTag"></Breadcrumb>
    </div>
    <div>
      <div class="fl left-menu">
        <manageMenu :menuList="menuList" :menutitle="menutitle"></manageMenu>
      </div>
      <div class="fr right-box">
        <Rightbox></Rightbox>
      </div>
    </div>
  </div>
</template>

<script>
import Breadcrumb from "@/components/breadcrumb";
import manageMenu from "@/pages/application/manage/manageMenu";
import Rightbox from "@/pages/application/manage/efficiency/rightbox";
export default {
  name: "manage",
  components: {
    manageMenu,
    Breadcrumb,
    Rightbox
  },
  data() {
    return {
      menuList: [
        {
          id: "1",
          text: "会员权限",
          path: ""
        },
        {
          id: "2",
          text: "消息中心",
          path: "notice"
        },
        {
          id: "3",
          text: "审文中心",
          path: "news"
        },
        {
          id: "4",
          text: "审批中心",
          path: "laws"
        },
        {
          id: "5",
          text: "报表中心",
          path: "application"
        },
        {
          id: "6",
          text: "任务中心",
          path: "supervise"
        },
        {
          id: "7",
          text: "效能中心",
          path: "supervise"
        },
        {
          id: "8",
          text: "管理中心",
          path: "supervise"
        }
      ],
      breadcrumbTag: "效能中心",
      menutitle: "效能中心"
    };
  },
  methods: {}
};
</script>

<style>
.el-breadcrumb {
  height: 60px;
  line-height: 60px;
}
.left-menu {
  float: left;
  width: 278px;
  box-shadow: -2px 2px 3px 0px rgba(0, 0, 0, 0.15);
  border-top: 3px solid rgba(7, 67, 139, 1);
}
.right-box {
  width: 900px;
  float: right;
}
</style>