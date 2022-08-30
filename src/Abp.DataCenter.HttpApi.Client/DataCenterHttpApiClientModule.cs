using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Abp.DataCenter;

[DependsOn(
    typeof(DataCenterApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DataCenterHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DataCenterApplicationContractsModule).Assembly,
            DataCenterRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DataCenterHttpApiClientModule>();
        });

    }
}
