using System.Collections.Generic;
using System.Net.Http;
using DeckAlchemist.Api.Utility;
using DeckAlchemist.Api.Sources.Messages;
using DeckAlchemist.Api.Utility;
using DeckAlchemist.Support.Objects.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/message")]
    public class MessageController : Controller
    {
        IMessageSource _messageSource;

        public MessageController(IMessageSource messageSource)
        {
            _messageSource = messageSource;
           
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<IMessage> GetUserMessages()
        {
            var userId = UserInfo.Id(HttpContext.User);
            return _messageSource.GetMessagesForUser(userId);
        }

        [Route("read")]
        [HttpPost]
        public void MarkMessageAsRead([FromBody] string messageId)
        {
            
        }

        [Route("send/user")]
        [HttpPost]
        public void SendMessageToUser([FromBody] UserMessage message)
        {
            _messageSource.SendMessage(message);
        }

        [Route("send/loan")]
        [HttpPost]
        public void SendLoanRequestToUser([FromBody] LoanRequestMessage message)
        {
            _messageSource.SendMessage(message);
        }

        [Route("send/invite")]
        [HttpPost]
        public void SendGroupInviteToUser([FromBody] GroupInviteMessage message)
        {
            _messageSource.SendMessage(message);
        }

        [Route("accept/invite")]
        [HttpPost]
        public void AcceptGroupInvite([FromBody] string messageId)
        {
            var userId = UserInfo.Id(HttpContext.User);
            var message = _messageSource.GetMessageById(userId, messageId);
            if (message.Type != "Group") return;
            var groupInvite = message as GroupInviteMessage;
            var groupId = groupInvite.GroupId;
            var client = new HttpClient();
            var task = client.UseAuthToken(UserInfo.GetIdToken(HttpContext)).PutObject($"http://localhost:5000/api/group/{groupId}/member", userId);
            task.Wait();
            task.Result.EnsureSuccessStatusCode();
            groupInvite.Accepted = true;
            _messageSource.Update(userId, groupInvite);
        }

        [Route("accept/loan")]
        [HttpPost]
        public void AcceptLoanRequest([FromBody] string messageId)
        {
            //TODO: Should accept the loan
        }

        
    }
}
