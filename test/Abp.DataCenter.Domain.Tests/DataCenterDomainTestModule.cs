using Abp.DataCenter.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.DataCenter;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(DataCenterEntityFrameworkCoreTestModule)
    )]
public class DataCenterDomainTestModule : AbpModule
{

}
