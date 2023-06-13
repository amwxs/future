using FluentValidation;

namespace Zoo.Application.Users.Queries.GetByUserId;
public class GetByUserIdValidator: AbstractValidator<GetByUserIdQuery>
{
    public GetByUserIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("id不能为空")
            .GreaterThan(1000).WithMessage("id错误");
    }
}
