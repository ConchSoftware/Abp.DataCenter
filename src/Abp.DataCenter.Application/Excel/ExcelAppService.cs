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

        public async Task<ListResultDto<dynamic>> ReadStreamAsync(byte[] input, Guid? configId = null)
        {
            var result = configId.HasValue ? 
                await _excelManager.ReadStreamAsync(input, configId.Value) : _excelManager.ReadStream(input);

            return new ListResultDto<dynamic>(result);
        }
    }
}
