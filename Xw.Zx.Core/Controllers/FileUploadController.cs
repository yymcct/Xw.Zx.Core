using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Xw.Zx.Core.Models.Dto;
using Microsoft.AspNetCore.Http.Internal;
using System.Drawing.Imaging;

namespace Xw.Zx.Core.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;


        public FileUploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

        }
        /// <summary>
        /// 上传文件,一次可以提交多个文件 支持格式:".png", ".jpg", ".jpeg", ".gif", ".bmp", ".mp4"
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HbzsResult<FileUploadDto> Files(IFormCollection formCollection)
        {
            var res = new FileUploadDto();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;

            foreach (IFormFile file in filelist)
            {
                string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssffffff")}{Path.GetExtension(file.FileName)}";
                string filePath = $"/UpLoad/{Path.GetExtension(file.FileName).Replace(".", "")}/{DateTime.Now.ToString("yyyy-MM-dd")}/";

                if (CheckImageFileType(fileName) == true)
                {
                    DirectoryInfo di = new DirectoryInfo(_hostingEnvironment.ContentRootPath + filePath);
                    if (!di.Exists) { di.Create(); }
                    using (FileStream fs = System.IO.File.Create(_hostingEnvironment.ContentRootPath + filePath + fileName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    res.Files.Add(new FileUploadDto.FileSate()
                    {
                        SoureName = file.FileName,
                        CurPathName = filePath + fileName,
                        IsSuccess = true
                    });
                }
                else
                {
                    res.Files.Add(new FileUploadDto.FileSate()
                    {
                        SoureName = file.FileName,
                        IsSuccess = false,
                        ErrMsg = "上传的媒体格式不正确"
                    });
                }
            }

            return new HbzsResult<FileUploadDto>(res);
        }

        /// <summary>
        /// 上传身份证专用接口 支持格式:".png", ".jpg", ".jpeg"
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        //[Authorize]
        public HbzsResult<FileUploadDto> IdentityCard(IFormCollection formCollection)
        {
            var res = new FileUploadDto();

            FormFileCollection filelist = (FormFileCollection)formCollection.Files;

            foreach (IFormFile file in filelist)
            {
                string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssffffff")}{Path.GetExtension(file.FileName)}";
                string filePath = $"/UpLoad/IdentityCard/{DateTime.Now.ToString("yyyy-MM-dd")}/";

                if (CheckImageFileType(fileName) == true)
                {
                    DirectoryInfo di = new DirectoryInfo(_hostingEnvironment.ContentRootPath + filePath);
                    if (!di.Exists) { di.Create(); }
                    using (FileStream fs = System.IO.File.Create(_hostingEnvironment.ContentRootPath + filePath + fileName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    res.Files.Add(new FileUploadDto.FileSate()
                    {
                        SoureName = file.FileName,
                        CurPathName = filePath + fileName,
                        IsSuccess = true
                    });
                }
                else
                {
                    res.Files.Add(new FileUploadDto.FileSate()
                    {
                        SoureName = file.FileName,
                        IsSuccess = false,
                        ErrMsg = "上传的媒体格式不正确"
                    });
                }
            }

            return new HbzsResult<FileUploadDto>(res);
        }

        private bool CheckImageFileType(string filename)
        {
            string[] ImageAllowImageExtensions = new string[] { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".mp3", ".mp4" };
            var fileExtension = Path.GetExtension(filename).ToLower();
            return ImageAllowImageExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }
        private bool CheckFileSize(long fileSize, string fileName)
        {
            //图片大于300k 无法上传
            //音乐大于6M 无法上传
            long MaxSize = 0;
            if (fileName.ToLower().Contains(".mp3"))
                MaxSize = 6000000;
            else
                MaxSize = 6000000;

            if (fileSize > MaxSize)
                return false;
            else
                return true;
        }        
    }
}
