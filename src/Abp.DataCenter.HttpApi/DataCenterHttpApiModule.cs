using Localization.Resources.AbpUi;
using Abp.DataCenter.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace Abp.DataCenter;

[DependsOn(
    typeof(DataCenterApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DataCenterHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DataCenterHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DataCenterResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
