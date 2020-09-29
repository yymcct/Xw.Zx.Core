import request from '@/utils/request'

//投票


export default {
    member: {
        login: (params) => request.post('/api/member/H5', params)

    },
    computer: {
        postComputerUser: (params) => request.post('/api/LxComputer/PostUser', params)
    },
    product: {
        gets: (params) => request.get('/api/Product/Gets', { params: params }),
        get: (params) => request.get('/api/Product/Get', { params: params })
    },
}