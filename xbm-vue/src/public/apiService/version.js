import request from '@/public/config'
import {apiUrl} from '@/public/apiUrl'
import { getToken } from '@/public/auth'

export function getVersion() {
  return request({
    url: apiUrl.GET_QUERY_VERSION,
    method: 'post',
    data: {
      token:getToken(),
      type:0
    }
  })
}