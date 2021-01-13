import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken,getUserInfo } from '@/public/auth'
//个人文件柜列表
export function getFileList(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_FILE_LIST,
    method: 'post',
    data:params
  })
}

//个人文件柜目录
export function getFileCatalog(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_FILE_CATALOG,
    method: 'post',
    data:params
  })
}

//个人文件柜目录添加GET_FILE_CATALOGFILE_ADD
export function getFileCatalogAdd(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_FILE_CATALOG_ADD,
    method: 'post',
    data:params
  })
}
//个人文件柜目录文件添加
export function getFileCatalogAddFile(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_FILE_CATALOGFILE_ADD,
    method: 'post',
    data:params
  })
}
//个人文件柜删除文件
export function getFileCatalogDelFile(params) {
  params.token=getToken();
  return request({
    url: apiUrl.GET_FILE_CATALOGFILE_DEL,
    method: 'post',
    data:params
  })
}

//个人文件柜删除文件目录
export function getFileCatalogDel(params) {
  params.token=getToken();
  params.ur_ident=getUserInfo().ur_ident;
  return request({
    url: apiUrl.GET_FILE_CATALOG_DEL,
    method: 'post',
    data:params
  })
}
//个人文件柜上传删除权限
export function getFileCatalogBtnAuth() {
  return request({
    url: apiUrl.GET_FILE_CATALOG_BTN_AUTH,
    method: 'post',
    data:{token:getToken(),uid:getUserInfo().ur_ident}
  })
}
