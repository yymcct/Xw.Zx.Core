<template>
    <view>
        <page-head :title="title"></page-head>
        <view class="uni-padding-wrap">
            <view style="background:#FFF; padding:50upx 0;">
                <view class="uni-hello-text uni-center">支付金额</text></view>
                <view class="uni-h1 uni-center uni-common-mt">
                    <text class="rmbLogo">￥</text>
                    <input class="price" type="digit" :value="price" maxlength="4" @input="priceChange" />
                </view>
            </view>
            <view class="uni-btn-v uni-common-mt">
                <!-- #ifdef MP-WEIXIN || MP-QQ -->
                <button type="primary" @click="weixinPay" :loading="loading">微信支付</button>
                <!-- #endif -->
                <!-- #ifdef APP-PLUS -->
                <template v-if="providerList.length > 0">
                    <button v-for="(item,index) in providerList" :key="index" @click="requestPayment(item,index)"
                        :loading="item.loading">{{item.name}}支付</button>
                </template>
                <!-- #endif -->
            </view>
        </view>
    </view>
    </view>
</template>
<script>
    export default {
        data() {
            return {
                title: 'request-payment',
                loading: false,
                price: 0.01,
                providerList: []
            }
        },
        onLoad: function() {
            // #ifdef APP-PLUS
            uni.getProvider({
                service: "payment",
                success: (e) => {
                    console.log("payment success:" + JSON.stringify(e));
                    let providerList = [];
                    e.provider.map((value) => {
                        switch (value) {
                            case 'alipay':
                                providerList.push({
                                    name: '支付宝',
                                    id: value,
                                    loading: false
                                });
                                break;
                            case 'wxpay':
                                providerList.push({
                                    name: '微信',
                                    id: value,
                                    loading: false
                                });
                                break;
                            default:
                                break;
                        }
                    })
                    this.providerList = providerList;
                },
                fail: (e) => {
                    console.log("获取支付通道失败：", e);
                }
            });
            // #endif
        },
        methods: {
            weixinPay() {
                console.log("发起支付");
                this.loading = true;
                uni.login({
                    success: (e) => {
                        console.log("login success", e);
                        uni.request({
                            url: `https://unidemo.dcloud.net.cn/payment/wx/mp?code=${e.code}&amount=${this.price}`,
                            success: (res) => {
                                console.log("pay request success", res);
                                if (res.statusCode !== 200) {
                                    uni.showModal({
                                        content: "支付失败，请重试！",
                                        showCancel: false
                                    });
                                    return;
                                }
                                if (res.data.ret === 0) {
                                    console.log("得到接口prepay_id", res.data.payment);
                                    let paymentData = res.data.payment;
                                    uni.requestPayment({
                                        timeStamp: paymentData.timeStamp,
                                        nonceStr: paymentData.nonceStr,
                                        package: paymentData.package,
                                        signType: 'MD5',
                                        paySign: paymentData.paySign,
                                        success: (res) => {
                                            uni.showToast({
                                                title: "感谢您的赞助!!"
                                            })
                                        },
                                        fail: (res) => {
                                            uni.showModal({
                                                content: "支付失败,原因为: " + res
                                                    .errMsg,
                                                showCancel: false
                                            })
                                        },
                                        complete: () => {
                                            this.loading = false;
                                        }
                                    })
                                } else {
                                    uni.showModal({
                                        content: res.data.desc,
                                        showCancel: false
                                    })
                                }
                            },
                            fail: (e) => {
                                console.log("fail", e);
                                this.loading = false;
                                uni.showModal({
                                    content: "支付失败,原因为: " + e.errMsg,
                                    showCancel: false
                                })
                            }
                        })
                    },
                    fail: (e) => {
                        console.log("fail", e);
                        this.loading = false;
                        uni.showModal({
                            content: "支付失败,原因为: " + e.errMsg,
                            showCancel: false
                        })
                    }
                })
            },
            async requestPayment(e, index) {
                this.providerList[index].loading = true;
                //let orderInfo = await this.getOrderInfo(e.id);
                //console.log("得到订单信息", orderInfo);
                // if (orderInfo.statusCode !== 200) {
                //     console.log("获得订单信息失败", orderInfo);
                //     uni.showModal({
                //         content: "获得订单信息失败",
                //         showCancel: false
                //     })
                //     return;
                // }
                uni.requestPayment({
                    provider: e.id,
                    orderInfo: 'app_id=2017121200619628&biz_content=%7b%22body%22%3a%22%e6%88%91%e6%98%af%e6%b5%8b%e8%af%95%e6%95%b0%e6%8d%ae%22%2c%22out_trade_no%22%3a%2220170216555555555555555501%22%2c%22product_code%22%3a%22QUICK_MSECURITY_PAY%22%2c%22subject%22%3a%22App%e6%94%af%e4%bb%98%e6%b5%8b%e8%af%95DoNet%22%2c%22timeout_express%22%3a%2230m%22%2c%22total_amount%22%3a%220.01%22%7d&charset=UTF-8&format=json&method=alipay.trade.app.pay&sign_type=RSA2&timestamp=2019-09-18+17%3a57%3a53&version=1.0&sign=NGLxKCpDIrkYnaicAGi%2fdUffkw8euoDPGS562NRNDPU7gBjzkHNlg3Uhyvfeb3BOijJDOLCIwmpmGYHpQCH2gaDHIXUcWfmwViSJHSKLZ%2bdxd1TlX%2f84RyNwjO%2bJPlIg7ZfJEXI3p2uLhzUy19yWuNZtcPXUT3TuebQ%2fvR4rVSe8DMYLANy4KKEjpgwNGRV6PbeFZGxnCNUr93%2bDUbwifTdg%2b9VLq23dFVxV6f3E4OFHgXntQDFjbVObQO%2bBHWxmc5qble6Hm6wdX0wqJRzDfWOXGUIpOifC2AM%2bzHkRrclDX63uHmSaXgfhhfMiAGkrANlOZomj9dexz6O0c32chw%3d%3d',
                    success: (e) => {
                        console.log("success", e);
                        uni.showToast({
                            title: "感谢您的赞助!"
                        })
                    },
                    fail: (e) => {
                        console.log("fail", e);
                        uni.showModal({
                            content: "支付失败,原因为: " + e.errMsg,
                            showCancel: false
                        })
                    },
                    complete: () => {
                        this.providerList[index].loading = false;
                    }
                })
            },
            getOrderInfo(e) {
                let appid = "";
                // #ifdef APP-PLUS
                appid = plus.runtime.appid;
                // #endif
                //let url = 'https://demo.dcloud.net.cn/payment/?payid=' + e + '&appid=' + appid + '&total=' + this.price;
				let url = 'https://demo.dcloud.net.cn/payment/?payid=' + e + '&appid=' + appid + '&total=' + this.price;
                return new Promise((res) => {
                    uni.request({
                        url: url,
                        success: (result) => {
							console.log(result);
                            res(result);
                        },
                        fail: (e) => {
                            res(e);
                        }
                    })
                })
            },
            priceChange(e) {
                console.log(e.detail.value)
                this.price = e.detail.value;
            }
        }
    }
</script>

<style>
    .rmbLogo {
        font-size: 40upx;
    }

    button {
        background-color: #007aff;
        color: #ffffff;
    }

    .uni-h1.uni-center {
        display: flex;
        flex-direction: row;
        justify-content: center;
        align-items: flex-end;
    }

    .price {
        border-bottom: 1px solid #eee;
        width: 200upx;
        height: 80upx;
        padding-bottom: 4upx;
    }

    .ipaPayBtn {
        margin-top: 30upx;
    }
</style>