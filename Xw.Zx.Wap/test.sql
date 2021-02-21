select top 10 * from Members where phone='18624938007'

--update Members set WxOpenId='' WHERE Phone='18624938007'

select * from Members where RealName like '%朱小勤%'

select top 10 * from Members where phone='14797716268'

--delete Members where phone='18824938007'

--update members set MemberVipType=0 where phone='18624938007'

select top 10 * from Members where phone='18688448080'

--update Members set AliPayAccount = 'yymcct@163.com' where phone='18688448080'

select * from WithdrawDeposits order by id desc

delete WithdrawDeposits where id = 152
--18688448080 312925xw

select top 10 * from UpdateVipAuthCodes order by id desc

--UPDATE UpdateVipAuthCodes 
--    set ExpiesTime='2020-10-24', UPdateVipAuthCodeState='0', MemberVipType='20'  
--    where id = 864

select * from Products

--update products set Price='0.1' where id = 10

select * from orders  order by id desc

--update orders set Amount='0.1', [Timestamp]='20201017134251806513' where id =1786


select * from SmsCheck where Phone='18624938007'


select * from Orders order by id desc

select * from Receivables order by id desc
update IncomeAccounts set Auditime=GETDATE(),auditmemberId=72,IncomeAccountState=10 where IsDelete = 0
-- 测试分润
select * from Members where id =789  
select * from Orders where Timestamp='20201224152821752346'
select * from ShareProfitConfigs
select top 100 * from IncomeAccounts order by id desc

select MemberId, count(*) from IncomeAccounts where IsDelete=0 group by MemberId  order by count(*) desc
select * from IncomeAccounts where MemberId = 750

select * from WithdrawDeposits

update WithdrawDeposits set WithdrawDepositState = 10 where id =167

update Members set AliPayAccount='18624988888', AliPayAccountName='测试' where Id = 750

--15982820184	heroswine520sh
update IncomeAccounts set IncomeAccountState =10 where MemberId = 47





-- memberID 784    InviteId 750 -> InviteId 47
--update Orders set orderstate = 0  where Timestamp='20201222230655723972'

select * from Members where id =789
select * from MemberBalanceLogs where Memberid=789

--update Members set MemberVipType = 20 where id =47

--update IncomeAccounts set MemberId=72 where id = 317

  select * from Members where id = 72
  select Memberid, max(CurMoney) from MemberBalanceLogs group by Memberid order by Memberid
  select memberId, sum(amount) from IncomeAccounts where IsDelete=0 group by Memberid order by Memberid

  select Memberid, sum(Amount) from MemberBalanceLogs group by Memberid order by Memberid

  select * from MemberBalanceLogs where Memberid = 72

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

--UPDATE Orders SET ProductAmount = Amount 

--UPDATE Orders SET Amount = 0 WHERE OrderPaymentType = 4 

-- 测试提现
SELECT * FROM WithdrawDeposits
select * from WithdrawDepositLogs
select * from Members where phone='18624938007'
--update Members set RoleName = 'Admin_Tongjibu' where phone='18624938007' 
--update Members set RoleName = 'Admin_Caiwu' where phone='18624938007' 
--update Members set RoleName = 'Admin_CaiwuManager' where phone='18624938007' 

--update WithdrawDeposits set WithdrawDepositState = 0 where id = 157

--update IncomeAccounts set IncomeAccountState=10 

--delete WithdrawDepositLogs where WithdrawDepositId=157
 
 -- 测试碧麒麟支付

 select * from BiqilinLogs

 -- 测试积分支付

 -- update Products set CanUseMemberIntegral = 1 where id =
 select * from Orders where id =4320
 select * from Orders where id = 2839
 select * from IncomeAccounts where SourceOrderId=2839 
 update Orders set OrderState=0,
		IsDelete=0,AddTime='2021-02-19 08:19:34'
		, Timestamp='20210204224144489995'
		, MemberId = 72
		where id = 4320

-- 测试手机端切换通道
select * from members where RoleName='Admin_CaiwuPayChange'

select * from  Products

-- update Products set Price=0.01 where id =9

select * from SysParams

-- update SysParams set Value=2-

-- 微信订单
select  * from WechatOrders where SubState = 20
select * from WechatSubDetail
select * from WechatSubLedgerReceivers

select * from SysLogs


select * from WechatSubDetail where SubState = '申请中'
--4200000781202101254581877145
711
-- delete WechatSubDetail where id = 265    

select * from WechatOrders where SubState = 0
select * from WechatSubDetail where SubState= 'SUCCESS'

delete WechatOrders where Out_Order_No='SH20210130093208248446'

