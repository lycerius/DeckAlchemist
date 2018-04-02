using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Decks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeckAlchemist.Api.Sources.Deck.Mtg
{
    public class MongoMtgDeckSource : IMtgDeckSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "Mtg";
        const string MongoCollection = "Decks";

        readonly IMongoCollection<MongoMtgDeck> collection;
        readonly IMongoDatabase database;
        readonly FilterDefinitionBuilder<MongoMtgDeck> _filter = Builders<MongoMtgDeck>.Filter;

        public MongoMtgDeckSource()
        {
            var client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoDatabase);
            collection = database.GetCollection<MongoMtgDeck>(MongoCollection);
        }

        public IEnumerable<IMtgDeck> GetAllDecks()
        {
            return collection.Find(_filter.Empty).ToList();
        }

        public IMtgDeck GetDeckByName(string name)
        {
            var query = _filter.Eq("Name", name);
            return collection.Find(query).FirstOrDefault();
        }

        public IEnumerable<IMtgDeck> SearchDecks(string name)
        {
            var query = _filter.Regex("Name", new BsonRegularExpression($".*{name}.*", "i"));
            return collection.Find(query).ToList();
        }

        public IMtgDeck GetById(string deckID)
        {
            var query = _filter.Eq("DeckID", deckID);
            return collection.Find(query).First();
        }

        public IMtgDeck GetByName(string deckName)
        {
            var query = _filter.Eq("Name", deckName);
            return collection.Find(query).First();
        }
    }
}
