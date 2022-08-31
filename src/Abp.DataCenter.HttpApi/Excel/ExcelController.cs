using Abp.DataCenter.Excel.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.DataCenter.Excel
{
    /// <summary>
    /// ExcelController
    /// </summary>
    [Route("api/DataCenter/excel")]
    public class ExcelController : DataCenterController
    {
        private readonly IExcelAppService _excelAppService;

        public ExcelController(IExcelAppService excelAppService)
        {
            _excelAppService = excelAppService;
        }

        /// <summary>
        /// Upload Excel File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ListResultDto<object>> UploadAsync([FromForm]UploadExcelInput input)
        {
            await using var memoryStream = new MemoryStream();

            await input.File.CopyToAsync(memoryStream);

            var data = memoryStream.ToArray();
            var result = await _excelAppService.ReadStreamAsync(data, input.ConfigId);
            return result;
        }

        [HttpPost]  
        [Route("export")]
        public async Task<ExportExcelOutput> ExportAsync(object data)
        {
            return default;
        }
    }
}
