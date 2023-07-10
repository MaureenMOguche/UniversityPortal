using Microsoft.AspNetCore.Mvc;
using UP.Application.Contracts.Services;
using UP.Application.Models;
using UP.Application.Models.Dto;

namespace UP.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacultyController : ControllerBase
{
    private readonly IFacultyService _facultyService;

    public FacultyController(IFacultyService facultyService)
    {
        _facultyService = facultyService;
    }

    /// <summary>
    /// Get all Faculties
    /// </summary>
    /// <returns></returns>
    [HttpGet("getAllFaculties")]
    [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetAllFaculties()
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var response = await _facultyService.GetAllAsync();
        return Ok(response);
    }
    
    /// <summary>
    /// Get One Faculty
    /// </summary>
    /// <returns></returns>
    [HttpGet("getOneFaculty")]
    [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> GetOneFaculty(int Id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var response = await _facultyService.GetOneAsync(x => x.Id == Id);
        return Ok(response);
    }
    
    /// <summary>
    /// Add Faculty
    /// </summary>
    /// <returns></returns>
    [HttpPost("createFaculty")]
    [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> CreateFaculty([FromBody] CreateFacultyDto facultyDto)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var response = await _facultyService.CreateAsync(facultyDto);
        return Ok(response);
    }
    
    /// <summary>
    /// Update Faculty
    /// </summary>
    /// <param name="facultyDto"></param>
    /// <returns></returns>
    [HttpPut("updateFaculty")]
    [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> UpdateFaculty([FromBody] UpdateFacultyDto facultyDto)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var response = await _facultyService.UpdateAsync(facultyDto);
        return Ok(response);
    }
    
    /// <summary>
    /// Delete Faculty
    /// </summary>
    /// <returns></returns>
    [HttpDelete("deleteFaculty")]
    [ProducesResponseType(typeof(APIResponse), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteFaculty(int id)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var response = await _facultyService.DeleteAsync(id);
        return Ok(response);
    }
}