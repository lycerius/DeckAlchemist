using System;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Cards.Mtg;
using MongoDB.Driver;

namespace DeckAlchemist.Api.Sources.Cards.Mtg
{
    public class MongoMtgCardSource : IMTGCardSource
    {
        const string MongoConnectionString = "mongodb://localhost:27017";
        const string MongoDatabase = "Cards";
        const string MongoCollection = "Mtg";

        readonly IMongoCollection<MongoMtgCard> collection;
        readonly FilterDefinitionBuilder<MongoMtgCard> _filter = Builders<MongoMtgCard>.Filter;

        public MongoMtgCardSource()
        {
            var client = new MongoClient(MongoConnectionString);
            collection = client.GetDatabase(MongoDatabase).GetCollection<MongoMtgCard>(MongoCollection);
        }

        public IEnumerable<IMtgCard> GetCardsByNames(params string[] names)
        {
            var byNameFilter = _filter.In("Name", names);
            return collection.Find(byNameFilter).ToList();
        }
    }
}
