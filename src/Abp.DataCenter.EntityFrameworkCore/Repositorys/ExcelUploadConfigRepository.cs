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
    public class ExcelUploadConfigRepository : EfCoreRepository<DataCenterDbContext, ExcelUploadConfigMaster, Guid>, IExcelUploadConfigRepository
    {
        public ExcelUploadConfigRepository(IDbContextProvider<DataCenterDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<ExcelUploadConfigMaster> GetByIdAsync(Guid id)
        {
            var dbContext = await GetDbContextAsync();
            return dbContext.Set<ExcelUploadConfigMaster>()
                .Include(c=> c.ExcelUploadConfigItems)
                .FirstOrDefault(c => c.Id == id);
        }
    }
}
