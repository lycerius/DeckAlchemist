using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeckAlchemist.Support.Objects.UserDeck
{
    public class MongoUserDeck : IUserDeck
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public string DeckId { get; set; }
        public string DeckName { get; set; }
        public IDictionary<string, int> CardsAndAmounts { get; set; } 

        public static MongoUserDeck FromUserDeck(IUserDeck userDeck)
        {
            return new MongoUserDeck
            {
                CardsAndAmounts = userDeck.CardsAndAmounts,
                DeckId = userDeck.DeckId,
                DeckName = userDeck.DeckName,
                UserId = userDeck.DeckId
            };
        }
    }
}
