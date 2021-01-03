import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'


Vue.use(Router)

export default new Router({
    routes: [{
        path: '/login',
        component: () =>
            import('./views/login/index'),
        hidden: true
    },
    {
        path: '/',
        name: '系统设置',
        component: Home,
        iconCls: 'el-icon-cpu',
        children: [{
            path: '/sys/paychange',
            component: () =>
                import('./views/sys/payChage/index'),
            name: '支付通道'
        }, {
            path: '/sys/orderinfo',
            component: () =>
                import('./views/sys/orderInfo/index'),
            name: '订单统计'
        },]
    },
    {
        path: '/',
        name: '用户管理',
        component: Home,
        iconCls: 'el-icon-menu',
        children: [{
            path: '/member',
            component: () =>
                import('./views/member/index'),
            name: '用户管理'
        },]
    },
    {
        path: '/',
        name: '订单管理',
        component: Home,
        iconCls: 'el-icon-tickets',
        children: [{
            path: '/order',
            component: () =>
                import('./views/order/index'),
            name: '已支付订单'
        },
        {
            path: '/order/waitpay',
            component: () =>
                import('./views/order/waitpay'),
            name: '待支付订单'
        },
        {
            path: '/updateVipAuthCode',
            component: () =>
                import('./views/updateVipAuthCode/index'),
            name: '升级码管理'
        },
        {
            path: '/applyForZx',
            component: () =>
                import('./views/applyForZx/index'),
            name: '申请追息管理'
        },
        {
            path: '/coupon',
            component: () =>
                import('./views/coupon/index'),
            name: '优惠券记录'
        },
        ]
    },
    {
        path: '/',
        name: '分润管理',
        component: Home,
        iconCls: 'el-icon-tickets',
        children: [
            {
                path: '/income/waitAudit',
                component: () =>
                    import('./views/income/waitAudit'),
                name: '待审核'
            },
            {
                path: '/income/sucess',
                component: () =>
                    import('./views/income/sucess'),
                name: '已通过'
            },
            {
                path: '/income/fail',
                component: () =>
                    import('./views/income/fail'),
                name: '已拒绝'
            },
            {
                path: '/incomeAccount',
                component: () =>
                    import('./views/incomeAccount/index'),
                name: '账户详情'
            },
        ]
    },
    {
        path: '/',
        name: '提现管理',
        component: Home,
        iconCls: 'el-icon-tickets',
        children: [{
            path: '/withdraw/tongjibuAudit',
            component: () =>
                import('./views/withdrawDeposit/tongjibuAudit'),
            name: '统计部审核'
        }, {
            path: '/withdraw/waitaduit',
            component: () =>
                import('./views/withdrawDeposit/caiwuAudit'),
            name: '财务部审核'
        },
        {
            path: '/withdraw/waitpay',
            component: () =>
                import('./views/withdrawDeposit/caiwuManagerAudit'),
            name: '财务部打款'
        },
        {
            path: '/withdraw/sucess',
            component: () =>
                import('./views/withdrawDeposit/sucess'),
            name: '提现成功'
        },
        {
            path: '/withdraw/fail',
            component: () =>
                import('./views/withdrawDeposit/fail'),
            name: '提现失败'
        },
        ]
    },
    {
        path: '/',
        name: '利息计算器',
        component: Home,
        iconCls: 'el-icon-tickets',
        children: [{
            path: '/lxcomputer',
            component: () =>
                import('./views/lxComputer/index'),
            name: '利息计算器'
        }
        ]
    }
    ]
})