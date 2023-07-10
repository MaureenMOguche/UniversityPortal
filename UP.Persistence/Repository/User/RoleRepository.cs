using UP.Application.Contracts.Persistence.User;

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UP.Application.Contracts.Persistence.User;
using UP.Domain;
using UP.Domain.User;

namespace UP.Persistence.Repository.User;

public class RoleRepository : GenericRepository<UserRole>, IRoleRepository
{
    public RoleRepository(UPDbContext db) : base(db)
    {
    }
}