using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace Abp.DataCenter.Excel
{
    public class ExcelUploadConfigItem : Entity<int>, IAuditedObject
    {
        public Guid ConfigId { get; set; }

        /// <summary>
        /// Excel列名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 如果为空的默认值
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// 是否必填，如果为空则跳过导入行
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNo { get; set; }

        public DateTime CreationTime { get; set; }

        public Guid? CreatorId { get; set; }

        public Guid? LastModifierId { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public ExcelUploadConfigItem(Guid configId, string columnName, string defaultValue, bool isRequired, int orderNo, 
            DateTime creationTime, Guid? creatorId = null)
        {
            ConfigId = configId;
            ColumnName = columnName;
            DefaultValue = defaultValue;
            IsRequired = isRequired;
            OrderNo = orderNo;
            CreationTime = creationTime;
            CreatorId = creatorId;
        }
    }
}
