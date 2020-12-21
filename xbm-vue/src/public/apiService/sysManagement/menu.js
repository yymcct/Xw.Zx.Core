import request from '@/public/config'
import { apiUrl } from '@/public/apiUrl'
import { getToken, getUserInfo } from '@/public/auth'
//获取收件箱列表
// let ur_ident= localStorage.getItem('ur_ident');
getUserInfo();
//获取带权限的菜单列表
export function getAuthMenuList(params) {
  params.token = getToken();
  params.ur_ident = getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_AUTH_MENU_LIST,
    method: 'post',
    data: params
  })
}
//业务审批菜单列表
export function getApprovalMenuList() {

  return request({
    url: apiUrl.GET_APPROVAL_MENU,
    method: 'post',
    data: { token: getToken(), ur_ident: getUserInfo().ur_ident }
  })
}
//政务管理菜单列表
export function getManageMenuList() {
  return request({
    url: apiUrl.GET_MANAGE_MENU,
    method: 'post',
    data: { token: getToken(), ur_ident: getUserInfo().ur_ident }
  })
}


//获取全部菜单列表
export function getMenuList() {
  return request({
    url: apiUrl.GET_MENU_LIST,
    method: 'post',
    data: { token: getToken() }
  })
}
//新增一级菜单
export function addLevel1Menu(params) {
  params.token = getToken();
  return request({
    url: apiUrl.ADD_LEVEL1_MENU,
    method: 'post',
    data: params
  })
}
//更新一级菜单
export function updateLevel1Menu(params) {
  params.token = getToken();
  return request({
    url: apiUrl.UPDATE_LEVEL1_MENU,
    method: 'post',
    data: params
  })
}
//新增二级菜单
export function addLevel2Menu(params) {
  params.token = getToken();
  return request({
    url: apiUrl.ADD_LEVEL2_MENU,
    method: 'post',
    data: params
  })
}
//更新二级菜单
export function updateLevel2Menu(params) {
  params.token = getToken();
  return request({
    url: apiUrl.UPDATE_LEVEL2_MENU,
    method: 'post',
    data: params
  })
}
//删除菜单目录
export function delMenu(params) {
  params.token = getToken();
  return request({
    url: apiUrl.DEL_MENU,
    method: 'post',
    data: params
  })
}




