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
<<<<<<< HEAD

        public void Initialize()
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
=======
        public bool UserExists(string recivingUser)
        {
            throw new NotImplementedException();
        }

        public string GetUIDByName(string recivingUser){
            throw new NotImplementedException();
>>>>>>> 853fd09c8ff411564306df816fab2c75416da9ce
        }
    }
}
