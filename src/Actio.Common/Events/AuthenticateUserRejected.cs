using Actio.Common.Events.Shared;

namespace Actio.Common.Events;

public class AuthenticateUserRejected : IRejectedEvent
{
    protected AuthenticateUserRejected()
    {
    }

    public AuthenticateUserRejected(string email, string reason, string code)
    {
        Email = email;
        Reason = reason;
        Code = code;
    }

    public string Email { get; private set; }
    public string Reason { get; private set; }
    public string Code { get; private set; }

}