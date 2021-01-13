import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'

//获取个人权限列表
export function getDataList(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_JURISDICTION_LIST,
    method: 'post',
    data: params
  })
}

//角色权限
export function getDataPersonList(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_JURISDICTION_PERSONLIST,
    method: 'post',
    data: params
  })
}

//获取所有角色权限
export function getDataAllList(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_JURISDICTION_ALLLIST,
    method: 'post',
    data: params
  })
}

//角色权限添加
export function getDataAdd(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_JURISDICTION_ADD,
    method: 'post',
    data: params
  })
}

//角色权限删除
export function getDataDel(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_JURISDICTION_DEL,
    method: 'post',
    data: params
  })
}