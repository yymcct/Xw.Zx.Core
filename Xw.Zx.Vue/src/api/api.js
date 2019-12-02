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

//会议
export const api_getMeetingSelectOptions = () => request.get('/manager/Meeting/GetMeetingSelectOptions');
export const api_getMeetings = (params) => request.get('/manager/Meeting/GetMeetings', { params: params });
export const api_postMeeting = (params) => request.post('/manager/Meeting/PostMeeting', params);
export const api_delMeeting = (id) => request.get('/manager/Meeting/DeleteMeeting', { params:{'id':id}});

//会议区域
export const api_getMeetingAreaOptions = () => request.get('/manager/MeetingArea/GetMeetingAreaOptions');
export const api_getMeetingAreaMDtos = (params) => request.get('/manager/MeetingArea/GetMeetingAreas', { params: params });
export const api_postMeetingAreaMDto = (params) => request.post('/manager/MeetingArea/PostMeetingArea', params);
export const api_delMeetingAreaMDto = (id) => request.get('/manager/MeetingArea/DeleteMeetingArea', { params:{'id':id}});

//参展公司管理
export const api_getCompanyMeetingMDtos = (params) => request.get('/manager/CompanyMeeting/GetCompanyMeetings', { params: params });
export const api_postCompanyMeetingMDto = (params) => request.post('/manager/CompanyMeeting/PostCompanyMeeting', params);
export const api_delCompanyMeetingMDto = (id) => request.get('/manager/CompanyMeeting/DeleteCompanyMeeting', { params:{'id':id}});

//顶部推荐公司
export const api_getHomeTopRecommendCompanyMDtos = (params) => request.get('/manager/HomeTopRecommendCompany/GetHomeTopRecommendCompanys', { params: params });
export const api_postHomeTopRecommendCompanyMDto = (params) => request.post('/manager/HomeTopRecommendCompany/PostHomeTopRecommendCompany', params);
export const api_delHomeTopRecommendCompanyMDto = (id) => request.get('/manager/HomeTopRecommendCompany/DeleteHomeTopRecommendCompany', { params:{'id':id}});

//推荐产品
export const api_getRecommendProductMDtos = (params) => request.get('/manager/RecommendProduct/GetRecommendProducts', { params: params });
export const api_postRecommendProductMDto = (params) => request.post('/manager/RecommendProduct/PostRecommendProduct', params);
export const api_delRecommendProductMDto = (id) => request.get('/manager/RecommendProduct/DeleteRecommendProduct', { params:{'id':id}});

//推荐公司
export const api_getHomeRecommendCompanyMDtos = (params) => request.get('/manager/HomeRecommendCompany/GetHomeRecommendCompanys', { params: params });
export const api_postHomeRecommendCompanyMDto = (params) => request.post('/manager/HomeRecommendCompany/PostHomeRecommendCompany', params);
export const api_delHomeRecommendCompanyMDto = (id) => request.get('/manager/HomeRecommendCompany/DeleteHomeRecommendCompany', { params:{'id':id}});

//最新动态
export const api_getNewAddTionMDtos = (params) => request.get('/manager/NewAddTion/GetNewAddTions', { params: params });
export const api_delNewAddTionMDto = (id) => request.get('/manager/NewAddTion/DeleteNewAddTion', { params:{'id':id}});

//公司管理
export const api_getCompanyMDtos = (params) => request.get('/manager/Company/GetCompanys', { params: params });
export const api_postCompanyMDto = (params) => request.post('/manager/Company/PostCompany', params);
export const api_delCompanyMDto = (id) => request.get('/manager/Company/DeleteCompany', { params:{'id':id}});

//公司评论管理
export const api_getCompanyReplyMDtos = (params) => request.get('/manager/CompanyReply/GetCompanyReplys', { params: params });
export const api_postCompanyReplyMDto = (params) => request.post('/manager/CompanyReply/PostCompanyReply', params);
export const api_delCompanyReplyMDto = (id) => request.get('/manager/CompanyReply/DeleteCompanyReply', { params:{'id':id}});

//公司点赞处理
export const api_getCompanyThumbsUpMDtos = (params) => request.get('/manager/CompanyThumbsUp/GetCompanyThumbsUps', { params: params });
export const api_delCompanyThumbsUpMDto = (id) => request.get('/manager/CompanyThumbsUp/DeleteCompanyThumbsUp', { params:{'id':id}});

