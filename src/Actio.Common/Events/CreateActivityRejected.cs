using Actio.Common.Events.Shared;

namespace Actio.Common.Events;

public class CreateActivityRejected : IRejectedEvent
{

    protected CreateActivityRejected()
    {
    }
    public CreateActivityRejected(Guid id, string reason, string code)
    {
        Id = id;
        Reason = reason;
        Code = code;
    }
    public Guid Id { get; private set; }
    public string Reason { get; private set; }
    public string Code { get; private set; }
}