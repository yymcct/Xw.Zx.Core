import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'

//数据字典父节点的添加
export function getDataParents() {
  return request({
    url: apiUrl.GET_DICTIONARIES_PARENTS,
    method: 'post',
    data: { token:getToken()}
  })
}
//数据字典的列表
export function getDataList(page) {
  return request({
    url: apiUrl.GET_DICTIONARIES_LIST,
    method: 'post',
    data: { token:getToken(),page}
  })
}
//数据字典一级添加
export function getDataLEVEL1(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_DICTIONARIES_PARENTS,
    method: 'post',
    data: params
  })
}

//数据字典二级添加
export function getDataLEVEL2(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_DICTIONARIES_CHILD,
    method: 'post',
    data: params
  })
}

//数据字典的删除
export function getDataDEL(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_DICTIONARIES_DEL,
    method: 'post',
    data: params
  })
}

//数据字典的修改
export function getDataEdit(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_DICTIONARIES_EDIT,
    method: 'post',
    data: params
  })
}