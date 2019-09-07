using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Model;

namespace Xw.Zx.Core.Models.Dto
{
    public class BankDto
    {
    }

    public class PostBankDto
    {
        public int Id { get; set; }
        public string CardNum { get; set; }
        public BankCardType Bank { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Remark { get; set; }
    }

    public class BankInfoDto
    {
        public int Id { get; set; }
        public string CardNum { get; set; }
        public BankCardType Bank { get; set; }

        public DateTime? LastSyncTime { get; set; }

        public bool LastSyncIsOk { get; set; }

        public decimal OverdueFine { get; set; } = 0;
    }
}
