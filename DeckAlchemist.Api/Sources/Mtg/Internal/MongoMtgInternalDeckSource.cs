using System.Collections.Generic;
using System.Linq;
using DeckAlchemist.Api.Objects.Mtg.Decks;
using MongoDB.Bson;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System;
using MongoDB.Driver;

namespace DeckAlchemist.Api.Sources.Mtg.Internal
{
    public class MongoMtgInternalDeckSource : IMtgInternalDeckSource
    {
        readonly string MongoConnectionString = Environment.GetEnvironmentVariable("MONGO_URI") ?? "mongodb://localhost:27017";
        const string MongoDatabase = "Mtg";
        const string MongoCollection = "Decks";

        readonly IMongoCollection<MongoMtgDeck> collection;
        readonly IMongoDatabase database;
        readonly FilterDefinitionBuilder<MongoMtgDeck> _filter = Builders<MongoMtgDeck>.Filter;

        public MongoMtgInternalDeckSource()
        {
            var client = new MongoClient(MongoConnectionString);
            database = client.GetDatabase(MongoDatabase);
            EnsureCollectionExists(database);
            collection = database.GetCollection<MongoMtgDeck>(MongoCollection);
        }


        public void UpdateAllDecks(IEnumerable<IMtgDeck> externalDecks)
        {
            database.DropCollection(MongoCollection);
            EnsureCollectionExists(database);
            var mongoCards = externalDecks.Select(externalDeck => { var deck = MongoMtgDeck.FromMtgDeck(externalDeck); deck.id = CreateUniqueIdForDeck(deck); return deck; });
            collection.InsertMany(mongoCards);

            /* We need a way to match an external deck to one that is already in the database. Name is not enough (because it can change and names can be duplicated, making it unable to be a PK)
            var existingDecks = FindDecksByName(externalDecks.Select(deck => deck.Name).ToList());
            var plan = CreateDeckUpdatePlan(existingDecks, externalDecks);
            collection.BulkWrite(plan);
            */
        }

        public string CreateUniqueIdForDeck(IMtgDeck deck)
        {
            var orderedCards = deck.Cards.Values.OrderBy(card => card.Name);
            var builder = new StringBuilder();
            foreach (var card in orderedCards)
                builder.Append(card.Name);
            var bytes = Encoding.UTF8.GetBytes(builder.ToString());
            var hasher = new SHA256Managed();
            var id = hasher.ComputeHash(bytes);
            return Convert.ToBase64String(id);
        }

        IDictionary<string, MongoMtgDeck> FindDecksByName(IEnumerable<string> names)
        {
            var decksByNameFilter = _filter.In("Name", names);
            var result = collection.Find(decksByNameFilter);
            return result.ToEnumerable().ToDictionary(deck => deck.Name);
        }

        IEnumerable<WriteModel<MongoMtgDeck>> CreateDeckUpdatePlan(IDictionary<string, MongoMtgDeck> internalDecks, IEnumerable<IMtgDeck> externalDecks)
        {
            var plan = new LinkedList<WriteModel<MongoMtgDeck>>();

            foreach(var externalDeck in externalDecks)
            {
                if(internalDecks.ContainsKey(externalDeck.Name))
                {
                    var internalDeck = internalDecks[externalDeck.Name];
                    if(DifferencesExist(internalDeck, externalDeck))
                    {
                        var mongoDeck = MongoMtgDeck.FromMtgDeck(externalDeck);
                        mongoDeck._id = internalDeck._id;
                        var replaceOneFilter = _filter.Eq("_id", mongoDeck._id);
                        var newPlan = new ReplaceOneModel<MongoMtgDeck>(
                            replaceOneFilter,
                            mongoDeck
                        );
                        plan.AddLast(newPlan);
                    }
                }
                else
                {
                    var newPlan = new InsertOneModel<MongoMtgDeck>(MongoMtgDeck.FromMtgDeck(externalDeck));
                    plan.AddLast(newPlan);
                }
            }

            return plan;
        }

        bool DifferencesExist(IMtgDeck deck1, IMtgDeck deck2)
        {
            //TODO: Determin proper differences
            return deck1.Name != deck2.Name ||
                        System.Math.Abs(deck1.Meta - deck2.Meta) > 0.001 ||
                        CardDifferencesExist(deck1.Cards, deck2.Cards);
        }

        bool CardDifferencesExist(IDictionary<string, IMtgDeckCard> cardSet1, IDictionary<string, IMtgDeckCard> cardSet2)
        {
            if(cardSet1 == null || cardSet2 == null)
            {
                if (cardSet1 == cardSet2) return false;
                return true;
            }

            foreach(var cardSet1KV in cardSet1)
            {
                if(cardSet2.ContainsKey(cardSet1KV.Key))
                {
                    if (cardSet2[cardSet1KV.Key].Count != cardSet1KV.Value.Count) return true;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        void EnsureCollectionExists(IMongoDatabase database)
        {
            var filter = new BsonDocument("name", MongoCollection);
            //filter by collection name
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            //check for existence
            var exists = collections.Any();

            if (!exists)
                database.CreateCollection(MongoCollection);
        }
    }
}
