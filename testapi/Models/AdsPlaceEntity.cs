using cloud.core.mongodb;

namespace testapi.Models
{
    public class AdsPlaceEntity : AbstractEntityObjectIdTracking
    {
        public MongoDB.Bson.ObjectId UserOwnerId { get; set; }
        public string PlaceName { get; set; }
        public string PlaceAddress { get; set; }

        public long LastUpdated { get; set; }
    }
}
