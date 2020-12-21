'use strict'
const merge = require('webpack-merge')
const prodEnv = require('./prod.env')
const utils = require('./utils')
const myHost = utils.getNetworkIp()
module.exports = merge(prodEnv, {
  NODE_ENV: '"development"',
  // API_ROOT:'/',
  MOCK: 'true',
  MAC: '"' + myHost + '"'
})
