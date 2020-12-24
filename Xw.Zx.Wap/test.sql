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

-- memberID 784    InviteId 750 -> InviteId 47
update Orders set orderstate = 0  where Timestamp='20201222230655723972'

select * from Members where id =72
select * from MemberBalanceLogs where Memberid=72

update Members set MemberVipType = 20 where id =47

update IncomeAccounts set MemberId=72 where id = 317


  select Memberid, max(CurMoney) from MemberBalanceLogs group by Memberid order by Memberid
  select memberId, sum(amount) from IncomeAccounts where IsDelete=0 group by Memberid order by Memberid

  select Memberid, sum(Amount) from MemberBalanceLogs group by Memberid order by Memberid

-- 测试优惠券
-- Insert rows into table 'Coupons'
INSERT INTO Coupons
( -- columns to insert data into
 [CreateTime], [Name], [StartTime],EndTime, Money,TotalCount,CurCount
)
VALUES
( -- first row: values for the columns in the list above
 GETDATE(), '永久5000VIP抵用卷', GETDATE(),GETDATE(), 5000, 999,999
)
-- add more rows here
GO

SELECT * FROM Coupons
SELECT * FROM CouponReceives

UPDATE Orders SET ProductAmount = Amount 

UPDATE Orders SET Amount = 0 WHERE OrderPaymentType = 4 

-- 测试提现
SELECT * FROM WithdrawDeposits
select 
