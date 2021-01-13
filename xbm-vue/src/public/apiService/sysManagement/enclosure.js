import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'
//附件列表
export function getDataList(page) {
  return request({
    url: apiUrl.GET_ENCLOSURE_LIST,
    method: 'post',
    data: {token:getToken(),page}
  })
}

//附件搜索
export function getDataSearch(page,sr_name,kssj,jssj) {
  return request({
    url: apiUrl.GET_ENCLOSURE_SEARCH,
    method: 'post',
    data: {token:getToken(),page,sr_name,kssj,jssj}
  })
}

//附件删除
export function getDataDel(wiid,ac_name) {
  return request({
    url: apiUrl.GET_ENCLOSURE_DEL,
    method: 'post',
    data: {token:getToken(),wiid,ac_name}
  })
}
//附件保存
export function saveFile(wiid,DATA) {
  return request({
    url: apiUrl.SAVE_ENCLOSURE,
    method: 'post',
    data: {token:getToken(),wiid,DATA}
  })
}