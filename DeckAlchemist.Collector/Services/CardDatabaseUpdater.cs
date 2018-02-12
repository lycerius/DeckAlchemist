using System.Collections.Generic;
using DeckAlchemist.Collector.Objects.Cards;
using DeckAlchemist.Collector.Sources.Cards.Mtg;
using DeckAlchemist.Collector.Sources.Cards.Mtg.Internal;
using System.Linq;
using DeckAlchemist.Api.Sources.Mtg.Internal;

namespace DeckAlchemist.Collector.Services
{
    public class CardDatabaseUpdater : ICardDatabaseUpdater
    {
        readonly IMtgExternalCardSource _externalCardSource;

        readonly IMtgInternalCardSource _internalCardSource;

        public CardDatabaseUpdater(IMtgExternalCardSource externalSource, IMtgInternalCardSource internalSource)
        {
            _externalCardSource = externalSource;
            _internalCardSource = internalSource;
        }

        public void UpdateCardDatabase()
        {
            var externalCards = _externalCardSource.GetAllCards();
            _internalCardSource.UpdateAllCards(externalCards);
        }
    }
}
 