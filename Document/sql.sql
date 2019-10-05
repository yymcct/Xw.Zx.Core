select * from Members where id = 6
select * from Members where InviteId = 6

------------------------------------------------------银行卡账单---------------------------------------------------
select * from BankCards where MemberId = 6
select * from MailSrcs where memberid=6 
select * from BankBillDetails where memberid=6

delete BankCards where  MemberId = 6
delete BankBillDetails where memberid=6
delete MailSrcs where MemberId = 6

update BankBillDetails set bankcardtype = 2 where CardNum='3000'

select * from UpdateVipAuthCodes

update MailSrcs  set IsPrased = 0 where  memberid=6 and [from] = 'creditcard@electronicbill.gzcb.com.cn'

select * from MailSrcs where MemberId=6  and [from] ='creditcardcenter@cardmail.psbc.com' and BodyText like '%利息交易%'

delete MailSrcs  where MemberId=6  and [from] ='creditcardcenter@cardmail.psbc.com'


------------------------------------------------------VIP升级分润--------------------------------------------------
select * from orders
select * from Receivables

-- VIP升级时会产生:
-- 订单, 支付单 一代收益单 二代收益单 合伙人收益单 服务站收益单 运营商收益单





