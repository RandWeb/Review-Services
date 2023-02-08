using Actio.Common.Events.Shared;

namespace Actio.Common.Events;

public class UserAuthenticated : IEvent
{
    protected UserAuthenticated()
    {
    }

    public UserAuthenticated(string email)
    {
        Email = email;
    }

    public string Email { get; private set; }
}