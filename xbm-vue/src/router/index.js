import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)
const originalPush = Router.prototype.push
Router.prototype.push = function push(location) {
  return originalPush.call(this, location).catch(err => err)
}
export const constantRouterMap = [{
    path: "/",
    component: resolve => require(['@/pages/login/index'], resolve),// /pages/index
    meta: {
       requireAuth: false, // 添加该字段，表示进入这个路由是需要登录的
    },
    children:[{
      path: '/announDetail',
      name:'announDetail',
      component: resolve => require(['@/pages/home/Announcement/Detail'], resolve),
      meta: {
        requireAuth: false, // 添加该字段，表示进入这个路由是需要登录的
      },
    }]
  }, {
    path: '/homeAnnounDetail',
    name:'homeAnnounDetail',
    component: resolve => require(['@/pages/home/Announcement/homeAnnounDetail'], resolve),
    meta: {
      requireAuth: false, // 添加该字段，表示进入这个路由是需要登录的
    },
  }, 
  {
    path: '/noticeDetail',
    component: resolve => require(['@/pages/home/notice/noticeDetail'], resolve),
    meta: {
      requireAuth: false, // 添加该字段，表示进入这个路由是需要登录的
    },
  }, 
 {
    path: '/newsDetail',
    component: resolve => require(['@/pages/home/news/newsDetail'], resolve),
    meta: {
      requireAuth: false, // 添加该字段，表示进入这个路由是需要登录的
    },
  },
  {
    path: '/lawsDetail',
    component: resolve => require(['@/pages/home/laws/lawsDetail'], resolve),
    meta: {
      requireAuth: false, // 添加该字段，表示进入这个路由是需要登录的
    },
  },
  {
    path: "/test",
    component: resolve => require(['@/pages/404'], resolve),
  },
  {
    path: "/login",
    component: resolve => require(['@/pages/login/index'], resolve),
  },
  {
    path: "/regist",
    component: resolve => require(['@/pages/login/Register'], resolve),
  },
  // 电子政务
  {
    path: "/manage",
    component: resolve => require(['@/pages/application/manage/index'], resolve),
    meta: {
      requireAuth: true, // 添加该字段，表示进入这个路由是需要登录的
    },
    children: [{
      path: '/manage/Organization', //组织机构管理
      component: resolve => require(['@/pages/application/approval/sysManagement/Organization/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, 
    {
      path: '/manage/email', //内部邮件
      name:'email',
      component: resolve => require(['@/pages/application/approval/sysManagement/email/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, 
    {
      path: '/manage/Enclosure', //附件管理
      component: resolve => require(['@/pages/application/manage/sysManagement/Enclosure/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, 
    {
      path: '/manage/ElectronicLicense', //电子证照
      component: resolve => require(['@/pages/application/manage/sysManagement/ElectronicLicense/index'], resolve),
      meta: {
        requireAuth: true,
      }
    },{
      path: '/manage/LicenseCancel', //电子证照作废
      component: resolve => require(['@/pages/home/PersonCenter/EleLicense'], resolve),
      // component: resolve => require(['@/pages/application/manage/LicenseCancel/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/manage/filingCabinet', //文档管理
      component: resolve => require(['@/pages/application/approval/sysManagement/filingCabinet/index'], resolve),
      meta: {
        requireAuth: true,
      }
    },{
      path: '/manage/schedule', //日程管理
      component: resolve => require(['@/pages/application/approval/sysManagement/schedule/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/manage/address', //通讯录管理
      component: resolve => require(['@/pages/application/approval/sysManagement/address/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/manage/notic', //通知公告
      component: resolve => require(['@/pages/application/approval/sysManagement/notic/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/manage/notice', //公告管理
      component: resolve => require(['@/pages/application/approval/sysManagement/notice/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/manage/News', //新闻中心
      component: resolve => require(['@/pages/application/approval/sysManagement/News/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/manage/Laws', //政策法规
      component: resolve => require(['@/pages/application/approval/sysManagement/Laws/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/manage/Jurisdiction', //一张图权限管理
      component: resolve => require(['@/pages/application/approval/sysManagement/Jurisdiction/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/manage/region', //行政区代码
      component: resolve => require(['@/pages/application/manage/sysManagement/Region/index'], resolve),
      meta: {
        requireAuth: true,
      }
    },{
      path: '/manage/msgManage', //消息提醒
      component: resolve => require(['@/pages/application/approval/sysManagement/msgManage/index'], resolve),
      meta: {
        requireAuth: true,
      }
    },{
      path: '/manage/LogMagement', //日志提醒
      component: resolve => require(['@/pages/application/manage/sysManagement/LogMagement/index'], resolve),
      meta: {
        requireAuth: true,
      }
    },
    {
      path: '/manage/Dictionaries', //shujuzidian
      component: resolve => require(['@/pages/application/approval/sysManagement/Dictionaries/index'], resolve),
      meta: {
        requireAuth: true,
      }
    },
    
    
  ]
  },
  // 网上自助申请
  {
    path: "/apply",
    component: resolve => require(['@/pages/application/apply/index'], resolve),
    meta: {
      requireAuth: true, // 添加该字段，表示进入这个路由是需要登录的
    },
  },
  // 窗口受理
  {
    path: "/cksl/receiptManage",
    component: resolve => require(['@/pages/application/cksl/index'], resolve),
    redirect:'/cksl/UnifiedAcceptance',
    children: [{
      path: '/cksl/receipt', //收件
      component: resolve => require(['@/pages/application/cksl/ReceiptManage/receipt'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/cksl/UnifiedAcceptance', //统一受理
      component: resolve => require(['@/pages/application/cksl/ReceiptManage/UnifiedAcceptance'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/cksl/powerOperation', //权力运行系统
      component: resolve => require(['@/pages/application/cksl/ReceiptManage/powerOperation'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/cksl/DoneBusiness', //办结管理--已办业务
      component: resolve => require(['@/pages/application/cksl/ReceiptManage/DoneBusiness'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/cksl/BackBusiness', //办结管理--补齐补正业务
      component: resolve => require(['@/pages/application/cksl/ReceiptManage/BackBusiness'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/cksl/CountAnalysis', //统计分析
      component: resolve => require(['@/pages/application/cksl/CountAnalysis/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/cksl/DayCount', //日统计分析
      component: resolve => require(['@/pages/application/cksl/CountAnalysis/dayCount'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/cksl/MonthCount', //月统计分析
      component: resolve => require(['@/pages/application/cksl/CountAnalysis/monthCount'], resolve),
      meta: {
        requireAuth: true,
      }
    }]
  },

  // 业务审批
  {
    path: "/approval",
    component: resolve => require(['@/pages/application/approval/index'], resolve),
    meta: {
      requireAuth: true, // 添加该字段，表示进入这个路由是需要登录的
    },
    children: [ {
      path: '/approval/Dictionaries', //数据字典设置
      component: resolve => require(['@/pages/application/approval/sysManagement/Dictionaries/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/approval/LogMagement', //日志管理
      component: resolve => require(['@/pages/application/approval/sysManagement/LogMagement/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/approval/Idioms', //日常用语
      component: resolve => require(['@/pages/application/approval/sysManagement/Idioms/index'], resolve),
      meta: {
        requireAuth: true,
      }
    },{
      path: '/approval/msgManage', //消息提醒
      component: resolve => require(['@/pages/application/approval/sysManagement/msgManage/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }, {
      path: '/approval/personalInformation', //个人信息
      component: resolve => require(['@/pages/application/approval/sysManagement/personalInformation/index'], resolve),
      meta: {
        requireAuth: true,
      }
    }]
  },
  // 重点事项督办
  {
    path: "/keyspecial",
    component: resolve => require(['@/pages/application/keyspecial/index'], resolve),
    meta: {
      requireAuth: true, // 添加该字段，表示进入这个路由是需要登录的
    },
  },
  // APP
  {
    path: "/handle",
    component: resolve => require(['@/pages/application/handle/index'], resolve),
  },
]
export default new Router({
  // mode: 'history',
  routes: constantRouterMap
})
