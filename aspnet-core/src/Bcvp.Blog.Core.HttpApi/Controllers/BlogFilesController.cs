using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bcvp.Blog.Core.BlogCore.Files;
using Bcvp.Blog.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Http;

namespace Bcvp.Blog.Core.Controllers
{
    
    [RemoteService(Name = "blog")] // 远程服务的组名
    [Area("blog")]// Mvc里的区域
    [Route("api/blog/files")] //Api路由
    public class BlogFilesController : AbpController, IFileAppService
    {
        private readonly IFileAppService _fileAppService;

        public BlogFilesController(IFileAppService fileAppService)
        {
            _fileAppService = fileAppService;
        }

        [HttpGet]
        [Route("{name}")]
        public Task<RawFileDto> GetAsync(string name)
        {
            return _fileAppService.GetAsync(name);
        }

        [HttpGet]
        [Route("www/{name}")]
        public async Task<FileResult> GetForWebAsync(string name) 
        {
            var file = await _fileAppService.GetAsync(name);
            return File(
                file.Bytes,
                MimeTypes.GetByExtension(Path.GetExtension(name))
            );
        }

        [HttpPost]
        public Task<FileUploadOutputDto> CreateAsync(FileUploadInputDto input)
        {
            return _fileAppService.CreateAsync(input);
        }


        [HttpPost]
        [Route("images/upload")]
        public async Task<JsonResult> UploadImage(IFormFile file)
        {
            //TODO: localize exception messages

            if (file == null)
            {
                throw new UserFriendlyException("没找到文件");
            }

            if (file.Length <= 0)
            {
                throw new UserFriendlyException("上传文件为空");
            }

            if (!file.ContentType.Contains("image"))
            {
                throw new UserFriendlyException("文件不是图片类型");
            }

            var output = await _fileAppService.CreateAsync(
                new FileUploadInputDto
                {
                    Bytes = file.GetAllBytes(),
                    Name = file.FileName
                }
            );

            return Json(new FileUploadResult(output.WebUrl));
        }

    }
}
