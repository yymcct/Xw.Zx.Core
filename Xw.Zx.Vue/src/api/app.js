import request from '../utils/request'

export default {
    sysParam: {
        setValue: (name, val) => request.post(`/manager/SysParam/SetValue?name=${name}&val=${val}`),
        getValue: (name) => request.post(`/manager/SysParam/getValue?name=${name}`)
    },
    member: {
        getParent: (memberId) => request.get(`/manager/Member/GetParent?memberId=${memberId}`),
        parentTree: (memberId) => request.get(`/manager/Member/ParentTree?memberId=${memberId}`),
        childrenTree: (memberId) => request.get(`/manager/Member/ChildrenTree?memberId=${memberId}`),
        getMember: (memberId) => request.get(`/manager/Member/GetMember?memberId=${memberId}`),
        changeInvite: (params) => request.post(`/manager/Member/PostChangeInvite`, params),
        queryMember: (key) => request.get(`/manager/Member/QueryMember?key=${key}`),
        put: (id, params) => request.post(`/manager/Member/PostMember?id=${id}`, params)
    },
    coupon: {
        getCouponList: () => request.get(`/manager/Coupon/GetCouponList`),
        giveCoupon: (params) => request.post(`/manager/Coupon/GiveCoupon`, params),
        getCoupon: (params) => request.get(`/manager/Coupon/GetCoupon`, { params: params }),
        couponToMemberIntegral: (params) => request.post(`/manager/Coupon/CouponToMemberIntegral?couponReceiveId=${params}`),
    },
    income: {
        getCoupon: (params) => request.get(`/manager/IncomeAccount/GetIncomes`, { params: params }),
        auditSucess: (id, params) => request.post(`/manager/IncomeAccount/AuditSucess?id=${id}`, params),
        auditFail: (id, params) => request.post(`/manager/IncomeAccount/AuditFail?id=${id}`, params),
    },
    withdraw: {
        get: (params) => request.get('/manager/WithdrawDeposit/GetWithdrawDeposits', { params: params }),
        tongjibuAudit: (id, params) => request.post(`/manager/WithdrawDeposit/TongjibuAudit?id=${id}`, params),
        caiwuAudit: (id, params) => request.post(`/manager/WithdrawDeposit/CaiwuAudit?id=${id}`, params),
        pay: (id, params) => request.post(`/manager/WithdrawDeposit/Pay?id=${id}`, params),
        fail: (id, params) => request.post(`/manager/WithdrawDeposit/Fail?id=${id}`, params),
    },
    order: {
        getInfo: (params) => request.get('/manager/order/getInfo', { params: params }),
    }, 
    weixinSubLedger: {
        getWechatSubLedgerReceivers: () => request.get(`/manager/WechatSubLedger/GetWechatSubLedgerReceivers`),
        getWechatOrderList: (params) => request.get('/manager/WechatSubLedger/GetWechatOrderList', { params: params }),
        getWechatPayOrder: (params) => request.get('/manager/WechatSubLedger/GetWechatPayOrder', { params: params }),
        dealWithSubLedger: (params) => request.post('/manager/WechatSubLedger/DealWithSubLedger', params),
        querySubLedgerResult: (params) => request.get('/manager/WechatSubLedger/QuerySubLedgerResult', { params: params }),
        getWechatOrderDetails: (params) => request.get('/manager/WechatSubLedger/GetWechatOrderDetails', { params: params }),
    }
}