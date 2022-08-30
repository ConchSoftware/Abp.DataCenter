using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.DataCenter.EntityFrameworkCore;

[ConnectionStringName(DataCenterDbProperties.ConnectionStringName)]
public interface IDataCenterDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
