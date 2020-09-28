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
        path: '/sqb/computer2',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/computer/index'),
        meta: {
            title: '计算器', keepAlive: false
        }
    },
    {
        path: '/sqb/computer/computer',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/computer/computer'),
        meta: {
            title: '计算器', keepAlive: false
        }
    },
    {
        path: '/sqb/index',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/music/index'),
        meta: {
            title: '法律债务处理大礼包课程-债减减', keepAlive: false
        }
    },
    {
        path: '/sqb/chapter',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/music/chapter'),
        meta: {
            title: '法律债务处理大礼包课程-债减减', keepAlive: false
        }
    },
    {
        path: '/sqb/home',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/home/index'),
        meta: {
            title: '首页', keepAlive: true
        }
    },
    {
        path: '/sqb/user',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/index'),
        meta: {
            title: '我的', keepAlive: true
        }
    },
    {
        path: '*',
        redirect: '/sqb/home'
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