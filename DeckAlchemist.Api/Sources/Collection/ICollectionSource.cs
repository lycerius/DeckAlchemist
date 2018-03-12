using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Collection;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Sources.Collection
{
    public interface ICollectionSource
    {
        void Initialize();
        string Create(IUser user);
        void Update(ICollection collection);
    }
}
