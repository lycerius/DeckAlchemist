using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace DeckAlchemist.Support.Objects.Messages
{
    public class MongoMessageBox : IMessageBox
    {
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public IList<IMessage> Messages { get; set; }

        public static MongoMessageBox FromMessageBox(IMessageBox box)
        {
            return new MongoMessageBox
            {
                Messages = box.Messages,
                UserId = box.UserId
            };
        }
    }
}
