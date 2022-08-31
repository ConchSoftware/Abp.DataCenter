using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Abp.DataCenter.Excel
{
    public class ExcelManager: DomainService, IExcelManager
    {
        private readonly IExcelUploadConfigRepository _excelUploadConfigRepository;

        public ExcelManager(IExcelUploadConfigRepository uploadConfig)
        {
            _excelUploadConfigRepository = uploadConfig;
        }

        /// <summary>
        /// 根据配置读取流到数据
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="configId"></param>
        /// <returns></returns>
        public async Task<List<dynamic>> ReadStreamAsync(byte[] file, Guid configId)
        {
            if (configId == null)
            {
                throw new UserFriendlyException("未读取到导入配置!");
            }
            if (file == null)
            {
                throw new UserFriendlyException("未读取到文件信息!");
            }

            MemoryStream ms = new MemoryStream(file);
            var configData = await _excelUploadConfigRepository.GetByIdAsync(configId);

            if (configData == null || configData.ExcelUploadConfigItems == null || configData.ExcelUploadConfigItems.Count < 1)
            {
                throw new UserFriendlyException("未找到导入配置!");
            }

            using var package = new ExcelPackage(ms);
            var worksheet = package.Workbook.Worksheets[configData.SheelName];
            Check.NotNull(worksheet, nameof(ExcelManager));

            var sc = worksheet.Dimension.Start.Column;
            var ec = worksheet.Dimension.End.Column;
            var sr = worksheet.Dimension.Start.Row + 1;
            var er = worksheet.Dimension.End.Row + 1;

            var result = new List<dynamic>();

            if (sr > 1 && er > 1)
            {
                for (int i = sr; i < er; i++)
                {
                    IDictionary<string, object> data = new ExpandoObject();
                    var list = configData.ExcelUploadConfigItems.OrderBy(c => c.OrderNo).ToList();
                    for (int ir = 0; ir < list.Count; ir++)
                    {
                        var value = worksheet.Cells[i, list[ir].OrderNo].Value?.ToString();
                        if (list[ir].IsRequired && value.IsNullOrWhiteSpace())
                        {
                            data = null;
                            break;
                        }
                        data.Add(list[ir].ColumnName, value == null ? list[ir].DefaultValue : value);
                    }
                    if (data != null)
                    {
                        result.Add(data);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 根据配置读取流到数据
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="configId"></param>
        /// <returns></returns>
        public List<dynamic> ReadStream(byte[] file)
        {
            Check.NotNull(file, nameof(ExcelManager));

            using var ms = new MemoryStream(file);
            using var package = new ExcelPackage(ms);
            var worksheet = package.Workbook.Worksheets["Sheet1"];
            Check.NotNull(worksheet, nameof(ExcelManager));
            var sc = worksheet.Dimension.Start.Column;
            var ec = worksheet.Dimension.End.Column;
            var sr = worksheet.Dimension.Start.Row + 1;
            var er = worksheet.Dimension.End.Row + 1;

            var result = new List<dynamic>();

            if (sr > 1 && er > 1)
            {
                for (int i = sr; i < er; i++)
                {
                    IDictionary<string, object> data = new ExpandoObject();
                    for (int ir = 1; ir < ec + 1; ir++)
                    {
                        var columnName = worksheet.Cells[1, ir].Value.ToString();
                        var value = worksheet.Cells[i, ir].Value.ToString();

                        data.Add(columnName, value);
                    }
                    result.Add(data);
                }
            }

            return result;
        }
    }
}
