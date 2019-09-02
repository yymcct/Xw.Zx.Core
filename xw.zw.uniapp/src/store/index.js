import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const store = new Vuex.Store({
    state: {
        /**
         * 是否需要强制登录
         */
        forcedLogin: false,
        hasLogin: false,
        userName: ""
    },
    mutations: {
        login(state, userName) {
            console.log('AAAAA111');
            state.userName = userName || '新用户';
            state.hasLogin = true;
            console.log(state);
        },
        logout(state) {
            state.userName = "";
            state.hasLogin = false;
        }
    }
})

export default store