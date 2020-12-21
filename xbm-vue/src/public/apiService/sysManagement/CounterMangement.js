import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'
//获取列表
export function getDataList(page) {
  return request({
    url: apiUrl.GET_COUNT_LIST,
    method: 'post',
    data: {token:getToken(),page}
  })
}
//获取类型
export function getDataType() {
  return request({
    url: apiUrl.GET_COUNT_TYPE,
    method: 'post',
    data: {token:getToken()}
  })
}
//模糊查询
export function getDataSearch(page,cc_ident,cc_bizid,cc_remark) {
  return request({
    url: apiUrl.GET_COUNT_SEARCH,
    method: 'post',
    data: {token:getToken(),page,cc_ident,cc_bizid,cc_remark}
  })
}
//新增
export function getDataAdd(cc_ident,cc_bizid,cc_coder,cc_leave,cc_style,cc_remark) {
  return request({
    url: apiUrl.GET_COUNT_ADD,
    method: 'post',
    data: {token:getToken(),cc_ident,cc_bizid,cc_coder,cc_leave,cc_style,cc_remark}
  })
}

//删除
export function getDataDel(cc_ident) {
  return request({
    url: apiUrl.GET_COUNT_DEL,
    method: 'post',
    data: {token:getToken(),cc_ident}
  })
}

//修改
export function getDataEdit(cc_ident,cc_bizid,cc_coder,cc_leave,cc_style,cc_remark) {
  return request({
    url: apiUrl.GET_COUNT_MODIFY,
    method: 'post',
    data: {token:getToken(),cc_ident,cc_bizid,cc_coder,cc_leave,cc_style,cc_remark}
  })
}
