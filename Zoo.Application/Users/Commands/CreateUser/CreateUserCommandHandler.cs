using Zoo.Application.Core.Abstractions.Messaging;
using Zoo.Domain.Core.Result;

namespace Zoo.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, CustResult<bool>>
{
    public async Task<CustResult<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return CustResult.Failure<bool>("0", "", null);
    }
}
