using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Models.Dto
{
    public class FileUploadDto
    {
        public FileUploadDto()
        {
            Files = new List<FileSate>();
        }
        public class FileSate
        {
            public string SoureName { get; set; }
            public string CurPathName { get; set; }

            public bool IsSuccess { get; set; }

            public string ErrMsg { get; set; }
        }
        public List<FileSate> Files { get; set; }
    }
}
