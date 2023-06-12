using FluentValidation;

namespace Zoo.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.NickName)
            .NotEmpty().WithMessage("昵称不能为空")
            .MaximumLength(20).WithMessage("昵称最大长度20");
    }
}
