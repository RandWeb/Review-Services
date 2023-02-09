using Actio.Command.Commands.Shared;
using Actio.Common.Commands.Shared;
using Actio.Common.Events;
using Actio.Common.Events.Shared;
using Actio.Common.RabbitMq;
using Actio.Common.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using static System.Formats.Asn1.AsnWriter;

public class ServiceHost : IServiceHost
{
    private readonly IWebHost _webHost;

    public ServiceHost(IWebHost webHost)
    {
        _webHost = webHost;
    }

    public void Run() => _webHost.Run();

    public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
    {
        Console.Title = typeof(TStartup).Namespace;
        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddCommandLine(args)
            .Build();
            
        var webHostBuilder = WebHost.CreateDefaultBuilder(args)
            .UseConfiguration(config)
            .UseStartup<TStartup>();

        return new HostBuilder(webHostBuilder.Build());
    }

    public abstract class BuilderBase
    {
        public abstract ServiceHost Build();
    }

    public class HostBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;
        private IBusClient _bus;

        public HostBuilder(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public BusBuilder UseRabbitMq()
        {
            _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));

            return new BusBuilder(_webHost, _bus);
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }

    public class BusBuilder : BuilderBase
    {
        private readonly IWebHost _webHost;
        private IBusClient _bus;

        public BusBuilder(IWebHost webHost, IBusClient bus)
        {
            _webHost = webHost;
            _bus = bus;
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            IServiceProvider services = _webHost.Services;
            var handler = services.GetService<ICommandHandler<TCommand>>() as ICommandHandler<TCommand>;
            _bus.WithCommandHandlerAsync(handler);

            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            IServiceProvider services = _webHost.Services;
            var handler = services.GetService<IEventHandler<TEvent>>() as IEventHandler<TEvent>;
            _bus.WithEventHandlerAsync(handler);

            return this;
        }

        public override ServiceHost Build()
        {
            return new ServiceHost(_webHost);
        }
    }

}