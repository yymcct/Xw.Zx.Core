import {getUnreadNum } from '@/public/apiService/PersonalAffairs/email'
const email = {
  state: {
    isCollapse: false,
    activeSideName: 'inBox',//inBox 收件箱 draft 草稿箱 outBox 发件箱
    unReadNum: 0
  },

  mutations: {
    changeCollapse: (state) => {
      state.isCollapse = !state.isCollapse
    },
    curSideName: (state, value) => {
      state.activeSideName = value
    },
    // setUnReadNum: (state, value) => {
    //   state.unReadNum = value
    // }
  },
  actions: {
    //获取未读条数
    getUnReadNums({ state }) {
      return new Promise((resolve, reject) => {
        getUnreadNum().then(response => {
          state.unReadNum=response.count;
          resolve(response)
        }).catch(error => {
          reject(error)
        })
      })
    }
  }
}

export default email
