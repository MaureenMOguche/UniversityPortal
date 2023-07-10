using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UP.Application.Contracts.Persistence.User;
using UP.Domain;
using UP.Domain.User;

namespace UP.Persistence.Repository.User;

public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(UPDbContext db):base(db)
    {
    }
    //public async Task<ApplicationUser> CreateUser(ApplicationUser user)
    //{
    //    var result = await _db.ApplicationUsers.AddAsync(user);
    //    await _db.SaveChangesAsync();
    //    return result.Entity;
    //}

    //public async Task DeleteUser(ApplicationUser user)
    //{
    //    var userFromDb = await _db.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == user.Id);
    //    _db.ApplicationUsers.Remove(userFromDb);
    //    await _db.SaveChangesAsync();
    //}

    //public async Task UpdateUser(ApplicationUser user)
    //{
    //    _db.ApplicationUsers.Update(user);
    //    await _db.SaveChangesAsync();
    //}

    //public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
    //{
    //    return await _db.ApplicationUsers.AsNoTracking().ToListAsync();
    //}

    //public async Task<ApplicationUser?> GetOneUser(
    //    Expression<Func<ApplicationUser, bool>> filter, string? includeProperties = null)
    //{
    //    IQueryable<ApplicationUser> query = _db.ApplicationUsers;
    //    if (filter != null)
    //    {
    //        query = query.Where(filter);
    //    }

    //    if (includeProperties != null)
    //    {
    //        query = query.Include(includeProperties);
    //    }
    //    return query.ToListAsync().Fir;
    //}
}