import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken,getUserInfo } from '@/public/auth'
//获取收件箱列表
export function getInbox(page) {
  return request({
    url: apiUrl.GET_INBOX_LIST,
    method: 'post',
    data: { token:getToken(),page,ur_ident:getUserInfo().ur_ident}
  })
}
//获取发件箱列表
export function getOutbox(page) {
  return request({
    url: apiUrl.GET_OUTBOX_LIST,
    method: 'post',
    data: { token:getToken(),page,ur_ident:getUserInfo().ur_ident}
  })
}
//写邮件
export function subAddEmail(params) {
  params.token=getToken();
  return request({
    url: apiUrl.ADD_EMAIL,
    method: 'post',
    data: params
  })
}
//回复邮件
export function subReplyEmail(fs_id,fsr,fs_stzte,fs_bt,jsrxm,jsrid,nr,FJLIST) {
  return request({
    url: apiUrl.REPLY_EMAIL,
    method: 'post',
    data:  {token:getToken(),fs_id,fsr,fs_stzte,fs_bt,jsrxm,jsrid,nr,FJLIST}
  })
}
//草稿箱发送
export function subDraftEmail(params) {
  params.token=getToken();
  return request({
    url: apiUrl.SUBMIT_DRAFT_FORM,
    method: 'post',
    data: params
  })
}
//草稿箱列表
export function getDraftEmailList(page,data) {
    return request({
      url: apiUrl.SEARCH_EMAIL_DRAFT_LIST,
      method: 'post',
      data: { token:getToken(),page,...data,ur_ident:getUserInfo().ur_ident}
    })
  }
  //已发送列表
  export function getOutEmailList(page,data) {
    return request({
      url: apiUrl.SEARCH_EMAIL_OUT_LIST,
      method: 'post',
      data: { token:getToken(),page,...data,ur_ident:getUserInfo().ur_ident}
    })
  }
  //收件箱列表
  export function getInxEmailList(page,data) {
    return request({
      url: apiUrl.SEARCH_EMAIL_INBOX_LIST,
      method: 'post',
      data: { token:getToken(),page,...data,ur_ident:getUserInfo().ur_ident}
    })
  }
//选择人员列表
export function getEmailPerson() {
  return request({
    url: apiUrl.GET_EMAIL_PERSON_LIST,
    method: 'post',
    data: { token:getToken()}
  })
}
//懒加载人员列表
export function getEmailDepartPerson(ur_node) {
  return request({
    url: apiUrl.GET_EMAIL_LOAD_PERSON_LIST,
    method: 'post',
    data: { token:getToken(),ur_node:ur_node}
  })
}
//懒加载部门列表
export function getEmailDepart() {
  return request({
    url: apiUrl.GET_EMAIL_DEPART_LIST,
    method: 'post',
    data: {token:getToken()}
  })
}
//查看邮件详情
export function checkEmailDetail(wiid) {
  return request({
    url: apiUrl.CHECK_EMAIL_DETAIL,
    method: 'post',
    data: { token:getToken(),ur_ident:getUserInfo().ur_ident,wiid}
  })
}
//获取草稿箱列表
export function getDraftData(page) {
  return request({
    url: apiUrl.GET_DRAFT_LIST,
    method: 'post',
    data: { token:getToken(),ur_ident:getUserInfo().ur_ident,page}
  })
}
//获取未读条数
export function getUnreadNum() {
  return request({
    url: apiUrl.GET_UNREAD_NUM,
    method: 'post',
    data: {token:getToken(),wiid:getUserInfo().ur_ident}
  })
}
//邮件删除<!-- data: {token:getToken(),uid:getUserInfo().ur_ident,wiid,zt:'f'} -->
export function delEmail(params) {
  params.token=getToken();
  params.uid=getUserInfo().ur_ident;
  return request({
    url: apiUrl.DEL_EMAIL,
    method: 'post',
    data:params

  })
}

  //电子邮件自定义添加分组
export function addSelfGroup(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.ADD_SELF_GROUP,
    method: 'post',
    data: params
  })
}
//电子邮件自定义添加分组成员
export function addSelfGroupPerson(params) {
  params.token=getToken();
  return request({
    url: apiUrl.ADD_SELF_GROUP_PERSON,
    method: 'post',
    data:params
  })
}

//电子邮件自定义分组列表
export function addSelfGroupList(){
  return request({
    url: apiUrl.ADD_SELF_GROUP_LIST,
    method: 'post',
    data: {token:getToken(),ur_ident:getUserInfo().ur_ident}
  })
}

//电子邮件删除自定义分组
export function delSelfGroup(params){
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.DEL_SELF_GROUP,
    method: 'post',
    data:params
  })
}
//电子邮件删除自定义分组人员
export function delSelfGroupPerson(params){
  params.token=getToken();
  return request({
    url: apiUrl.DEL_SELF_GROUP_PERSON,
    method: 'post',
    data: params
  })
}


