using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace testapi.Models
{
    public class UserTest
    {
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public string name { get; set; }
        public string gender { get; set; }
        public int is_deleted { get; set; } = 0;

        public string IdAsString => Id.ToString();
    }
}