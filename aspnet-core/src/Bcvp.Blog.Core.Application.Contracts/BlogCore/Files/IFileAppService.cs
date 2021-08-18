using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Bcvp.Blog.Core.BlogCore.Files
{
    public interface IFileAppService : IApplicationService
    {
        Task<RawFileDto> GetAsync(string name);

        Task<FileUploadOutputDto> CreateAsync(FileUploadInputDto input);
    }
}
