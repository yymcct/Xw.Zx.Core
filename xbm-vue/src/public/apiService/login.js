import request from '@/public/config'
import { apiUrl } from '@/public/apiUrl'
import { getToken } from '@/public/auth'
import axios from 'axios'
export function login(ur_login, ur_crypt,ur_dllogin) {
  return axios.post(apiUrl.LOGIN, {
    "ur_login": ur_login,
    "ur_crypt": ur_crypt,
    ur_dllogin
  })
}

export function getInfo(token, ur_ident) {
  return request({
    url: apiUrl.GET_USER_INFO,
    method: 'post',
    data: {
      token,
      ur_ident
    }
  })
}
export function logout() {
  return request({
    url: '/user/logout',
    method: 'post'
  })
}

export function getQrcode() {
  return request({
    url: apiUrl.GET_QRCODE,
    method: 'post'
  })
}

export function getQrType(params) {
  return request({
    url: apiUrl.GET_QRTYPE,
    method: 'post',
    data: params
  })
}
export function getServerIp() {
  return request({
    url: apiUrl.GET_SERVERIP,
    method: 'post',
  })
}
