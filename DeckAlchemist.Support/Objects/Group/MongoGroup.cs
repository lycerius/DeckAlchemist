using System;
using System.Collections;
using System.Collections.Generic;
using DeckAlchemist.Support.Objects.Group;
using DeckAlchemist.Support.Objects.User;

namespace DeckAlchemist.Api.Objects.Group
{
namespace DeckAlchemist.Support.Objects.Group
{
    public class MongoGroup : IGroup
    {
        public string name { get; set; }
        public string owner { get; set; }
        public string game { get; set; }
        public IEnumerable<string> membersIDs { get; set; }
    }
}
}