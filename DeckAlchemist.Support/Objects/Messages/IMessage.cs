using System;
namespace DeckAlchemist.Support.Objects.Messages
{
    public interface IMessage
    {
        string MessageId { get; set; }
        string Type { get; set; }
        bool UnRead { get; set; }
        string SenderId { get; set; }
        string RecipientId { get; set; }
        string GroupId { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
    }
}
