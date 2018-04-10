using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Group
{
    public class MongoGroup : IGroup
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string GroupName { get; set; }
        public string Owner { get; set; }
        public List<string> Members { get; set; }
        public string GroupId { get; set; }

        public static MongoGroup FromGroup(IGroup group)
        {
            return new MongoGroup
            {
                GroupName = group.GroupName,
                Members = group.Members,
                Owner = group.Owner,
                GroupId = group.GroupId
            };
        }
    }
}