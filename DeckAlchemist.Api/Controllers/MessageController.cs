using System.Collections.Generic;
using System.Net.Http;
using DeckAlchemist.Api.Utility;
using DeckAlchemist.Api.Sources.Messages;
using DeckAlchemist.Support.Objects.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DeckAlchemist.Api.Contracts;

namespace DeckAlchemist.Api.Controllers
{
    [Authorize(Policy = "Email")]
    [Route("api/message")]
    public class MessageController : Controller
    {
        readonly IMessageSource _messageSource;

        public MessageController(IMessageSource messageSource)
        {
            _messageSource = messageSource;
           
        }

        [Route("all")]
        [HttpGet]
        public IEnumerable<IMessage> GetUserMessages()
        {
            var userId = HttpContext.User.Id();
            return _messageSource.GetMessagesForUser(userId);
        }

        [Route("read")]
        [HttpPost]
        public void MarkMessageAsRead([FromBody] string messageId)
        {
            
        }

        [Route("send/user")]
        [HttpPost]
        public void SendMessageToUser([FromBody] UserMessageContract message)
        {
            _messageSource.SendMessage(message.ToUserMessage());
        }

        [Route("send/loan")]
        [HttpPost]
        public void SendLoanRequestToUser([FromBody] LoanRequestMessageContract message)
        {
            _messageSource.SendMessage(message.ToLoanRequestMessage());
        }

        [Route("send/invite")]
        [HttpPost]
        public void SendGroupInviteToUser([FromBody] GroupInviteContract message)
        {
            _messageSource.SendMessage(message.ToGroupInviteMessage());
        }

        [Route("accept/invite")]
        [HttpPost]
        public void AcceptGroupInvite([FromBody] string messageId)
        {
            var userId = HttpContext.User.Id();
            var message = _messageSource.GetMessageById(userId, messageId);
            if (message.Type != "Group") return;
            var groupInvite = message as GroupInviteMessage;
            var groupId = groupInvite.GroupId;
            var client = new HttpClient();
            var task = client.Auth(HttpContext.GetIdToken()).PutAsync($"http://localhost:5000/api/group/{groupId}/member", userId);
            task.Wait();
            task.Result.EnsureSuccessStatusCode();
            groupInvite.Accepted = true;
            _messageSource.Update(userId, groupInvite);
        }

        [Route("accept/loan")]
        [HttpPost]
        public void AcceptLoanRequest([FromBody] string messageId)
        {
            var userId = HttpContext.User.Id();
            var message = _messageSource.GetMessageById(userId, messageId);
            if (message.Type != "Loan") return;
            var loanRequest = message as LoanRequestMessage;

            var client = new HttpClient();
            client.Auth(HttpContext.GetIdToken());

            var loanTask = client.PostAsync("http://localhost/api/collection/lend", 
                                            new LendContract { Lender = loanRequest.RecipientId, Lendee = loanRequest.SenderId, CardNames = loanRequest.RequestedRecipientCardIds });
            loanTask.Wait();
            loanTask.Result.EnsureSuccessStatusCode();
        }

        
    }
}
