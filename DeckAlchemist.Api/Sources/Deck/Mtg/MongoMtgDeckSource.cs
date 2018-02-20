using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DeckAlchemist.Api.Objects.Deck;
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
        
        public void UpdateAllDecks(IEnumerable<IMtgDeck> externalDecks)
        {
            database.DropCollection(MongoCollection);
            EnsureCollectionExists(database);
            var mongoCards = externalDecks.Select(externalDeck => { var deck = MongoMtgDeck.FromMtgDeck(externalDeck); deck.id = CreateUniqueIdForDeck(deck); return deck; });
            collection.InsertMany(mongoCards);

            //TODO We can match based on a decks feature space
            //Save the vector array with the deck and then use the DeckCompare to compare the feature spaces
            /* We need a way to match an external deck to one that is already in the database. Name is not enough (because it can change and names can be duplicated, making it unable to be a PK)
            var existingDecks = FindDecksByName(externalDecks.Select(deck => deck.Name).ToList());
            var plan = CreateDeckUpdatePlan(existingDecks, externalDecks);
            collection.BulkWrite(plan);
            */
        }
        
        public string CreateUniqueIdForDeck(IMtgDeck deck)
        {
            var orderedCards = deck.Cards.Values.OrderBy(card => card.Name);
            var builder = new StringBuilder();
            foreach (var card in orderedCards)
                builder.Append(card.Name);
            var bytes = Encoding.UTF8.GetBytes(builder.ToString());
            var hasher = new SHA256Managed();
            var id = hasher.ComputeHash(bytes);
            return Convert.ToBase64String(id);
        }

        void EnsureCollectionExists(IMongoDatabase database)
        {
            var filter = new BsonDocument("name", MongoCollection);
            //filter by collection name
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            //check for existence
            var exists = collections.Any();

            if (!exists)
                database.CreateCollection(MongoCollection);
        }
    }
}
