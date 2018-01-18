using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects.CardDatabase;

namespace DeckAlchemist.WebApp.Api.Managers.CardDatabase
{
    public class CardDatabaseCollection
    {
        public readonly IReadOnlyDictionary<string, ICard> Collection;

        public CardDatabaseCollection(IReadOnlyDictionary<string, ICard> results)
        {
            Collection = results;
        }

    }
}