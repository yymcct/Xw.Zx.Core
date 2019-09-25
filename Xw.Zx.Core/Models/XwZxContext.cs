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

        public DbSet<Mailconfig> Mailconfigs { get; set; }

        public DbSet<MailSrc> MailSrcs { get; set; }

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

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Receivable> Receivables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("Data Source=blogging.db");
        }
    }
}
