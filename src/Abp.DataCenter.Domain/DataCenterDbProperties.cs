namespace Abp.DataCenter;

public static class DataCenterDbProperties
{
    public static string DbTablePrefix { get; set; } = "DataCenter";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "DataCenter";
}
