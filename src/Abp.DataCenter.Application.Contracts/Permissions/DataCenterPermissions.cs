using Volo.Abp.Reflection;

namespace Abp.DataCenter.Permissions;

public class DataCenterPermissions
{
    public const string GroupName = "DataCenter";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DataCenterPermissions));
    }
}
