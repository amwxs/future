using Zoo.Application.Core.Abstractions;

namespace Zoo.Application.Users.Commands.CreateUser;

public  class CreateUserCommand: ICommand<bool>
{
    public string? NickName { get; set; }
}

