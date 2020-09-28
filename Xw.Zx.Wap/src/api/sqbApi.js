import request from '@/utils/request'

//投票


export default {
    computer: {
        postComputerUser: (params) => request.post('/api/LxComputer/PostUser', params)
    },
    product: {
        gets : (params) => request.get('/api/Product/Gets', { params: params }),
        get : (params) => request.get('/api/Product/Get', { params: params })
    },
}