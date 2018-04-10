using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.UserDeck;
using MongoDB.Driver;

namespace DeckAlchemist.Api.Sources.UserDeck
{
    public class MongoUserDeckSource : IUserDeckSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "UserData";
        const string MongoCollection = "UserDecks";
        readonly IMongoDatabase database;
        readonly IMongoCollection<MongoUserDeck> collection;
        readonly FilterDefinitionBuilder<MongoUserDeck> _filter = Builders<MongoUserDeck>.Filter;

        public MongoUserDeckSource()
        {
            var client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoDatabase);
            collection = database.GetCollection<MongoUserDeck>(MongoCollection);
        }

        public bool DeleteDeck(string uId, string deckName)
        {
            var query = _filter.Eq("UserId", uId);
            collection.DeleteOne(query);
            return true;
        }

        public bool AddCardToDeck(string uId, string deckName, string cardName)
        {
            var query = _filter.And(_filter.Eq("UserId", uId), _filter.Eq("DeckName", deckName));
            var userDeck = collection.Find(query).FirstOrDefault();

            if(userDeck.CardsAndAmounts.ContainsKey(cardName))
            {
                userDeck.CardsAndAmounts[cardName]++;
            }
            else
            {
                userDeck.CardsAndAmounts[cardName] = 1;
            }

            collection.FindOneAndReplace(query, userDeck);
            return true;

        }

        public bool RemoveCardFromDeck(string uId, string deckName, string cardName)
        {
            var query = _filter.And(_filter.Eq("UserId", uId), _filter.Eq("DeckName", deckName));
            var userDeck = collection.Find(query).FirstOrDefault();
            if (userDeck == null) return false;

            if (userDeck.CardsAndAmounts.ContainsKey(cardName))
            {
                userDeck.CardsAndAmounts[cardName]--;
                if (userDeck.CardsAndAmounts[cardName] <= 0)
                    userDeck.CardsAndAmounts.Remove(cardName);
            }

            collection.FindOneAndReplace(query, userDeck);
            return true;
        }

        public IUserDeck GetDeckByName(string uId, string deckName)
        {
            var query = _filter.And(_filter.Eq("UserId", uId), _filter.Eq("DeckName", deckName));
            return collection.Find(query).FirstOrDefault();
        }

        public bool CreateDeck(string uId, string deckName)
        {
            var newDeck = new MongoUserDeck
            {
                CardsAndAmounts = new Dictionary<string, int>(),
                DeckId = Guid.NewGuid().ToString(),
                DeckName = deckName,
                UserId = uId
            };

            collection.InsertOne(newDeck);
            return true;
        }

        public IEnumerable<IUserDeck> GetAll(string userId)
        {
            var query = _filter.Eq("UserId", userId);
            return collection.Find(query).ToList();
        }
    }
}
