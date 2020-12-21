import Vue from 'vue'
import { login, logout, getInfo, getServerIp } from '@/public/apiService/login'
import { getToken, setToken, removeToken, removeUser } from '@/public/auth'
const user = {
  state: {
    token: getToken(),
  },

  mutations: {
    SET_TOKEN: (state, token) => {
      setToken(token);
      state.token = token
    },
  },

  actions: {
    // 登录
    Login({ commit }, userInfo) {
      return new Promise((resolve, reject) => {
        login(userInfo.username, userInfo.password,userInfo.ur_dllogin).then(response => {
          const temp = response.data
          commit('SET_TOKEN', temp.token);
          getInfo(temp.token, temp['ur_ident']).then(response => {
            const data = response.data[0];
            data.token=temp.token;
            data.showname=temp.showname||'';
            localStorage.setItem('data', JSON.stringify(data));
          })
          resolve(response)
        }).catch(error => {
          reject(error)
        })
      })
    },
    // 获取用户信息
    GetServerIp({ commit }, data) {
      return new Promise((resolve, reject) => {
        getServerIp().then(response => {
          resolve(response)
        }).catch(error => {
          reject(error)
        })
      })
    },

    // 前端 登出
    FedLogOut({ commit }) {
      return new Promise(resolve => {
        commit('SET_TOKEN', '')
        removeToken();
        // removeUser();
        localStorage.clear();
        sessionStorage.clear();
        resolve()
      })
    }
  }
}

export default user
