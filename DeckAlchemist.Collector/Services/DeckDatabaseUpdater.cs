using DeckAlchemist.Api.Sources.Mtg.Internal;
using DeckAlchemist.Collector.Sources.Decks.Mtg.External;

namespace DeckAlchemist.Collector.Services
{
    public class DeckDatabaseUpdater : IDeckDatabaseUpdater
    {
        readonly IMtgExternalDeckSource _externalDeckSource;
        readonly IMtgInternalDeckSource _internalDeckSource;

        public DeckDatabaseUpdater(IMtgExternalDeckSource externalSource, IMtgInternalDeckSource internalSource)
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
