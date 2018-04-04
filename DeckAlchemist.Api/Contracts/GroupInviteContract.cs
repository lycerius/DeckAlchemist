using System;
using DeckAlchemist.Support.Objects.Messages;

namespace DeckAlchemist.Api.Contracts
{
    public class GroupInviteContract
    {
        
        public string GroupId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string RecipientUserName { get; set; }

        public GroupInviteMessage ToGroupInviteMessage()
        {
            return new GroupInviteMessage
            {
                Body = Body,
                GroupId = GroupId,
                Subject = Subject
            };
        }
    }
}
