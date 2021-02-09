const USERINfO_KEY = 'USER_INFO';
const LOGINFROM_KEY = 'LOGIN_FROM'

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
    setLoginFrom(url){
        this.clearLoginFrom();
        localStorage.setItem(LOGINFROM_KEY, url);
    },
    getLoginFrom() {
        const from = localStorage.getItem(LOGINFROM_KEY);
        this.clearLoginFrom();
        return from;
    },
    clearLoginFrom() {
        localStorage.removeItem(LOGINFROM_KEY);
    },
}