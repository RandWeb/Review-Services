using System.Reflection;
using Actio.Command.Commands.Shared;
using Actio.Common.Commands.Shared;
using Actio.Common.Events.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Instantiation;
using RawRabbit.Pipe;

namespace Actio.Common.RabbitMq;
public static class RabbitExtensions
{
    public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus,
        ICommandHandler<TCommand> handler) where TCommand : ICommand
        => bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
            ctx => ctx.UseConsumerConfiguration(cfg =>
            cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>()))));

    public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus,
        IEventHandler<TEvent> handler) where TEvent : IEvent
        => bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
            ctx => ctx.UseConsumerConfiguration(cfg =>
            cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>()))));

    private static string GetQueueName<T>()
        => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

    public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new RabbitMqOptions();
        var section = configuration.GetSection("rabbitmq");
        section.Bind(options);
        var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
        {
            ClientConfiguration = options
        });
        services.AddSingleton<IBusClient>(_ => client);
    }
}
