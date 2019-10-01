import Vue from 'vue'
import App from './App'

import store from './store'

Vue.config.productionTip = false

Vue.prototype.$store = store

App.mpType = 'app'

Vue.prototype.getUser = function(backpage) {
        var USER = uni.getStorageSync('USERS_KEY'); //本地持久化存储
        if (USER == '') {
            uni.redirectTo({ url: '../login/login?backpage=' + backpage });
            return false;
        }
        var user = JSON.parse(USER)

        return user;
    }
    Vue.prototype.baseUrl = 'http://139.155.8.217'
//Vue.prototype.baseUrl = 'http://localhost:63836'

const app = new Vue({
    store,
    ...App
})
app.$mount()