using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Service
{
    public interface IXtsuoService
    {
        public bool SyncXtsuoOrders(XtsuoOrdersRequestDto getThirdQueryOrdersRequestPsDto);
    }
}
