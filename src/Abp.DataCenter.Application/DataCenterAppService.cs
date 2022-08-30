using Abp.DataCenter.Localization;
using Volo.Abp.Application.Services;

namespace Abp.DataCenter;

public abstract class DataCenterAppService : ApplicationService
{
    protected DataCenterAppService()
    {
        LocalizationResource = typeof(DataCenterResource);
        ObjectMapperContext = typeof(DataCenterApplicationModule);
    }
}
