
using Actio.Command.Commands.Shared;

namespace Actio.Command.Commands.Shared;
public interface IAuthenticatedCommand : ICommand
{
    public Guid UserId { get; set; }
}
