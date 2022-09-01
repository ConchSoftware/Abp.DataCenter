using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Abp.DataCenter.Excel
{
    public class ExcelAppService : DataCenterAppService, IExcelAppService
    {
        private readonly IExcelManager _excelManager;

        public ExcelAppService(IExcelManager excelManager)
        {
            _excelManager = excelManager;
        }

        public async Task<ExportExcelOutput> GetByListDataAsync(List<dynamic> data, Guid configId)
        {
            return await _excelManager.GetByListDataAsync(data, configId);
        }

        public async Task<ListResultDto<dynamic>> GetByDataListAsync(byte[] input, Guid? configId = null)
        {
            var result = configId.HasValue ? 
                await _excelManager.GetByDataListAsync(input, configId.Value) : _excelManager.GetByDataList(input);

            return new ListResultDto<dynamic>(result);
        }
    }
}
