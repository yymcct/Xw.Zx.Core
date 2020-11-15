

<template>
  <section>
    <el-dialog
      title="关系树"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
      width="60%"
    >
      <!--列表-->
      <el-table
        :data="members"
        highlight-current-row
        v-loading="loading"
        style="width: 100%"
      >
        <el-table-column
          prop="id"
          label="Id"
          width="100px"
          sortable
        ></el-table-column>
        <el-table-column
          prop="realName"
          label="姓名"   
          sortable
        ></el-table-column>
        <el-table-column
          prop="phone"
          label="手机"
          sortable
        ></el-table-column>
        <el-table-column
          prop="memberVipTypeName"
          label="级别"
          width="120px"
          sortable
        ></el-table-column>
        <el-table-column
          prop="createDate"
          label="添加时间"
          width="100px"
          sortable
        ></el-table-column>
      </el-table>

      <div slot="footer" class="dialog-footer">
        <el-button @click="cancelSubmit">取消</el-button>
      </div>
    </el-dialog>
  </section>
</template>

<script>
import api from "@/api/app";

export default {
  name: "parent123",
  components: {},
  props: {
    value: Boolean,
    memberId: Number,
  },
  watch: {
    value: {
      handler(val) {
        this.dialogVisible = val;
        if (this.memberId > 0 && val) {
          this.init();
        }
      },
    },
  },
  data() {
    return {
      dialogVisible: false,
      loading: false,
      members: [],
    };
  },
  methods: {
    cancelSubmit: function () {
      this.dialogVisible = false;
      this.members = [];
      this.$emit("input", false);
    },
    init() {
      this.loading = true;
      api.member
        .getParent(this.memberId)
        .then((res) => {
          this.members = res.result;
          this.loading = false;
        })
        .catch(() => {
          this.loading = false;
        });
    },
  },
  mounted() {},
};
</script>

<style scoped>
</style>