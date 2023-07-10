using System.Linq.Expressions;
using UP.Application.Models.Dto;
using UP.Domain;
using UP.Domain.User;

namespace UP.Application.Contracts.Persistence.User;

public interface IUserRepository : IGenericRepository<ApplicationUser>
{
    //Task<ApplicationUser> CreateUser(ApplicationUser user);
    //Task DeleteUser(ApplicationUser user);
    //Task UpdateUser(ApplicationUser user);
    //Task<IEnumerable<ApplicationUser>> GetAllUsers();
    //Task<ApplicationUser?> GetOneUser(
    //    Expression<Func<ApplicationUser, bool>> filter, string? includeProperties = null);
}