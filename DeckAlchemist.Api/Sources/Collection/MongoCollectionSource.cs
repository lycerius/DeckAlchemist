using System;
using DeckAlchemist.Support.Objects.Collection;
using MongoDB.Driver;
using System.Collections.Generic;


namespace DeckAlchemist.Api.Sources.Collection
{
    public class MongoCollectionSource : ICollectionSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "UserData";
        const string MongoCollection = "Collection";

        readonly IMongoDatabase database;
        readonly IMongoCollection<MongoCollection> collection;

        readonly FilterDefinitionBuilder<MongoCollection> _filter = Builders<MongoCollection>.Filter;

        public MongoCollectionSource()
        {
            var client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoDatabase);
            collection = database.GetCollection<MongoCollection>(MongoCollection);
        }
        public bool AddCardToCollection(string uId, IList<string> cardName)
        {
            throw new NotImplementedException();
        }
        public bool RemoveCardFromCollection(string uId, IList<string> cardName){
            throw new NotImplementedException();
        }
        public bool MarkCardAsLent(string uId, IList<string> cardName)
        {
            throw new NotImplementedException();
        }
        public bool AddCardAsLent(string uId, IList<string> cardName)
        {
            throw new NotImplementedException();
        }
    }
}
