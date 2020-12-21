import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'

//获取数据对象列表
export function getDataList(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_DATABASEMAGEMENT_LIST,
    method: 'post',
    data: params
  })
}
//数据对象详情
export function getDataDetail(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_DATABASEMAGEMENT_DETAIL,
    method: 'post',
    data: params
  })
}