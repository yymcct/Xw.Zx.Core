import { api_GetWxConfig } from '@/api/weixin';
import wx from "weixin-js-sdk/index";

export function wxShare(url, shareData) {
    api_GetWxConfig({ url: url }).then(res => {
        wx.config({
            debug: false,
            appId: res.result.appId,
            timestamp: res.result.timeStamp,
            nonceStr: res.result.nonceStr,
            signature: res.result.signaTure,
            jsApiList: [
                'onMenuShareTimeline',
                'onMenuShareAppMessage',
                'onMenuShareQQ',
                'onMenuShareQZone'
            ]
        });

        wx.ready(function() {
            // Vue.prototype.$wx = wx

            if (shareData) {
                let msg = shareData.desc;
                msg = msg.replace(/<\/?[^>]*>/g, ''); //去除HTML Tag
                msg = msg.replace(/[|]*\n/, '') //去除行尾空格
                msg = msg.replace(/&npsp;/ig, ''); //去掉npsp
                shareData.desc = msg;
                // 注册分享自定义的信息
                wx.onMenuShareTimeline(shareData);
                wx.onMenuShareAppMessage(shareData);
                //wx.onMenuShareQQ(shareData);
                //wx.onMenuShareQZone(shareData);
            }


        })

        wx.error(function() {
            console.log('微信jdk 出错了')
        })
    }).catch(e => {
        console.log(e)
    })
}