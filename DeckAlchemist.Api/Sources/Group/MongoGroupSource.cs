using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Group;
using DeckAlchemist.Support.Objects.User;
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

        public void AddUser(string groupName, string user)
        {
            var query = _filter.Eq("GroupId", groupName);
            var group = collection.Find(query).FirstOrDefault();
            if (group == null) return;
            group.Members.Add(user);
            collection.FindOneAndReplace(query, group);
        }

        public void RemoveUser(string groupName, string user)
        {
            var query = _filter.Eq("GroupId", groupName);
            var group = collection.Find(query).FirstOrDefault();
            if (group == null) return;
            group.Members.Remove(user);
            collection.FindOneAndReplace(query, group);
        }

        public void CreateGroup(IGroup group)
        {
            var mongoGroup = MongoGroup.FromGroup(group);
            collection.InsertOne(mongoGroup);

        }

        public IGroup GetGroupInfo(string groupId)
        {
            var query = _filter.Eq("GroupId", groupId);
            var group = collection.Find(query).FirstOrDefault();
            return group;
        }

        public IEnumerable<IGroup> GetGroups(IEnumerable<string> groupIds)
        {
            var query = _filter.In("GroupId", groupIds);
            var groups = collection.Find(query);
            return groups.ToList();
        }
    }
}
