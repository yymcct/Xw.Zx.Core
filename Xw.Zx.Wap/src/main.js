import 'amfe-flexible';
import Vue from 'vue'
import App from './App.vue'
import { router } from './router';
import {
  Field, Toast, Loading,
  Button, NavBar, Image, Tab, Tabs, Picker, Popup, Dialog, NoticeBar,
  Icon, Cell, CellGroup, Checkbox, CheckboxGroup, DatetimePicker, Stepper, Switch
  , RadioGroup, Radio,Tag 
} from "vant";
import globalFun from '@/utils/globalFun'
import store from './store'

// import VueLazyLoad from 'vue-lazyload'
import VueAudio from 'vue-audio-better'

Vue.use(VueAudio)
Vue.config.productionTip = false

Vue.use(require('vue-wechat-title'))

// Vue.use(VueLazyLoad, {
//   error: require('@/assets/images/expo/unimg.png'),
//   loading: require('@/assets/images/expo/lazyload.gif')
// });

Vue.use(Field);
Vue.use(Toast);
Vue.use(Loading);
Vue.use(Button);
Vue.use(NavBar);
Vue.use(Image);
Vue.use(Tab);
Vue.use(Tabs);
Vue.use(Icon);
Vue.use(Picker);
Vue.use(Popup);
Vue.use(Dialog);
Vue.use(Cell);
Vue.use(CellGroup);
Vue.use(Checkbox);
Vue.use(CheckboxGroup);
Vue.use(DatetimePicker);
Vue.use(Stepper);
Vue.use(Switch);
Vue.use(RadioGroup);
Vue.use(Radio);
Vue.use(NoticeBar);
Vue.use(Tag);




Vue.prototype.$globalFun = globalFun;
Vue.prototype.$fieldFormatter = (value) => {
  return value.replace(/ /g, '');
}

var _hmt = _hmt || [];
window._hmt = _hmt; // 必须把_hmt挂载到window下，否则找不到
(function () {
  var hm = document.createElement("script");
  hm.src = "https://hm.baidu.com/hm.js?" + '08075c6efe7a54693805f2805ee5364d';
  var s = document.getElementsByTagName("script")[0];
  s.parentNode.insertBefore(hm, s);
})();

router.beforeEach((to, from, next) => {
  //from页面有a参数, 但是to页面没有
  if (to.meta && to.meta.private) {
    const user = globalFun.userInfoAPI.get();
    if (!user) {
      //globalFun.userInfoAPI.setLoginFrom(to.path);
      next({
        path: `/sqb/login?redirect_uri=${encodeURIComponent(to.path)}`
      });
    }
  }

  //设置页面title
  const title = to.meta && to.meta.title;
  if (title) {
    document.title = title;
  }

  //加载统计代码
  if (window._hmt) {
    if (to.path) {
      window._hmt.push(['_trackPageview', to.path]);
    }
  }
  next();
});
Date.prototype.Format = function (fmt) {
  var o = {
    "M+": this.getMonth() + 1, //月份 
    "d+": this.getDate(), //日 
    "H+": this.getHours(), //小时 
    "m+": this.getMinutes(), //分 
    "s+": this.getSeconds(), //秒 
    "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
    "S": this.getMilliseconds() //毫秒 
  };
  if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
  for (var k in o)
    if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
  return fmt;
}

Vue.component('remote-script', {

  render: function (createElement) {
      var self = this;
      return createElement('script', {
          attrs: {
              type: 'text/javascript',
              src: this.src
          },
          on: {
              load: function (event) {
                  self.$emit('load', event);
              },
              error: function (event) {
                  self.$emit('error', event);
              },
              readystatechange: function (event) {
                  if (this.readyState == 'complete') {
                      self.$emit('load', event);
                  }
              }
          }
      });
  },

  props: {
      src: {
          type: String,
          required: true
      }
  }
});

new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app')
