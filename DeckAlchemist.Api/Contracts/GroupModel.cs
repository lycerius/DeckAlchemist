using System.Collections.Generic;

namespace DeckAlchemist.Api.Contracts
{
    public class GroupModel
    {
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public IEnumerable<UserModel> Users { get; set; }
    }
}
