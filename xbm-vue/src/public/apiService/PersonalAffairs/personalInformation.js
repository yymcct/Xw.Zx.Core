import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'
//获取个人信息
export function getData(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_PERSONAL_INFORMATION,
    method: 'post',
    data:params
  })
}
//个人信息修改
export function getDataEdit(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_PERSONAL_INFORMATION_EDIT,
    method: 'post',
    data:params
  })
}
//修改密码
export function getChangeWord(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_PERSONAL_INFORMATION_PASSWORD,
    method: 'post',
    data:params
  })
}
//个人部门的获取
export function getDepartment(params) {
  return request({
    url: apiUrl.GET_PERSONAL_INFORMATION_DEPARTMENT,
    method: 'post',
    data:{token:getToken()}
  })
}