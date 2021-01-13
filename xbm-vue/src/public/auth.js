import Cookies from 'js-cookie'

const TokenKey = 'token'
const userKey = 'user'
const loginInfo = 'loginInfo'
const Login = 'Login'
export function getToken() {
  return Cookies.get(TokenKey)
}

export function setToken(token) {
  return Cookies.set(TokenKey, token)
}

export function removeToken() {
  return Cookies.remove(TokenKey)
}
//清空xbm存储的Login
export function removeIfrmeLogin() {
  return Cookies.remove(TokenKey)
}
// export function getUser() {
//   return Cookies.get(userKey)
// }

// export function setUser(user) {
//   return Cookies.set(userKey, user)
// }
// export function removeUser() {
//   return Cookies.remove(userKey)
// }
export function setLogin(key) {
  return Cookies.set(loginInfo, key)
}
export function getLogin() {
  return Cookies.get(loginInfo)
}
export function removeLogin() {
  return Cookies.remove(loginInfo)
}

export function getUserInfo() {
  return JSON.parse(localStorage.getItem("data"))
}
