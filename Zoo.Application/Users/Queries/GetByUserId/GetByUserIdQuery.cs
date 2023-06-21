using Zoo.Application.Core.Abstractions.Messaging;
using Zoo.Domain.Core.Result;

namespace Zoo.Application.Users.Queries.GetByUserId
{
    public class GetByUserIdQuery : IQuery<CustResult<GetByUserIdRes>>
    {
        public int Id { get; set; }
    }
}
