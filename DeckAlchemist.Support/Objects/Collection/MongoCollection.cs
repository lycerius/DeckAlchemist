using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeckAlchemist.Support.Objects.Collection
{
    public class MongoCollection : ICollection
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public string CollectionId { get; set; }
        public List<IOwnedCard> OwnedCards { get; set; }
        public List<IBorrowedCard> BorrowedCards { get; set; }
    }
}
