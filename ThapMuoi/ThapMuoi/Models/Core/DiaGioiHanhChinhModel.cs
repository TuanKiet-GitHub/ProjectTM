using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace ThapMuoi.Models.Core
{
    public class DiaGioiHanhChinhModel : Audit,TEntity<string>
    {
        
        public  string IdDonViCha { get ; set;  }
        
        public  string Code { get; set; }
        
        public int Sort { get; set; }
        public int Level { get; set; }
        
       
    }

    public class DiaGioiHanhChinhShortModel 
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [JsonProperty(PropertyName = "_id")]
        public string Id { get; set; }
        public  string Code { get; set; }
        public string Name { get; set; }

    }
}
