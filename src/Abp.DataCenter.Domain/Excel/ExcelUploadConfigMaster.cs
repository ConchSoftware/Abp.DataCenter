using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.DataCenter.Excel
{
    public class ExcelUploadConfigMaster : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// SheetName
        /// </summary>
        public string SheetName { get; set; }

        /// <summary>
        /// Config Name
        /// </summary>
        public string ConfigName { get; set; }

        public virtual ICollection<ExcelUploadConfigItem> ExcelUploadConfigItems { get; set; }

        public ExcelUploadConfigMaster(Guid id, string configName, string sheetName)
        {
            Id = id;
            ConfigName = configName;
            SheetName = sheetName;
            ExcelUploadConfigItems = new List<ExcelUploadConfigItem>();
        }

        public void AddItems(string columnName, bool isRequired, int orderNo, string defaultValue = "")
        {
            this.ExcelUploadConfigItems.Add(new ExcelUploadConfigItem(this.Id, columnName, defaultValue, isRequired, orderNo));
        }
    }
}
