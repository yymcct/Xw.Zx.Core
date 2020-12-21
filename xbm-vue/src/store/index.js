import Vue from 'vue'
import Vuex from 'vuex';
Vue.use(Vuex);
import user from './modules/User'
import email from './modules/Email'
import approvalMenu from './modules/approvalMenu'
import expireLicense from './modules/expireLicense'
export default new Vuex.Store({
	state: {
		isCollapse: false,
	},
	getters: {},
	mutations: {
		setNavCollapse(state, curVal) {
			this.state.isCollapse = curVal;
		},
	},
	modules: {
		user,
		email,
		approvalMenu,
		expireLicense
	}
})

