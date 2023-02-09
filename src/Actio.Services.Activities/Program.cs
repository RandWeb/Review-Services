

using Actio.Common.Events;

namespace Actio.Services.Activities;
public class Program
{
    public static void Main(string[] args)
    {
        ServiceHost.Create<Startup>(args)
        .UseRabbitMq()
        .SubscribeToEvent<ActivityCreated>()
        .Build()
        .Run();
    }
}