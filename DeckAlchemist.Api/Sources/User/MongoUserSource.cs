using System;
using DeckAlchemist.Support.Objects.User;
using MongoDB.Driver;

namespace DeckAlchemist.Api.Sources.User
{
    public class MongoUserSource : IUserSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "UserData";
        const string MongoCollection = "Users";
        readonly IMongoDatabase database;
        readonly IMongoCollection<MongoUser> collection;
        readonly FilterDefinitionBuilder<MongoUser> _filter = Builders<MongoUser>.Filter;

        public MongoUserSource()
        {
            var client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoDatabase);
            collection = database.GetCollection<MongoUser>(MongoCollection);
        }
    }
}
