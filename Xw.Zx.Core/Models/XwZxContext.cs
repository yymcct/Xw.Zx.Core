using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Xw.Zx.Core.Models.Model
{
    public class XwZxContext : DbContext
    {
        public XwZxContext(DbContextOptions<XwZxContext> options)
          : base(options)
        { }

        public DbSet<Member> Members { get; set; }

        public DbSet<ApplyForZxMDto> ApplyForZxs { get; set; }

        #region 邮箱
        public DbSet<Mailconfig> Mailconfigs { get; set; }
        public DbSet<MailSrc> MailSrcs { get; set; }

        #endregion

        #region 信用卡

        /// <summary>
        /// 信用卡
        /// </summary>
        public DbSet<BankCard> BankCards { get; set; }

        /// <summary>
        /// 信用卡月账单
        /// </summary>
        public DbSet<BankBill> BankBills { get; set; }

        /// <summary>
        /// 消费明细账单
        /// </summary>
        public DbSet<BankBillDetail> BankBillDetails { get; set; }

        #endregion

        #region VIP 升级分润
        /// <summary>
        /// 会员码 普通会员可以通过会员码升级为会员
        /// </summary>
        public DbSet<UpdateVipAuthCode> UpdateVipAuthCodes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Receivable> Receivables { get; set; }

        //用户收益表
        public DbSet<IncomeAccount> IncomeAccounts { get; set; }

        //用户提现表
        public DbSet<WithdrawDeposit> WithdrawDeposits { get; set; }

        //支付宝支付记录表
        public DbSet<AlipayLog> AlipayLogs { get; set; }
        #endregion


        #region 其他 
        /// <summary>
        /// 验证码
        /// </summary>
        public DbSet<SmsCheck> SmsCheck { get; set; }

        public DbSet<AppVersion> AppVersions { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=blogging.db");
        }
    }
}
