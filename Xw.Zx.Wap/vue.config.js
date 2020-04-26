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
        proxy: 'http://localhost:8222' // 'https://app.huobaowang.com' //    // 'http://192.168.0.34'  // 
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