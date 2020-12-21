import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken,getUserInfo } from '@/public/auth'

//新建修改项目
export function saveProject(params) {
  params.token=getToken();
  params.uid=getUserInfo().ur_ident;
  params.unm=getUserInfo().ur_name;
  params.zone=getUserInfo().ur_zone;
  return request({
    url: apiUrl.SAVE_PROJECT,
    method: 'post',
    data: params
  })
}
//项目列表
export function getProjectList(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  // params.pagesize=10;
  return request({
    url: apiUrl.GET_PRO_RESERVELIB_DATA,
    method: 'post',
    data: params
  })
}
//项目详情
export function getProjectDetail(wiid) {
  return request({
    url: apiUrl.GET_PRO_RESERVELIB_DETAIL,
    method: 'post',
    data: {token:getToken(),ur_ident:getUserInfo().ur_ident,wiid:wiid}
  })
}
//项目详情时间节点
export function getProjectDetailNode(wiid) {
  return request({
    url: apiUrl.GET_PRO_DETAIL_NODE,
    method: 'post',
    data: {token:getToken(),wiid:wiid}
  })
}
//项目删除
export function delProject(wiid) {
  return request({
    url: apiUrl.DEL_PRO_RESERVELIB,
    method: 'post',
    data: {token:getToken(),wiid:wiid}
  })
}

// var obj={
//   token:"henan123456789shuhui",
//   // uid:7,
//   // unm:"管理员",
//   // zone:"管理员",
//   // xmmc:"项目名称",
//   // xmlx:1,
//   // cjsj:"2019-8-7",
//   // xmtzly:1,
//   // tdhqfs:2,
//   // gcfl:1,
//   // jsxz:1,
//   // gbhydm:"A001",
//   // nkgsj:"2020-10-10",
//   // njcsj:"2021-12-31",ztze:200,xmxzqh:"老城区",
//   // jsdd:"项目建设具体地点",jsgmjnr:"建设规模及内容",
//   // ydmj:500,"jzmj":450,jzmj:1,
//   // jsdwmc:"建设单位名称",jsdwzjlx:"建设单位证件类型",
//   // jsdwzjhm:"建设单位证件号码",dwlx:1,
//   // lxr:"刘露露",
//   // lxrzjhm:"123456789",lrrdh:"13015510175",lxryx:"1297971096@qq.com",
//   // wiid:"190808008",jsdwid:"JSDW190808020",xmbh:"190808018",
//   DATA:[{"jzwjmc":"测试界址红线22"},{"jzwjmc":"测试界址红线11"}],
//   DATA1:[{"ac_name":"测试普通文件上传1"},{"ac_name":"测试普通文件上传2"}],
//   }
//项目策划-待办
export function getProjectPlan(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_PRO_PLAN,
    method: 'post',
    data: params
  })
}
//责任待办列表
export function GetResponsePro(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_PRO_RESPONSE_LIST,
    method: 'post',
    data: params
  })
}
//项目策划-已办列表
export function getProjectPlanDone(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_PRO_PLAN_DONE,
    method: 'post',
    data: params
  })
}
//项目策划-挂起列表
export function getProHangUp(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_PRO_HANGUP,
    method: 'post',
    data: params
  })
}
//项目策划-办结列表
export function getProjectConclusion(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_PRO_CONCLUSION,
    method: 'post',
    data: params
  })
}
//项目策划挂起操作
export function handleProHangUp(params) {
  params.token=getToken();
  params.wi_gqid=getUserInfo().ur_ident;
  return request({
    url: apiUrl.HANDLE_PRO_HANGUP,
    method: 'post',
    data: params
  })
}
//审批意见
export function getApproveIssue(wiid) {
  return request({
    url: apiUrl.GET_PRO_ISSUES,
    method: 'post',
    data: {token:getToken(),wiid:wiid}
  })
}
//项目实施库新增修改
export function UpdateProImplementLib(params) {
  params.token=getToken();
  params.fqrid=getUserInfo().ur_ident;
  return request({
    url: apiUrl.UPDATE_PRO_SCHEMELIB,
    method: 'post',
    data: params
  })
}
//项目实施库列表
export function GetProImplementLib(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_PRO_SCHEMELIB,
    method: 'post',
    data: params
  })
}

//项目实施库详情
export function GetProSchemeLibDetail(wiid) {
  return request({
    url: apiUrl.GET_PRO_SCHEMELIB_DETAIL,
    method: 'post',
    data: {token:getToken(),wiid:wiid}
  })
}

// 发起权限

export function GetProStartPermiss() {
  return request({
    url: apiUrl.GET_PRO_START_PERMISS,
    method: 'post',
    data: {token:getToken(),ur_ident:getUserInfo().ur_ident}
  })
}
//推送消息新增
export function addPushMsg(wiid,gzname,at_theme) {
  return request({
    url: apiUrl.ADD_MSG_TIPS,
    method: 'post',
    data:{token:getToken(),ur_ident:getUserInfo().ur_ident,wiid,gzname,at_theme}
  })
}
//同意不同意权限
export function GetApprovePermiss(wiid) {
  return request({
    url: apiUrl.GET_APPROVE_PREMISS,
    method: 'post',
    data: {token:getToken(),wiid:wiid}
  })
}
//策划启动未发送人员关闭回调
export function handleCloseCallBack(wiid) {
  return request({
  url: apiUrl.HANDLE_CLOSE_SEND,
    method: 'post',
    data: {token:getToken(),wiid:wiid}
  })
}
//项目计划库
export function getProSchemeList(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_PROPLANLIB,
    method: 'post',
    data: params
  })
}
//首页项目策划待办
export function getProjectPend() {
  return request({
    url: apiUrl.GET_PROJECT_PEND,
    method: 'post',
    data: {token:getToken(),ur_ident:getUserInfo().ur_ident}
  })
}