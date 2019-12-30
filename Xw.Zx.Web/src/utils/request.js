import axios from 'axios'
//import { MessageBox, Message } from 'element-ui'
import { removeToken } from './auth'

// create an axios instance
const service = axios.create({
 // baseURL: 'http://localhost:8222',//process.env.VUE_APP_BASE_API,
  headers: { 'Content-Type': 'application/json' },
  timeout: 5000 * 2 // request timeout
})

// request interceptor
service.interceptors.request.use(
  config => {
   // config.headers['Authorization'] = 'Bearer ' + getToken()
    return config
  },
  error => {
    // do something with request error
    return Promise.reject(error)
  }
)

// response interceptor
service.interceptors.response.use(
  /**
   * If you want to get http information such as headers or status
   * Please return  response => response
  */

  /**
   * Determine the request status by custom code
   * Here is just an example
   * You can also judge the status by HTTP Status Code
   */
  response => {
    const res = response.data
    if (res.statusCode === 401) {
     
      removeToken();
      sessionStorage.removeItem('user');
    }
    else if (res.statusCode !== 200) {
      // Message({
      //   message: res.msg || 'Error:' + res.statusCode,
      //   type: 'error',
      //   duration: 5 * 1000
      // })

      return Promise.reject(new Error(res.message || 'Error'))
    } else {
      return res
    }
  },
  error => {
    var msg = String(error);
    if (msg.indexOf("400") != -1)
      msg = '账号或密码错误,或没有权限!';
    if (msg.indexOf("403") != -1)
      msg = '您的账号没有权限!';
    // Message({
    //   message: msg,
    //   type: 'error',
    //   duration: 5 * 1000
    // })

    return Promise.reject(error)
  }
)

export default service
