import request from '@/public/config'
import {
  apiUrl
} from '@/public/apiUrl'
import {
  getToken,
  getUserInfo
} from '@/public/auth'
import axios from 'axios'
export function homeTodoList(ur_ident) {
  return request({
    url: apiUrl.GET_TO_List,
    method: 'post',
    data: {
      token: getToken(),
      ur_ident
    }
  })
}
export function homeEmailCount() {
  return request({
    url: apiUrl.GET_EMAIL_COUNT,
    method: 'post',
    data: {
      token: getToken(),
      uid:getUserInfo().ur_ident
    }
  })
}
//督察计数
export function supervisionCount() {
  return request({
    url: apiUrl.GET_SUPERVISION_COUNT,
    method: 'post',
    data: {
      token: getToken()
    }
  })
}
//业务能效
export function efficiencyCount(year) {
  return request({
    url: apiUrl.GET_EFFICIENCY_COUNT,
    method: 'post',
    data: {
      token: getToken(),
      year
    }
  })
}

//通知公告
export function homeNotice(params) {
  params.token = getToken();
  return request({
    url: apiUrl.GET_HOME_NOTICE,
    method: 'post',
    data: params
  })
}
export function checkNotice(params) {
    params.token = getToken();
  return request({
    url: apiUrl.CHECK_NOTICE,
    method: 'post',
    data: params
  })
}
//个人中心-效能监管
export function getHomeEfficiencyCount() {
  return request({
    url: apiUrl.GET_HOME_EFFICIENCY_COUNT,
    method: 'post',
    data: {token:getToken(),uid:getUserInfo().ur_ident}
  })
}
//效能监管_局长-统计
export function getLeaderEfficiencyCount() {
  return request({
    url: apiUrl.GET_LEADER_EFFICIENCY_COUNT,
    method: 'post',
    data: {token:getToken()}
  })
}
//效能监管不动产登记
export function getRealStateDetail(data) {
  return request({
    url: '/api/BDCBizStatusQuery',
    method: 'get',
    data
  })
}
//效能监管_局长-行政审批
export function CheckSPEfficiencyDetail(page,lx) {
  return request({
    url: apiUrl.CHECK_SP_EFFICIENCY_DETAIL,
    method: 'post',
    data: {token:getToken(),page,lx}
  })
}
//效能监管_局长-政务管理
export function CheckZWEfficiencyDetail(page,lx) {
  return request({
    url: apiUrl.CHECK_ZW_EFFICIENCY_DETAIL,
    method: 'post',
    data: {token:getToken(),page,lx}
  })
}
//效能监管_局长-项目策划
export function CheckCHEfficiencyDetail(page,lx) {
  return request({
    url: apiUrl.CHECK_CH_EFFICIENCY_DETAIL,
    method: 'post',
    data: {token:getToken(),page,lx}
  })
}
//效能监管_局长-联合审查
export function CheckSCEfficiencyDetail(page,lx) {
  return request({
    url: apiUrl.CHECK_SC_EFFICIENCY_DETAIL,
    method: 'post',
    data: {token:getToken(),page,lx}
  })
}
// //公示公告详情
// export function CheckAnnouncementDetail(ZZID) {
//   // params.token = getToken();
//   return request({
//     url: apiUrl.GET_HOME_ANNOUNCEMENT_DETAIL,
//     method: 'get',
//     data: {params:{ZZID}}
//   })
// }
// export function checkNotice(params) {
//     params.token = getToken();
//   return request({
//     url: apiUrl.CHECK_NOTICE,
//     method: 'post',
//     data: params
//   })
// }


//学习教育

export function homeStudy(params) {
  params.token = getToken();
  return request({
    url: apiUrl.GET_HOME_STUDY,
    method: 'post',
    data: params
  })
}
export function getHomeOfficePend(page) {
  return request({
    url: apiUrl.GET_TODO_OFFICE,
    method: 'post',
    data: {token:getToken(),uid:getUserInfo().ur_ident,page:page}
  })
}
//证照作废权限列表
export function getAuthLiense() {
  return request({
    url: apiUrl.GET_AUTHLICENSE_LIST,
    method: 'post',
    data: {token:getToken(),uid:getUserInfo().ur_ident}
  })
}
//联合图审待办
export function GetPendJiontList(page) {
  return request({
    url: apiUrl.GET_PEND_LIST,
    method: 'post',
    data:{
        xmmc: '',
        sxmc: '',
        page: page,
        pagesize: 10,
       token:getToken(),
       uid:getUserInfo().ur_ident
    }
  })
}
export function getProjectList(page) {
  return request({
    url: apiUrl.GET_PRO_RESERVELIB_DATA,
    method: 'post',
    data: {
          page: page,
          pagesize:10,
          xmmc:'',
          kjss:'',
          cjsj:'',
          cjsj2:'',
          tdhqfs:'',
          ztze1:'',
          ztze2:'',
          xmtzly:'',
          lxlx:'',
          token:getToken(),
          uid:getUserInfo().ur_ident
     }
  })
}
// //快捷方式应用列表
// export function shortcutList(params) {
//   params.token = getToken();
//   return request({
//     url: apiUrl.GET_SHORTCUT_LIST,
//     method: 'post',
//     data: params
//   })
// }

