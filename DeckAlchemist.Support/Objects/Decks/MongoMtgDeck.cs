using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DeckAlchemist.Support.Objects.Decks
{
    public class MongoMtgDeck : IMtgDeck
    {

        [BsonId]
        public ObjectId _id;
        public string Name { get; set; }
        public double Meta { get; set; }
        public IDictionary<string, IMtgDeckCard> Cards { get; set; }
        public string id { get; set; }

        public static MongoMtgDeck FromMtgDeck(IMtgDeck deck)
        {
            return new MongoMtgDeck
            {
                Name = deck.Name,
                Cards = deck.Cards,
                Meta = deck.Meta
            };
        }
       

        public override int GetHashCode()
        {
            var space = DeckCompare.FeatureSpaceFor(this);
            return space.GetHashCode();
        }
    }
}
