using Abp.DataCenter.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.DataCenter;

public abstract class DataCenterController : AbpControllerBase
{
    protected DataCenterController()
    {
        LocalizationResource = typeof(DataCenterResource);
    }
}
