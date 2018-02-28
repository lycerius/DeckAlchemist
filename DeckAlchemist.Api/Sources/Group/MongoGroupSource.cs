using System;
<<<<<<< HEAD
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Group;
using DeckAlchemist.Api.Objects.User;
=======
using DeckAlchemist.Support.Objects.Group;
>>>>>>> devel
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
        public IEnumerable<IUser> GetAllUsers(string groupName){
            var byNameFilter = _filter.In("name", groupName);
            IList<MongoGroup> groupobjs = collection.Find(byNameFilter).ToList();
            return groupobjs[0].members;
        }
        public void AddUser(string groupName, string user){
            
        }
        public void RemoveUser(string groupName, string user){

        }
    }
}
