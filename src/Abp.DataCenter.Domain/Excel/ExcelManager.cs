using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
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
        private readonly IExcelExportConfigRepository _excelExportConfigRepository;

        public ExcelManager(IExcelUploadConfigRepository uploadConfig, IExcelExportConfigRepository excelExportConfigRepository)
        {
            _excelUploadConfigRepository = uploadConfig;
            _excelExportConfigRepository = excelExportConfigRepository;
        }

        /// <summary>
        /// 根据配置读取流到数据
        /// </summary>
        /// <param name="memoryStream"></param>
        /// <param name="configId"></param>
        /// <returns></returns>
        public async Task<List<dynamic>> GetByDataListAsync(byte[] file, Guid configId)
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
            var worksheet = package.Workbook.Worksheets[configData.SheetName];
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
        public List<dynamic> GetByDataList(byte[] file)
        {
            Check.NotNull(file, nameof(ExcelManager));

            using var ms = new MemoryStream(file);
            using var package = new ExcelPackage(ms);
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
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

        public async Task<ExportExcelOutput> GetByListDataAsync(List<object> data, Guid? configId)
        {
            var configdata = new ExcelExportConfigMaster(GuidGenerator.Create(), "员工信息导出", "员工信息", true, ExcelExportConfigTypeEnum.xlsx);
            configdata.AddItems("name", "员工姓名", ExcelColumnType.String, 120, 1);
            configdata.AddItems("age", "员工年龄", ExcelColumnType.String, 60, 2);
            configdata.AddItems("area", "个人爱好", ExcelColumnType.String, 200, 3);
            configdata = await _excelExportConfigRepository.InsertAsync(configdata);

            var exportData = ListToDataTable(data);
            var config = await _excelExportConfigRepository.GetByIdAsync(configId ?? configdata.Id);
            var result = ExportDataTableExcel(exportData, config);
            return new ExportExcelOutput()
            {
                Content = result,
                FileName = config.FileName,
                MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataTable">数据源</param>
        /// <param name="heading">工作簿Worksheet</param>
        /// <param name="showSrNo">//是否显示行编号</param>
        /// <param name="config">要导出的列</param>
        /// <returns></returns>
        private byte[] ExportDataTableExcel(DataTable dataTable, ExcelExportConfigMaster config = null)
        {
            byte[] result;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add($"{config.SheetName}");
                int startRowFrom = 1;  // 开始的行
                // 是否显示行编号
                if (config.ShowRowNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);
                int columnIndex = 1;
                foreach (DataColumn item in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    int maxLength = columnCells.Max(cell => cell.Value != null ? cell.Value.ToString().Count() : 0);
                    if (maxLength < 150)
                    {
                        workSheet.Column(columnIndex).AutoFit();
                    }
                    columnIndex++;
                }

                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#708090"));
                }

                if (dataTable.Rows.Count > 0)
                {
                    // format cells - add borders 
                    using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                    {
                        r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                        r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                    }
                }

                if (config != null)
                {
                    var list = new List<int>();
                    for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                    {
                        if (i == 0 && config.ShowRowNo)
                        {
                            continue;
                        }
                        if (!config.ExcelExportConfigItems.Any(c => c.ColumnName.ToUpper() == dataTable.Columns[i].ColumnName.ToUpper()))
                        {
                            workSheet.DeleteColumn(i + 1);
                        }
                        else
                        {
                            // 设置单元格别名
                            var data = config.ExcelExportConfigItems.FirstOrDefault(c => c.FieldName.ToUpper() == dataTable.Columns[i].ColumnName.ToUpper());
                            workSheet.Cells[1, i + 1].Value = data.ColumnName ?? data.FieldName;
                            if (data.Type == ExcelColumnType.Datetime)
                            {
                                workSheet.Column(i + 1).Style.Numberformat.Format = "yyyy-MM-dd";
                            }
                            if (data.Width.HasValue)
                            {
                                workSheet.Column(i + 1).Width = data.Width.Value;
                            }
                        }
                    }
                }

                result = package.GetAsByteArray();
            }
            return result;
        }

        /// <summary>
        /// List转换成DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private DataTable ListToDataTable<T>(IList<T> list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                T ss = list[0];
                PropertyInfo[] propertys = ss.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    //获取类型
                    Type colType = pi.PropertyType;
                    //当类型为Nullable<>时
                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }
                    result.Columns.Add(pi.Name, colType);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }
    }
}
