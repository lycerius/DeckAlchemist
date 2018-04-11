namespace DeckAlchemist.Support.Objects.Messages
{
    public class UserMessage : IMessage
    {
        public string Type { get => "User"; set { } }
        public string MessageId { get; set; }
        public bool UnRead { get; set; }
        public string SenderId { get; set; }
        public string GroupId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string RecipientId { get; set; }
    }
}
