using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Group
{
    public class Group : IGroup
    {
        public string GroupName { get; set; }
        public string Owner { get; set; }
        public List<string> Members { get; set; }
        public string GroupId { get; set; }
    }
}
