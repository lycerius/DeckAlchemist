using DeckAlchemist.Api.Sources.Deck.Mtg;
using DeckAlchemist.Collector.Sources.Decks.Mtg.External;

namespace DeckAlchemist.Collector.Services
{
    public class DeckDatabaseUpdater : IDeckDatabaseUpdater
    {
        readonly IMtgExternalDeckSource _externalDeckSource;
        readonly IMtgDeckSource _internalDeckSource;

        public DeckDatabaseUpdater(IMtgExternalDeckSource externalSource, IMtgDeckSource internalSource)
        {
            _externalDeckSource = externalSource;
            _internalDeckSource = internalSource;
        }

        public void UpdateDecks()
        {
            var decks = _externalDeckSource.GetAllDecks();
            _internalDeckSource.UpdateAllDecks(decks);

        }
    }
}
