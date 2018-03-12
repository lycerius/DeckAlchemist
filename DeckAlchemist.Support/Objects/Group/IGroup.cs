using System.Collections.Generic;

namespace DeckAlchemist.Support.Objects.Group
{
    public interface IGroup
    {
        string GroupName { get; set; }
        string Owner { get; set; }
        List<string> Members { get; set; }
    }
}
