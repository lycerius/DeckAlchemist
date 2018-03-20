namespace DeckAlchemist.Support.Objects.Messages
{
    public class GroupInviteMessage : IMessage
    {
        public string Type => "Group";
        public bool UnRead { get; set; }
        public string SenderId { get; set; }
        public string GroupId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; 
        public bool Accepted { get; set; }
    }
}
