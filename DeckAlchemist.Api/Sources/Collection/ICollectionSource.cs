using System;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.Collection;

namespace DeckAlchemist.Api.Sources.Collection
{
    public interface ICollectionSource
    {
        string GetCollectionIdOf(string userId);
        bool AddCard(string cardName, string userId);
        bool DeleteCard(string cardName, string userId);
    }
}
