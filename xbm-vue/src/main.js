// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import ElementUI from 'element-ui'
import 'element-ui/lib/theme-chalk/index.css'
import App from './App'
import router from './router'
import axios from 'axios'
import store from './store/index'
import './assets/font/iconfont.css'
import 'font-awesome/css/font-awesome.css'
import '@/public/directive/directives.js'
import echarts from 'echarts'
const Base64 = require("js-base64").Base64;
// import md5 from 'js-md5'
Vue.prototype.$http = axios
Vue.prototype.Base64 = Base64
Vue.config.productionTip = false
Vue.use(ElementUI)
Vue.prototype.$echarts = echarts;
import { MessageBox } from 'element-ui'
import '../static/ueditor/ueditor.config.js'
import '../static/ueditor/ueditor.all.min.js'
import '../static/ueditor/lang/zh-cn/zh-cn.js'
import '../static/ueditor/ueditor.parse.min.js'



/* eslint-disable no-new */
router.beforeEach((to, from, next) => {
  if (to.meta.requireAuth) { // 判断该路由是否需要登录权限
    if (store.state.user.token) { // 通过vuex state获取当前的token是否存在
      next();
    } else {
      MessageBox.confirm('暂未登录,请先登录!', '登录提示', {
        confirmButtonText: '登录',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
          next({
          path: '/login',
          query: {
            redirect: to.fullPath
          } // 将跳转的路由path作为参数，登录成功后跳转到该路由
        })
      }).catch(()=>{
        console.log('取消..');
      })
    }
  } else {
    next();
  }
})
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
