using Zoo.Application.Core;
using Zoo.Application.Core.Abstractions;

namespace Zoo.Application.Users.Queries.GetByUserId;
public class GetByUserIdQueryHandler : IQueryHandler<GetByUserIdQuery, BizResult<GetByUserIdRes>>
{
    public Task<BizResult<GetByUserIdRes>> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(BizResult.Success(new GetByUserIdRes { Hello = "OK" }));
    }
}
