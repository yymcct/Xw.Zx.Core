import 'amfe-flexible';
import Vue from 'vue'
import App from './App.vue'
import { router } from './router';
import { Dialog, Toast } from "vant";
import globalFun from '@/utils/globalFun'
import APlayer from '@moefe/vue-aplayer';


Vue.config.productionTip = false

Vue.use(Dialog);
Vue.use(Toast);
Vue.use(APlayer);

Vue.prototype.$globalFun = globalFun;

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
