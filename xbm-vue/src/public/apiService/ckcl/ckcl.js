import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken,getUserInfo } from '@/public/auth'

//收件事项列表
export function getAuthIssues(params) {
  params.token = getToken();
  return request({
    url: apiUrl.GET_AuthIssue_list,
    method: 'post',
    data: params
  })
}

//窗口受理-保存
export function SaveAcceptanceInfo(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.SAVE_ACCEPT_INFO,
    method: 'post',
    data:params
  })
}

//根据事项获取材料清单列表
export function GetIssueFile(SXLB) {
  return request({
    url: apiUrl.GET_ISSUE_fileList,
    method: 'post',
    data:{token:getToken(),SXLB:SXLB}
  })
}
//审批事项材料清单
export function GetFileSheets(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_FileSheets,
    method: 'post',
    data:params
  })
}
//附件保存
export function SaveFileSheets(params) {
  params.token=getToken();
  return request({
    url: apiUrl.SAVE_FileSheets,
    method: 'post',
    data:params
  })
}
//附件删除
export function DelFileSheets(params) {
  params.token=getToken();
  return request({
    url: apiUrl.DEL_FileSheets,
    method: 'post',
    data:params
  })
}
