import 'amfe-flexible';
import Vue from 'vue'
import App from './App.vue'
import { router } from './router';
import {
  Field, Toast, Loading,
  Button, NavBar, Image, Tab, Tabs, Picker, Popup, Dialog,
  Icon, Cell, CellGroup,Checkbox, CheckboxGroup
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





Vue.prototype.$globalFun = globalFun;
Vue.prototype.$fieldFormatter = (value) => {
  return value.replace(/ /g, '');
}


router.beforeEach((to, from, next) => {
  //from页面有a参数, 但是to页面没有
  if (to.meta && to.meta.private) {
    const user = globalFun.userInfoAPI.get();
    console.log(user)
    if (!user) {
      next({
        path: '/sqb/login'
      });
    }
  }

  //设置页面title
  const title = to.meta && to.meta.title;
  if (title) {
    document.title = title;
  }
  next();
});



new Vue({
  router,
  store,
  render: h => h(App),
}).$mount('#app')
