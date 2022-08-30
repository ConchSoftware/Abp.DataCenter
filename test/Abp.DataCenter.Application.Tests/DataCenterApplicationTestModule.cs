using Volo.Abp.Modularity;

namespace Abp.DataCenter;

[DependsOn(
    typeof(DataCenterApplicationModule),
    typeof(DataCenterDomainTestModule)
    )]
public class DataCenterApplicationTestModule : AbpModule
{

}
