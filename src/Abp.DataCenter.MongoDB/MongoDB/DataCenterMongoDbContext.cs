using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Abp.DataCenter.MongoDB;

[ConnectionStringName(DataCenterDbProperties.ConnectionStringName)]
public class DataCenterMongoDbContext : AbpMongoDbContext, IDataCenterMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureDataCenter();
    }
}
