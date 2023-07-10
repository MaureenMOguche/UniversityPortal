using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UP.Application.Models.Dto.Auth;
using UP.Application.Models;
using UP.Application.Contracts.Services;
using UP.Domain;
using UP.Application.Models.Dto;

namespace UP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentContoller : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentContoller(IDepartmentService departmentService)
        {
            this._departmentService = departmentService;
        }

        /// <summary>
        /// Add Department
        /// </summary>
        /// <param name="departmentDto"></param>
        /// <returns></returns>
        [HttpPost("CreateDepartment")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> NewStudentRegister([FromBody] DepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _departmentService.CreateAsync(departmentDto);
            return Ok(response);
        }


        /// <summary>
        /// Update Department
        /// </summary>
        /// <param name="departmentDto"></param>
        /// <returns></returns>
        [HttpPut("updateDepartment")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _departmentService.UpdateAsync(departmentDto);
            return Ok(response);
        }

        /// <summary>
        /// Delete Department
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteDepartment")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _departmentService.DeleteAsync(id);
            return Ok(response);
        }

        /// <summary>
        /// Get all Departments
        /// </summary>
        /// <returns></returns>
        [HttpGet("allDepartments")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllDepartments()
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _departmentService.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// Get one Department
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("departmentDetails")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetDepartmentDetails(string departmentCode)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _departmentService.GetOneAsync(departmentCode);
            return Ok(response);
        }

        /// <summary>
        /// Get Department by Faculty
        /// </summary>
        /// <param name="facultyCode"></param>
        /// <returns></returns>
        [HttpGet("departmentsByFaculty")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DepartmentByFaculty(int facultyId)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);

            var response = await _departmentService.GetDepartmentsByFaculty(facultyId);
            return Ok(response);
        }

    }
}
