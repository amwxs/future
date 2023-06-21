using Microsoft.EntityFrameworkCore;
using Zoo.Application.Core.Abstractions.Data;

namespace Zoo.Infrastructure;
public class ZooDBContext : DbContext, IDbContext, IUnitOfWork
{
}
