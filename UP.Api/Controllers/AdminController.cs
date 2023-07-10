using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UP.Application.Config.Authorization;
using UP.Application.Contracts.Services.User;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Application.Models.Dto.User;

namespace UP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IPermissionService _permissionService;

        public AdminController(IAdminService adminService, IPermissionService permissionService)
        {
            _adminService = adminService;
            _permissionService = permissionService;
        }


        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <returns></returns>
        [HttpGet("getAllPermissions")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPermissions()
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _permissionService.GetAllPermissions();
            return Ok(response);
        }

        /// <summary>
        /// Get one permssion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getOnePermission")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetOnePermission([Required]int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _permissionService.GetPermission(id);
            return Ok(response);
        }

        /// <summary>
        /// Get permissions for user
        /// </summary>
        /// <returns></returns>
        [HttpGet("getUserPermissions")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUserPermissions(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _permissionService.GetPermissionsForUser(id);
            return Ok(response);
        }

        /// <summary>
        /// Add Permission
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="permissionName"></param>
        /// <returns></returns>
        //[HasPermission()]
        [HttpPost("addPermission")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddPermssion([FromBody]AddPermissionDto addPermissionDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            //if (!Enum.IsDefined(typeof(PermissionEnum), permissionName))
            //{
            //    ModelState.AddModelError("Permission name can only be either of\n" +
            //        "CanRead\nCanWrite\nCanUpdate\nCanDelete");
            //    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            //}

            var response = await _permissionService
                .CreatePermission(addPermissionDto.controllerName, addPermissionDto.permission);
            return Ok(response);
        }
        

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deletPermission")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeletePermission(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _permissionService.DeletePermission(id);
            return Ok(response);
        }


        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="userRoleDto"></param>
        /// <returns></returns>
        //[HasPermission()]
        [HttpPost("addRole")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddRole(CreateUserRoleDto userRoleDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _adminService.CreateRole(userRoleDto, HttpContext);
            return Ok(response);
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="userRoleDto"></param>
        /// <returns></returns>
        [HttpPut("updateRole")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateRole(UpdateUserRoleDto userRoleDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _adminService.UpdateRole(userRoleDto);
            return Ok(response);
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deleteRole")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteRole(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _adminService.DeleteRole(id);
            return Ok(response);
        }

        /// <summary>
        /// Get roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("allRoles")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetRoles()
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _adminService.GetAllRoles();
            return Ok(response);
        }

        /// <summary>
        /// Get userDetail
        /// </summary>
        /// <returns></returns>
        [HttpGet("roleDetail")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> RoleDetail(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _adminService.GetOneRole(x => x.Id == id);
            return Ok(response);
        }


        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPost("addUser")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddUser(ApplicationUserDto userDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _adminService.CreateUser(userDto);
            return Ok(response);
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut("updateUser")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateUser(ApplicationUserDto userDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _adminService.UpdateUser(userDto);
            return Ok(response);
        }

        /// <summary>
        /// Delete Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deleteUser")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _adminService.DeleteUser(id);
            return Ok(response);
        }

        /// <summary>
        /// Get roles
        /// </summary>
        /// <returns></returns>
        [HttpGet("allUsers")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetUsers()
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _adminService.GetAllUsers();
            return Ok(response);
        }

        /// <summary>
        /// Get roleDetails
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("userDetail")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UserDetail(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _adminService.GetOneUser(x => x.Id == id);
            return Ok(response);
        }
    }
}
