select * from Members where id = 6
select * from Members where InviteId = 6

------------------------------------------------------“¯––ø®’Àµ•---------------------------------------------------
select * from BankCards where MemberId = 6
select * from MailSrcs where memberid=6
select * from BankBillDetails where memberid=6

delete BankCards where  MemberId = 6
delete BankBillDetails where memberid=6
delete MailSrcs where MemberId = 6

update BankBillDetails set bankcardtype = 2 where CardNum='3000'

select * from UpdateVipAuthCodes

update MailSrcs  set IsPrased = 0 where  memberid=6 and [from] = 'creditcard@electronicbill.gzcb.com.cn'










