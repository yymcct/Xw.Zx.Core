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
