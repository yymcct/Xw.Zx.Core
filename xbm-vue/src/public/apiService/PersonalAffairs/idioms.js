import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'
//惯用语添加
export function getAdd(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_IDIOMS_ADD,
    method: 'post',
    data:params
  })
}

//我的惯用语
export function getMy(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_IDIOMS_MY,
    method: 'post',
    data:params
  })
}

//我的惯用语删除
export function getDel(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_IDIOMS_DEL,
    method: 'post',
    data:params
  })
}

//我的惯用语修改
export function getEdit(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_IDIOMS_EDIT,
    method: 'post',
    data:params
  })
}

//我的惯用语使用次数
export function getNum(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_IDIOMS_NUM,
    method: 'post',
    data:params
  })
}