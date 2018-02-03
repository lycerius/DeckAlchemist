using System.Collections.Generic;
using DeckAlchemist.WebApp.Api.Objects.Deck;

namespace DeckAlchemist.WebApp.Api.Managers.Deck
{
    public class DeckCollection
    {
        public IEnumerable<IDeck> Collection {get; set;}
    }
}