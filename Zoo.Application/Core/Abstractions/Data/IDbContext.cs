using Microsoft.EntityFrameworkCore;

namespace Zoo.Application.Core.Abstractions.Data;
public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>()
        where TEntity : class;
}
