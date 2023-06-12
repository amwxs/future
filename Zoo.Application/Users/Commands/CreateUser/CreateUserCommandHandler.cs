using Zoo.Application.Core.Abstractions;
using Zoo.Application.Core.Primitives;

namespace Zoo.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<bool>>
{
    public Task<Result<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Success(true));
    }
}