//产品管理
export const api_getProductMDtos = (params) => request.get('/manager/Product/GetProducts', { params: params });
export const api_postProductMDto = (params) => request.post('/manager/Product/PostProduct', params);
export const api_delProductMDto = (id) => request.get('/manager/Product/DeleteProduct', { params:{'id':id}});

//产品评论
export const api_getProductReplyMDtos = (params) => request.get('/manager/ProductReply/GetProductReplys', { params: params });
export const api_postProductReplyMDto = (params) => request.post('/manager/ProductReply/PostProductReply', params);
export const api_delProductReplyMDto = (id) => request.get('/manager/ProductReply/DeleteProductReply', { params:{'id':id}});

//直播专题
export const api_getLiveBroadCastOptions = () => request.get('/manager/LiveBroadCast/GetLiveBroadCastOptions');
export const api_getLiveBroadCastMDtos = (params) => request.get('/manager/LiveBroadCast/GetLiveBroadCasts', { params: params });
export const api_postLiveBroadCastMDto = (params) => request.post('/manager/LiveBroadCast/PostLiveBroadCast', params);
export const api_delLiveBroadCastMDto = (id) => request.get('/manager/LiveBroadCast/DeleteLiveBroadCast', { params:{'id':id}});

//直播专题类别
export const api_getTypeInLiveBroadCastOptions = () => request.get('/manager/TypeInLiveBroadCast/GetTypeInLiveBroadCastOptions');
export const api_getTypeInLiveBroadCastMDtos = (params) => request.get('/manager/TypeInLiveBroadCast/GetTypeInLiveBroadCasts', { params: params });
export const api_postTypeInLiveBroadCastMDto = (params) => request.post('/manager/TypeInLiveBroadCast/PostTypeInLiveBroadCast', params);
export const api_delTypeInLiveBroadCastMDto = (id) => request.get('/manager/TypeInLiveBroadCast/DeleteTypeInLiveBroadCast', { params:{'id':id}});

//直播详情
export const api_getLiveBroadCastInfoMDtos = (params) => request.get('/manager/LiveBroadCastInfo/GetLiveBroadCastInfos', { params: params });
export const api_postLiveBroadCastInfoMDto = (params) => request.post('/manager/LiveBroadCastInfo/PostLiveBroadCastInfo', params);
export const api_delLiveBroadCastInfoMDto = (id) => request.get('/manager/LiveBroadCastInfo/DeleteLiveBroadCastInfo', { params:{'id':id}});

//参展产品管理
export const api_postAddProductMeeting = (params) => request.post('/manager/ProductMeeting/PostAddProductMeeting', params);
export const api_getProductMeetingMDtos = (params) => request.get('/manager/ProductMeeting/GetProductMeetings', { params: params });
export const api_postProductMeetingMDto = (params) => request.post('/manager/ProductMeeting/PostProductMeeting', params);
export const api_delProductMeetingMDto = (id) => request.get('/manager/ProductMeeting/DeleteProductMeeting', { params:{'id':id}});

//用户管理
export const api_getMemberMDtos = (params) => request.get('/manager/Member/GetMembers', { params: params });
export const api_postMemberMDto = (params) => request.post('/manager/Member/PostMember', params);
export const api_delMemberMDto = (id) => request.get('/manager/Member/DeleteMember', { params:{'id':id}});

//动态
export const api_getShortMsgMDtos = (params) => request.get('/manager/ShortMsg/GetShortMsgs', { params: params });
export const api_postShortMsgMDto = (params) => request.post('/manager/ShortMsg/PostShortMsg', params);
export const api_delShortMsgMDto = (id) => request.get('/manager/ShortMsg/DeleteShortMsg', { params:{'id':id}});

//动态评论
export const api_getReplyReplyMDtos = (params) => request.get('/manager/ReplyReply/GetReplyReplys', { params: params });
export const api_postReplyReplyMDto = (params) => request.post('/manager/ReplyReply/PostReplyReply', params);
export const api_delReplyReplyMDto = (id) => request.get('/manager/ReplyReply/DeleteReplyReply', { params:{'id':id}});

//评论的评论
export const api_getShortMsgReplyMDtos = (params) => request.get('/manager/ShortMsgReply/GetShortMsgReplys', { params: params });
export const api_postShortMsgReplyMDto = (params) => request.post('/manager/ShortMsgReply/PostShortMsgReply', params);
export const api_delShortMsgReplyMDto = (id) => request.get('/manager/ShortMsgReply/DeleteShortMsgReply', { params:{'id':id}});