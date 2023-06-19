using Zoo.Application.Core;
using Zoo.Application.Core.Abstractions;

namespace Zoo.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, BizResult<bool>>
{
    public Task<BizResult<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(BizResult.Success(true));
    }
}
