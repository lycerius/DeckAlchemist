using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace DeckAlchemist.Support.Objects.Messages
{
    public class MongoMessageBox : IMessageBox
    {
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public IEnumerable<IMessage> Messages { get; set; }
    }
}
