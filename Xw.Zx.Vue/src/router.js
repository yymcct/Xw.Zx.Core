import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import About from './views/About.vue'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/login',
      component: () => import('./views/login/index'),
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
      name: '用户管理',
      component: Home,
      iconCls: 'el-icon-menu',
      children: [
        { path: '/member', component: () => import('./views/member/index'), name: '用户管理' },
      ]
    },
    {
      path: '/',
      name: '账户管理',
      component: Home,
      iconCls: 'el-icon-tickets',
      children: [
        { path: '/order', component: () => import('./views/order/index'), name: '毛收入管理' },
        { path: '/withdrawDeposit', component: () => import('./views/order/index'), name: '提现管理' },
      ]
    }
  ]
})
