using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Abp.DataCenter;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DataCenterDomainSharedModule)
)]
public class DataCenterDomainModule : AbpModule
{

}
