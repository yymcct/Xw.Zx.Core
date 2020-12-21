import Vue from 'vue'
import axios from 'axios'
import {getAuthLiense} from "@/public/apiService/home.js";
import { apiUrl } from "@/public/apiUrl";
const expireLicense = {
  state: {
    dataList:[]
  },
  getters: {
    ExpireLicense: state => {
      return state.dataList
    }
    },
  mutations: {
    ChangeData: (state, value) => {
      state.dataList = value
    },
  },
  actions: {
    GetExpireLicenseList({ commit }) {
      return new Promise((resolve, reject) => {
        getAuthLiense().then(response=>{
        let temp=Object.keys(response.data);
         let tempArr = temp.filter(item => response.data[item] != 0);
         axios.get(apiUrl.GET_EXPIRE_LICENSEFILE_LIST, { params: {ZZLX:tempArr.toString()} })
        .then(res => {
          commit('ChangeData',res.data.data)
          resolve(res)
        })
        }).catch(error => {
          reject(error)
        })
      })
    },
  }
}

export default expireLicense
