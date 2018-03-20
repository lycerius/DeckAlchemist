using System;
namespace DeckAlchemist.Support.Objects.Messages
{
    public class UserMessage : IMessage
    {
        public string Type => "User";
        public bool UnRead { get; set; }
        public string SenderId { get; set; }
        public string GroupId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
