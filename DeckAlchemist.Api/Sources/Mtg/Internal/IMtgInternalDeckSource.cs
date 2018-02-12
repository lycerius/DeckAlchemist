using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Mtg.Decks;

namespace DeckAlchemist.Api.Sources.Mtg.Internal
{
    public interface IMtgInternalDeckSource
    {
        void UpdateAllDecks(IEnumerable<IMtgDeck> externalDecks);
    }
}
