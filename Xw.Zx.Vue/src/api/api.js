import qs from 'qs';
import request from '../utils/request'

//export const fileUploadUrl = `${process.env.VUE_APP_BASE_API}/manager/FileUpload/PostFilesWithNoWater`;

//TODO Md5加密密码
export const requestLogin = (username, password) => {
    var login = {
        grant_type: "password",
        client_id: "App.Manager.Ro",
        client_secret: "DEsjpJFtokIOhMKuE6BVMczYUEEyPGTOLrur3PXw26VMLNwKOfAKFZZgR2vVJDKG",
        username: username,
        password: password
    };
    return request({
        url: `${process.env.VUE_APP_BASE_API}/connect/token`,
        method: 'post',
        data: qs.stringify(login),
        headers: { 'Content-Type': 'application/x-www-form-urlencoded'}
})
};

export const getUser = () => request.get('/manager/Member/GetUser');

//用户管理
export const api_getMemberMDtos = (params) => request.get('/manager/Member/GetMembers', { params: params });
export const api_postMemberMDto = (params) => request.post('/manager/Member/PostMember', params);
export const api_delMemberMDto = (id) => request.get('/manager/Member/DeleteMember', { params:{'id':id}});

//订单管理
export const api_getOrderMDtos = (params) => request.get('/manager/Order/GetOrders', { params: params });

//提现管理
export const api_getWithdrawDepositMDtos = (params) => request.get('/manager/WithdrawDeposit/GetWithdrawDeposits', { params: params });
export const api_AuditWithdrawDepositdetail = (params) => request.post('/manager/WithdrawDeposit/AuditWithdrawDepositdetail', params);
