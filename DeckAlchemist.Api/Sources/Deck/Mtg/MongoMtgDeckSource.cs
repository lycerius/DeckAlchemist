using System;
using DeckAlchemist.Api.Objects.Deck;
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
    }
}
