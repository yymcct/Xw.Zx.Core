# hbzs.web.vue

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Run your tests
```
npm run test
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).


```
this.$confirm("确认提交吗？", "提示", {}).then(() => {
        this.editLoading = true;
        api_postMemberMDto(this.editForm).then(res => {
            this.editLoading = false;           
        });
        });
```

```
this.$message({
    message: "删除成功",
    type: "success",
});
```
 <el-link type="success" style="margin-left:5px;" @click="showMemberInfo(scope.row)">查看详情</el-link>

      <member-info v-model="memberInfo.show" :memberId="memberInfo.memberId" />

      import memberInfo from "@/components/memberInfo"

      memberInfo: {
        show: false,
        memberId: 0,
      },

    showMemberInfo(row) {
      this.memberInfo.memberId = row.id;
      this.memberInfo.show = true;
    },