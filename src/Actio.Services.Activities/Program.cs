

using Actio.Command.Commands;
using Actio.Common.Events;

namespace Actio.Services.Activities;
public class Program
{
    public static void Main(string[] args)
    {
        ServiceHost.Create<Startup>(args)
        .UseRabbitMq()
        .SubscribeToCommand<CreateActivity>()
        .Build()
        .Run();
    }
}