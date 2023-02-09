using Actio.Common.Events;
using Actio.Common.Events.Shared;

namespace Actio.Api.Handlers;
public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
{

    public async Task HandleAsync(ActivityCreated @event)
    {

        Console.WriteLine($"Activity created: {@event}");
    }
}
