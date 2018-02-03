using System.Collections.Generic;
using System.Linq;
using DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.External;
using DeckAlchemist.WebApp.Api.Managers.CardDatabase.Source.Local;
using DeckAlchemist.WebApp.Api.Objects.CardDatabase;

//TODO: The integrator will not be staying here. There should be a seperate service for this
namespace DeckAlchemist.WebApp.Api.Managers.CardDatabase.Integration {
    public class CardDatabaseIntegrator : ICardDatabaseIntegrator {
        private readonly IExternalCardDatabaseSource _externalSource;

        private readonly ILocalCardDatabaseSource _localSource;

        public CardDatabaseIntegrator (IExternalCardDatabaseSource external, ILocalCardDatabaseSource local) {
            _externalSource = external;
            _localSource = local;
        }

        public void Integrate () {
            var externalCards = _externalSource.GetAllCards ();
            var internalCards = _localSource.GetAllCards ();
            var cardsToAdd = filter (internalCards, externalCards);
            if (cardsToAdd.Any ())
                _localSource.Update (cardsToAdd.ToArray());

        }

        private IEnumerable<ICard> filter (CardDatabaseCollection internalCards, CardDatabaseCollection externalCards) {
            var addList = new List<ICard> ();
            foreach (var externalCard in externalCards.Collection.Values) {
                if (internalCards.Collection.ContainsKey (externalCard.Name)) {
                    //Check for differences
                    var internalCard = internalCards.Collection[externalCard.Name];
                    if (!internalCard.Equals(externalCard)) {
                        addList.Add (externalCard);
                    }
                } else {
                    addList.Add (externalCard);
                }
            }
            return addList;
        }
    }
}