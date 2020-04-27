import request from '@/utils/request'

//投票
export const api_PostComputerUser = (params) => request.post('/api/LxComputer/PostUser', params);