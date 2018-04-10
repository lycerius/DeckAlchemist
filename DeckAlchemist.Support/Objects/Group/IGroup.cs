using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Group
{
    public interface IGroup
    {
        string GroupId { get; set; }
        string GroupName { get; set; }
        string Owner { get; set; }
        List<string> Members { get; set; }
    }
}
