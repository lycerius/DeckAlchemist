using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Cards;
using MongoDB.Driver;

namespace DeckAlchemist.Api.Sources.Cards.Mtg
{
    public class MongoMtgCardSource : IMtgCardSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "Mtg";
        const string MongoCollection = "Cards";

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
