//import { Dialog } from 'vant';
import { router } from '@/router';

const USERINfO_KEY = 'USER_INFO';
const LOGINFROM_KEY = 'LOGIN_FROM'
const CHATANONYMOUSINFO_KEY = "CHATANONYMOUSINFO_KEY"//匿名聊天用户

export const userInfoAPI = {

    get() {
        const userInfo = JSON.parse(localStorage.getItem(USERINfO_KEY));
        if (userInfo === null || userInfo === 'undefined' || userInfo.expires_in < (Date.now() / 1000)) {
            this.clear();
            return null;
        }
        return userInfo;
    },

    set(userInfo) {
        userInfo.expires_in = userInfo.expires_in + (Date.now() / 1000);
        localStorage.setItem(USERINfO_KEY, JSON.stringify(userInfo));
    },

    updateMember(member) {
        let userInfo = JSON.parse(localStorage.getItem(USERINfO_KEY));
        if (userInfo) {
            userInfo.member = member;
            localStorage.setItem(USERINfO_KEY, JSON.stringify(userInfo));
        }
        return userInfo;
    },
    clear() {
        localStorage.removeItem(USERINfO_KEY);
    },

    // 检查是否login 如果没有则提示跳转
    ifLogin(meetingId, callback, toLogin = true, autoLogin = false) {
        let userLoginInfo = this.get();
        if (userLoginInfo != null) {
            if (callback != null) {
                callback(userLoginInfo.member);
            }
        } else {
            if (toLogin) {
                //登录成功后返回到此地址
                localStorage.setItem(LOGINFROM_KEY, window.location.href);
                console.log(window.location.href)
                console.log(autoLogin);
                //router.push(`/meeting/expo/${meetingId}/user/login`);
                if (autoLogin == false) {
                    router.push(`/meeting/expo/${meetingId}/user/login`);
                } else {
                    const isWeixin = () =>
                        /micromessenger/.test(navigator.userAgent.toLowerCase());
                    if (isWeixin()) {
                        router.push(`/meeting/expo/${meetingId}/user/login/weixin`);
                    } else {
                        this.$toast("请在微信中打开此网页");
                    }
                }

            }
        }
    },
    ballotAutoLogin(ballotId, backUrl) {
        let userLoginInfo = this.get();
        if (userLoginInfo == null) {
            localStorage.setItem(LOGINFROM_KEY, backUrl);
            router.push(`/meeting/ballot/${ballotId}/login/weixin`);
        }
    },
    getLoginFrom() {
        const from = localStorage.getItem(LOGINFROM_KEY);
        this.clearLoginFrom();
        return from;
    },
    clearLoginFrom() {
        localStorage.removeItem(LOGINFROM_KEY);
    },

    //匿名聊天用户信息
    getChatAnonymous() {
        const userInfo = JSON.parse(localStorage.getItem(CHATANONYMOUSINFO_KEY));
        if (userInfo === null || userInfo === 'undefined' || userInfo.expires_in < (Date.now() / 1000)) {
            this.clearChatAnonymous();
            return null;
        }
        return userInfo;
    },

    setChatAnonymous(userInfo) {
        userInfo.expires_in = userInfo.expires_in + (Date.now() / 1000);
        localStorage.setItem(CHATANONYMOUSINFO_KEY, JSON.stringify(userInfo));
    },
    clearChatAnonymous() {
        localStorage.removeItem(CHATANONYMOUSINFO_KEY);
    },

}