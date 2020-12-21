import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'
//消息管理列表
export function getMsgList() {
  // params.token=getToken();
  return request({
    url: apiUrl.GET_MSG_LIST,
    method: 'post',
    data:{token:getToken()}
  })
}
//推送消息列表
export function getMsgPushList(params) {
   params.token=getToken();
  return request({
    url: apiUrl.GET_MSG_PUSH_LIST,
    method: 'post',
    data:params
  })
}
//全部消息列表
export function getMsgAllList(params) {
  params.token=getToken();
 return request({
   url: apiUrl.GET_MSG_ALL_LIST,
   method: 'post',
   data:params
 })
}
 //推送消息状态更新
export function getMsgUpdateList(params) {
  params.token=getToken();
 return request({
   url: apiUrl.GET_MSG_UPDATE_LIST,
   method: 'post',
   data:params
 })
}






