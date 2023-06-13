using Zoo.Application.Core;
using Zoo.Application.Core.Abstractions;

namespace Zoo.Application.Users.Queries.GetByUserId
{
    public class GetByUserIdQuery : IQuery<BizResult<GetByUserIdRes>>
    {
        public int Id { get; set; }
    }
}
