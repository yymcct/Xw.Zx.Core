import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'

//获取组织机构列表
export function getOrgTree() {
  return request({
    url: apiUrl.GET_ORG_LIST,
    method: 'post',
    data: { token:getToken()}
  })
}

//用户信息列表UR_NAME,UR_NODE
export function getUserList(ur_name,ur_node,page) {
  return request({
    url: apiUrl.GET_USER_LIST,
    method: 'post',
    data: {token:getToken(),ur_name,ur_node,page}
  })
}
//用户信息新增
export function addUser(params) {
  params.token=getToken();
  return request({
    url: apiUrl.ADD_USER,
    method: 'post',
    data: params
  })
}
//用户信息修改
export function editUser(params) {
  params.token=getToken();
  return request({
    url: apiUrl.EDIT_USER,
    method: 'post',
    data:params
  })
}
//用户信息删除
export function delUser(ur_ident) {
  return request({
    url: apiUrl.DEL_USER,
    method: 'post',
    data: {token:getToken(),ur_ident}
  })
}
//部门管理查询列表
export function getDepartList(or_uper,page) {
  return request({
    url: apiUrl.GET_DEPART_LIST,
    method: 'post',
    data: {token:getToken(),or_uper,page}
  })
}
//部门管理_新增
export function addDepart(params) {
  params.token=getToken();
  return request({
    url: apiUrl.ADD_DEPART,
    method: 'post',
    data: params
  })
}
//部门管理_修改
export function editDepart(params) {
  params.token=getToken();
  return request({
    url: apiUrl.EDIT_DEPART,
    method: 'post',
    data:params
  })
}
//部门管理_删除
export function delDepart(or_code) {
  return request({
    url: apiUrl.DEL_DEPART,
    method: 'post',
    data: {token:getToken(),or_code}
  })
}
//单位管理列表
export function getUnitList(page) {
  return request({
    url: apiUrl.GET_UNIT_LIST,
    method: 'post',
    data: {token:getToken(),page}
  })
}
//单位管理添加
export function addUnit(params) {
  params.token=getToken();
  return request({
    url: apiUrl.ADD_UNIT,
    method: 'post',
    data: params
  })
}

//用户密码重置
export function resetPassword(params) {
  params.token=getToken();
  return request({
    url: apiUrl.RESET_PASSWORD,
    method: 'post',
    data: params
  })
}





