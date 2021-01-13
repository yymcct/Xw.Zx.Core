import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken,getUserInfo } from '@/public/auth'

//办件类型
export function getBJLXDict() {
  return request({
    url: apiUrl.GET_BJLX_DICT,
    method: 'post',
    data: { token:getToken()}
  })
}
//事项名称
export function getSXMCDict() {
  return request({
    url: apiUrl.GET_SXMC_DICT,
    method: 'post',
    data: { token:getToken()}
  })
}
//有效证件
export function getCardDict() {
  return request({
    url: apiUrl.GET_CARD_DICT,
    method: 'post',
    data: { token:getToken()}
  })
}

//审批类型
export function getApprovalType() {
  return request({
    url: apiUrl.GET_APPROVAL_TYPE,
    method: 'post',
    data: { token:getToken()}
  })
}
//项目性质
export function getProjectNature() {
  return request({
    url: apiUrl.GET_PROJECT_NATURE,
    method: 'post',
    data: { token:getToken()}
  })
}

//项目类型
export function getProTypeData() {
  return request({
    url: apiUrl.GET_PROJECT_TYPE,
    method: 'post',
    data: { token:getToken()}
  })
}
//项目立项类型
export function getProRegistType() {
  return request({
    url: apiUrl.GET_PROJECT_REGIST_TYPE,
    method: 'post',
    data: { token:getToken()}
  })
}
//建设性质
export function getConstNatData() {
  return request({
    url: apiUrl.GET_CONSTRUCTIVE_NATURE,
    method: 'post',
    data: { token:getToken()}
  })
}
//申报来源
export function getDeclareSource() {
  return request({
    url: apiUrl.GET_DECLARE_SOURCE,
    method: 'post',
    data: { token:getToken()}
  })
}
//法人有效证件
export function getLegalPerson() {
  return request({
    url: apiUrl.GET_LEGAL_PERSON,
    method: 'post',
    data: { token:getToken()}
  })
}
//个人有效证件
export function getOwerPerson() {
  return request({
    url: apiUrl.GET_OWER_PERSON,
    method: 'post',
    data: { token:getToken()}
  })
}