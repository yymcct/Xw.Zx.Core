import Vue from 'vue';
import Router from 'vue-router';


Vue.use(Router);

const routes = [
    {
        path: '/sqb/computer',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/index/computer'),
        meta: {
            title: '计算器', keepAlive: false
        }
    },
    {
        path: '/sqb/chapter',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/music/chapter'),
        meta: {
            title: '计算器', keepAlive: false
        }
    },
    {
        path: '*',
        redirect: '/sqb/computer'
    },

];

// add route path
routes.forEach(route => {
    route.path = route.path || '/' + (route.name || '');
});

const router = new Router({
    mode: 'history',
    routes: routes,
    scrollBehavior(to, from, savedPosition) {
        if (savedPosition) {
            return savedPosition
        } else {
            if (from.meta.keepAlive) {
 
                let p = document.documentElement.scrollTop + document.body.scrollTop;
                if (p != 0)
                    from.meta.savedPosition = p;
            }
            return { x: 0, y: to.meta.savedPosition || 0 }
        }
    }
});



export {
    router
};