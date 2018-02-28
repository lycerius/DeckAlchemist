using System;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Objects.Group
{
    public class Group : IGroup
    {
        public string name { get; set; }
        public string owner { get; set; }
        public string game { get; set; }
        public IEnumerable<IUser> members { get; set; }
    }
}
