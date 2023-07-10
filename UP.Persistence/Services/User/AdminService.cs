using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using UP.Application.Contracts.Persistence.User;
using UP.Application.Contracts.Services;
using UP.Application.Contracts.Services.User;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Application.Models.Dto.User;
using UP.Domain;
using UP.Domain.User;

namespace UP.Persistence.Services.User;

public class AdminService : IAdminService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly ILogger<AdminService> _logger;
    private readonly IMapper _mapper;

    public AdminService(IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        ILogger<AdminService> logger, 
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<APIResponse> CreateUser(ApplicationUserDto userDto)
    {
        APIResponse response = new APIResponse();
        try
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(userDto);
            
            await _userRepository.CreateAsync(user);
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = user;
            response.Messages.Add("Successfully created new User");
        }
        catch(Exception e)
        {
            _logger.LogError(e, "User could not be created");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { $"{e}" };
        }

        return response;
    }

    public async Task<APIResponse> DeleteUser(int id)
    {
        APIResponse response = new APIResponse();
        try
        {
            ApplicationUser user = await _userRepository.GetOneAsync(x => x.Id == id);
            if (user == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages.Add("User does not exist");
                return response;
            }

            await _userRepository.DeleteAsync(user);
            
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Messages.Add("Successfully deleted User");
        }
        catch(Exception e)
        {
            _logger.LogError(e, "User could not be deleted");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { $"{e}" };
        }

        return response;
    }

    public async Task<APIResponse> UpdateUser(ApplicationUserDto userDto)
    {
        APIResponse response = new APIResponse();
        try
        {
            ApplicationUser user = _mapper.Map<ApplicationUser>(userDto);
            await _userRepository.UpdateAsync(user);
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Messages.Add("Successfully updated User");
        }
        catch(Exception e)
        {
            _logger.LogError(e, "User could not be updated");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { $"{e}" };
        }

        return response;
    }

    public async Task<APIResponse> GetAllUsers(Expression<Func<ApplicationUser, bool>>? filter = null,
        string? includeProperties = null)
    {
        APIResponse response = new APIResponse();
        try
        {
            var result = await _userRepository.GetAllAsync(includeProperties, filter);
            response.Result = result;
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            if (result != null)
                response.Messages.Add("Successfully retrieved all users");
            else
                response.Messages.Add("There are currently no users");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not retrieve users");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { $"{e}" };
        }

        return response;
    }

    public async Task<APIResponse> GetOneUser(Expression<Func<ApplicationUser, bool>> filter,
        string? includeProperties = null)
    {
        APIResponse response = new APIResponse();
        try
        {
            var result = await _userRepository.GetOneAsync(filter, includeProperties);
            if (result == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages.Add("User not found");
                return response;
            }
            response.Result = result;
            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Messages.Add("Successfully retrieved user");
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not retrieve users");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { $"{e}" };
            return response;
        }
    }

    public async Task<ApplicationUser?> FindUserByEmail(string email)
    {
        var user = await _userRepository.GetOneAsync(x => x.EmailAddress == email);
        return user;
    }

    public async Task<APIResponse> CreateRole(CreateUserRoleDto userRoleDto, HttpContext httpContext)
    {
        APIResponse response = new APIResponse();
        try
        {
            var roleExists = await _roleRepository
                .GetOneAsync(x => x.Name.ToLower() == userRoleDto.Name.ToLower());
  
            if (roleExists != null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Messages = new() { "User Role already exists" };
                return response;
            }

            var userName = httpContext.User.FindFirst("UserName")?.Value;
            
            //var mappedRole = _mapper.Map<UserRole>(userRoleDto);
            var mappedRole = new UserRole()
            {
                Name = userRoleDto.Name,
                CreatedBy = userName,
                Description = userRoleDto.Description,
                CreatedDate = DateTime.Now,

            };
            
            foreach(var permissionId in userRoleDto.Permissions)
            {
                var permission = await _permissionRepository.GetOneAsync(x => x.Id == permissionId);
                if (permission != null)
                {
                    mappedRole.Permissions.Add(permission);
                }
            }

            var newRole = await _roleRepository.CreateAsync(mappedRole);
            if (newRole != null)
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Result = newRole;
                response.Messages = new() { "Successfully created new role" };
                return response;
            }

            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.NotImplemented;
            response.Messages = new() { "failed to create role" };
            return response;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed not create role");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { "failed to create role" };
            return response;
        }
    }

    public async Task<APIResponse> DeleteRole(int id)
    {
        APIResponse response = new APIResponse();
        try
        {
            var role = await _roleRepository.GetOneAsync(x => x.Id == id);
            if (role != null)
            {
                var deleteRes = await _roleRepository.DeleteAsync(role);
                if (deleteRes > 0)
                {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Messages = new() { "Successfully deleted role" };
                    return response;
                }
            }

            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.NotFound;
            response.Messages = new() { "Role does not exist" };
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "failed to delete role");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { "failed to delete role" };
            return response;
        }
    }

    public async Task<APIResponse> UpdateRole(UpdateUserRoleDto userRoleDto)
    {
        APIResponse response = new APIResponse();
        try
        {
            var roleExists = await _roleRepository.GetOneAsync(x => x.Id == userRoleDto.Id);

            if (roleExists == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages = new() { "Role not found" };
                return response;
            }

            var mappedRole = _mapper.Map<UserRole>(userRoleDto);
            var newRoleResponse = await _roleRepository.UpdateAsync(mappedRole);
            if (newRoleResponse > 0)
            {
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                response.Messages = new() { "Successfully updated role" };
                return response;
            }

            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.NotImplemented;
            response.Messages = new() { "failed to update role" };
            return response;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not update role");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { "failed to update role" };
            return response;
        }
    }

    public async Task<APIResponse> GetAllRoles(Expression<Func<UserRole, bool>>? filter = null,
        string? includeProperties = null)
    {
        APIResponse response = new APIResponse();
        try
        {
            var roles = await _roleRepository.GetAllAsync(includeProperties, filter);

            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Result = roles;
            if (roles.Count() <= 0)
                response.Messages = new() { "There currently are no roles" };
            else
                response.Messages = new() { "Successfully retrieved roles" };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not retrieve roles");
            response.Messages = new List<string>() { $"{e.Message}" };
        }

        return response;
    }

    public async Task<APIResponse> GetOneRole(Expression<Func<UserRole, bool>> filter)
    {
        APIResponse response = new APIResponse();
        try
        {
            var role = await _roleRepository.GetOneAsync(filter);
            if (role == null)
            {
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                response.Messages = new() { "Role does not exist" };
                return response;
            }

            response.isSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            response.Messages = new() { "Successfully retrieved role" };
            response.Result = role;
            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to retrieve role");
            response.isSuccess = false;
            response.StatusCode = HttpStatusCode.InternalServerError;
            response.Messages = new() { "Failed to retrieve role" };
            return response;
        }
    }
}