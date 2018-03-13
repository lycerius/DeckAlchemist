<<<<<<< HEAD
﻿using System;
=======
﻿using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
>>>>>>> 853fd09c8ff411564306df816fab2c75416da9ce

namespace DeckAlchemist.Support.Objects.Collection
{
    public class MongoCollection : ICollection
    {
<<<<<<< HEAD
        public static MongoCollection FromCollection(ICollection collec)
        {
            return new MongoCollection
            {

            };
        }
=======
        [BsonId]
        public ObjectId _id { get; set; }
        public string UserId { get; set; }
        public string CollectionId { get; set; }
        public List<IOwnedCard> OwnedCards { get; set; }
        public List<IBorrowedCard> BorrowedCards { get; set; }
>>>>>>> 853fd09c8ff411564306df816fab2c75416da9ce
    }
}
