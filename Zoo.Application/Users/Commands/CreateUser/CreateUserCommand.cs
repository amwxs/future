using Zoo.Application.Core;
using Zoo.Application.Core.Abstractions;

namespace Zoo.Application.Users.Commands.CreateUser;

public  class CreateUserCommand: ICommand<BizResult<bool>>
{
    public string? NickName { get; set; }

    public int Age { get; set; }
}

