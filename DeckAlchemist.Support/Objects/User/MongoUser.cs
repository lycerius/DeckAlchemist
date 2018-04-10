using System;
﻿using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace DeckAlchemist.Support.Objects.User
{
    public class MongoUser : IUser
    {
        public static MongoUser FromUser(IUser user)
        {
            return new MongoUser
            {
                CollectionId = user.CollectionId,
                Decks = user.Decks,
                Email = user.Email,
                Groups = user.Groups,
                UserId = user.UserId,
                UserName = user.UserName
            };
        }

        [BsonId]
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string CollectionId { get; set; }
        public List<string> Groups { get; set; }
        public List<string> Decks { get; set; }
    }
}
