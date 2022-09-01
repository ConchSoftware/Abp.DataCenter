using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.DataCenter.Excel
{
    public interface IExcelExportConfigRepository : IRepository<ExcelExportConfigMaster, Guid>
    {
        Task<ExcelExportConfigMaster> GetByIdAsync(Guid id);
    }
}
