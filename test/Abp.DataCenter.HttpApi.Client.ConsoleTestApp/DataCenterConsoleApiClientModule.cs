using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Abp.DataCenter;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DataCenterHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class DataCenterConsoleApiClientModule : AbpModule
{

}
