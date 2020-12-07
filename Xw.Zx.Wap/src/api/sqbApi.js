import request from '@/utils/request'

//投票

export default {
    member: {
        login: (params) => request.post('/api/member/H5', params),
        weixinLogin: (code) => request.post(`/api/member/WeixinLogin?code=${code}`),
        anyUserName: (phone) => request.get(`/api/member/AnyUserName?phone=${phone}`),
        weixinBind: (params) => request.post(`/api/member/WeixinBind`, params),
        reg: (params) => request.post('/api/member/PostRegisterUser', params),
        pwd: (params) => request.post('/api/member/PostChangePasswordBySmsCode', params),
        smscode: (params) => request.get('/api/member/GetSmsCode', { params: params }),
        edit: (params) => request.post('/api/member/PostMember', params),
        getSelf: (params) => request.get('/api/member/GetSelf', { params: params }),
        isWhite: () => request.get('/api/member/IsWhite'),
        getMyTeam: () => request.get('/api/member/GetMyTeam'),
        getMyFirstTeamUser: (params) => request.get('/api/member/GetMyFirstTeamUser', { params: params }),
    },
    computer: {
        postComputerUser: (params) => request.post('/api/LxComputer/PostUser', params)
    },
    product: {
        gets: (params) => request.get('/api/Product/Gets', { params: params }),
        get: (params) => request.get('/api/Product/Get', { params: params })
    },
    order: {
        post: (params) => request.post(`/api/Order`, params),
        get: (params) => request.get(`/api/Order/${params}`),
        delete: (params) => request.post(`/api/Order/Delete?id=${params}`),
        gets: (params) => request.get('/api/Order', { params: params }),
    },
    alipay: {
        wapPay: (params) => request.post(`/api/Alipay/WapPay/${params.id}?returnurl=${params.returnUrl}`),
        scanCodeGen: (params) => request.post(`/api/Alipay/ScanCodeGen/${params}`),
        //使用通道
        firstUseAlipay: () => request.get(`/api/Alipay/firstUseAlipay`),
    },
    biqilin: {
        scanCodeGen: (params) => request.post(`/api/Biqilin/ScanCodeGen`, params),
        jsapiPay: (orderId, openid) => request.post(`/api/Biqilin/JsapiPay?orderId=${orderId}&openId=${openid}`),
    },
    income: {
        getIncomeInfo: () => request.get('/api/Income/GetIncomeInfo'),
        getDetails: () => request.get('/api/Income/GetDetails'),
    },
    withdrawDeposit: {
        withdrawDeposit: (params) => request.post('/api/WithdrawDeposit/PostWithdrawDeposit', params),
        getDetails: () => request.get('/api/WithdrawDeposit/GetWithdrawDepositdetails'),
        getAuditDetails: () => request.get('/api/WithdrawDeposit/GetAuditWithdrawDepositdetails'),
        auditwithdrawDeposit: (params) => request.post('/api/WithdrawDeposit/AuditWithdrawDepositdetail', params),
    },
    updateVipAuthCode: {
        use: (code) => request.post(`/api/UpdateVipAuthCode/Use?code=${code}`),
    }
}