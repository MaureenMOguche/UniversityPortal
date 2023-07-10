using System.Linq.Expressions;
using UP.Application.Models.Dto;
using UP.Domain.User;

namespace UP.Application.Contracts.Persistence.User;

public interface IRoleRepository :  IGenericRepository<UserRole>
{
    //Task<int> CreateRole(CreateUserRoleDto userRoleDto);
    //Task<int> DeleteRole(int id);
    //Task<int> UpdateRole(UpdateUserRoleDto userRoleDto);
    //Task<IEnumerable<UserRole>> GetAllRoles();
    //Task<UserRole?> GetOneRole(Expression<Func<UserRole, bool>> filter);
}