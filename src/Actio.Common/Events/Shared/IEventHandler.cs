namespace Actio.Common.Events.Shared;

public interface IEventHandler<in T> where T : IEvent
{
  public Task HandleAsync(T @event);
}