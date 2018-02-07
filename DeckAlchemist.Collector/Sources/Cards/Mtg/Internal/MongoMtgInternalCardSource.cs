using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeckAlchemist.Collector.Objects.Cards;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DeckAlchemist.Collector.Sources.Cards.Mtg.Internal
{
    public class MongoMtgInternalCardSource : IMtgInternalCardSource
    {
        const string MongoConnectionString = "mongodb://localhost:27017";
        const string MongoDatabase = "Cards";
        const string MongoCollection = "Mtg";

        readonly IMongoCollection<MongoMtgCard> collection;
        readonly FilterDefinitionBuilder<MongoMtgCard> _filter = Builders<MongoMtgCard>.Filter;

        public MongoMtgInternalCardSource()
        {
            var client = new MongoClient(MongoConnectionString);
            var database = client.GetDatabase(MongoDatabase);
            EnsureCollectionExists(database);
            collection = database.GetCollection<MongoMtgCard>(MongoCollection);
        }

        public IEnumerable<IMtgCard> GetAllCards()
        {
            return collection.Find(_ => true).ToList();
        }

        public void UpdateAllCards(IEnumerable<IMtgCard> cards)
        {
            var existingCards = FindCardsByNames(cards.Select(card => card.Name));
            var plan = CreateCardUpdatePlan(existingCards, cards);
            if(plan.Any()) collection.BulkWrite(plan);
        }

        Dictionary<string, MongoMtgCard> FindCardsByNames(IEnumerable<string> cardNames)
        {
            var findCardsFilter = _filter.In("Name", cardNames);
            var result = collection.Find(findCardsFilter);
            return result.ToEnumerable().ToDictionary(card => card.Name);
        }

        IEnumerable<WriteModel<MongoMtgCard>> CreateCardUpdatePlan(Dictionary<string, MongoMtgCard> internalCards, IEnumerable<IMtgCard> externalCards)
        {
            var plan = new LinkedList<WriteModel<MongoMtgCard>>();

            foreach(var externalCard in externalCards)
            {
                if(internalCards.ContainsKey(externalCard.Name))
                {
                    var internalCard = internalCards[externalCard.Name];
                    if(DifferencesExist(externalCard, internalCard))
                    {
                        var updatedInternalCard = MongoMtgCard.FromMtgCard(externalCard);
                        updatedInternalCard._id = internalCard._id;

                        var internalCardFilter = _filter.Eq("_id", internalCard._id);

                        var newPlan = new ReplaceOneModel<MongoMtgCard>(
                            internalCardFilter,
                            updatedInternalCard
                        );

                        plan.AddLast(newPlan);
                    }
                }
                else
                {
                    var insertPlan = new InsertOneModel<MongoMtgCard>(MongoMtgCard.FromMtgCard(externalCard));
                    plan.AddLast(insertPlan);
                }
            }

            return plan;
        }


        bool DifferencesExist(IMtgCard card1, IMtgCard card2)
        {
            //TODO: Expand this to include all property checks
            return
                card1.Name != card2.Name ||
                     LegalDifferencesExist(card1.Legality, card2.Legality);
        }

        bool LegalDifferencesExist(IEnumerable<IMtgLegality> legalitySet1, IEnumerable<IMtgLegality> legalitySet2)
        {
            if (legalitySet1 == null || legalitySet2 == null){
                if (legalitySet2 != legalitySet1) return true;
                return false;
            }
            if (legalitySet1.Count() != legalitySet2.Count()) return true;
            foreach(var legality in legalitySet1)
            {
                if (!legalitySet2.Where(legality2 => legality2.Format == legality.Format && legality.Legality == legality2.Legality).Any())
                    return true;
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
