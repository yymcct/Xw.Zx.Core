import request from '../utils/request'

export default {
    sysParam: {
        setValue : (name, val) => request.post(`/manager/SysParam/SetValue?name=${name}&val=${val}`),
        getValue : (name) => request.post(`/manager/SysParam/getValue?name=${name}`)
    },
    member:{
        getParent:(memberId) => request.get(`/manager/Member/GetParent?memberId=${memberId}`)
    }

}