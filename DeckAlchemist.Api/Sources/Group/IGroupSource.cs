using System;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.User;

namespace DeckAlchemist.Api.Sources.Group
{
    public interface IGroupSource
    {
        IEnumerable<IUser> GetAllUsers(string groupName);
        void AddUser(string groupName, string user);
        void RemoveUser(string groupName, string user);
                     
    }
}
