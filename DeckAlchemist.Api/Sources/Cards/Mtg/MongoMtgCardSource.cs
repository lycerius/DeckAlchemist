using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Cards;
using MongoDB.Bson;
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

        public IEnumerable<string> CheckExistance(IList<string> cardNames)
        {
            var byNameFilter = _filter.In("Name", cardNames);
            var cards = collection.Find(byNameFilter).ToList();
            var cardSet = new HashSet<string>();
            var notFound = new List<string>();
            foreach (var card in cards) cardSet.Add(card.Name);
            foreach (var card in cardNames) if (!cardSet.Contains(card)) notFound.Add(card);
            return notFound;
        }

        public IEnumerable<IMtgCard> SearchByName(string byName)
        {
            var searchQuery = _filter.Regex("Name", new BsonRegularExpression($".*{byName}.*", "i"));
            return collection.Find(searchQuery).SortBy(card => card.Name).Limit(50).ToList();
        }
    }
}
