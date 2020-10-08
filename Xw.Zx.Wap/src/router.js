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
        path: '/sqb/computer1',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/index/computer1'),
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
        path: '/sqb/product/:id',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/product/index'),
        meta: {
            title: '产品', keepAlive: false
        }
    },
    {
        path: '/sqb/order/:id',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/order/index'),
        meta: {
            title: '订单详情', keepAlive: false
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
        path: '/sqb/user/myteam',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/myteam'),
        meta: {
            title: '我的团队', keepAlive: false
        }
    },
    {
        path: '/sqb/user/income',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/income'),
        meta: {
            title: '我的收益', keepAlive: false
        }
    },
    {
        path: '/sqb/user/incomeaudit',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/incomeAudit'),
        meta: {
            title: '收益审核', keepAlive: false
        }
    },
    {
        path: '/sqb/user/withdrawdeposit',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/withdrawDeposit'),
        meta: {
            title: '提现', keepAlive: false
        }
    },
    {
        path: '/sqb/user/withdrawdepositlist',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/withdrawDepositList'),
        meta: {
            title: '提现详情', keepAlive: false
        }
    },
    {
        path: '/sqb/user/incomelist',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/incomelist'),
        meta: {
            title: '收益详情', keepAlive: false
        }
    },
    {
        path: '/sqb/user/code',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/code'),
        meta: {
            title: '兑换码', keepAlive: false
        }
    },
    {
        path: '/sqb/user/user',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/user'),
        meta: {
            title: '个人信息', keepAlive: false
        }
    },
    {
        path: '/sqb/login',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/login/login'),
        meta: {
            title: '登录', keepAlive: false
        }
    },
    {
        path: '/sqb/login/reg',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/login/reg'),
        meta: {
            title: '注册', keepAlive: false
        }
    },
    {
        path: '/sqb/login/pwd',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/login/pwd'),
        meta: {
            title: '重置密码', keepAlive: false
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