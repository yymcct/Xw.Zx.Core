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

        public DbSet<MailSrc> MailSrcs { get; set; }

        public DbSet<BankCard> BankCards { get; set; }

        public DbSet<BankBill> BankBills { get; set; }

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
