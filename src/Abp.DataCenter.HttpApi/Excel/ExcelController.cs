using Abp.DataCenter.Excel.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        /// 导入文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload")]
        [Consumes("multipart/form-data")]
        public async Task<ListResultDto<object>> UploadAsync([FromForm]UploadExcelInput input)
        {
            await using var memoryStream = new MemoryStream();

            await input.File.CopyToAsync(memoryStream);

            var data = memoryStream.ToArray();
            var result = await _excelAppService.GetByDataListAsync(data, input.ConfigId);
            return result;
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("export")]
        public async Task<IActionResult> ExportAsync(ExportExcelInput input)
        {
            var list = JsonConvert.DeserializeObject<List<dynamic>>(input.Data);
            var result = await _excelAppService.GetByListDataAsync(list, input.ConfigId);
            return File(result.Content, result.MimeType, result.FileName);
        }
    }
}
