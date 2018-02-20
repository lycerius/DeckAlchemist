using DeckAlchemist.Collector.Sources.Cards.Mtg;
using DeckAlchemist.Api.Sources.Cards.Mtg;

namespace DeckAlchemist.Collector.Services
{
    public class CardDatabaseUpdater : ICardDatabaseUpdater
    {
        readonly IMtgExternalCardSource _externalCardSource;

        readonly IMtgCardSource _internalCardSource;

        public CardDatabaseUpdater(IMtgExternalCardSource externalSource, IMtgCardSource internalSource)
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
 