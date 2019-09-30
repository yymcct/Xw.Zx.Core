using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    public interface IMailSyncService
    {
        int MemberId { set; }
        IMailService MailService { set; }

        Task<int> SyncMailDirToDbAsync();

        Task<int> SyncMailToDbAsync();
    }
}
