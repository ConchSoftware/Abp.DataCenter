using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.DataCenter.Excel
{
    public interface IExcelAppService : IApplicationService
    {
        Task<ListResultDto<object>> GetByDataListAsync(byte[] input, Guid? configId = null);

        Task<ExportExcelOutput> GetByListDataAsync(List<dynamic> data, Guid configId);
    }
}
