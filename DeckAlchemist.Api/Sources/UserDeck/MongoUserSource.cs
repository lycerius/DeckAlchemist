using System;
using DeckAlchemist.Support.Objects.User;
using MongoDB.Driver;

namespace DeckAlchemist.Api.Sources.UserDeck
{
    public class MongoUserDeckSource:IUserDeckSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "UserData";
        const string MongoCollection = "UserDecks";
        readonly IMongoDatabase database;
        readonly IMongoCollection<MongoUser> collection;
        readonly FilterDefinitionBuilder<MongoUser> _filter = Builders<MongoUser>.Filter;

        public MongoUserDeckSource()
        {
            var client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoDatabase);
            collection = database.GetCollection<MongoUser>(MongoCollection);
        }
    }
}
