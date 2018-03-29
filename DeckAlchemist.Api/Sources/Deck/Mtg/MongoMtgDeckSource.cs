using System;
using DeckAlchemist.Support.Objects.Decks;
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
