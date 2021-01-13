import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'
//登录日志列表
export function getDataList(page) {
  return request({
    url: apiUrl.GET_LOG_LIST,
    method: 'post',
    data: {token:getToken(),page}
  })
}

//登录日志搜索
export function getDataSearch(page,lg_user,lg_time,lg_move) {
  return request({
    url: apiUrl.GET_LOG_SEARCH,
    method: 'post',
    data: {token:getToken(),page,lg_user,lg_time,lg_move}
  })
}

//登录日志删除
export function getDataDel(lg_code) {
  return request({
    url: apiUrl.GET_LOG_DEL,
    method: 'post',
    data: {token:getToken(),lg_code}
  })
}

//登录日志添加
export function getDataAdd(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_LOG_ADD,
    method: 'post',
    data:params
  })
}

//登录日志注销时间
export function getDataLeave(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_LOG_LEAVE,
    method: 'post',
    data:params
  })
}

//操作日志列表
export function getOperationList(params) {
  params.token=getToken();
  return request({
    url:apiUrl.GET_OPERATION_LOG_LIST,
    method: 'post',
    data: params
  })
}

//操作日志删除
export function getOperationListDel(ss_id) {
  return request({
    url: apiUrl.GET_OPERATION_LOG_DEL,
    method: 'post',
    data: {token:getToken(),ss_id}
  })
}