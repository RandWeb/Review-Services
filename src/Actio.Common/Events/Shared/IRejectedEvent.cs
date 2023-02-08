namespace Actio.Common.Events.Shared;

public interface IRejectedEvent : IEvent
{
    public string Reason { get; }

    public string Code { get; }
}
