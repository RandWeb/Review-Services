using Actio.Common.Events.Shared;

namespace Actio.Common.Events;

public class UserCreated : IEvent{
    protected UserCreated()
    {
    }

    public UserCreated(string email, string name)
    {
        Email = email;
        Name = name;
    }

    public string Email { get; private set; }
    public string Name { get;private set; }
}