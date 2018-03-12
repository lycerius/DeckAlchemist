using System;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Sources.User
{
    public interface IUserSource
    {
        void Initialize():
        IUser Get(string userId);
        void Update(IUser user);
        void Create(IUser user);
    }
}
