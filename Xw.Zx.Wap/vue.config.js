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
        proxy:'http://jsq.lawss360.com/'// 'http://localhost:63836'//
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