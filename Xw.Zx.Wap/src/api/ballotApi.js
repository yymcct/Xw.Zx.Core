import request from '@/utils/request'


//获取投票详情
export const api_GetBallotingContent = (params) => request.get('/api/meeting/Balloting/GetBallotingContent', { params: params });
//获取投票类别
export const api_GetBallotingType = (params) => request.get('/api/meeting/Balloting/GetBallotingType', { params: params });
//获取投票选手数据
export const api_GetBallotingPlayerList = (params) => request.get('/api/meeting/Balloting/GetBallotingPlayerList', { params: params });
//获取投票选手数据
export const api_GetBallotingPlayer = (params) => request.get('/api/meeting/Balloting/GetBallotingPlayer', { params: params });
//获取是否报名
export const api_IsHaveApply = (params) => request.get('/api/meeting/Balloting/IsHaveApply', { params: params });
//提交报名信息
export const api_PostPlayerApply = (params) => request.post('/api/meeting/Balloting/PostPlayerApply', params);
//投票
export const api_PostBalloting = (params) => request.post('/api/meeting/Balloting/PostBalloting', params);