import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'


//数据字典的列表
export function GetDataList() {
  return request({
    url: apiUrl.GET_REGION_LIST,
    method: 'post',
    data: { token:getToken()}
  })
}

//数据字典的删除
export function DelRegionData(params) {
  params.token=getToken();
  return request({
    url: apiUrl.DEL_REGION_LIST,
    method: 'post',
    data: params
  })
}

//数据字典的添加与修改
export function UpdateRegionData(params) {
  params.token=getToken();
  return request({
    url: apiUrl.UPDATE_REGION_LIST,
    method: 'post',
    data: params
  })
}