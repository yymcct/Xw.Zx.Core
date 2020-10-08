// vue.config.js
const autoprefixer = require('autoprefixer');
const pxtorem = require('postcss-pxtorem');
module.exports = {
    // 选项...
    publicPath: '/sqb',
    productionSourceMap: false,
    devServer: {
        port: 80,
        disableHostCheck: true,
        proxy: 'http://localhost:63836'//'http://139.155.8.217'//
    },
    css: {
        loaderOptions: {
            postcss: {
                plugins: [
                    autoprefixer(),
                    pxtorem({
                        rootValue: 37.5,
                        propList: ['*']
                    })
                ]
            }
        }
    }

}