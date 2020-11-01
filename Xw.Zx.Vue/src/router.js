import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'


Vue.use(Router)

export default new Router({
    routes: [{
            path: '/login',
            component: () =>
                import ('./views/login/index'),
            hidden: true
        },
        // {
        //   path: '/',
        //   name: '会议管理',
        //   component: Home,
        //   iconCls: 'el-icon-setting',
        //   children: [
        //     { path: '/main', component: About, name: '主页', hidden: true },
        //     { path: '/meeting', component: () => import('./views/meeting/index'), name: '会议管理' },
        //   ]
        // },
        {
            path: '/',
            name: '系统设置',
            component: Home,
            iconCls: 'el-icon-cpu',
            children: [{
                path: '/sys/paychange',
                component: () =>
                    import ('./views/sys/payChage/index'),
                name: '支付通道'
            }, ]
        },
        {
            path: '/',
            name: '用户管理',
            component: Home,
            iconCls: 'el-icon-menu',
            children: [{
                path: '/member',
                component: () =>
                    import ('./views/member/index'),
                name: '用户管理'
            }, ]
        },
        {
            path: '/',
            name: '账户管理',
            component: Home,
            iconCls: 'el-icon-tickets',
            children: [{
                    path: '/order',
                    component: () =>
                        import ('./views/order/index'),
                    name: '毛收入管理'
                },
                {
                    path: '/incomeAccount',
                    component: () =>
                        import ('./views/incomeAccount/index'),
                    name: '分润管理'
                },
                {
                    path: '/withdrawDeposit',
                    component: () =>
                        import ('./views/withdrawDeposit/index'),
                    name: '提现管理'
                },
                {
                    path: '/updateVipAuthCode',
                    component: () =>
                        import ('./views/updateVipAuthCode/index'),
                    name: '升级码管理'
                }, {
                    path: '/applyForZx',
                    component: () =>
                        import ('./views/applyForZx/index'),
                    name: '申请追息管理'
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
                        import ('./views/lxComputer/index'),
                    name: '利息计算器'
                }
            ]
        }
    ]
})