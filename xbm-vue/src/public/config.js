import axios from 'axios'
import { Message, MessageBox } from 'element-ui'
import store from '@/store/index'
import { getToken } from '@/public/auth'
// 创建axios实例
const service = axios.create({
  baseURL: process.env.API_ROOT, // api的base_url
  // timeout: 25000 // 请求超时时间
})
// request拦截器
service.interceptors.request.use(config => {
  if (store.state.user.token) {
    config.headers['Authorization'] = getToken() // 让每个请求携带自定义token 请根据实际情况自行修改
  }
  return config
}, error => {
  Promise.reject(error)
})

// respone拦截器
service.interceptors.response.use(
  response => {
  /**
  * code为非20000是抛错 可结合自己业务进行修改
  */
    const res = response.data
    // if (response.status !== 200) {
    //   Message({
    //     message: res.msg,
    //     type: 'error',
    //     duration: 5 * 1000
    //   })
    //   return Promise.reject('error')
    // }
    
      // 50008:非法的token;  50014:Token 过期了;
      if (res.code === 50008 || res.code === 50014) {
       
        var tips='登录已过期,请重新登录';
        if(res.code === 50008){
          tips='登录异常,请重新登录';
        }
        MessageBox.confirm(tips, '确定重新登录', {
          confirmButtonText: '重新登录',
          // cancelButtonText: '取消',
          type: 'warning'
        }).then(() => {
          store.dispatch('FedLogOut').then(() => {
            location.reload()// 为了重新实例化vue-router对象 避免bug
          })
        })
        return Promise.reject('error')
      }
     
    // } else {
      return response.data
    // }
    
  },
  error => {
    console.log('err' + error)// for debug
    Message({
      message: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    return Promise.reject(error)
  }
)

export default service
