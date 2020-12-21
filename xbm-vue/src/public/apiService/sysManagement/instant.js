import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'

//获取消息列表
export function getInstantList(params) {
    params.token=getToken();
    return request({
        url: apiUrl.GET_INSTANT_LIST,
        method: 'post',
        data: params
    })
}
//获取新的消息列表
export function getNewInstantList(params) {
    params.token=getToken();
    return request({
        url: apiUrl.GET_NEW_INSTANT_LIST,
        method: 'post',
        data: params
    })
}

//发送消息
export function getInstantSend(params) {
    params.token=getToken();
    return request({
        url: apiUrl.GET_INSTANT_SEND,
        method: 'post',
        data: params
    })
}

//消息人员列表
export function getInstantPerson(params) {
    params.token=getToken();
    return request({
        url: apiUrl.GET_INSTANT_PERSON,
        method: 'post',
        data: params
    })
}