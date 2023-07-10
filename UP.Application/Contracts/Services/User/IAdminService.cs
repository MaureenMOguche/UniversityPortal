using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Application.Models.Dto.User;
using UP.Domain;
using UP.Domain.User;

namespace UP.Application.Contracts.Services.User;

public interface IAdminService
{
    Task<APIResponse> CreateUser(ApplicationUserDto user);
    Task<APIResponse> DeleteUser(int id);
    Task<APIResponse> UpdateUser(ApplicationUserDto user);
    Task<APIResponse> GetAllUsers(Expression<Func<ApplicationUser, bool>>? filter = null,
        string? includeProperties = null);
    Task<APIResponse> GetOneUser(Expression<Func<ApplicationUser, bool>> filter, 
        string? includeProperties = null);
    Task<ApplicationUser?> FindUserByEmail(string email);

    Task<APIResponse> CreateRole(CreateUserRoleDto userRoleDto, HttpContext httpContext);
    Task<APIResponse> DeleteRole(int id);
    Task<APIResponse> UpdateRole(UpdateUserRoleDto userRoleDto);
    Task<APIResponse> GetAllRoles(Expression<Func<UserRole, bool>>? filter = null,
        string? includeProperties = null);
    Task<APIResponse> GetOneRole(Expression<Func<UserRole, bool>> filter);
}