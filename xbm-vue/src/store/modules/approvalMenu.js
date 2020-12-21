const menuState = {
    state: {
        active: JSON.parse(sessionStorage.getItem('approvalMenu')),
        manageActive: JSON.parse(sessionStorage.getItem('manageMenu')),
        password: false

    },

    mutations: {
        changeMenuDefault: (state, path) => {
            sessionStorage.setItem('approvalMenu', JSON.stringify(path));
            state.active = path
        },
        // 电子政务
        manageMenuDefault: (state, path) => {
            sessionStorage.setItem('manageMenu', JSON.stringify(path));
            state.manageActive = path
        },
        changePassword(state) {
            state.password = true;
          },
      
    },
    actions: {

    }
}

export default menuState