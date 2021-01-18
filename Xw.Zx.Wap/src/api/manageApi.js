import request from '@/utils/request'

//投票

export default {
    sysParam: {
        setValue: (name, val) => request.post(`/manager/SysParam/SetValue?name=${name}&val=${val}`),
        getValue: (name) => request.post(`/manager/SysParam/getValue?name=${name}`)
    },
}