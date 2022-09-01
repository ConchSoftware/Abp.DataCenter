using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.DataCenter.Excel
{
    public class ExcelExportConfigMaster : AuditedAggregateRoot<Guid>
    {   
        /// <summary>
        /// 导出文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 工作簿名称
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// 是否显示行号
        /// </summary>
        public bool ShowRowNo { get; set; }

        /// <summary>
        /// 导出文件类型
        /// </summary>
        public ExcelExportConfigTypeEnum FileType { get; set; }

        public virtual ICollection<ExcelExportConfigItem> ExcelExportConfigItems { get; set; }

        public ExcelExportConfigMaster(Guid id, string fileName, string sheetName, bool showRowNo, ExcelExportConfigTypeEnum fileType)
        {
            this.Id = id;
            this.FileName = fileName;
            this.FileType = fileType;
            this.SheetName = sheetName;
            this.ShowRowNo = showRowNo;

            this.ExcelExportConfigItems = new List<ExcelExportConfigItem>();
        }

        public void AddItems(string fieldName, string columnName, ExcelColumnType type,
            int? width, int orderNo)
        {
            this.ExcelExportConfigItems.Add(new ExcelExportConfigItem(this.Id, fieldName, columnName, type, width, orderNo));
        }
    }
}