// //获取屏幕

// export function getScreen(params) {
//   params.token = getToken();
//   return request({
//     url: apiUrl.GET_SHORTCUT_SCREEN,
//     method: 'post',
//     data: params
//   })
// }

// //增加屏

// export function addScreen(params) {
//   params.token = getToken();
//   return request({
//     url: apiUrl.GET_SHORTCUT_ADDSCREEN,
//     method: 'post',
//     data: params
//   })
// }

// //删除屏
// export function delScreen(params) {
//   params.token = getToken();
//   return request({
//     url: apiUrl.GET_SHORTCUT_DELSCREEN,
//     method: 'post',
//     data: params
//   })
// }
//获取主页的日志
// export function homeLog(params) {
//   params.token = getToken();
//   return request({
//     url: apiUrl.GET_HOME_LOG,
//     method: 'post',
//     data: params
//   })
// }
// //增加应用
// export function addApp(params) {
//   params.token = getToken();
//   return request({
//     url: apiUrl.GET_SHORTCUT_ADDAPP,
//     method: 'post',
//     data: params
//   })
// }

// //增加应用
// export function delApp(params) {
//   params.token = getToken();
//   return request({
//     url: apiUrl.GET_SHORTCUT_DELAPP,
//     method: 'post',
//     data: params
//   })
// }

// //增加应用
// export function screenOrder(params) {
//   params.token = getToken();
//   return request({
//     url: apiUrl.GET_SHORTCUT_SCREENORDER,
//     method: 'post',
//     data: params
//   })
// }

// //在线人数
// export function getOnLine(params) {
//   params.token = getToken();
//   return request({
//     url: apiUrl.GET_HOME_ONLINE,
//     method: 'post',
//     data: params
//   })
// }
//政策法规列表,新闻中心列表
export function getLawsData(params) {
  params.token = getToken();
  return request({
    url: apiUrl.GET_LAWS_LIST,
    method: 'post',
    data: params
  })
}
//政策法规新增
export function addLaws(params) {
  params.token = getToken();
  params.ur_ident = getUserInfo().ur_ident;
  return request({
    url: apiUrl.ADD_LAWS,
    method: 'post',
    data: params
  })
}
//删除政策法规
export function delLaws(wiid) {
  return request({
    url: apiUrl.DEL_LAWS,
    method: 'post',
    data: {
      token: getToken(),
      wiid: wiid
    }
  })
}
//查看政策法规
export function checkLaws(wiid) {
  return request({
    url: apiUrl.CHECK_LAWS,
    method: 'post',
    data: {
      token: getToken(),
      wiid: wiid
    }
  })
}
//政策法规目录 type:1 政策法规
export function getLawsCat(type) {
  return request({
    url: apiUrl.GET_LAWS_CAT,
    method: 'post',
    data: {
      token: getToken(),
      fl: type
    }
  })
}
//政策法规目录新增
export function addCat(name, type) {
  return request({
    url: apiUrl.ADD_LAWS_CAT,
    method: 'post',
    data: {
      token: getToken(),
      name: name,
      fl: type
    }
  })
}
//政策法规目录删除
export function delCat(mlid) {
  return request({
    url: apiUrl.DEL_LAWS_CAT,
    method: 'post',
    data: {
      token: getToken(),
      mlid: mlid
    }
  })
}
//使用帮助目录
export function getHelpCat(params) {
  params.token = getToken();
  return request({
    url: apiUrl.GET_HELP_LIST,
    method: 'post',
    data: params
  })
}
//使用帮助目录添加
export function addHelpCat(nodename, parentid) {
  return request({
    url: apiUrl.ADD_HELP_LIST,
    method: 'post',
    data: {
      token: getToken(),
      nodename: nodename,
      parentid: parentid
    }
  })
}
//使用帮助目录删除
export function delHelpCat(nodeid) {
  return request({
    url: apiUrl.DEL_HELP_LIST,
    method: 'post',
    data: {
      token: getToken(),
      nodeid: nodeid
    }
  })
}
//使用帮助添加
export function addHelpCont(nodeid, DATA) {
  return request({
    url: apiUrl.UPDATE_HELP_CONT,
    method: 'post',
    data: {
      token: getToken(),
      nodeid: nodeid,
      DATA: DATA
    }
  })
}
//使用帮助详情
export function getHelpCont(nodeid) {
  return request({
    url: apiUrl.GET_HELP_CONT,
    method: 'post',
    data: {
      token: getToken(),
      nodeid: nodeid
    }
  })
}

