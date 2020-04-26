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
        url: '/connect/token',
        method: 'post',
        data: qs.stringify(login),
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
    })
};

//聊天的匿名用户登录
export const chatAnonymousLogin = () => {
    var login = {
        grant_type: "password",
        client_id: "App.Chat.User",
        client_secret: "NQWHfxg0VSZQb7pJkYDsy9COXDCN7wRdIjeyopJrOKFksRbT8cbl7dyd",
        username: 'username',
        password: 'password'
    };
    return request({
        url: '/connect/token',
        method: 'post',
        data: qs.stringify(login),
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
    })
};

export const getUser = () => request.get('/manager/Member/GetUser');

//获取直播列表
export const api_GetLives = (params) => request.get('/api/hblive/HbLive/GetLives', { params: params });

//获取直播详情
export const api_GetLiveDetails = (params) => request.get('/api/hblive/HbLive/GetLiveDetails', { params: params });

//获取直播栏目
export const api_GetHbLiveLiveColumn = (params) => request.get('/api/hblive/HbLive/GetHbLiveLiveColumn', { params: params });

//获取直播栏目下内容
export const api_GetHbLiveLiveColumnContent = (params) => request.get('/api/hblive/HbLive/GetHbLiveLiveColumnContent', { params: params });




`                                                                                       `