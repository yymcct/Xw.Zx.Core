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

        /// <summary>
        /// 系统参数表
        /// </summary>
        public DbSet<SysParam> SysParams { get; set; }

        public DbSet<Member> Members { get; set; }

        public DbSet<MemberIntegral> MemberIntegrals { get; set; }

        public DbSet<MemberIntegralRecord> MemberIntegralRecords { get; set; }

        public DbSet<MemberBalanceLog> MemberBalanceLogs { get; set; }

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

        public DbSet<BiqilinLog> BiqilinLogs { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Receivable> Receivables { get; set; }

        //用户收益表
        public DbSet<IncomeAccount> IncomeAccounts { get; set; }

        //用户提现表
        public DbSet<WithdrawDeposit> WithdrawDeposits { get; set; }

        public DbSet<WithdrawDepositLog> WithdrawDepositLogs { get; set; }

        //支付宝支付记录表
        public DbSet<AlipayLog> AlipayLogs { get; set; }

        public DbSet<ShareProfitConfig> ShareProfitConfigs { get; set; }
        #endregion

        #region 其他 
        /// <summary>
        /// 验证码
        /// </summary>
        public DbSet<SmsCheck> SmsCheck { get; set; }

        public DbSet<AppVersion> AppVersions { get; set; }

        /// <summary>
        /// 语音新闻
        /// </summary>
        public DbSet<VoiceNew> VoiceNews { get; set; }

        /// <summary>
        /// 利息计算
        /// </summary>
        public DbSet<LxComputer> LxComputers { get; set; }
        #endregion

        #region 优惠券
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponReceive> CouponReceives { get; set; }
        public DbSet<CouponUseLog> CouponUseLogs { get; set; }
        #endregion


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=blogging.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(o => o.OrderPaymentType)
                .HasDefaultValue(OrderPaymentType.支付宝);

            modelBuilder.Entity<UpdateVipAuthCode>()
                  .Property(o => o.MemberVipType)
                  .HasDefaultValue(MemberVipType.客户);

            modelBuilder.Entity<SysParam>()
               .HasQueryFilter(c => !c.IsDelete)
               .Property(c => c.IsDelete)
               .HasDefaultValue(false);

            modelBuilder.Entity<Member>()
                .HasQueryFilter(c => !c.IsDelete)
                .Property(c => c.IsDelete)
                .HasDefaultValue(false);
    
            modelBuilder.Entity<Member>()
               .Property(c => c.Money)
               .HasDefaultValue(0m);

            modelBuilder.Entity<MemberBalanceLog>()
                .HasQueryFilter(c => !c.IsDelete)
                .Property(c => c.IsDelete)
                .HasDefaultValue(false);

            modelBuilder.Entity<MemberIntegral>()
               .HasQueryFilter(c => !c.IsDelete)
               .Property(c => c.IsDelete)
               .HasDefaultValue(false);
        
            modelBuilder.Entity<MemberIntegral>()
               .HasOne(m=> m.Member);

            modelBuilder.Entity<MemberIntegralRecord>()
               .HasQueryFilter(c => !c.IsDelete)
               .Property(c => c.IsDelete)
               .HasDefaultValue(false);

            modelBuilder.Entity<MemberIntegralRecord>()
                .HasOne(m => m.Member);


            modelBuilder.Entity<ApplyForZxMDto>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<Mailconfig>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<MailSrc>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<BankCard>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<BankBill>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<BankBillDetail>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<UpdateVipAuthCode>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<Product>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<Order>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<Payment>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<Receivable>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<IncomeAccount>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<WithdrawDeposit>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);

            modelBuilder.Entity<WithdrawDepositLog>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);

            modelBuilder.Entity<AlipayLog>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<AppVersion>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<VoiceNew>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<LxComputer>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<ShareProfitConfig>()
                           .HasQueryFilter(c => !c.IsDelete)
                           .Property(c => c.IsDelete)
                           .HasDefaultValue(false);
            modelBuilder.Entity<Coupon>()
               .HasQueryFilter(c => !c.IsDelete)
               .Property(c => c.IsDelete)
               .HasDefaultValue(false);
            modelBuilder.Entity<CouponReceive>()
               .HasQueryFilter(c => !c.IsDelete)
               .Property(c => c.IsDelete)
               .HasDefaultValue(false);

            modelBuilder.Entity<CouponReceive>()
              .HasOne(p => p.Coupon);

            modelBuilder.Entity<CouponUseLog>()
               .HasQueryFilter(c => !c.IsDelete)
               .Property(c => c.IsDelete)
               .HasDefaultValue(false);

            modelBuilder.Entity<BiqilinLog>()
               .HasQueryFilter(c => !c.IsDelete)
               .Property(c => c.IsDelete)
               .HasDefaultValue(false);
        }
    }
}
