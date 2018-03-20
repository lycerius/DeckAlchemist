using System;
namespace DeckAlchemist.Support.Objects.Messages
{
    public interface IMessage
    {
        string Type { get; }
        bool UnRead { get; set; }
        string SenderId { get; set; }
        string GroupId { get; set; }
        string Subject { get; set; }
        string Body { get; set; }
    }
}
