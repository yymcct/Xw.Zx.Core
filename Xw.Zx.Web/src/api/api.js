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
        headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
    })
};

export const getUser = () => request.get('/manager/Member/GetUser');

//获取直播列表
export const api_GetLives = (params) => request.get('/api/hblive/HbLive/GetLives', { params: params });

//获取MP3列表
export const api_GetVoiceNews = (params) => request.get('/api/VoiceNew/GetVoiceNews', { params: params });






`                                                                                       `