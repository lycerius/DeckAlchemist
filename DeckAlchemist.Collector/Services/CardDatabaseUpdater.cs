using DeckAlchemist.Collector.Sources.Cards.Mtg;
using DeckAlchemist.Collector.Sources.Cards.Mtg.Internal;

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
 