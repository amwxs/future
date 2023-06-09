using Zoo.Application.Core.Abstractions;

namespace Zoo.Application.Users.Commands.CreateUser;

public record class CreateUserCommand(string NickName): ICommand<bool>
{ }

