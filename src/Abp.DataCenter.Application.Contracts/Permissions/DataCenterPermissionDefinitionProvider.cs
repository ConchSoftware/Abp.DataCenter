using Abp.DataCenter.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.DataCenter.Permissions;

public class DataCenterPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DataCenterPermissions.GroupName, L("Permission:DataCenter"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DataCenterResource>(name);
    }
}
