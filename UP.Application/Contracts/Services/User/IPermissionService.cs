using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UP.Application.Config.Authorization;
using UP.Application.Models;
using UP.Domain.User;

namespace UP.Application.Contracts.Services.User
{
    public interface IPermissionService
    {
        Task<APIResponse> CreatePermission(string moduleName, string permission);
        Task<APIResponse> DeletePermission(int id);
        Task<APIResponse> GetAllPermissions();
        Task<APIResponse?> GetPermission(int id);
        Task<APIResponse> GetPermissionsForUser(int Id);
    }
}
