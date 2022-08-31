using Abp.DataCenter.Excel;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.DataCenter.EntityFrameworkCore;

[ConnectionStringName(DataCenterDbProperties.ConnectionStringName)]
public class DataCenterDbContext : AbpDbContext<DataCenterDbContext>, IDataCenterDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<ExcelUploadConfigMaster> ExcelUploadConfigMaster { get; set; }

    public DataCenterDbContext(DbContextOptions<DataCenterDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDataCenter();
    }
}
