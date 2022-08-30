using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Abp.DataCenter.MongoDB;

public static class DataCenterMongoDbContextExtensions
{
    public static void ConfigureDataCenter(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
