import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken,getUserInfo } from '@/public/auth'

//材料清单列表
export function getJiontFileList(fx_bizid) {
  return request({
    url: apiUrl.GET_JOINT_FILELIST,
    method: 'post',
    data: {token:getToken(),fx_bizid:fx_bizid}
  })
}
//材料清单列表
export function CheckJiontFileList(wiid) {
  return request({
    url: apiUrl.CHECK_JOINT_FILELIST,
    method: 'post',
    data: {token:getToken(),wiid:wiid}
  })
}
//带方案接件保存
export function SavePlanJiontData(params) {
  params.token=getToken();
  
  return request({
    url: apiUrl.SAVE_PLAN_JOINT_DATA,
    method: 'post',
    data:params
  })
}
//不带方案接件保存
export function SaveNoPlanJiontData(params) {
  params.token=getToken();
  return request({
    url: apiUrl.SAVE_NOPLAN_JOINT_DATA,
    method: 'post',
    data:params
  })
}
//联合图审待办
export function GetJiontList(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_JOINT_LIST,
    method: 'post',
    data:params
  })
}
//联合图审待办
export function GetPendJiontList(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_PEND_LIST,
    method: 'post',
    data:params
  })
}
//联合图审已办
export function GetDoneJiontList(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_DONE_LIST,
    method: 'post',
    data:params
  })
}
//联合图审办结
export function GetConcluseJiontList(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_CONCLUSE_LIST,
    method: 'post',
    data:params
  })
}
//联合图审挂起
export function GetHandleUpJiontList(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_HANDLEUP_LIST,
    method: 'post',
    data:params
  })
}

