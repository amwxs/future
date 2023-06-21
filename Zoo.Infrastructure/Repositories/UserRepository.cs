using Zoo.Application.Core.Abstractions.Data;
using Zoo.Domain.Entities;
using Zoo.Domain.Repositories;

namespace Zoo.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly IDbContext _dbContext;

    public UserRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(User user)
    {
        _dbContext.Set<User>().Add(user);
    }
}
