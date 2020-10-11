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

select * from orders  order by id desc

update orders set ProductId =10 , ProducName ='原价999元  限时三天9.9元 法律援助大礼包' where id =1770