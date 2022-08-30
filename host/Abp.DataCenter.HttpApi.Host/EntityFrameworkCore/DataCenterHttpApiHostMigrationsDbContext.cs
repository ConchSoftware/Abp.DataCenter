using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.DataCenter.EntityFrameworkCore;

public class DataCenterHttpApiHostMigrationsDbContext : AbpDbContext<DataCenterHttpApiHostMigrationsDbContext>
{
    public DataCenterHttpApiHostMigrationsDbContext(DbContextOptions<DataCenterHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDataCenter();
    }
}
