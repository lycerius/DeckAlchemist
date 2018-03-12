using System.Collections.Generic;
using MongoDB.Bson;

namespace DeckAlchemist.Support.Objects.Collection
{
    public class MongoCollection : ICollection
    {
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public List<IOwnedCard> OwnedCards { get; set; }
        public List<IBorrowedCard> BorrowedCards { get; set; }
    }
}
