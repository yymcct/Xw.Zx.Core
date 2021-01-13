import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'
//获取接口列表
export function getDataList() {
  return request({
    url: apiUrl.GET_INTERFACE_LIST,
    method: 'post',
    data: {
      token:getToken()
    }
  })
}
//获取三级列表
export function getDataListChild(bz_ident,bu_ident) {
  return request({
    url: apiUrl.GET_INTERFACE_LISTCHILD,
    method: 'post',
    data: {
      token:getToken(),bz_ident,bu_ident
    }
  })
}

//接口搜索
export function getDataSearch() {
  return request({
    url: apiUrl.GET_INTERFACE_SEARCH,
    method: 'post',
    data: {
      token:getToken()
    }
  })
}

//接口详情
export function getDataDetail(bz_ident,bu_ident) {
  return request({
    url: apiUrl.GET_INTERFACE_DETAIL,
    method: 'post',
    data: {
      token:getToken(),bz_ident,bu_ident
    }
  })
}


//接口添加
export function getDataAdd() {
  return request({
    url: apiUrl.GET_INTERFACE_ADD,
    method: 'post',
    data: {
      token:getToken()
    }
  })
}
//接口删除
export function getDataDel() {
  return request({
    url: apiUrl.GET_INTERFACE_DEL,
    method: 'post',
    data: {
      token:getToken()
    }
  })
}

//接口修改
export function getDataEdit() {
  return request({
    url: apiUrl.GET_INTERFACE_EDIT,
    method: 'post',
    data: {
      token:getToken()
    }
  })
}