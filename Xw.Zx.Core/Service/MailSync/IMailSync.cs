using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service.MailSync
{
    public interface IMailSync
    {
        int MemberId { set; }
        IMailService MailService { set; }

        int SyncMailDirToDb();

        int SyncMailToDb();
    }
}
