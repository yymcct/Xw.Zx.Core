using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    public interface IQQMailSyncService
    {
        void Init(int memberId, string sid, string cookie);
        Task SyncMailAsync();
    }
}
