import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken,getUserInfo } from '@/public/auth'

//通讯录列表
export function getAddressList(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_ADDRESS_LIST,
    method: 'post',
    data: params
  })
}

//通讯录列表
export function getAddressPersonList(params) {
  params.token=getToken();
  return request({
    url: apiUrl.SEL_ADDRESS_PERSON,
    method: 'post',
    data: params
  })
}
//通讯录新增
export function addAddress(params) {
  params.token=getToken();
  return request({
    url: apiUrl.ADD_ADDRESS,
    method: 'post',
    data: params
  })
}
//通讯录修改
export function editAddress(params) {
  params.token=getToken();
  return request({
    url: apiUrl.EDIT_ADDRESS,
    method: 'post',
    data:params
  })
}
//通讯录删除
export function delAddress(epid) {
  return request({
    url: apiUrl.DEL_ADDRESS,
    method: 'post',
    data:{token:getToken(),epid}
  })
}
//通讯录公共新增修改删除权限
export function delPublicAddress(uid) {
  return request({
    url: apiUrl.SEL_ADDRESS_DEL,
    method: 'post',
    data:{token:getToken(),uid}
  })
}
//通讯录部门选择
export function selAddressDep() {
  return request({
    url: apiUrl.SEL_ADDRESS_DEPART,
    method: 'post',
    data:{token:getToken()}
  })
}
//通知公告列表-已发布
export function getNoticeList(nt_name,page,nt_sender) {
  return request({
    url: apiUrl.GET_RELEASED_NOTICE,
    method: 'post',
    data: {token:getToken(),uid:getUserInfo().ur_ident,nt_name,page,nt_sender}
  })
}
//通知公告列表-未发布
export function getUnreleasedNotice(page,nt_name) {
  return request({
    url: apiUrl.GET_UNRELEASED_NOTICE,
    method: 'post',
    data: {token:getToken(),page,uid:getUserInfo().ur_ident,nt_name}
  })
}
//通知公告新建
export function addNotice(params) {
  params.token=getToken();
  return request({
    url: apiUrl.ADD_NOTICE,
    method: 'post',
    data: params
  })
}
//通知公告修改
export function editNotice(params) {
  params.token=getToken();
  return request({
    url: apiUrl.EDIT_NOTICE,
    method: 'post',
    data:params
  })
}
//通知公告详情
export function checkNotice(wiid) {
  return request({
    url: apiUrl.CHECK_NOTICE,
    method: 'post',
    data:{token:getToken(),uid:getUserInfo().ur_ident,wiid}
  })
}

//通知公告删除
export function delNotice(wiid) {
  return request({
    url: apiUrl.DEL_NOTICE,
    method: 'post',
    data:{token:getToken(),wiid}
  })
}
//通知公告更新发布状态
export function updateNoticeState(wiid) {
  return request({
    url: apiUrl.UPDATE_NOTICE_STATE,
    method: 'post',
    data:{token:getToken(),wiid}
  })
}
//通知公告列表（模糊查询）
export function getNoticeSearchList(params){
  params.token=getToken();
  params.uid=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_NOTICE_LIST,
    method: 'post',
    data:params
  })
}
//本人公告列表（模糊查询）
export function getMyNoticeList(params){
  params.token=getToken();
  params.uid=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_MY_NOTICE_LIST,
    method: 'post',
    data:params
  })
}


 




