import request from '@/utils/request'
import { userInfoAPI } from '@/utils/auth'
import Compressor from 'compressorjs';
//export const api_postMeeting = (params) => request.post('/manager/Meeting/PostMeeting', params);

//上传图片
export const api_PostImgWithWater = (params) => {
        const _compress = (file) => {
            return new Promise((resolve, reject) => {
                new Compressor(file, {
                    quality: 0.5,
                    success(result) {
                        resolve(result);
                    },
                    error(error) {
                        reject(error);
                    }
                });
            });
        };

        const promises = params.map(iteam => {
            return _compress(iteam.file)
        })

        return Promise.all(promises).then(res => {
            const userInfo = userInfoAPI.get();
            let fromdata = new FormData();
            res.map(iteam => {
                fromdata.append('file', iteam, iteam.name);
            });
            let config = {
                headers: [
                    { "Content-Type": "multipart/form-data" },
                    { 'Authorization': `${userInfo.token_type} ${userInfo.access_token}` }
                ],
            };

            return request.post('/api/meeting/FileUpload/PostFilesWithWater', fromdata, config);
        })
    }
    //获取直播列表
export const api_GetMeetingList = (params) => request.get('/api/meeting/Meeting/GetMeetingList', { params: params });
export const api_GetMeeting = (params) => request.get('/api/meeting/Meeting/GetMeeting', { params: params });
export const api_GetCompanyByTopRecommend = (params) => request.get('/api/meeting/Company/GetCompanyByTopRecommend', { params: params });
export const api_GetMeetingArea = (params) => request.get('/api/meeting/MeetingArea/GetArea', { params: params });
export const api_GetMeetingAreaCompany = (params) => request.get('/api/meeting/Company/GetCompany', { params: params });
export const api_GetGetCompanyByRecommend = (params) => request.get('/api/meeting/Company/GetCompanyByRecommend', { params: params });

export const api_GetMeetingAreaProduct = (params) => request.get('/api/meeting/Product/GetProductByIteam', { params: params });
export const api_GetProductByRecommend = (params) => request.get('/api/meeting/Product/GetProductByIteam', { params: params });

//公司详情页
export const api_PostCompanyMemberHits = (params) => request.post('/api/meeting/Company/PostCompanyMemberHits', params);
export const api_GetCompanyContent = (params) => request.get('/api/meeting/Company/GetCompanyContent', { params: params });
export const api_GetComThumbsUpMemberNick = (params) => request.get('/api/meeting/Company/GetComThumbsUpMemberNick', { params: params });
export const api_GetProductByIteam = (params) => request.get('/api/meeting/Product/GetProductByIteam', { params: params });
export const api_GetCompanyReplys = (params) => request.get('/api/meeting/Company/GetCompanyReplys', { params: params });
export const api_PostMeetingHits = (params) => request.post('/api/meeting/Meeting/PostMeetingHits', params);
export const api_PostCompanyReply = (params) => request.post('/api/meeting/Company/PostCompanyReply', params);



//公司点赞
export const api_PostCompanyThumbUp = (params) => request.post('/api/meeting/Company/PostCompanyThumbUp', params);


//小组件
export const api_GetNewAddition = (params) => request.get('/api/meeting/Company/GetNewAddition', { params: params });

//产品详情页
export const api_PostProductMemberHits = (params) => request.post('/api/meeting/Product/PostProductMemberHits', params);
export const api_GetProductContent = (params) => request.get('/api/meeting/Product/GetProductContent', { params: params });
export const api_GetProductReplys = (params) => request.get('/api/meeting/Product/GetProductReplys', { params: params });
export const api_GetCompanyOtherProduct = (params) => request.get('/api/meeting/Product/GetCompanyOtherProduct', { params: params });
//产品评论
export const api_PostProductReply = (params) => request.post('/api/meeting/Product/PostProductReply', params);
//产品点赞
export const api_PostProductThumbUp = (params) => request.post('/api/meeting/Product/PostProductThumbUp', params);

//动态
export const api_GetMeetingShortMsg = (params) => request.get('/api/meeting/ShortMsg/GetShortMsg', { params: params });
export const api_GetShortMsgContent = (params) => request.get('/api/meeting/ShortMsg/GetShortMsgContent', { params: params });
export const api_PostShortMsg = (params) => request.post('/api/meeting/ShortMsg/PostShortMsg', params);
export const api_PostShortMsgReply = (params) => request.post('/api/meeting/ShortMsg/postShortMsgReply', params);
export const api_PostShortMsgThumbUp = (params) => request.post('/api/meeting/ShortMsg/PostShortMsgThumbUp', params);
export const api_GetShortMsgReplys = (params) => request.get('/api/meeting/ShortMsg/GetShortMsgReplys', { params: params });

