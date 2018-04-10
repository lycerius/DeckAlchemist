using System;
using System.Collections.Generic;
using System.Linq;
using DeckAlchemist.Support.Objects.User;
using MongoDB.Bson;
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

        public void Init()
        {
            var filter = new BsonDocument("name", MongoCollection);
            //filter by collection name
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            //check for existence
            var exists = collections.Any();

            if (!exists)
                database.CreateCollection(MongoCollection);
        }

        public IUser Get(string userId)
        {
            var search = _filter.Eq("UserId", userId);
            var user = collection.Find(search).FirstOrDefault();
            return user;
        }

        public void Create(IUser user)
        {
            var mongoUser = MongoUser.FromUser(user);
            collection.InsertOne(mongoUser);
        }

        public void Update(IUser user)
        {
            var mongoUser = user as MongoUser;
            var query = _filter.Eq("UserId", mongoUser.UserId);
            collection.FindOneAndReplace(query, mongoUser);
        }

        public bool UserExists(string userId)
        {
            var query = _filter.Eq("UserId", userId);
            return collection.Find(query).Any();
        }

        public IDictionary<string, string> GetUserNamesByUserIds(string[] userIds)
        {
            var query = _filter.In("UserId", userIds);
            var users = collection.Find(query).ToList();
            var userDictionary = new Dictionary<string, string>();
            foreach(var user in users)
            {
                userDictionary.Add(user.UserId, user.UserName);
            }

            return userDictionary;
        }

        public IUser GetUserByUserName(string userName)
        {
            var query = _filter.Eq("UserName", userName);
            var result = collection.Find(query).FirstOrDefault();
            return result;
        }
    }
}
