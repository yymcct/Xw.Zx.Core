import request from '@/utils/request'

//投票


export default {
    member: {
        login: (params) => request.post('/api/member/H5', params),
        reg: (params) => request.post('/api/member/PostRegisterUser', params),
        pwd: (params) => request.post('/api/member/PostChangePasswordBySmsCode', params),
        smscode: (params) => request.get('/api/member/GetSmsCode', { params: params }),
    },
    computer: {
        postComputerUser: (params) => request.post('/api/LxComputer/PostUser', params)
    },
    product: {
        gets: (params) => request.get('/api/Product/Gets', { params: params }),
        get: (params) => request.get('/api/Product/Get', { params: params })
    },
    order: {
        post: (params) => request.post(`/api/Order/${params}`),
        get: (params) => request.get(`/api/Order/${params}`),
        delete: (params) => request.delete(`/api/Order/${params}`),
        gets: (params) => request.get('/api/Order', { params: params }),
    },
    alipay:{
        wapPay:(params) => request.post(`/api/Alipay/WapPay/${params}`)
    },
    income:{
        getIncomeInfo: () => request.get('/api/Income/GetIncomeInfo'),
    }
}