using Zoo.Application.Core.Abstractions.Messaging;
using Zoo.Domain.Core.Result;

namespace Zoo.Application.Users.Queries.GetByUserId;
public class GetByUserIdQueryHandler : IQueryHandler<GetByUserIdQuery, CustResult<GetByUserIdRes>>
{
    public Task<CustResult<GetByUserIdRes>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(CustResult.Success(new GetByUserIdRes { Hello = "OK" }));
    }
}
