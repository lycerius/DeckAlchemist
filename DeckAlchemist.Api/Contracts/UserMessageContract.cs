using DeckAlchemist.Support.Objects.Messages;

namespace DeckAlchemist.Api.Contracts
{
    public class UserMessageContract
    {
        public string GroupId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string RecipientId { get; set; }

        public UserMessage ToUserMessage()
        {
            return new UserMessage
            {
                Body = Body,
                GroupId = GroupId,
                RecipientId = RecipientId,
                Subject = Subject,
                UnRead = true
            };
        }
    }
}
