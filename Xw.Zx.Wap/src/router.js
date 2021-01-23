import Vue from 'vue';
import Router from 'vue-router';


Vue.use(Router);

const routes = [
    {
        path: '/sqb/computer',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/index/computer'),
        meta: {
            title: '计算器',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/computer0_1',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/index/computer0_1'),
        meta: {
            title: '计算器',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/computer1',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/index/computer1'),
        meta: {
            title: '计算器',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/computer2',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/index/computer2'),
        meta: {
            title: '计算器',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/computer2',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/computer/index'),
        meta: {
            title: '计算器',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/computer/computer',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/computer/computer'),
        meta: {
            title: '计算器',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/index',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/music/index'),
        meta: {
            title: '法律债务处理大礼包课程-债减减',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/chapter',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/music/chapter'),
        meta: {
            title: '法律债务处理大礼包课程-债减减',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/home',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/home/index'),
        meta: {
            title: '首页',
            keepAlive: true,
            private: false
        }
    },
    {
        path: '/sqb/app/computer',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/computer/index'),
        meta: {
            title: '利息减免计算器',
            keepAlive: true,
            private: false
        }
    },
    {
        path: '/sqb/product/:id',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/product/index'),
        meta: {
            title: '产品',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/product/content/chapter',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/product/content/chapter'),
        meta: {
            title: '法律债务处理大礼包课程-债减减',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/order/:id',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/order/index'),
        meta: {
            title: '订单详情',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/index'),
        meta: {
            title: '我的',
            keepAlive: true,
            private: true
        }
    },
    {
        path: '/sqb/user/share',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/share'),
        meta: {
            title: '分享债减减',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/user/download',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/download'),
        meta: {
            title: '下载债减减',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/user/kefu',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/kefu'),
        meta: {
            title: '我的',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/user/myteam',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/myteam'),
        meta: {
            title: '我的团队',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/income',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/income'),
        meta: {
            title: '我的收益',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/incomeaudit',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/incomeAudit'),
        meta: {
            title: '审核提现',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/withdrawdeposit',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/withdrawDeposit'),
        meta: {
            title: '提现',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/withdrawdepositlist',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/withdrawDepositList'),
        meta: {
            title: '提现详情',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/incomelist',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/incomelist'),
        meta: {
            title: '收益详情',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/code',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/code'),
        meta: {
            title: '兑换码',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/user',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/user'),
        meta: {
            title: '编辑个人信息',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/order',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/order'),
        meta: {
            title: '我的订单',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/coupon',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/coupon'),
        meta: {
            title: '我的优惠券',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/coupon/:id',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/couponContent'),
        meta: {
            title: '我的优惠券',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/user/paychange',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/user/paychange'),
        meta: {
            title: '切换支付通道',
            keepAlive: false,
            private: true
        }
    },
    {
        path: '/sqb/login',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/login/login'),
        meta: {
            title: '登录',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/login/reg',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/login/reg'),
        meta: {
            title: '注册',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/login/pwd',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/login/pwd'),
        meta: {
            title: '重置密码',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/login/weixin',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/login/weixin'),
        meta: {
            title: '微信登录',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/login/bind',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/login/bind'),
        meta: {
            title: '绑定',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/notice/20210121',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/notice/20210121'),
        meta: {
            title: '公告',
            keepAlive: false,
            private: false
        }
    },
    {
        path: '/sqb/report/boss',
        component: () =>
            import(/* webpackChunkName: "sqb" */ './view/app/report/boss'),
        meta: {
            title: '公告',
            keepAlive: false,
            private: false
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