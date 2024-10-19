using cloud.core.mongodb;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace testapi.Models
{
    public class UserTest : AbstractEntityObjectIdTracking
    {
        public string name { get; set; } 
        public string gender { get; set; } 
        public int is_deleted { get; set; } = 0;

       /* public long LastUpdated { get; set; }*/

        public string IdAsString => Id.ToString();
    }
}
