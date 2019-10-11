select * from Members where id = 6
select * from Members where InviteId = 6

select * from Members where AliPayAccount !=''

--update Members set AliPayAccount = ''

-- delete Mailconfigs where MemberId = 72

------------------------------------------------------银行卡账单---------------------------------------------------
select * from BankCards where MemberId = 6
select * from MailSrcs where memberid=6 
select * from BankBillDetails where memberid=6

select * from Members where Phone = 18624938007
update Members set MemberVipType = 0 where Phone = 18624938007


select * from UpdateVipAuthCodes



select * from MailSrcs where MemberId=6  and [from] ='creditcardcenter@cardmail.psbc.com' and BodyText like '%利息交易%'


------------------------------------------------------VIP升级分润--------------------------------------------------
select * from orders where MemberId = 7
select * from Receivables
select * from IncomeAccounts
select * from WithdrawDeposits
select * from Payments
select * from AlipayLogs
--delete WithdrawDeposits where MemberId =6
--delete AlipayLogs where id > 10

-- VIP升级时会产生:
-- 订单, 支付单 一代收益单 二代收益单 合伙人收益单 服务站收益单 运营商收益单
delete WithdrawDeposits where id=13

------------------------------------------------------追息申请--------------------------------------------------


select * from UpdateVipAuthCodes


-- 提现
select WithdrawDeposits.*, Members.RealName , 
case when WithdrawDepositState=0 then '待审核' when WithdrawDepositState=1 then '成功' when WithdrawDepositState=3 then '失败' end as 状态
from WithdrawDeposits join Members on WithdrawDeposits.MemberId = Members.Id