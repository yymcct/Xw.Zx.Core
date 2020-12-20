using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Model
{
    public abstract class ModelBase_Id_CreateTime:ModelBase
    {
        public int Id { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
