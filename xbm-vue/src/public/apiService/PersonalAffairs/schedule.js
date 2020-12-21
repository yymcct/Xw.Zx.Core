import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken ,getUserInfo} from '@/public/auth'
//日程列表
export function getScheduleList(params) {
  params.token=getToken();
  params.planorder=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_SCHEDULE_LIST,
    method: 'post',
    data:params
  })
}

//日程安排增加
export function getScheduleAdd(params) {
  params.token=getToken();
  params.planorder=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_SCHEDULE_ADD,
    method: 'post',
    data:params
  })
}

///日程安排修改
export function getScheduleEdit(params) {
  params.token=getToken();
  params.planorder=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_SCHEDULE_EDIT,
    method: 'post',
    data:params
  })
}

//日程安排删除
export function getScheduleDel(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_SCHEDULE_DEL,
    method: 'post',
    data:params
  })
}

