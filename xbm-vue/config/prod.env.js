'use strict'
const utils = require('./utils')
const myHost = utils.getNetworkIp();

module.exports = {
  NODE_ENV: '"production"',
  // API_ROOT:'/ly/',
  MOCK: 'false',
  MAC: '"' + myHost + '"'
}
