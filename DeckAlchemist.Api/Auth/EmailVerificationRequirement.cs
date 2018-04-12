using Microsoft.AspNetCore.Authorization;

namespace DeckAlchemist.Api.Utility
{
    public class EmailVerificationRequirement : IAuthorizationRequirement
    {
        //Even though this class is empty, it is required to be here for email verification requirements
    }
}
