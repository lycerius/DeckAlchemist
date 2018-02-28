using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Objects.Group
{
    public interface IGroup
    {
        string name { get; set; }
        string owner { get; set; }
        string game { get; set; }
        IEnumerable<IUser> members { get; set; }
    }
}
