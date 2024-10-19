using cloud.core;
using cloud.core.mongodb;

namespace testapi.Models
{
    public class AdsMongoDbContext : BaseMongoObjectIdDbContext
    {
        public AdsMongoDbContext() : base(AppSettingsHelper.GetValueByKey("AdsMongoDbContext:ConnectionString"))
        {

        }

        public DbSetObjectId<UserTest> user_tests { get; set; }

    }
}