//直播
export const api_GetLiveBroadCast = (params) => request.get('/api/meeting/LiveBroadCast/GetLiveBroadCast', { params: params });
export const api_GetLiveBroadCastInfoType = (params) => request.get('/api/meeting/LiveBroadCast/GetLiveBroadCastInfoType', { params: params });
export const api_GetLiveBroadCastInfo = (params) => request.get('/api/meeting/LiveBroadCast/GetLiveBroadCastInfo', { params: params });

//我的评论
export const api_GetUserReplysByMemberId = (params) => request.get('/api/meeting/Member/GetUserReplysByMemberId', { params: params });
export const api_GetMemberReplyReply = (params) => request.get('/api/meeting/Member/GetMemberReplyReply', { params: params });
//我的足迹
export const api_GetMemberFootPrint = (params) => request.get('/api/meeting/Member/GetMemberFootPrint', { params: params });
//我的点赞
export const api_GetMemberThumbsUp = (params) => request.get('/api/meeting/Member/GetMemberThumbsUp', { params: params });
//获取我的公司
export const api_GetMemberCompanyinfo = (params) => request.get('/api/meeting/Member/GetMemberCompanyinfoByToken', { params: params });
//展商添加公司
export const api_PostAddCompanyInfo = (params) => request.post('/api/meeting/Company/PostAddCompanyInfo', params);
//展商添加产品
export const api_PostMeetingProduct = (params) => request.post('/api/meeting/Product/PostProduct', params);
//获取产品详情
export const api_GetProductContentById = (params) => request.get('/api/meeting/Product/GetProductContentById', { params: params });
//删除产品
export const api_DeleteMeetingProduct = (params) => request.post('/api/meeting/Product/DeleteProduct', params);

//获取公司参展情况
export const api_GetCompanyMeeting = (params) => request.get('/api/meeting/Company/GetCompanyMeeting', { params: params });
//获取验证码
export const api_GetSmsCheck = (params) => request.post('/api/meeting/Member/GetSmsCheck', params);

//检查手机号是否验证过
export const api_GetPhoneIsChecked = (params) => request.get('/api/meeting/Member/GetPhoneIsChecked', { params: params });

//添加参展公司
export const api_PostCompanyMeeting = (params) => request.post('/api/meeting/Company/PostCompanyMeeting', params);

//观众注册
export const api_PostMemberInfo = (params) => request.post('/api/meeting/Member/PostMemberInfo', params);

//意见反馈
export const api_PostUserFaceBack = (params) => request.post('/api/v0.1/Member/PostUserFaceBack', params);

//掌上糖酒会报名表
export const api_PostCompanyRegister = (params) => request.post('/api/meeting/CompanyRegister/PostCompanyRegister', params);

//掌上糖酒会报名表
export const api_GetCompanyIsRegister = (params) => request.get('/api/meeting/CompanyRegister/GetCompanyIsRegister', { params: params });


//根据token 手机号, 验证码 绑定手机号
export const api_PostMemberBindPhone = (params) => request.post('/api/meeting/Member/BindPhone', params);

//手机号 验证码登录
export const api_PostloginByPhone = (params) => request.post('/api/meeting/LogIn/WebPhoneProgram',params);

//export const api_GetMemberInfo = () => request.get('/api/meeting/Member/GetMemberInfo', { });

//搜索
export const api_GetSearch = (params) => request.get('/api/meeting/Member/GetSearch', { params: params });


//直播预告
export const api_GetVideoLiveList = (params) => request.get('/api/meeting/VideoLive/GetVideoLiveList', { params: params });
//直播预告详情
export const api_GetVideoLiveContent = (params) => request.get('/api/meeting/VideoLive/GetVideoLiveContent', { params: params });

//记录拨打次数
export const api_PostCallCount = (params) => request.post('/api/meeting/Company/PostCallCount', params);

//记录代理留言
export const api_PostDlMessage = (params) => request.post('/api/meeting/Product/PostDlMessage', params);

//客服名片
export const api_GetCustomerService = (params) => request.get('/api/meeting/company/GetCustomerService', { params: params });