
export default {
    namespaced: true,
    state: {
        user: null
    },

    getters: {
        user: (state) => {
            return state.user;
        }
    },

    mutations: {
        setUser(state, user) {
            state.user = user;
        },
        clearUser(state) {
            state.user = null;
        }
    },
}