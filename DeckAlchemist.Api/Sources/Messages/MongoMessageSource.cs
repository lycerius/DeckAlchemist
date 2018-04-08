using System;
using System.Collections.Generic;
using System.Linq;
using DeckAlchemist.Support.Objects.Messages;
using MongoDB.Driver;

namespace DeckAlchemist.Api.Sources.Messages
{
    public class MongoMessageSource : IMessageSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "UserData";
        const string MongoCollection = "Message";

        readonly IMongoDatabase database;
        readonly IMongoCollection<MongoMessageBox> collection;

        readonly FilterDefinitionBuilder<MongoMessageBox> _filter = Builders<MongoMessageBox>.Filter;

        public MongoMessageSource()
        {
            var client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoDatabase);
            collection = database.GetCollection<MongoMessageBox>(MongoCollection);
        }

        public void AcceptGroupInvite(string messageId)
        {
            throw new NotImplementedException();
        }

        public void AcceptLoanRequest(string messageId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMessage> GetMessagesForUser(string uId)
        {
            var query = _filter.Eq("UserId", uId);
            var messageBox = collection.Find(query).FirstOrDefault();
            if (messageBox != null) return messageBox.Messages.Values;
            return null;
        }

        public void SendMessage(IMessage message)
        {
            var boxQuery = _filter.Eq("UserId", message.RecipientId);
            var box = collection.Find(boxQuery).FirstOrDefault();
            if (box == null) return;
            message.MessageId = Guid.NewGuid().ToString();
            box.Messages.Add(message.MessageId, message);
            collection.FindOneAndReplace(boxQuery, box);
        }

        public void SetMessageRead(string messageId, bool read)
        {
            throw new NotImplementedException();
        }

        public void Create(IMessageBox box)
        {
            collection.InsertOne(MongoMessageBox.FromMessageBox(box));
        }

        public void Update(IMessage message)
        {
            var messageBoxQuery = _filter.Eq("UserId", message.RecipientId);
            var messageBox = collection.Find(messageBoxQuery).FirstOrDefault();
            

        }

        public bool ExistsForUser(string uId)
        {
            var query = _filter.Eq("UserId", uId);
            return collection.Find(query).FirstOrDefault() != null;
        }

        public IMessage GetMessageById(string userId, string messageId)
        {
            var userBoxQuery = _filter.Eq("UserId", userId);
            return collection.Find(userBoxQuery).FirstOrDefault()?.Messages[messageId];
        }

        public void Update(string userId, IMessage message)
        {
            var boxQuery = _filter.Eq("UserId", userId);
            var box = collection.Find(boxQuery).FirstOrDefault();
            if (box == null) return;
            box.Messages[message.MessageId] = message;
            collection.FindOneAndReplace(boxQuery, box);
        }
    }
}
