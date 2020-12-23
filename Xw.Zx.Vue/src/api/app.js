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
        getCoupon: (params) => request.get(`/manager/Coupon/GetCoupon`,{params: params}),
    },
    income:{
        getCoupon: (params) => request.get(`/manager/IncomeAccount/GetIncomes`,{params: params}),
    }

}