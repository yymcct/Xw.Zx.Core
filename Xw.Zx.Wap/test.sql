select top 10 * from Members where phone='18624938007'

update Members set WxOpenId='' WHERE Phone='18624938007'

select top 10 * from Members where phone='18824938007'

delete Members where phone='18824938007'

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

update products set Price='0.1' where id = 10

select * from orders  order by id desc

update orders set Amount='0.1', [Timestamp]='20201017134251806513' where id =1786


select * from SmsCheck where Phone='18624938007'


select * from Orders order by id desc

select * from Receivables order by id desc

-- 测试分润
select * from Members where id =784
select * from Orders where Timestamp='20201201153851803671'
select * from ShareProfitConfigs
select top 100 * from IncomeAccounts order by id desc

update Orders set orderstate = 0  where Timestamp='20201201153851803671'



