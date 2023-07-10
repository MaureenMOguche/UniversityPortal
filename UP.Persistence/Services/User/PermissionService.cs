using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Config.Authorization;
using UP.Application.Contracts.Persistence.User;
using UP.Application.Contracts.Services.User;
using UP.Application.Models;
using UP.Domain.User;

namespace UP.Persistence.Services.User
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IAdminService _adminService;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(IPermissionRepository permissionRepository,
            IAdminService adminService,
            ILogger<PermissionService> logger)
        {
            _permissionRepository = permissionRepository;
            _adminService = adminService;
            _logger = logger;
        }
        public async Task<APIResponse> CreatePermission(string moduleName, string permission)
        {
            APIResponse response = new();
            try
            {
                if (!Enum.IsDefined(typeof(PermissionEnum), permission))
                {
                    response.isSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Messages = new() { "permission must be one of: " +
                        "CanRead, CanWrite, CanUpdate, CanDelete"};
                    return response;
                }

                Permission newPermission = new()
                {
                    Name = permission,
                    ControllerName = moduleName,
                };

                var createdPermission = await _permissionRepository.CreateAsync(newPermission);
                if (createdPermission != null)
                {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Messages = new() { "permission successfully created"};
                    return response;
                }
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { "failed to create permission" };
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "failed to create permission");
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { ex.Message };
                return response;
            }
        }

        public async Task<APIResponse> DeletePermission(int id)
        {
            APIResponse response = new();
            try
            {
                var permission = await _permissionRepository.GetOneAsync(x => x.Id == id);
                if (permission == null)
                {
                    response.isSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Messages = new() { "permission does not exist" };
                    return response;
                }

                var delete = await _permissionRepository.DeleteAsync(permission);

                if (delete > 0)
                {
                    response.isSuccess = true;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Messages = new() { "successfully deleted permission" };
                    return response;
                }
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { "failed to delete permission" };
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message}");
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { ex.Message };
                return response;
            }
        }

        public async Task<APIResponse> GetAllPermissions()
        {
            APIResponse response = new();
            try
            {
                var permissions = await _permissionRepository.GetAllAsync();
                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                if (permissions != null)
                {
                    response.Result = permissions;
                    response.Messages = new() { "successfully retrieved all permissions" };
                }
                else
                {
                    response.Messages = new() { "There are no permissions" };
                }
                return response;
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex, ex.Message );
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { ex.Message };
                return response;
            }
        }

        public async Task<APIResponse> GetPermission(int id)
        {
            APIResponse response = new();
            try
            {
                var permission = await _permissionRepository.GetOneAsync(x => x.Id == id);
                if (permission != null)
                {
                    response.isSuccess = true;
                    response.Result = permission;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Messages = new() { "successfully retrieved permission" };
                    return response;
                }

                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Messages = new() { "permission does not exist" };
                return response;
            }
            catch (Exception ex )
            {
                _logger.LogError(ex, ex.Message);
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() { ex.Message };
                return response;
            }
        }

        public async Task<APIResponse> GetPermissionsForUser(int Id)
        {
            APIResponse response = new();
            try
            {
                var userResponse = await _adminService.GetOneUser(x => x.Id == Id);
                if (userResponse.isSuccess == false)
                {
                    response.isSuccess = false;
                    response.StatusCode = userResponse.StatusCode;
                    response.Messages = userResponse.Messages;
                    return response;
                }

                var user = userResponse.Result as ApplicationUser;

                List<Permission> permissions = new();
                foreach(var role in user.UserRoles)
                {
                    foreach(var permission in role.Permissions)
                    {
                        permissions.Add(permission);
                    }
                }

                response.isSuccess = true;
                response.StatusCode = HttpStatusCode.OK;

                if (permissions.Any())
                    response.Messages = new() { $"Successfully retrieved permssions for {user.EmailAddress}" };
                else
                    response.Messages = new() { $"{user.EmailAddress} does not any permissions" };

                return response;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "failed to retrieve user permissions");
                response.isSuccess = false;
                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Messages = new() {  ex.Message };
                return response;
            }
        }
    }
}
