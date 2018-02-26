using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace DeckAlchemist.Api.Auth
{
    public class EmailVerificationHandler : AuthorizationHandler<EmailVerificationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmailVerificationRequirement requirement)
        {
            var emailVerification = context.User?.FindFirst("email_verified");
            if (emailVerification == null) return Task.CompletedTask;
            if (emailVerification.Value.ToLower().Equals(Boolean.TrueString.ToLower())) context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
