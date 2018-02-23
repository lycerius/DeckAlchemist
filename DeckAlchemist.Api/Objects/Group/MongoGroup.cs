using System;
using System.Collections;
using System.Collections.Generic;
using DeckAlchemist.Api.Objects.User;

namespace DeckAlchemist.Api.Objects.Group
{
    public class MongoGroup : IGroup
    {
        public string name { get; set; }
        public string owner { get; set; }
        public string game { get; set; }
        public IEnumerable<IUser> members { get; set; }
    }
}
