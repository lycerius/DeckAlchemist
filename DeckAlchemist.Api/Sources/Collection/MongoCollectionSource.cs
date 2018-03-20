using System;
using DeckAlchemist.Support.Objects.Collection;
using DeckAlchemist.Support.Objects.User;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;


namespace DeckAlchemist.Api.Sources.Collection
{
    public class MongoCollectionSource : ICollectionSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "UserData";
        const string collectionName = "Collection";

        readonly IMongoDatabase database;
        readonly IMongoCollection<MongoCollection> collection;

        readonly FilterDefinitionBuilder<MongoCollection> _filter = Builders<MongoCollection>.Filter;

        public MongoCollectionSource()
        {
            var client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoDatabase);
            collection = database.GetCollection<MongoCollection>(collectionName);
        }
        public void Init()
        {
            var filter = new BsonDocument("name", collectionName);
            //filter by collection name
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            //check for existence
            var exists = collections.Any();

            if (!exists)
                database.CreateCollection(collectionName);    
        }

        public void Create(ICollection collec)
        {
            var mongoCollection = MongoCollection.FromCollection(collec);
            collection.InsertOne(mongoCollection);
        }

        public void Update(ICollection collec)
        {
            var mongoCollection = collec as MongoCollection;
            var query = _filter.Eq("CollectionId", mongoCollection.CollectionId);
            collection.FindOneAndReplace(query, mongoCollection);
        }
        public ICollection GetCollection(string uId){
            throw new NotImplementedException();
        }

        public bool AddCardToCollection(string uId, IEnumerable<string> cardName)
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

        public bool ExistsForUser(string userId)
        {
            var query = _filter.Eq("UserId", userId);
            return collection.Find(query).Any();
        }

        public bool AddCardToCollection(string uId, IList<string> cardName)
        {
            throw new NotImplementedException();
        }
    }
}
