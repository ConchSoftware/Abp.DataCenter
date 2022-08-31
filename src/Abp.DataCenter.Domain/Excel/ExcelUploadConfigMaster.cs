using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Abp.DataCenter.Excel
{
    public class ExcelUploadConfigMaster : AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// Sheel Name
        /// </summary>
        public string SheelName { get; set; }

        /// <summary>
        /// Config Name
        /// </summary>
        public string ConfigName { get; set; }

        public virtual ICollection<ExcelUploadConfigItem> ExcelUploadConfigItems { get; set; }

        public ExcelUploadConfigMaster(Guid id, string configName, string sheelName)
        {
            Id = id;
            ConfigName = configName;
            SheelName = sheelName;
            ExcelUploadConfigItems = new List<ExcelUploadConfigItem>();
        }

        public void AddItems(string columnName, bool isRequired, int orderNo, string defaultValue = "")
        {
            this.ExcelUploadConfigItems.Add(new ExcelUploadConfigItem(this.Id, columnName, defaultValue, isRequired, orderNo,
                DateTime.Now));
        }
    }
}
