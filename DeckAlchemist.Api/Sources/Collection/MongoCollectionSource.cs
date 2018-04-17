using System;
using DeckAlchemist.Support.Objects.Collection;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Microsoft.Extensions.Logging.Console;

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

            if (userCollection.OwnedCards == null)
            {
                userCollection.OwnedCards = new Dictionary<string, IOwnedCard>();
            }

            if (userCollection.BorrowedCards == null)
            {
                userCollection.BorrowedCards = new Dictionary<string, IDictionary<string, IBorrowedCard>>();
            }
            foreach(var cardA in cardName)
            {
                var card = cardA.Replace("\\\"", "\"");
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
            if (userCollection.OwnedCards == null) userCollection.OwnedCards = new Dictionary<string, IOwnedCard>();
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
            if (userCollection.OwnedCards == null) userCollection.OwnedCards = new Dictionary<string, IOwnedCard>();
            foreach(var nameAndAmount in namesAndAmounts)
            {
                var cardName = nameAndAmount.Key;
                var amount = nameAndAmount.Value;
                if (!userCollection.OwnedCards.ContainsKey(cardName)) return false;

                var card = userCollection.OwnedCards[cardName];

                if (card.Available < amount) return false;

                card.LentTo[lendee] = amount;
                collection.FindOneAndReplace(query, userCollection);
            }
            return true;

        }

        public bool AddCardAsLent(string lender, string lendee, IDictionary<string, int> namesAndAmounts)
        {
            var query = _filter.Eq("UserId", lendee);
            var userCollection = collection.Find(query).FirstOrDefault();
            if (userCollection == null) return false;
            if (userCollection.BorrowedCards == null)
            {
                userCollection.BorrowedCards = new Dictionary<string, IDictionary<string, IBorrowedCard>>();
            }

            foreach (var nameAndAmount in namesAndAmounts)
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

        public bool AddCardToCollection(string uId, IDictionary<string, int> cardsAndAmounts)
        {
            var query = _filter.Eq("UserId", uId);
            var userCollection = collection.Find(query).FirstOrDefault();
            if (userCollection == null) return false;
            if (userCollection.OwnedCards == null) userCollection.OwnedCards = new Dictionary<string, IOwnedCard>();
            foreach(var entry in cardsAndAmounts)
            {
                var cardName = entry.Key;
                var amount = entry.Value;
                if (userCollection.OwnedCards.ContainsKey(cardName))
                    userCollection.OwnedCards[cardName].TotalAmount += amount;
                else
                    userCollection.OwnedCards.Add(cardName, new OwnedCard
                    {
                        CardId = cardName,
                        InDecks = new Dictionary<string, int>(),
                        LentTo = new Dictionary<string, int>(),
                        TotalAmount = amount
                    });
            }

            collection.FindOneAndReplace(query, userCollection);
            return true;
        }

        public bool MarkCardsAsLendable(string userId, IDictionary<string, bool> lendingStatus)
        {
            var query = _filter.Eq("UserId", userId);
            var col = collection.Find(query).FirstOrDefault();
            if (col == null) return false;
            var ownedCards = col.OwnedCards;
            foreach(var status in lendingStatus) {
                var cardName = status.Key;
                var lendable = status.Value;
                if (ownedCards.ContainsKey(cardName))
                    ownedCards[cardName].Lendable = lendable;
            }
            collection.FindOneAndReplace(query, col);
            return true;
        }

        public bool RemoveBorrowedCards(string ownerId, string lendingUserId, string cardName)
        {
            var ownerQuery = _filter.Eq("UserId", ownerId);
            var lenderQuery = _filter.Eq("UserId", lendingUserId);

            var ownerCollection = collection.Find(ownerQuery).FirstOrDefault();
            if (ownerCollection == null) return false;
            var lenderCollection = collection.Find(lenderQuery).FirstOrDefault();
            if (lenderCollection == null) return false;
            //Validation
            if (!ownerCollection.OwnedCards.ContainsKey(cardName)) return false;
            if (!ownerCollection.OwnedCards[cardName].LentTo.ContainsKey(lendingUserId)) return false;
            if (!lenderCollection.BorrowedCards.ContainsKey(cardName)) return false;
            if (!lenderCollection.BorrowedCards[cardName].ContainsKey(ownerId)) return false;

            var borrowedCard = lenderCollection.BorrowedCards[cardName][ownerId];
            borrowedCard.AmountBorrowed--;
            //Remove entry for borrowed card for this lending user if no borrowed cards 
            if (borrowedCard.AmountBorrowed == 0) lenderCollection.BorrowedCards[cardName].Remove(ownerId);
            //If no more borrowed cards of this type, remove
            if (lenderCollection.BorrowedCards[cardName].Count == 0) lenderCollection.BorrowedCards.Remove(cardName);

            ownerCollection.OwnedCards[cardName].LentTo[lendingUserId]--;
            //Card is no longer lent to anyone
            if (ownerCollection.OwnedCards[cardName].LentTo[lendingUserId] == 0) 
                ownerCollection.OwnedCards[cardName].LentTo.Remove(lendingUserId);
            
            collection.FindOneAndReplace(ownerQuery, ownerCollection);
            collection.FindOneAndReplace(lenderQuery, lenderCollection);

            return true;
        }
    }
}
