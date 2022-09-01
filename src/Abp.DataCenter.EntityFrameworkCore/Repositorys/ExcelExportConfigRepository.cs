using Abp.DataCenter.EntityFrameworkCore;
using Abp.DataCenter.Excel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.DataCenter.Repositorys
{
    public class ExcelExportConfigRepository : EfCoreRepository<DataCenterDbContext, ExcelExportConfigMaster, Guid>, IExcelExportConfigRepository
    {
        public ExcelExportConfigRepository(IDbContextProvider<DataCenterDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<ExcelExportConfigMaster> GetByIdAsync(Guid id)
        {
            var dbContext = await GetDbContextAsync();
            return dbContext.Set<ExcelExportConfigMaster>()
                .Include(c => c.ExcelExportConfigItems)
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
