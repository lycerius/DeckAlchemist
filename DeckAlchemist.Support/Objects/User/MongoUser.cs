<<<<<<< HEAD
﻿using System;
=======
﻿using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
>>>>>>> 853fd09c8ff411564306df816fab2c75416da9ce

namespace DeckAlchemist.Support.Objects.User
{
    public class MongoUser : IUser
    {
<<<<<<< HEAD
        public static MongoUser FromUser(IUser user)
        {
            return new MongoUser
            {

            };
        }
=======
        [BsonId]
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string CollectionId { get; set; }
        public List<string> Groups { get; set; }
        public List<string> Decks { get; set; }
>>>>>>> 853fd09c8ff411564306df816fab2c75416da9ce
    }
}
