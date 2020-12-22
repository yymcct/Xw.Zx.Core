

<template>
  <section>
    <el-dialog
      title="团队树"
      :visible.sync="dialogVisible"
      :close-on-click-modal="false"
      @close="cancelSubmit"
      width="60%"
      ref="dialog"
      class="loadingtarget"
    >
      <el-tree :data="tree" :props="defaultProps" default-expand-all>
        <template slot-scope="scope">
          <div class="categorys">
            <span
              :class="{
                level0: scope.data.members.isDirectLine == true,
              }"
            >
              {{ scope.data.members.realName }} {{ scope.data.members.phone }} <span v-if="scope.data.members.id!=6">[{{ scope.data.members.memberVipTypeName}}]</span>
              <span>
                <el-link
                  type="primary"
                  class="get-children"
                  v-if="scope.data.children == null"
                  @click="getChildren(scope.data)"
                  >查看下级</el-link
                ></span
              >
            </span>
          </div>
        </template>
      </el-tree>

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
      members: [],
      tree: [],
      defaultProps: {
        children: "children",
        label: "label",
      },
    };
  },
  methods: {
    cancelSubmit: function () {
      this.dialogVisible = false;
      this.members = [];
      this.$emit("input", false);
    },
    init() {
      this.tree = [];
      const loading = this.$loading({
        // target: document.querySelector(".loadingtarget"),
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(255, 80, 0, 0.05)",
      });
      api.member
        .parentTree(this.memberId)
        .then((res) => {
          this.tree = res.result;
          loading.close();
        })
        .catch(() => {
          loading.close();
        });
    },
    getChildren(data) {
      const loading = this.$loading({
        //target: document.querySelector(".loadingtarget"),
        lock: true,
        text: "Loading",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.2)",
      });
      api.member
        .childrenTree(data.members.id)
        .then((res) => {
          data.children = res.result;
          loading.close();
        })
        .catch(() => {
          loading.close();
        });
    },
  },
  mounted() {},
};
</script>


<style lang="scss" scoped>
.level0 {
  color: #ff5000;
  font-weight: bold;
}
.get-children {
  font-size: 13px;
  margin-left: 10px;
}
</style>