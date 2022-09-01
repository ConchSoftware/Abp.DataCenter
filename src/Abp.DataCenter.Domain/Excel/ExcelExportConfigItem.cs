using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Abp.DataCenter.Excel
{
    public class ExcelExportConfigItem : Entity<int>, IAuditedObject
    {
        public Guid ConfigId { get; set; }

        public string FieldName { get; set; }

        public string ColumnName { get; set; }

        public ExcelColumnType Type { get; set; }

        public int? Width { get; set; }

        public int OrderNo { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        public Guid? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public ExcelExportConfigItem(Guid configId, string fieldName, string columnName, ExcelColumnType type, 
            int? width, int orderNo, Guid? creatorId = null)
        {
            ConfigId = configId;
            FieldName = fieldName;
            ColumnName = columnName;
            Type = type;
            Width = width;
            OrderNo = orderNo;
            CreationTime = DateTime.Now;
            CreatorId = creatorId;
        }
    }
}
