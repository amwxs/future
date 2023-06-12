using Zoo.Application.Core.Abstractions;
using Zoo.Application.Core.Primitives;

namespace Zoo.Application.Users.Commands.CreateUser;

public record class CreateUserCommand(string NickName): ICommand<Result<bool>>
{ }

