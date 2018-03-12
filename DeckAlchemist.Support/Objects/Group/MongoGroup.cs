using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Group
{
     class MongoGroup : IGroup
    {
        public string GroupName { get; set; }
        public string Owner { get; set; }
        public List<string> Members { get; set; }
    }
}