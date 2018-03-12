using System;
using DeckAlchemist.Support.Objects.Group;
using MongoDB.Driver;

namespace DeckAlchemist.Api.Sources.Group
{
    public class MongoGroupSource : IGroupSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "UserData";
        const string MongoCollection = "Groups";

        readonly IMongoDatabase database;
        readonly IMongoCollection<MongoGroup> collection;

        readonly FilterDefinitionBuilder<MongoGroup> _filter = Builders<MongoGroup>.Filter;

        public MongoGroupSource()
        {
            var client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoDatabase);
            collection = database.GetCollection<MongoGroup>(MongoCollection);
        }
    }
}
