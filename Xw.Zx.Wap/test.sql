select top 10 * from Members where phone='18624938007'

update members set MemberVipType=0 where phone='18624938007'

select top 10 * from Members where phone='18688448080'

update Members set AliPayAccount = 'yymcct@163.com' where phone='18688448080'

select * from WithdrawDeposits order by id desc

delete WithdrawDeposits where id = 152
--18688448080 312925xw

select top 10 * from UpdateVipAuthCodes order by id desc

UPDATE UpdateVipAuthCodes 
    set ExpiesTime='2020-10-24', UPdateVipAuthCodeState='0', MemberVipType='20'  
    where id = 864

select * from Products

update products set Price='19800.00' where id = 9

select * from orders  order by id desc

update orders set ProductId =10 , ProducName ='原价999元  限时三天9.9元 法律援助大礼包' where id =1770

请求时间: 2020-10-14 13:42:58,946
请求信息: http://openapi.alipay.com/gateway.do?charset=UTF-8&charset=UTF-8&biz_content={"body":"补差价拍此处","goods_type":"0","out_trade_no":"20201014134251806513","product_code":"QUICK_WAP_PAY","subject":"补差价拍此处","timeout_express":"50m","total_amount":"19800.00"}&method=alipay.trade.wap.pay&format=json&sign=***&return_url=http://192.168.0.121/sqb/order/1783&notify_url=http://139.155.8.217/api/Alipay/WapNotifyurl&app_id=2017121200619628&sign_type=RSA2&version=1.0&timestamp=2020-10-14 13:42:58
错误信息: -  , -  , -

