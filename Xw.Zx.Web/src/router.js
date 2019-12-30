import Vue from 'vue';
import Router from 'vue-router';



Vue.use(Router);

const routes = [
  {
    path: '*',
    redirect: '/news/audioNews'
  },
  /*小程序 */
  {
    path: '/news/audioNews',
    component: () => import('./view/news/audioNews'),
    meta: {
      title: '音频教程'
    }
  }
];

// add route path
routes.forEach(route => {
  route.path = route.path || '/' + (route.name || '');
});

const router = new Router({
  routes: routes,
  scrollBehavior(to, from, savedPosition) {
    if (savedPosition) {
      return savedPosition
    } else {
      return { x: 0, y: 0 }
    }
  }
});

router.beforeEach((to, from, next) => {
  const title = to.meta && to.meta.title;
  if (title) {
    document.title = title;
  }
  next();
});

export {
  router
};
