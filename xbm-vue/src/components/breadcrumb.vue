<template>
  <div class="breadcrumb-box">
    <el-breadcrumb separator-class="el-icon-arrow-right">
      <el-breadcrumb-item>首页</el-breadcrumb-item>
      <el-breadcrumb-item>{{title}}</el-breadcrumb-item>
    </el-breadcrumb>
  </div>
</template>

<script>
export default {
  name: "breadcrumb",
  props: ["title"]
};
</script>

<style>
	.breadcrumb-box{
		padding-left: 15px;
	}
.el-breadcrumb {
  line-height: 60px;
  font-size: 14px;
}
</style>