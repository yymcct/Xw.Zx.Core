import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import './plugins/element.js'
import axios from 'axios'
import 'font-awesome/css/font-awesome.min.css'
import '../theme/index.css'
import ElementUI from 'element-ui'
Vue.use(ElementUI)

import VideoPlayer from 'vue-video-player'
require('video.js/dist/video-js.css')
require('vue-video-player/src/custom-theme.css')
Vue.use(VideoPlayer)
Vue.config.productionTip = false;


axios.interceptors.request.use(function (config) {
  let token = localStorage.getItem('authorization');
  if (token) {
    config.headers['Authorization'] = token;
  }
  return config;
})

router.beforeEach((to, from, next) => {
  if (to.path == '/login') {
    sessionStorage.removeItem('user');
  }
  let user = JSON.parse(sessionStorage.getItem('user'));
  if (!user && to.path != '/login') {
    next({ path: '/login' });
  } else {
    next();
  }
})

// 组件相关
Vue.prototype.glpickerOptions = {
  shortcuts: [
    {
      text: "今天",
      onClick(picker) {
        picker.$emit("pick", new Date());
      }
    },
    {
      text: "昨天",
      onClick(picker) {
        const date = new Date();
        date.setTime(date.getTime() - 3600 * 1000 * 24);
        picker.$emit("pick", date);
      }
    },
    {
      text: "一周前",
      onClick(picker) {
        const date = new Date();
        date.setTime(date.getTime() - 3600 * 1000 * 24 * 7);
        picker.$emit("pick", date);
      }
    },
    {
      text: "30天前",
      onClick(picker) {
        const date = new Date();
        date.setTime(date.getTime() - 3600 * 1000 * 24 * 30);
        picker.$emit("pick", date);
      }
    },
    {
      text: "90天前",
      onClick(picker) {
        const date = new Date();
        date.setTime(date.getTime() - 3600 * 1000 * 24 * 90);
        picker.$emit("pick", date);
      }
    }
  ]
};

Vue.prototype.glhandleBeforeImgUpload = function (file) {
  const isJPG = file.type === "image/jpeg";
  const isPng = file.type === "image/png";
  const isLt2M = file.size / 1024 / 1024 < 2;

  if (!isJPG && !isPng) {
    this.$message.error("上传图片只能是 JPG, PNG 格式!");
  }
  if (!isLt2M) {
    this.$message.error("上传图片大小不能超过 2MB!");
  }
  return (isJPG || isPng) && isLt2M;
}

Vue.prototype.glAppBaseApi= process.env.VUE_APP_BASE_API;
Vue.prototype.glfileUploadUrl=`${process.env.VUE_APP_BASE_API}/manager/FileUpload/PostFilesWithNoWater`;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