select * from WechatOrders where TranTime = '1970-01-01 08:00:00.000'

delete  WechatOrders where TranTime = '1970-01-01 08:00:00.000'


select * from Members where MemberVipType in(10, 20,30, 40)

select Phone as 电话, RealName as 姓名 , (case MemberVipType when 10 then '业务经理' when 20 then '运营中心' when 30 then '大区经理' when 40 then '分公司'  end) as 级别,
	(select phone from Members as B where B.id =  A.InviteId) as 上级电话
	,(select RealName from Members as B where B.id =  A.InviteId) as 上级姓名
	,(select case MemberVipType when 10 then '业务经理' when 20 then '运营中心' when 30 then '大区经理' when 40 then '分公司'  end from Members as B where B.id =  A.InviteId) as 上级级别
from Members as A where MemberVipType in(10, 20,30, 40)
select * from WechatOrders where id = 528 


select sum(amount) from Orders where MemberId = 80


-------------------------------------------------
DECLARE @memberId Int
DECLARE @startTime DateTime
DECLARE @endTime DateTime

set @memberId = 771
set @startTime = '2021-01-01'
set @endTime = '2021-02-01';

WITH T
AS( 
    SELECT Id,InviteId as 上级ID,RealName as 姓名, Phone as 电话,0  as 上下级层级 FROM Members WHERE Id=@memberId
    UNION ALL 
    SELECT U.Id,U.InviteId,U.RealName,U.Phone,上下级层级+1   
    FROM Members U INNER JOIN T ON U.InviteId=T.Id  
) 
SELECT *,
	(select sum(amount) 
		from Orders where MemberId = T.Id and Orders.Amount!='9.9'  and OrderState=1 and Amount!=0 and AddTime between @startTime and  @endTime
	) as 合计  
FROM T 
where (select sum(amount) from Orders where MemberId = T.Id and Orders.Amount!='9.9'  and OrderState=1 and Amount!=0 and AddTime between @startTime and  @endTime ) is not null

---------------------------------------------------
DECLARE @memberId Int
DECLARE @startTime DateTime
DECLARE @endTime DateTime

set @memberId = 771
set @startTime = '2021-01-01'
set @endTime = '2021-02-01';

WITH T
AS( 
    SELECT Id,InviteId as 上级ID,RealName as 姓名, Phone as 电话,0  as 上下级层级 FROM Members WHERE Id=@memberId
    UNION ALL 
    SELECT U.Id,U.InviteId,U.RealName,U.Phone,上下级层级+1   
    FROM Members U INNER JOIN T ON U.InviteId=T.Id  
) 
SELECT *,
	(select sum(amount) 
		from Orders where MemberId = T.Id and Orders.Amount!='9.9'  and OrderState=1 and Amount!=0 and AddTime between @startTime and  @endTime
	) as 合计  
FROM T 
where (select sum(amount) from Orders where MemberId = T.Id and Orders.Amount!='9.9'  and OrderState=1 and Amount!=0 and AddTime between @startTime and  @endTime ) is not null

-------------------------------------------------------

 exec GetAmount @memberId = 47,@startTime = '2020-11-01',@endTime = '2020-12-01'

 

if (not exists (select 1 from CiticbankMchIds where exists (select 1 from Members where id = CiticbankMchIds.MemberId and phone = '18624948007'))) and exists(select 1 from Members where  phone = '18624948007')
	INSERT INTO [CiticbankMchIds]([CreateTime],[MemberId],[MchId], IsDelete)VALUES('2021-01-14',(select id from Members where phone = '18624948007'),'1234',0)

select top 100 * FROM orders order by id desc
select * from BiqilinLogs order by id desc


select CONVERT(varchar(100), AddTime, 23), sum(amount)
from Orders 
where orders.AddTime > '2020-01-20' and IsDelete =0 and OrderState=1
group by CONVERT(varchar(100), AddTime, 23) 
order by sum(amount) desc



--
select orders.Timestamp as 单号,
	Orders.ProducName as 产品名称,
	Orders.AddTime as 下单时间,
	Orders.Amount as 金额合计, 
	Orders.ProductAmount as 单价,
	ProductCount as 数量, 
	A.RealName 购买人, 
	A.Phone 购买人电话, 
	B.RealName 购买人上级, 
	B.Phone 购买人上级电话
from orders left join Members as A on Orders.MemberId = A.Id
join Members as B on A.InviteId = B.Id
where OrderState = 1 and orders.IsDelete=0 
	and AddTime between '2020-10-01 00:00:00' and '2021-02-19 00:00:00'