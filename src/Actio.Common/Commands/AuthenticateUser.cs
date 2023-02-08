
using Actio.Command.Commands.Shared;

namespace Actio.Common.Commands;
public class AuthenticateUser : ICommand{
  public string Email { get; set; } 
  public string Password { get; set; }  
}