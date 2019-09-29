using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xw.Zx.Core.Models.Dto;

namespace Xw.Zx.Core.Service
{
    public interface ISyncService
    {
       PostSyncMailResuleDto SyncAsync(PostSyncMailDto postSyncMailDto);
    }
}
