using FluentValidation;
using Zoo.Application.Core.Abstractions.Messaging;
using Zoo.Domain.Core.Result;

namespace Zoo.Application.Users.Commands.CreateUser;

public  class CreateUserCommand: ICommand<CustResult<bool>>
{
    public string? NickName { get; set; }

    public int Age { get; set; }
}


public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.NickName)
            .NotEmpty().WithMessage("昵称不能为空")
            .MaximumLength(20).WithMessage("昵称最大长度20");
        RuleFor(x => x.Age).GreaterThan(10).WithMessage("最小10");
    }
}
