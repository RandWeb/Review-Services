namespace Actio.Common.Events.Shared;

public interface IAuthenticatedEvent : IEvent
{
    public Guid UserId { get; }
}