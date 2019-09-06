using Sieve.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public enum BankCardType
    {
         招商银行=0,
         浦发银行=1,
         中信银行=2,
         平安银行=3,
         广大银行=4,
         华夏银行=5,
         民生银行=6
    }
    public class BankCard
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int Id { get; set; }

        public int MemberId { get; set; }
        //卡号
        public string CardNum { get; set; } = "";
        //银行
        public BankCardType Bank { get; set; }

        //用户名
        public string Email { get; set; }
        //密码
        public string Password { get; set; }
       
        //创建日期
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //是否已删除
        public bool Disabled { get; set; } = false;

        //备注
        public string Remark { get; set; } = "";
    }
}
