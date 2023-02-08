using Actio.Command.Commands.Shared;

namespace Actio.Common.Commands.Shared;

public interface ICommandHandler<in T> where T : ICommand{
Task HandleAsync(T command);
}