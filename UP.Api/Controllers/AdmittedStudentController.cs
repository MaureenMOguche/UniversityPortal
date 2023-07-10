using Microsoft.AspNetCore.Mvc;
using UP.Application.Contracts.Services;
using UP.Application.Models;
using UP.Application.Models.Dto;
using UP.Domain;

namespace UP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AdmittedStudentController : ControllerBase
    {
        private readonly IAdmittedStudentService _admittedStudentService;

        public AdmittedStudentController(IAdmittedStudentService admittedStudentService)
        {
            this._admittedStudentService = admittedStudentService;
        }
        
        /// <summary>
        /// Get all admitted students
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllAdmittedStudents")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllAdmittedStudents()
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            
            var response = await _admittedStudentService.GetAllAsync();
            return Ok(response);
        }

        /// <summary>
        /// check student admission status
        /// </summary>
        /// <param name="checkAdmittedDto"></param>
        /// <returns></returns>
        [HttpPost("GetAdmittedStudent")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAdmittedStudent(CheckAdmittedDto checkAdmittedDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            
            var response = await _admittedStudentService.CheckIfAdmitted(checkAdmittedDto);
            return Ok(response);
        }
        
        /// <summary>
        /// Add admitted student
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddAdmittedStudent")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddAdmittedStudent([FromBody] AddAdmittedStudentDto admittedStudentDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            
            var response = await _admittedStudentService.CreateAsync(admittedStudentDto);
            return Ok(response);
        }
        
        
        
        /// <summary>
        /// check student admission status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteAdmittedStudent")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteAdmittedStudent(int id)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            
            var response = await _admittedStudentService.DeleteAsync(id);
            return Ok(response);
        }
        
        /// <summary>
        /// check student admission status
        /// </summary>
        /// <param name="admittedStudents"></param>
        /// <returns></returns>
        [HttpPost("BulkUploadAdmittedStudents")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> BulkUpload(IFormFile admittedStudents)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            
            var response = await _admittedStudentService.BulkUpload(admittedStudents);
            return Ok(response);
        }
        
        /// <summary>
        /// check student admission status
        /// </summary>
        /// <param name="admittedStudentUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("UpdateAdmittedStudent")]
        [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateAdmittedStudent(AdmittedStudentUpdateDto admittedStudentUpdateDto)
        {
            if (!ModelState.IsValid)
                return StatusCode(StatusCodes.Status400BadRequest, ModelState);
            
            var response = await _admittedStudentService.UpdateAsync(admittedStudentUpdateDto);
            return Ok(response);
        }
    }
}
