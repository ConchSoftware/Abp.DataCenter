using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Abp.DataCenter.Excel
{
    public interface IExcelManager : IDomainService
    {
        List<dynamic> GetByDataList(byte[] input);

        Task<List<dynamic>> GetByDataListAsync(byte[] input, Guid configId);

        Task<ExportExcelOutput> GetByListDataAsync(List<object> data, Guid configId);
    }
}
