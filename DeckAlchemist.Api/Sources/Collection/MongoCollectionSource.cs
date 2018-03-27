using System;
using DeckAlchemist.Support.Objects.Collection;
using DeckAlchemist.Support.Objects.User;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;


namespace DeckAlchemist.Api.Sources.Collection
{
    public class MongoCollectionSource : ICollectionSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "UserData";
        const string collectionName = "Collection";

        readonly IMongoDatabase database;
        readonly IMongoCollection<MongoCollection> collection;

        readonly FilterDefinitionBuilder<MongoCollection> _filter = Builders<MongoCollection>.Filter;

        public MongoCollectionSource()
        {
            var client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoDatabase);
            collection = database.GetCollection<MongoCollection>(collectionName);
        }
        public void Init()
        {
            var filter = new BsonDocument("name", collectionName);
            //filter by collection name
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            //check for existence
            var exists = collections.Any();

            if (!exists)
                database.CreateCollection(collectionName);    
        }

        public void Create(ICollection collec)
        {
            var mongoCollection = MongoCollection.FromCollection(collec);
            collection.InsertOne(mongoCollection);
        }

        public void Update(ICollection collec)
        {
            var mongoCollection = collec as MongoCollection;
            var query = _filter.Eq("CollectionId", mongoCollection.CollectionId);
            collection.FindOneAndReplace(query, mongoCollection);
        }
        public ICollection GetCollection(string uId){
            var query = _filter.Eq("UserId", uId);
            var userCollection = collection.Find(query).FirstOrDefault();
            return userCollection;
        }

        public bool AddCardToCollection(string uId, IEnumerable<string> cardName)
        {
            var query = _filter.Eq("UserId", uId);
            var userCollection = collection.Find(query).FirstOrDefault();
            if (userCollection == null) return false;
            foreach(var card in cardName)
            {
                if(userCollection.OwnedCards.ContainsKey(card))
                {
                    userCollection.OwnedCards[card].TotalAmount++;
                }
                else
                {
                    userCollection.OwnedCards[card] = new OwnedCard
                    {
                        Available = 1,
                        CardId = card,
                        InDecks = new Dictionary<string, int>(),
                        LentTo = new Dictionary<string, int>(),
                        TotalAmount = 1
                    };
                }
            }
            collection.FindOneAndReplace(query, userCollection);
            return true;
        }

        public bool RemoveCardFromCollection(string uId, IEnumerable<string> cardName)
        {
            var query = _filter.Eq("UserId", uId);
            var userCollection = collection.Find(query).FirstOrDefault();
            if (userCollection == null) return false;
            foreach (var card in cardName)
            {
                if (userCollection.OwnedCards.ContainsKey(card))
                {
                    userCollection.OwnedCards[card].TotalAmount--;
                    if(userCollection.OwnedCards[card].TotalAmount == 0)
                    {
                        userCollection.OwnedCards.Remove(card);
                    }
                }
            }
            collection.FindOneAndReplace(query, userCollection);
            return true;
        }

        public bool MarkCardAsLent(string lender, string lendee, IDictionary<string, int> namesAndAmounts)
        {
            var query = _filter.Eq("UserId", lender);
            var userCollection = collection.Find(query).FirstOrDefault();
            if (userCollection == null) return false;
            foreach(var nameAndAmount in namesAndAmounts)
            {
                var cardName = nameAndAmount.Key;
                var amount = nameAndAmount.Value;
                if (!userCollection.OwnedCards.ContainsKey(cardName)) return false;

                var card = userCollection.OwnedCards[cardName];
                if (card.TotalAmount < amount) return false;

                card.LentTo[lendee] = amount;
                card.TotalAmount -= amount;
                collection.FindOneAndReplace(query, userCollection);
            }
            return true;

        }

        public bool AddCardAsLent(string lender, string lendee, IDictionary<string, int> namesAndAmounts)
        {
            var query = _filter.Eq("UserId", lendee);
            var userCollection = collection.Find(query).FirstOrDefault();
            if (userCollection == null) return false;

            foreach(var nameAndAmount in namesAndAmounts)
            {
                var amount = nameAndAmount.Value;
                var cardName = nameAndAmount.Key;

                //Card borrowed before
                if (userCollection.BorrowedCards.ContainsKey(cardName))
                {
                    var borrowedEntry = userCollection.BorrowedCards[cardName];
                    //Card borrowed by lender before
                    if (borrowedEntry.ContainsKey(lender))
                        borrowedEntry[lender].AmountBorrowed += amount;
                    else //Card borrowed by new lender
                        borrowedEntry[lender] = new BorrowedCard
                        {
                            AmountBorrowed = amount,
                            CardId = cardName,
                            Lender = lender
                        };

                }
                else //Card not borrowed before
                {
                    userCollection.BorrowedCards[cardName] = new Dictionary<string, IBorrowedCard>
                    {
                        {
                            lender,
                            new BorrowedCard
                            {
                                AmountBorrowed = amount,
                                CardId = cardName,
                                Lender = lender
                            }
                        }
                    };
                }
                collection.FindOneAndReplace(query, userCollection);
            }
            return true;
        }

        public bool ExistsForUser(string userId)
        {
            var query = _filter.Eq("UserId", userId);
            return collection.Find(query).Any();
        }

    }
}
